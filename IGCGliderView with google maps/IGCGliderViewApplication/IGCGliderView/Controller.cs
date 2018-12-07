using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.CacheProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsPresentation;
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;
using GMapPolygon = GMap.NET.WindowsForms.GMapPolygon;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Remotion.Linq.Clauses;
using GMap.NET.WindowsForms.Markers;

namespace IGCGliderView
{
    class Controller
    {
        private List<Color> colors = new List < Color >
            {
                Color.FromArgb(25, Color.IndianRed),
                Color.FromArgb(25, Color.Yellow),
                Color.FromArgb(25, Color.Orange),
                Color.FromArgb(25, Color.PaleVioletRed),
                Color.FromArgb(25, Color.Red),
                Color.FromArgb(25, Color.DarkRed)
            };

        private List<Double> Divisors;

        private string Last_Wind_Speed_Loaded;
        private string Last_Wind_Direction_Loaded;
        private string metrics_for_color_coding;
        private FinalModel db;
        private List<HOTSPOT> hotspots_array;
        private List<GridCell> cells;
        private GMapOverlay polygons;
        private GMapOverlay markers;

        #region: Constructor
        public Controller(string metrics_type)
        {
            // Cut-off numbers for selecting a color for a corresponding vertical speed gain
            Divisors = new List<double>(6);
            Last_Wind_Direction_Loaded = "none";
            Last_Wind_Speed_Loaded = "none";

            //Initialize connection to DB
            db = new FinalModel();

            // initialize the legend of our polygon overlay
            metrics_for_color_coding = metrics_type;
            SetSpeedRangeData("N", "L");
        }
        #endregion

        public List<GMapOverlay> getHotspots (string wind_dir, string wind_speed, string metrics)
        {
            GMapOverlay temp_polygons = new GMapOverlay("Polygons");
            GMapOverlay temp_markers = new GMapOverlay("Marker");
            List<Hotspot_container> hotspots_temp = new List<Hotspot_container>();
            decimal ledger = -1;
            Hotspot_container hotspot_Container = null;



            // 1st step: check if there has been a previous load
            if (Last_Wind_Direction_Loaded == "none" && Last_Wind_Speed_Loaded == "none")
            {
                //if this is the first load, we check if the metrics for color coding is the same as the standard
                //the standard is "Speed"
                if(metrics_for_color_coding != metrics)
                {
                    //if there is a difference, we reload the color coding
                    metrics_for_color_coding = metrics;
                    SetSpeedRangeData(wind_dir, wind_speed);
                }

                // We query for the hotspots with the pre-conditions for search given through the GUI
                var hotspots = (from hs in db.HOTSPOTS
                                where hs.WIND_SPEED_TYPE == wind_speed && hs.CARDINAL_WIND_DIRECTION == wind_dir
                                orderby hs.GRID_CELL_GROUP_ID
                                select hs).ToList();
                foreach(var hotspot_data in hotspots)
                {
                    if (ledger!= hotspot_data.GRID_CELL_GROUP_ID)
                    {
                        ledger = hotspot_data.GRID_CELL_GROUP_ID;
                        if(hotspot_Container!= null)        // is this the beginning of the run?
                        {
                            hotspots_temp.Add(hotspot_Container); // if not, we insert into list of hotspots_temp
                        }
                        // Create a new hotspot container
                        hotspot_Container = new Hotspot_container((double)hotspot_data.AVERAGE_SPEED, 
                            (double)hotspot_data.AVERAGE_ALTITUDEG,
                            hotspot_data.GRID_CELL_GROUP_ID);
                        hotspot_Container.wind_speed = hotspot_data.WIND_SPEED_TYPE;
                        hotspot_Container.cardinal_wind_direction = hotspot_data.CARDINAL_WIND_DIRECTION;
                        hotspot_Container.add_gridCell((double)hotspot_data.LATITUDE_START,
                            (double)hotspot_data.LONGITUDE_START);
                        if (metrics == "Speed")
                        {
                            hotspot_Container.color = GetColor((double)hotspot_data.AVERAGE_SPEED);
                        }
                        else if (metrics == "Altitude")
                        {
                            hotspot_Container.color = GetColor((double)hotspot_data.AVERAGE_ALTITUDEG);
                        }
                        hotspot_Container.Speed = (double) hotspot_data.AVERAGE_SPEED;
                        hotspot_Container.Altitude = (double) hotspot_data.AVERAGE_ALTITUDEG;
                    }
                    else if(ledger == hotspot_data.GRID_CELL_GROUP_ID)
                    {
                        hotspot_Container.add_gridCell((double)hotspot_data.LATITUDE_START,
                            (double)hotspot_data.LONGITUDE_START);
                    }
                }
                if(!hotspots_temp.Contains(hotspot_Container) )
                {
                    hotspots_temp.Add(hotspot_Container);
                }
            }


            List<GMapOverlay> envelope = new List<GMapOverlay>(2);
            envelope.Add(Get_Polygons_Overlay(hotspots_temp));
            envelope.Add(Get_Markers_Overlay(hotspots_temp));
            return envelope;
        }

        // this method generates the polygons
        private GMapOverlay Get_Polygons_Overlay(List<Hotspot_container> hotspot_s)
        {
            GMapOverlay gMapOverlay = new GMapOverlay("Polygons");

            for(int i=0; i<hotspot_s.Count; i++)
            {
                for(int j=0; j< hotspot_s[i].gridCells.Count; j++)
                {
                    var Polygon = new GMapPolygon(GetPointLatLngs(hotspot_s[i].gridCells[j]),
                        "PolygonNo" + ((i + 1) * (j + 1)).ToString())
                    {
                        Stroke = new Pen(Color.Transparent),
                        Fill = new SolidBrush(hotspot_s[i].color)
                    };
                    gMapOverlay.Polygons.Add(Polygon);
                }
            }


            return gMapOverlay;
        }

        //this method generates the markers
        private GMapOverlay Get_Markers_Overlay(List<Hotspot_container> hotspot_s)
        {
            GMapOverlay gMo = new GMapOverlay("Markers");
            for(int i=0; i<hotspot_s.Count; i++)
            {
                var Marker = new GMarkerGoogle(GetLatLng(hotspot_s[i].gridCells[0]), GMarkerGoogleType.arrow);
                Marker.ToolTipText = "Horizontal Speed: " + hotspot_s[i].Speed.ToString()
                    + "m/s \r Altitude Gain: " + hotspot_s[i].Altitude.ToString() + "m";
                Marker.ToolTip.Fill = Brushes.Black;
                Marker.ToolTip.Foreground = Brushes.White;
                Marker.ToolTip.Stroke = Pens.Black;
                Marker.ToolTip.TextPadding = new Size(20, 20);
                Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                Marker.IsHitTestVisible = true;
                
                gMo.Markers.Add(Marker);
            }
            return gMo;
        }

        private PointLatLng GetLatLng (GridCell gc)
        {
            return new PointLatLng(gc.Lat_center, gc.Long_center);
        }

        //this method returns a proper polygon with four points that is prepared for inserting into an overlay container
        private List<PointLatLng> GetPointLatLngs(GridCell gc)
        {
            // the steps are the distance towards the end of grid from the entry of the cell
            // the entry of the cell is the lower left corner
            double Lat_step = 0.00833333, Long_step = 0.01666667;
            //double Lat_step = 0.00833333, Long_step = 0.01666667;
            List<PointLatLng> Points = new List<PointLatLng>();
            Points.Add(new PointLatLng(Math.Round(+gc.Latitude, 7), Math.Round(+gc.Longitude, 7)));
            Points.Add(new PointLatLng(Math.Round(+gc.Latitude, 7), Math.Round(+gc.Longitude + Long_step, 7)));
            Points.Add(new PointLatLng(Math.Round(+gc.Latitude + Lat_step, 7), Math.Round(+gc.Longitude + Long_step, 7)));
            Points.Add(new PointLatLng(Math.Round(+gc.Latitude + Lat_step, 7), Math.Round(+gc.Longitude, 7)));

            return Points;
        }

        // this method determines the ranges for each color code
        private void SetSpeedRange(double min_speed, double max_speed)
        {
            Divisors = new List<double>(6);
            double range = max_speed - min_speed;
            double step = range / 5;
            for (int i=0; i<5; i++)
            {
                Divisors.Add(min_speed + (step * (i + 1)));
            }
        }




        // this method allows to set the range of min and max (altitude gain / speed) in the dataset
        private void SetSpeedRangeData(string cardinal_wind_dir, string Wind_speed)
        {
            if (metrics_for_color_coding == "Speed")
            {
                var min = 
                    (from hs in db.HOTSPOTS
                           where hs.MIN_SPEED >0 
                           && hs.CARDINAL_WIND_DIRECTION == cardinal_wind_dir
                           && hs.WIND_SPEED_TYPE == Wind_speed 
                           select hs.MIN_SPEED).Min();
                var max = 
                    (from hs in db.HOTSPOTS
                           where hs.MIN_SPEED > 0
                           && hs.CARDINAL_WIND_DIRECTION == cardinal_wind_dir
                           && hs.WIND_SPEED_TYPE == Wind_speed
                           select hs.MAX_SPEED).Max();

                SetSpeedRange((double)min, (double)max);
            }
            else if (metrics_for_color_coding == "Altitude")
            {
                var min = 
                    (from hs in db.HOTSPOTS
                           where hs.MIN_SPEED > 0
                           && hs.CARDINAL_WIND_DIRECTION == cardinal_wind_dir
                           && hs.WIND_SPEED_TYPE == Wind_speed
                           where hs.MIN_ALTITUDEG > 0
                           select hs.MIN_ALTITUDEG).Min();
                var max = 
                    (from hs in db.HOTSPOTS
                     where hs.MIN_SPEED > 0
                           && hs.CARDINAL_WIND_DIRECTION == cardinal_wind_dir
                           && hs.WIND_SPEED_TYPE == Wind_speed
                           select hs.MAX_ALTITUDEG).Max();

                SetSpeedRange((double)min, (double)max);
            }

        }



        private Color GetColor(double variable)
        {
            for(int i=0; i<Divisors.Count; i++)
            {
                if (variable < Divisors[i])
                {
                    return colors[i];
                }
            }
            return Color.Crimson;
        }


        // this was used just for testing purposes
        public string getHotSpot_example()
        {

            var hotspots = (from hs in db.HOTSPOTS
                            where hs.CARDINAL_WIND_DIRECTION == "N"
                            select hs.MIN_SPEED).Min();

            return hotspots.ToString();
        }
    }
}

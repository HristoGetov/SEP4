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
                Color.FromArgb(100, Color.GreenYellow),
                Color.FromArgb(150, Color.LimeGreen),
                Color.FromArgb(150, Color.Yellow),
                Color.FromArgb(150, Color.Orange),
                Color.FromArgb(150, Color.Red),
                Color.FromArgb(150, Color.DarkRed)
            };

        private List<GMarkerGoogleType> gMarkerGoogleTypes = new List<GMarkerGoogleType>
        {
            GMarkerGoogleType.green_small,
            GMarkerGoogleType.green,
            GMarkerGoogleType.yellow,
            GMarkerGoogleType.orange,
            GMarkerGoogleType.red
        };

        private List<Double> Divisors;

        private string Last_Wind_Speed_Loaded;
        private string Last_Wind_Direction_Loaded;
        private string metrics_for_color_coding;

        public double MaxSpeedGlobal { set; get; }
        public double MinSpeedGlobal { set; get; }
        public double MaxAgGlobal { set; get; }
        public double MinAgGlobal { set; get; }
        public int MaxThermalsCountGlobal { set; get; }
        public int MinThermalsCountGlobal { set; get; }

        private FinalModel db;
        private List<Hotspot_container> hotspots_array;
        private List<GridCell> cells;
        private GMapOverlay polygons;
        private GMapOverlay markers;

        #region: Constructor
        /// <summary>
        /// Constructor initializes the entity model for querying the DB
        /// </summary>
        /// <param name="metrics_type"></param>
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
            
        }
        #endregion

        public List<GMapOverlay> getHotspots (string wind_dir, string wind_speed, string metrics)
        {
            GMapOverlay temp_polygons = new GMapOverlay("Polygons");
            GMapOverlay temp_markers = new GMapOverlay("Marker");
            List<Hotspot_container> hotspots_temp = new List<Hotspot_container>();



            // 1st step: check if there has been a previous load
            if (Last_Wind_Direction_Loaded == "none" && Last_Wind_Speed_Loaded == "none")
            {
                //if this is the first load, we check if the metrics for color coding is the same as the standard
                //the standard is "Speed"
                if (metrics_for_color_coding != metrics)
                {
                    //if there is a difference, we reload the color coding
                    metrics_for_color_coding = metrics;
                    
                }

                // We query for the hotspots with the pre-conditions for search given through the GUI
                hotspots_temp = GetHotspotsFromDB(wind_dir, wind_speed);
            }

            hotspots_temp = filter_hotspots(hotspots_temp);
            // update color coding
            hotspots_array = hotspots_temp;
            SetSpeedRangeData();
            //update statistics of filtered hotspots
            for (int i = 0; i<hotspots_temp.Count; i++)
            {
                if (metrics_for_color_coding == "Speed")
                {
                    hotspots_temp[i].color = GetColor(Convert.ToDouble(hotspots_temp[i].averageSpeed));
                }
                else if (metrics_for_color_coding == "Altitude")
                {
                    hotspots_temp[i].color = GetColor(Convert.ToDouble(hotspots_temp[i].averageAG));
                }
                else if (metrics_for_color_coding == "Count")
                {
                    hotspots_temp[i].color = GetColor(hotspots_temp[i].gridCells.Count);
                }
            }
            


            List<GMapOverlay> envelope = new List<GMapOverlay>(2);
            envelope.Add(Get_Polygons_Overlay(hotspots_temp));
            envelope.Add(Get_Markers_Overlay(hotspots_temp));
            return envelope;
        }

        // ##########################################################################################
        //###########################################################################################
        //                      END OF MAIN METHOD FOR PASSING HOTSPOTS
        //###########################################################################################
        //###########################################################################################

        private List<Hotspot_container> filter_hotspots(List<Hotspot_container> list)
        {
            List<Hotspot_container> resulting_list = new List<Hotspot_container>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!ReferenceEquals(list[0], null))
                {
                    for (int j = 0; j < list[i].gridCells.Count; j++)
                    {
                        if (list[i].gridCells.Count > 1)
                        {
                            resulting_list.Add(list[i]);
                            list[i].refresh_statistics();
                        }
                    }
                }
            }
            return resulting_list;
        }

        /// <summary>
        /// This method is in charge of querying for data and preparing it for use
        /// </summary>
        /// <param name="Wind_dir"> CARDINAL WIND DIRECTION (N,S,W,E)</param>
        /// <param name="Wind_type"> WIND TYPE (light, medium, strong)</param>
        /// <returns>List of unfiltered thermals</returns>
        private List<Hotspot_container> GetHotspotsFromDB(string Wind_dir, string Wind_type)
        {
            Hotspot_container hotspot_Container = null;
            List<Hotspot_container> hotspots_temp = new List<Hotspot_container>();
            decimal? ledger = -1;

            var hotspots = (from hs in db.HOTSPOTS
                            where hs.WIND_SPEED_TYPE == Wind_type && hs.CARDINAL_WIND_DIRECTION == Wind_dir
                            orderby hs.GRID_CELL_ID
                            select hs).ToList();
            foreach (var hotspot_data in hotspots)
            {
                if (ledger != hotspot_data.GRID_CELL_ID)
                {
                    ledger = hotspot_data.GRID_CELL_ID;
                    if (hotspot_Container != null)        // is this the beginning of the run?
                    {
                        hotspots_temp.Add(hotspot_Container); // if not, we insert into list of hotspots_temp
                    }
                    // Create a new hotspot container
                    hotspot_Container = new Hotspot_container(Convert.ToDouble(hotspot_data.AVERAGE_SPEED),
                        Convert.ToDouble(hotspot_data.AVERAGE_ALTITUDEG),
                        hotspot_data.GRID_CELL_ID);
                    hotspot_Container.wind_speed = hotspot_data.WIND_SPEED_TYPE;
                    hotspot_Container.cardinal_wind_direction = hotspot_data.CARDINAL_WIND_DIRECTION;
                    hotspot_Container.grid_cell_id = hotspot_data.GRID_CELL_ID;

                    // add the current thermal to the hotspot container
                    GridCell gc = new GridCell(Convert.ToDouble(hotspot_data.LATITUDE_START),
                        Convert.ToDouble(hotspot_data.LONGITUDE_START));
                    gc.speed = Convert.ToDouble(hotspot_data.AVERAGE_SPEED);
                    gc.altitude_gain = Convert.ToDouble(hotspot_data.AVERAGE_ALTITUDEG);


                    hotspot_Container.add_gridCell(gc);
                    
                    
                }
                else if (ledger == hotspot_data.GRID_CELL_ID)
                {
                    GridCell gc = new GridCell(Convert.ToDouble(hotspot_data.LATITUDE_START),
                        Convert.ToDouble(hotspot_data.LONGITUDE_START));
                    gc.speed = Convert.ToDouble(hotspot_data.AVERAGE_SPEED);
                    gc.altitude_gain = Convert.ToDouble(hotspot_data.AVERAGE_ALTITUDEG);
                    hotspot_Container.add_gridCell(gc);
                }
            }
            if (!hotspots_temp.Contains(hotspot_Container))
            {
                hotspots_temp.Add(hotspot_Container);
            }

            return hotspots_temp;
        }


        // this method generates the polygons
        private GMapOverlay Get_Polygons_Overlay(List<Hotspot_container> hotspot_s)
        {
            GMapOverlay gMapOverlay = new GMapOverlay("Polygons");

            for(int i=0; i<hotspot_s.Count; i++)
            {
                if (!ReferenceEquals(hotspot_s[0],null))
                {
                    for (int j = 0; j < hotspot_s[i].gridCells.Count; j++)
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
            }


            return gMapOverlay;
        }

        /// <summary>
        /// This method generates the markers that are put on the map with the statistics about the hotspots
        /// </summary>
        /// <param name="hotspot_s">We insert the list of filtered hotspots</param>
        /// <returns>A GMap.Net overlay with all of the markers created</returns>
        private GMapOverlay Get_Markers_Overlay(List<Hotspot_container> hotspot_s)
        {
            GMapOverlay gMo = new GMapOverlay("Markers");
            if(!ReferenceEquals(hotspot_s,null))
            {
                for (int i = 0; i < hotspot_s.Count; i++)
                {
                    GMarkerGoogleType temp = GMarkerGoogleType.black_small;
                    if (metrics_for_color_coding == "Altitude")
                    {
                        temp = getGMarker(hotspot_s[i].averageAG);
                    }
                    else if(metrics_for_color_coding == "Speed")
                    {
                        temp = getGMarker(hotspot_s[i].averageSpeed);
                    }
                    else if (metrics_for_color_coding == "Count")
                    {
                        temp = getGMarker(hotspot_s[i].gridCells.Count);
                    }
                    var Marker = new GMarkerGoogle(GetLatLng(hotspot_s[i].gridCells[0]), temp);
                    Marker.ToolTipText = "\nAvg Vertical Speed: " + hotspot_s[i].averageSpeed.ToString() + "m/s\n" +
                        "Max Vertical Speed: " + hotspot_s[i].MaxSpeed.ToString() + " m/s\n" +
                        "Min Vertical Speed: " + hotspot_s[i].MinSpeed.ToString() + "m/s\n"
                        +"\nAvg Altitude Gain: " + hotspot_s[i].averageAG.ToString() + "m\n"
                        + "Max Altitude Gain: " + hotspot_s[i].MaxAG.ToString() + "m\n"
                        + "Min Altitude Gain: " + hotspot_s[i].MinAG.ToString() + "m\n"
                        + "\nNumber of thermals found: " + hotspot_s[i].gridCells.Count.ToString();
                    
                    Marker.ToolTip.Fill = Brushes.Black;
                    Marker.ToolTip.Foreground = Brushes.White;
                    Marker.ToolTip.Stroke = Pens.Black;
                    Marker.ToolTip.TextPadding = new Size(20, 20);
                    Marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    Marker.IsHitTestVisible = true;

                    gMo.Markers.Add(Marker);
                }
            }
            return gMo;
        }


        /// <summary>
        /// This metthod allows us to find the center coordinates for a grid_cell on the map 
        /// for adding the marker on top of it
        /// </summary>
        /// <param name="gc"></param>
        /// <returns></returns>
        private PointLatLng GetLatLng (GridCell gc)
        {
            return new PointLatLng(gc.Lat_center, gc.Long_center);
        }

        /// <summary>
        /// This method returns a proper polygon with four points that is prepared for inserting into an overlay container
        /// The insertion order of the points into the list is lower-left, lower-right, upper-right, upper-left 
        /// </summary>
        /// <param name="gc">We feed a gridcell that has the start lat and long of a grid_cell square</param>
        /// <returns>A list of coordinates points for forming a square polygon</returns>
        private List<PointLatLng> GetPointLatLngs(GridCell gc)
        {
            // the steps are the distance towards the end of grid from the entry of the cell
            // the entry of the cell is the lower left corner
            double Lat_step = 0.00833333, Long_step = 0.01666667;
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




        /// <summary>
        /// This method allows to set the range of min and max (altitude gain / speed) in the filtered
        /// hotspots dataset
        /// </summary>
        private void SetSpeedRangeData()
        {
            if (metrics_for_color_coding == "Speed")
            {
                double min = 100, max = 0;
                for(int i =0; i<hotspots_array.Count; i++)
                {
                    if (hotspots_array[i].averageSpeed > max )
                    {
                        max = hotspots_array[i].averageSpeed;
                    }
                    if (hotspots_array[i].averageSpeed < min)
                    {
                        min = hotspots_array[i].averageSpeed;
                    }
                }
                MaxSpeedGlobal = max;
                MinSpeedGlobal = min;
                SetSpeedRange(min, max);
            }
            else if (metrics_for_color_coding == "Altitude")
            {
                double min = 1000, max = 0;
                for (int i = 0; i < hotspots_array.Count; i++)
                {
                    if (hotspots_array[i].averageAG > max)
                    {
                        max = hotspots_array[i].averageAG;
                    }
                    if (hotspots_array[i].averageAG < min)
                    {
                        min = hotspots_array[i].averageAG;
                    }
                }
                MaxAgGlobal = max;
                MinAgGlobal = min;
                SetSpeedRange(min, max);
            }
            else if (metrics_for_color_coding == "Count")
            {
                int min = 100, max = 0;
                for (int i = 0; i < hotspots_array.Count; i++)
                {
                    if (hotspots_array[i].gridCells.Count > max)
                    {
                        max = hotspots_array[i].gridCells.Count;
                    }
                    if (hotspots_array[i].gridCells.Count < min)
                    {
                        min = hotspots_array[i].gridCells.Count;
                    }
                }
                MaxThermalsCountGlobal = max;
                MinThermalsCountGlobal = min;
                SetSpeedRange(min, max);
            }
        }


        /// <summary>
        /// This method returns the proper color for a polygon in accordance to the color legend and
        /// the max-min values of the metrics used for color coding
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        private Color GetColor(double variable)
        {
            for(int i=0; i<Divisors.Count; i++)
            {
                if (variable < Divisors[i])
                {
                    return colors[i];
                }
            }
            return Color.FromArgb(150, Color.Crimson);
        }

        private GMarkerGoogleType getGMarker (double variable)
        {
            for (int i = 0; i < Divisors.Count; i++)
            {
                if (variable < Divisors[i])
                {
                    return gMarkerGoogleTypes[i];
                }
            }
            return GMarkerGoogleType.arrow;
        }


        /// <summary>
        /// this was used just for testing purposes of the connection of the Entity Framework to the DB
        /// </summary>
        /// <returns></returns>
        public string getHotSpot_example()
        {

            var hotspots = (from hs in db.HOTSPOTS
                            where hs.CARDINAL_WIND_DIRECTION == "N"
                            select hs.MIN_SPEED).Min();

            return hotspots.ToString();
        }
    }
}

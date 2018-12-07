using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using GMap.NET.WindowsPresentation;
using GMapPolygon = GMap.NET.WindowsForms.GMapPolygon;
using GMapMarker = GMap.NET.WindowsForms.GMapMarker;
using GMap.NET.WindowsForms.Markers;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace IGCGliderView
{
    public partial class MapForm : Form
    {
        private Controller controller;
        private string current_metrics_color_coding;
        private string current_wind_speed;
        private string current_wind_direction;
        //static helpers
        private static string speed_metrics_color_coding = "Speed";
        private static string altitude_metrics_colord_coding = "Altitude";


        public MapForm()
        {
            InitializeComponent();
            

            current_metrics_color_coding = speed_metrics_color_coding;
            current_wind_direction = "N";
            current_wind_speed = "L";
            comboBoxColorCoding.SelectedIndex = Top;
            comboBoxWindDirection.SelectedIndex = Top;
            radioButtonLightWind.Checked = true;
            controller = new Controller(speed_metrics_color_coding);
        }
       
              

        private void Generate_polygons(object sender, EventArgs e)
        {
            System_messages.Text = "Started loading your hotspots ;--)";
            List<GMapOverlay> overlays = controller.getHotspots(current_wind_direction, 
                current_wind_speed, current_metrics_color_coding);
            System_messages.Text = "Total number of Hotspots: " + overlays[1].Markers.Count.ToString();

            foreach(GMapOverlay gmo in overlays)
            {
                if(gmo!= null)
                {
                    map.Overlays.Add(gmo);
                }
            }


            //int lim = 68800, i=0, j=0;
            //double beginLong = +8.0000000, //we begin in the lower left corner of the grid 
            //        endLong = +11.0000000, // we end in the upper right corner of the grid
            //        beginLat = +54.5000000,
            //        endLat = +57.5000000,
            //        Long_step = 0.01666667,
            //        Lat_step = 0.00833333,
            //        Lat_index = beginLat,
            //        Long_index = beginLong;
            //List< PointLatLng>  Points = new List<PointLatLng>();
            
            //do
            //{
            //    Points.Clear();
            //    // start creating the square
            //    Points.Add(new PointLatLng( Math.Round(+Lat_index, 7), Math.Round(+Long_index, 7)));
            //    Points.Add(new PointLatLng( Math.Round(+Lat_index,7), Math.Round(+Long_index + Long_step, 7)));
            //    Points.Add(new PointLatLng( Math.Round(+Lat_index + Lat_step, 7), Math.Round(+Long_index + Long_step, 7)));
            //    Points.Add(new PointLatLng( Math.Round(+Lat_index + Lat_step,7), Math.Round(+Long_index, 7)));
                
            //    // use this square and insert it into a polygon
            //    var Polygon = new GMapPolygon(Points, ("tester" + i.ToString()))
            //    {
            //        Stroke = new Pen(Color.Transparent),
            //        Fill = new SolidBrush(Color.FromArgb(150, Color.Blue)),
                    
            //    };

            //    polygons.Polygons.Add(Polygon);


            //    Long_index += Long_step;
            //    if(Long_index >= endLong)
            //    {
            //        Long_index = beginLong;
            //        j++;
                    
            //        map.Overlays.Add(polygons);
            //        map.Update();
            //        polygons = new GMapOverlay("polygons" + j.ToString());
            //    }
            //    Lat_index = beginLat + j * Lat_step;
            //    if(Lat_index >= endLat)
            //    {
            //        polygons_created = true;
            //        map.Refresh();
            //        return;
            //    }
            //    i++;
            //} while (i < lim);
            ////textBox_Message.Text = "Last Poly coord" + polygons.Polygons[polygons.Polygons.Count-1].Points[3].Lat.ToString();
            //polygons_created = true;
            //map.Refresh();
            //return;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.BingMap;
            map.SetPositionByKeywords("Denmark");
            map.ShowCenter = false;
            map.MaxZoom = 200;
            map.MinZoom = 1;
            map.Zoom = 7;
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if(map.MapProvider == GMapProviders.BingMap)
            {
                map.MapProvider = GMapProviders.BingHybridMap;
                map.ShowCenter = false;
                map.MinZoom = 5;
            }
            else if (map.MapProvider == GMapProviders.BingHybridMap)
            {
                map.MapProvider = GMapProviders.BingMap;
                map.ShowCenter = false;
                map.MinZoom = 5;
            }
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void map_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxWindDirection.SelectedIndex)
            {
                case 0:
                    current_wind_direction = "N";
                    break;
                case 1:
                    current_wind_direction = "S";
                    break;
                case 2:
                    current_wind_direction = "E";
                    break;
                case 3:
                    current_wind_direction = "W";
                    break;
            } 
            
        }

        private void comboBoxColorCoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBoxColorCoding.SelectedIndex == 1)
            {
                current_metrics_color_coding = altitude_metrics_colord_coding;
            }
            else if (comboBoxColorCoding.SelectedIndex == 0)
            {
                current_metrics_color_coding = speed_metrics_color_coding;
            }

        }

        private void radioButton_LightWind_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLightWind.Checked)
            {
                current_wind_speed = "L";
            }
        }

        private void RadioButtonStrongWind_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonStrongWind.Checked)
            {
                current_wind_speed = "S";
            }
        }

        private void radioButtonModerateWind_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonModerateWind.Checked)
            {
                current_wind_speed = "M";
            }
        }

        private void Clear_Map(object sender, EventArgs e)
        {
            map.Overlays.Clear();
            map.Refresh();

        }
    }
}

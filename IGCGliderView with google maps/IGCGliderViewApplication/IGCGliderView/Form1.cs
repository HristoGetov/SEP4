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
        private static string thermals_count_color_coding = "Count";


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
            Initialize_legend();
        }
       
              
        private void Initialize_legend()
        {
            legend_1.BackColor = Color.FromArgb(100, Color.GreenYellow);
            legend_2.BackColor = Color.FromArgb(150, Color.LimeGreen);
            legend_3.BackColor = Color.FromArgb(150, Color.Yellow);
            legend_4.BackColor = Color.FromArgb(150, Color.Orange);
            legend_5.BackColor = Color.FromArgb(150, Color.Red);
            
        }


        /// <summary>
        /// This method is the main method to fetch the polygons and the markers from the controller
        /// it will then continue to reload the legend of the map
        /// </summary>
        /// <param name="sender"> Button "Load Hotspots"</param>
        /// <param name="e"></param>
        private void Generate_polygons(object sender, EventArgs e)
        {
            System_messages.Text = "Started loading your hotspots ; - )";
            List<GMapOverlay> overlays = controller.getHotspots(current_wind_direction, 
                current_wind_speed, current_metrics_color_coding);
            System_messages.Text = "Total number of Hotspots: " + overlays[1].Markers.Count.ToString();

            foreach(GMapOverlay gmo in overlays)
            {
                if(gmo!= null)
                {
                    map.Overlays.Add(gmo);
                    map.Update();
                    map.Zoom += 0.1;
                }
            }
            if(current_metrics_color_coding == speed_metrics_color_coding)
            {
                int delimiter = controller.MaxSpeedGlobal.ToString().Length;
                if(delimiter > 6)
                {
                    delimiter = 6;
                }
                label_legendmax.Text = controller.MaxSpeedGlobal.ToString().Substring(0, delimiter) + "m/s";

                delimiter = controller.MinSpeedGlobal.ToString().Length;
                if (delimiter > 6)
                {
                    delimiter = 6;
                }
                label_legendmin.Text = controller.MinSpeedGlobal.ToString().Substring(0, delimiter) + "m/s";
            }
            else if (current_metrics_color_coding == altitude_metrics_colord_coding)
            {
                int delimiter = controller.MaxAgGlobal.ToString().Length;
                if (delimiter > 8)
                {
                    delimiter = 8;
                }
                label_legendmax.Text = controller.MaxAgGlobal.ToString().Substring(0, delimiter) + "m";

                delimiter = controller.MinAgGlobal.ToString().Length;
                if (delimiter > 8)
                {
                    delimiter = 8;
                }
                
                label_legendmin.Text = controller.MinAgGlobal.ToString().Substring(0, delimiter) + "m";
            }
            else if (current_metrics_color_coding == thermals_count_color_coding)
            {

                label_legendmax.Text = controller.MaxThermalsCountGlobal.ToString();

               

                label_legendmin.Text = controller.MinThermalsCountGlobal.ToString();
            }




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
            else
            {
                current_metrics_color_coding = thermals_count_color_coding;
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

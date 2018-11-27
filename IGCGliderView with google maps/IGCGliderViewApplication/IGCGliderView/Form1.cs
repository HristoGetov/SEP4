using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace IGCGliderView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
              

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
         

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.DragButton = MouseButtons.Right;
            map.MapProvider = GMapProviders.BingMap;
            map.SetPositionByKeywords("Denmark");
            map.ShowCenter = false;
            map.MaxZoom = 100;
            map.MinZoom = 5;
            map.Zoom = 7;
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if(map.MapProvider == GMapProviders.BingMap)
            {
                map.MapProvider = GMapProviders.BingHybridMap;               
                map.ShowCenter = false;
                map.MaxZoom = 100;
                map.MinZoom = 5;
                
            }
            else if (map.MapProvider == GMapProviders.BingHybridMap)
            {
                map.MapProvider = GMapProviders.BingMap;              
                map.ShowCenter = false;
                map.MaxZoom = 100;
                map.MinZoom = 5;
                
            }
         
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    var pinPoint = map.FromLocalToLatLng(e.X, e.Y);

            //    //Load the Location on the map
            //    map.Position = pinPoint;

            //    // Add marker to the location
            //    var markers = new GMapOverlay("markers");
            //    var marker = new GMarkerGoogle(pinPoint, GMarkerGoogleType.red_small);

            //    markers.Markers.Add(marker);
            //    map.Overlays.Add(markers);



            //}
        }

       
    }
}

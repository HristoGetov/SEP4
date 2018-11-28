using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace IGCGliderView
{
    public partial class Form1 : Form
    {
        private List<PointLatLng> _points, _points2;
        private List<Color> colors;
        private GMapOverlay polygons;
        private bool polygons_created;

        public Form1()
        {
            InitializeComponent();
            _points = new List<PointLatLng>();
            _points2 = new List<PointLatLng>();
            _points.Add(new PointLatLng(55.8695819, 9.8765162));
            _points.Add(new PointLatLng(55.8757938, 9.8763445));
            _points.Add(new PointLatLng(55.8762632, 9.8592213));
            _points.Add(new PointLatLng(55.8685706, 9.8588779));

            _points2.Add(new PointLatLng(55.8600000, 9.8700000)); //a,b
            _points2.Add(new PointLatLng(55.8600000+0.005, 9.8700000)); //a+c, b
            _points2.Add(new PointLatLng(55.8600000 + 0.005, 9.8700000 + 0.01));//a+c, b+d
            _points2.Add(new PointLatLng(55.8600000, 9.8700000+0.01)); // a, b+d

            colors = new List<Color>();
            Color test_color = Color.Yellow;
            test_color = Color.FromArgb(170, test_color);
            colors.Add(test_color);
            test_color = Color.Gold;
            test_color = Color.FromArgb(170, test_color);
            colors.Add(test_color);
            test_color = Color.Teal;
            test_color = Color.FromArgb(170, test_color);
            colors.Add(test_color);
            test_color = Color.Sienna;
            test_color = Color.FromArgb(170, test_color);
            colors.Add(test_color);
            test_color = Color.Olive;
            test_color = Color.FromArgb(170, test_color);
            colors.Add(test_color);
            polygons = new GMapOverlay("polygons");
            polygons_created = false;

        }
       
              

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Generate_polygons(object sender, EventArgs e)
        {
            if (polygons_created == true)
            {
                return;
            }
            textBox_Message.Text = "Started Loading Polygons";
            int lim = 68800, i=0, j=0;
            double beginLong = +8.0000000, //we begin in the lower left corner of the grid 
                    endLong = +11.0000000, // we end in the upper right corner of the grid
                    beginLat = +54.5000000,
                    endLat = +57.5000000,
                    Long_step = 0.01666667,
                    Lat_step = 0.00833333,
                    Lat_index = beginLat,
                    Long_index = beginLong;
            List< PointLatLng>  Points = new List<PointLatLng>();
            
            do
            {
                Points.Clear();
                // start creating the square
                Points.Add(new PointLatLng( Math.Round(+Lat_index, 7), Math.Round(+Long_index, 7)));
                Points.Add(new PointLatLng( Math.Round(+Lat_index,7), Math.Round(+Long_index + Long_step, 7)));
                Points.Add(new PointLatLng( Math.Round(+Lat_index + Lat_step, 7), Math.Round(+Long_index + Long_step, 7)));
                Points.Add(new PointLatLng( Math.Round(+Lat_index + Lat_step,7), Math.Round(+Long_index, 7)));
                
                // use this square and insert it into a polygon
                var Polygon = new GMapPolygon(Points, ("tester" + i.ToString()))
                {
                    Stroke = new Pen(Color.Transparent),
                    Fill = new SolidBrush(Color.FromArgb(150, Color.Blue))
                };
                polygons.Polygons.Add(Polygon);
                Long_index += Long_step;
                if(Long_index >= endLong)
                {
                    Long_index = beginLong;
                    j++;
                    
                    map.Overlays.Add(polygons);
                    map.Update();
                    polygons = new GMapOverlay("polygons" + j.ToString());
                }
                Lat_index = beginLat + j * Lat_step;
                if(Lat_index >= endLat)
                {
                    textBox_Message.Text = "Polygons created" + polygons.Polygons.Count.ToString();
                    polygons_created = true;
                    return;
                }
                i++;
            } while (i < lim);
            //textBox_Message.Text = "Last Poly coord" + polygons.Polygons[polygons.Polygons.Count-1].Points[3].Lat.ToString();
            polygons_created = true;
            return;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Color test_color = Color.Yellow;
            test_color = Color.FromArgb(100, test_color);
            Color test_color_2 = Color.Blue;
            test_color_2 = Color.FromArgb(0, test_color_2);
            var Polygon = new GMapPolygon(_points, "test")
            {
                Stroke = new Pen(Color.Black),
                Fill = new SolidBrush(test_color)
            };
            polygons.Polygons.Add(Polygon);
            Polygon = new GMapPolygon(_points2, "test2")
            {
                Stroke = new Pen(Color.White),
                Fill = new SolidBrush(test_color)
            };
            polygons.Polygons.Add(Polygon);
            //textBox_Message.Text = "last Polygon : long " + polygons.Polygons[30000].Points[3].Lng.ToString();
            //map.Overlays.Add(polygons);
            //map.MarkersEnabled = true;
        }
    }
}

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
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.BingMap;
            map.SetPositionByKeywords("Denmark");
            map.ShowCenter = false;
            map.MaxZoom = 100;
            map.MinZoom = 5;
            map.Zoom = 7;
        }
    }
}

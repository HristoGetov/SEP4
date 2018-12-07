using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGCGliderView
{
    class Hotspot_container
    {
        public double Speed { set; get; }
        public double Altitude { set; get; }
        public decimal grid_cell_group { get; }
        public List<GridCell> gridCells { get; }
        public string cardinal_wind_direction { set; get; }
        public string wind_speed { set; get; }
        public Color color { set; get; }

        public Hotspot_container(double speed_read,
            double Altitude_gain, decimal gcg)
        {
            Speed = speed_read;
            Altitude = Altitude_gain;
            grid_cell_group = gcg;
            gridCells = new List<GridCell>();
        }


        public void add_gridCell (double lat, double lng)
        {
            gridCells.Add(new GridCell(lat, lng));
        }
    }
}

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
        public decimal? grid_cell_group { get; }
        public List<GridCell> gridCells { get; }
        public string cardinal_wind_direction { set; get; }
        public string wind_speed { set; get; }
        public Color color { set; get; }
        public decimal? grid_cell_id { set; get; }
        public double averageSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public double MinSpeed { get; set;  }

        //AG stands for Altitude Gain
        public double averageAG { get; set; }
        public double MaxAG { get; set; }
        public double MinAG { get; set; }

        public Hotspot_container(double speed_read,
            double Altitude_gain, decimal? gc_id)
        {
            Speed = speed_read;
            Altitude = Altitude_gain;
            grid_cell_group = gc_id;
            gridCells = new List<GridCell>();
        }

        /// <summary>
        /// This method goes through all the thermals in this hotspot and then refreshes the average,
        /// min & max for both altitude and speed
        /// </summary>
        public void refresh_statistics()
        {
            double temp_avg_speed=0, temp_maxSpeed=0, temp_minSpeed=100,
                temp_avg_ag=0, temp_max_ag=0, temp_min_ag = 100;
            for(int i=0; i<gridCells.Count; i++)
            {
                if(gridCells[i].speed > temp_maxSpeed)// Look if update is needed for max speed
                {
                    temp_maxSpeed = gridCells[i].speed;
                }

                if (gridCells[i].speed < temp_minSpeed)// Look if udpate is needed for min speed
                {
                    temp_minSpeed = gridCells[i].speed;
                }

                if (gridCells[i].altitude_gain > temp_max_ag)// Look if update is needed for max altitude gain
                {
                    temp_max_ag = gridCells[i].altitude_gain;
                }

                if (gridCells[i].altitude_gain < temp_min_ag)// Look if update is needed for min altitude gain
                {
                    temp_minSpeed = gridCells[i].altitude_gain;
                }
                temp_avg_speed += gridCells[i].speed;
                temp_avg_ag += gridCells[i].altitude_gain;
            }
            averageSpeed = temp_avg_speed / gridCells.Count;
            averageAG = temp_avg_ag / gridCells.Count;
            MaxSpeed = temp_maxSpeed;
            MinSpeed = temp_minSpeed;
            MaxAG = temp_max_ag;
            MinAG = temp_min_ag;
        }

        public void add_gridCell (double lat, double lng)
        {
            gridCells.Add(new GridCell(lat, lng));
        }

        public void add_gridCell (GridCell gridCell)
        {
            gridCells.Add(gridCell);
        }
    }
}

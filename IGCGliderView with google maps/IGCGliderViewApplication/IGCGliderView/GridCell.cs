using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGCGliderView
{
    class GridCell
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public double Long_center { set; get; }
        public double Lat_center { set; get; }

        public GridCell(double Lat, double lng)
        {
            Latitude = Lat;
            Longitude = lng;

            Long_center = Longitude + (0.01666667/2);
            Lat_center = Latitude + (0.00833333/2);
        }
    }
}

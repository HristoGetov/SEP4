namespace IGCGliderView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FRAANCUUS.HOTSPOTS")]
    public partial class HOTSPOT
    {
        [Key]
        public decimal HOTSPOTS_ID { get; set; }

        public decimal? GRID_CELL_ID { get; set; }

        public decimal MAX_SPEED { get; set; }

        public decimal MIN_SPEED { get; set; }

        public decimal MED_SPEED { get; set; }

        public decimal AVERAGE_SPEED { get; set; }

        public decimal MAX_ALTITUDEG { get; set; }

        public decimal MIN_ALTITUDEG { get; set; }

        public decimal MED_ALTITUDEG { get; set; }

        public decimal AVERAGE_ALTITUDEG { get; set; }

        public decimal NUMBER_OF_THERMAL { get; set; }

        [Required]
        [StringLength(1)]
        public string CARDINAL_WIND_DIRECTION { get; set; }

        [Required]
        [StringLength(1)]
        public string WIND_SPEED_TYPE { get; set; }

        public decimal LATITUDE_START { get; set; }

        public decimal LONGITUDE_START { get; set; }

        public decimal GRID_CELL_GROUP_ID { get; set; }
    }
}

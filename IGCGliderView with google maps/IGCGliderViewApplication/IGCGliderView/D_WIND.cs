namespace IGCGliderView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FRAANCUUS.D_WIND")]
    public partial class D_WIND
    {
        [Key]
        public decimal WIND_ID { get; set; }

        public decimal WIND_DIRECTION { get; set; }

        public decimal WIND_SPEED { get; set; }

        [Required]
        [StringLength(1)]
        public string CARDINAL_WIND_DIRECTION { get; set; }

        [Required]
        [StringLength(1)]
        public string WIND_SPEED_TYPE { get; set; }
    }
}

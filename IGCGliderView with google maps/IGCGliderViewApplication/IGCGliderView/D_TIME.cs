namespace IGCGliderView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FRAANCUUS.D_TIME")]
    public partial class D_TIME
    {
        [Key]
        public decimal TIME_ID { get; set; }

        public decimal? DHOUR { get; set; }

        public decimal? DMINUTE { get; set; }

        public decimal? DSECOND { get; set; }
    }
}

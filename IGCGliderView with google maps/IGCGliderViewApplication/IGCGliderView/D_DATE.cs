namespace IGCGliderView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FRAANCUUS.D_DATE")]
    public partial class D_DATE
    {
        [Key]
        public decimal DATE_ID { get; set; }

        public decimal? DDAY { get; set; }

        public decimal? DMONTH { get; set; }

        public decimal? DYEAR { get; set; }
    }
}

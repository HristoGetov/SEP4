namespace IGCGliderView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FRAANCUUS.D_GRID_CELL")]
    public partial class D_GRID_CELL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public D_GRID_CELL()
        {
            HOTSPOTS = new HashSet<HOTSPOT>();
        }

        [Key]
        public decimal GRID_CELL_ID { get; set; }

        public decimal LATITUDE_START { get; set; }

        public decimal LATITUDE_END { get; set; }

        public decimal LONGITUDE_START { get; set; }

        public decimal LONGITUDE_END { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOTSPOT> HOTSPOTS { get; set; }
    }
}

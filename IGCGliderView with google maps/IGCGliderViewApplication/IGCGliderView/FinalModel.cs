namespace IGCGliderView
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FinalModel : DbContext
    {
        public FinalModel()
            : base("name=FinalModel")
        {
        }

        public virtual DbSet<HOTSPOT> HOTSPOTS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.HOTSPOTS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.GRID_CELL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MAX_SPEED)
                .HasPrecision(3, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MIN_SPEED)
                .HasPrecision(3, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MED_SPEED)
                .HasPrecision(3, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.AVERAGE_SPEED)
                .HasPrecision(3, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MAX_ALTITUDEG)
                .HasPrecision(5, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MIN_ALTITUDEG)
                .HasPrecision(5, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.MED_ALTITUDEG)
                .HasPrecision(5, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.AVERAGE_ALTITUDEG)
                .HasPrecision(5, 1);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.NUMBER_OF_THERMAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.CARDINAL_WIND_DIRECTION)
                .IsUnicode(false);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.WIND_SPEED_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.LATITUDE_START)
                .HasPrecision(8, 6);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.LONGITUDE_START)
                .HasPrecision(7, 5);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.GRID_CELL_GROUP_ID)
                .HasPrecision(38, 0);
        }
    }
}

namespace IGCGliderView
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<D_DATE> D_DATE { get; set; }
        public virtual DbSet<D_GRID_CELL> D_GRID_CELL { get; set; }
        public virtual DbSet<D_TIME> D_TIME { get; set; }
        public virtual DbSet<D_WIND> D_WIND { get; set; }
        public virtual DbSet<HOTSPOT> HOTSPOTS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<D_DATE>()
                .Property(e => e.DATE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_DATE>()
                .Property(e => e.DDAY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_DATE>()
                .Property(e => e.DMONTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_DATE>()
                .Property(e => e.DYEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_GRID_CELL>()
                .Property(e => e.GRID_CELL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_GRID_CELL>()
                .Property(e => e.LATITUDE_START)
                .HasPrecision(5, 3);

            modelBuilder.Entity<D_GRID_CELL>()
                .Property(e => e.LATITUDE_END)
                .HasPrecision(5, 3);

            modelBuilder.Entity<D_GRID_CELL>()
                .Property(e => e.LONGITUDE_START)
                .HasPrecision(4, 2);

            modelBuilder.Entity<D_GRID_CELL>()
                .Property(e => e.LONGITUDE_END)
                .HasPrecision(4, 2);

            modelBuilder.Entity<D_TIME>()
                .Property(e => e.TIME_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_TIME>()
                .Property(e => e.DHOUR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_TIME>()
                .Property(e => e.DMINUTE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_TIME>()
                .Property(e => e.DSECOND)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_WIND>()
                .Property(e => e.WIND_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_WIND>()
                .Property(e => e.WIND_DIRECTION)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_WIND>()
                .Property(e => e.WIND_SPEED)
                .HasPrecision(38, 0);

            modelBuilder.Entity<D_WIND>()
                .Property(e => e.CARDINAL_WIND_DIRECTION)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<D_WIND>()
                .Property(e => e.WIND_SPEED_TYPE)
                .IsFixedLength()
                .IsUnicode(false);

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
                .Property(e => e.NUMBER_OF_THERMAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.CARDINAL_WIND_DIRECTION)
                .IsUnicode(false);

            modelBuilder.Entity<HOTSPOT>()
                .Property(e => e.WIND_SPEED_TYPE)
                .IsUnicode(false);
        }
    }
}

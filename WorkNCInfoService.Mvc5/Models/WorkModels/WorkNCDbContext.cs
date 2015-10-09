namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WorkNCDbContext : DbContext
    {
        public WorkNCDbContext()
            : base("name=WorkNCDbContext")
        {
        }

        public virtual DbSet<WorkNC_Company> WorkNC_Company { get; set; }
        public virtual DbSet<WorkNC_Factory> WorkNC_Factory { get; set; }
        public virtual DbSet<WorkNC_Machine> WorkNC_Machine { get; set; }
        public virtual DbSet<WorkNC_WorkZone> WorkNC_WorkZone { get; set; }
        public virtual DbSet<WorkNC_WorkZoneDetail> WorkNC_WorkZoneDetail { get; set; }
        public virtual DbSet<WorkNC_WorkZoneDetailProblem> WorkNC_WorkZoneDetailProblem { get; set; }
        public virtual DbSet<ImageGallery> ImageGallery { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkNC_Factory>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<WorkNC_WorkZone>()
                .Property(e => e.MachiningTimeTotal)
                .IsFixedLength();

            modelBuilder.Entity<WorkNC_WorkZone>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<WorkNC_WorkZoneDetail>()
                .Property(e => e.MachineTime)
                .IsFixedLength();

            modelBuilder.Entity<WorkNC_WorkZoneDetail>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<WorkNC_WorkZoneDetailProblem>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);
        }
    }
}

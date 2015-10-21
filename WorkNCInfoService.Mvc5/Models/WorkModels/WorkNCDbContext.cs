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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<WorkNC_Company> WorkNC_Company { get; set; }
        public virtual DbSet<WorkNC_Factory> WorkNC_Factory { get; set; }
        public virtual DbSet<WorkNC_Machine> WorkNC_Machine { get; set; }
        public virtual DbSet<WorkNC_UserPermission> WorkNC_UserPermission { get; set; }
        public virtual DbSet<WorkNC_WorkZone> WorkNC_WorkZone { get; set; }
        public virtual DbSet<WorkNC_WorkZoneDetail> WorkNC_WorkZoneDetail { get; set; }
        public virtual DbSet<WorkNC_WorkZoneDetailProblem> WorkNC_WorkZoneDetailProblem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

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

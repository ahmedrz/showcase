namespace PortSystem.DAL
{
    using System.Data.Entity;
    using PortSystem.DAL.Migrations;

    public partial class EmanifestContext : DbContext
    {
        public EmanifestContext()
            : base("name=EmanifestContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmanifestContext, Configuration>());
        }

        public virtual DbSet<BOLDetails> BOLDetails { get; set; }
        public virtual DbSet<Carriers> Carriers { get; set; }
        public virtual DbSet<ConsignmentDetails> ConsignmentDetails { get; set; }
        public virtual DbSet<ContainerDetails> ContainerDetails { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VehicleDetails> VehicleDetails { get; set; }
        public virtual DbSet<VoyageDetails> VoyageDetails { get; set; }
        public virtual DbSet<Vessels> Vessels { get; set; }
        public virtual DbSet<PackageCodes> PackageCodes { get; set; }
        public virtual DbSet<UNHSCodes> UNHSCodes { get; set; }
        public virtual DbSet<CountryCodes> CountryCodes { get; set; }
        public virtual DbSet<LocationCodes> LocationCodes { get; set; }
        public virtual DbSet<Lines> Lines { get; set; }
        public virtual DbSet<ContainerIsoCodes> ContainerIsoCodes { get; set; }
        public virtual DbSet<UNHazardousCodes> UNHazardousCodes { get; set; }
        public virtual DbSet<Ports> Ports { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

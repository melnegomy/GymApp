namespace GymApp.DAL.Context
{
    public class GymDbContext : DbContext
    {
        // Constructor for the GymDbContext class, which takes DbContextOptions as a parameter and passes it to the base DbContext class.
        // option will created in Program.cs file and will be injected into the constructor of GymDbContext class.
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
        }
    }
}

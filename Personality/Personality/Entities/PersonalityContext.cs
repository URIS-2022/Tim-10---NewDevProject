using Microsoft.EntityFrameworkCore;

namespace Personality.Entities
{
    public class PersonalityContext : DbContext
    {
        public PersonalityContext() { }
        public readonly IConfiguration? configuration;
        public DbSet<Personality> Personalities { get; set; }
        public PersonalityContext(DbContextOptions<PersonalityContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=PersonalityDB;Integrated Security=True;Connect Timeout=30");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personality>()
                .HasData(
                new Personality
                {
                    personalityId = Guid.Parse("E5EE8B4D-110F-4066-81F2-A03144FBCAAF"),
                    name = "Pera",
                    surname = "Peric",
                    function = "test",
                },
                new Personality
                {
                    personalityId = Guid.Parse("1F6F2144-1655-410C-9E6A-ADB3662A46F8"),
                    name = "Ivana",
                    surname = "Ivanovic",
                    function = "funkcija",
                }
                );
        }


    }
}

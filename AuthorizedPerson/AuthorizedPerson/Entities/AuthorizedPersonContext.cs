using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthorizedPerson.Entities
{
    public class AuthorizedPersonContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AuthorizedPersonContext() { }
 
        public AuthorizedPersonContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<AuthorizedPersonModel> authorizedPeople { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuthorizedPersonDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorizedPersonModel>().
                HasData(new 
                {
                    authorizedPersonId = Guid.Parse("6659FEF1-30DC-4C5B-93E6-7F96BEB1AFEF"),
                    name= "Amila",
                    surname="Salihbegovic",
                    ducumentNumber="1234453",
                    tableNumber="234",
                    addressId = Guid.Parse("E5A687A0-8F6E-4DE4-8241-3B0FEB36B0FD")
                });
            modelBuilder.Entity<AuthorizedPersonModel>().
               HasData(new 
               {
                   authorizedPersonId = Guid.Parse("23F2A8FF-E5DF-495B-8C11-0B64016B8551"),
                   name = "Almir",
                   surname = "Salihbegovic",
                   ducumentNumber = "2345323",
                   tableNumber = "12345",
                   addressId = Guid.Parse("FCC355E0-28C8-44F3-8E4B-3C5AFF7D3903")
               });

        }
    }
}

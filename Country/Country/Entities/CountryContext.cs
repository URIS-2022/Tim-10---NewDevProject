using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Country.Entities
{
    public class CountryContext : DbContext
    {
        public CountryContext() { }

        public readonly IConfiguration configuration;
        public CountryContext(DbContextOptions<CountryContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Country1> Country1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CountryDB;Integrated Security=True;Connect Timeout=30");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasData(new 
                {
                    addressId = Guid.Parse("0DC88D24-82A7-4AEC-9464-0D06B9E119CA"),
                    place = "NS",
                    zipCode = 21000,
                    street = "Milosa Crnjanskog 4"
                }
                );
            modelBuilder.Entity<Country1>()
                .HasData(new 
                {
                    countryId = Guid.Parse("C1BA9FAC-43FA-4502-9F93-D3F772DCA929"),
                    nameConuntry = "Srbija"
                }
                );
        }
    }
}

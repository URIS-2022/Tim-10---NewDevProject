using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Contract.Entities
{
    public class Context : DbContext
    {
 
        public readonly IConfiguration configuration;
        public DbSet<TypeOfGuaranteeEntity> TypeOfGuaranteeEntity { get; set; }
        public DbSet<ContractEntity> ContractEntity { get; set; }
        public Context() { }
        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Contract;Integrated Security=True;Connect Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeOfGuaranteeEntity>()
               .HasData(new TypeOfGuaranteeEntity
               {
                   typeId = Guid.Parse("06CFBBBF-39E1-485C-BD54-3CB336F25242"),
                   type = "Monthly"
               });

            modelBuilder.Entity<TypeOfGuaranteeEntity>()
                .HasData(new TypeOfGuaranteeEntity
                {
                    typeId = Guid.Parse("5E6F0201-B31A-4767-8087-910E3C91DCC4"),
                    type = "Quarterly"
                });

            modelBuilder.Entity<ContractEntity>()
                .HasData(new ContractEntity
                {
                    contractId = Guid.Parse("42889DFC-4E97-49B0-827E-80066DCF48A4"),
                    typeId = Guid.Parse("06CFBBBF-39E1-485C-BD54-3CB336F25242"),
                    documentId = Guid.Parse("E8522D7B-5261-4588-907F-4DBBA12D6AED"),
                    referenceNumber = "123/RS",
                    publicBiddingId = Guid.Parse("192CB74B-D8D9-4430-82A3-F585A7E89689"),
                    dateOfContract = DateTime.Parse("2022-12-12T10:00:00"),
                    buyerId = Guid.Parse("D14EE77F-B24C-4F06-9C6F-016552927E94"),
                    deadline = DateTime.Parse("2024-11-15T09:00:00"),
                    place = "Subotica",
                    dateOfSigning = DateTime.Parse("2022-12-12T10:00:00")
                });

            modelBuilder.Entity<ContractEntity>()
                .HasData(new ContractEntity
                {
                    contractId = Guid.Parse("EDF365DC-83F7-4402-B1C4-ECD794952FD4"),
                    typeId = Guid.Parse("5E6F0201-B31A-4767-8087-910E3C91DCC4"),
                    documentId = Guid.Parse("D450BE56-6CA0-4624-8673-21D9B57517AF"),
                    referenceNumber = "123/RS",
                    publicBiddingId = Guid.Parse("9128178C-B6BC-4C61-A58E-4D994EE9A4F5"),
                    dateOfContract = DateTime.Parse("2020-09-17T09:00:00"),
                    buyerId = Guid.Parse("BBDE3AF2-1804-43AE-9D83-AC631A72D6F5"),
                    deadline = DateTime.Parse("2023-12-27T09:00:00"),
                    place = "Novi Sad",
                    dateOfSigning = DateTime.Parse("2020-09-17T09:00:00")
                });
        }
    }
}

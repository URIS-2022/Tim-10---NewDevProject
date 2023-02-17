using Microsoft.EntityFrameworkCore;

namespace Buyer.Entities
{
    public class BuyerContext : DbContext
    {
        private readonly IConfiguration configuration;
        public BuyerContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Individual> individuals { get; set; }
        public DbSet<LegalEntity> legalEntities { get; set; }
        public DbSet<ContactPerson> contactPerson { get; set; }
        public DbSet<PriorityModel> priorities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BuyerDB"));
        }

        //Initial data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creating priorities
            modelBuilder.Entity<PriorityModel>().HasData(new
            {
                priorityId = Guid.Parse("1BB9CB0A-A2AD-4FF3-BBAA-BA312E968A9B"),
                priorityType = "Test priority number 1"
            });
            modelBuilder.Entity<PriorityModel>().HasData(new
            {
                priorityId = Guid.Parse("12C7B642-416E-4358-90CA-9DDB67336F63"),
                priorityType = "Test priority type number 2"
            });
            //Creating priorities

            //Creating contact people
            modelBuilder.Entity<ContactPerson>().HasData(new
            {
                contactPersonId = Guid.Parse("E1ED563F-E902-4D84-92C9-AE1E066952A2"),
                name = "Amila",
                surname = "Salihbegovic",
                function = "function1",
                phoneNumber = "03245345654"
            });
            modelBuilder.Entity<ContactPerson>().HasData(new
            {
                contactPersonId = Guid.Parse("65979E67-38D1-4B1F-B636-2D8C09DE25EA"),
                name = "Almir",
                surname = "Salihbegovic",
                function = "function2",
                phoneNumber = "02434354224"
            });
            //Creating contact people

            //Individuals
            modelBuilder.Entity<Individual>().HasData(new
            {
                buyerId = Guid.Parse("F5A13586-1A5C-4FEA-ADBE-0F352CA13371"),
                buyerType = true,
                area = "15000",
                ban = false,
                banStartingDate = DateTime.Parse("1900-01-01T09:00:00"),
                banLasting = "0",
                banEndingDate = DateTime.Parse("1900-01-01T09:00:00"),
                authorizedPersonId = Guid.Parse("4F22E39E-3E7D-4063-AECE-CB9BF65B37CE"),
                priorityId = Guid.Parse("12C7B642-416E-4358-90CA-9DDB67336F63"),
                phoneNumber1 = "2131231412",
                phoneNumber2 = "8974839473",
                emailAddress = "123@gmail.com",
                addressId = "addresstesttt",
                paymentId = "111111111111",
                publicBiddingId = "bidding1",
                individualName = "Amila",
                individualSurname = "Salihbegovic",
                individualId = "280100798916",
                accountNumber = "2489de9e32"
            });
            //individuals

            //Legal entity 
            modelBuilder.Entity<LegalEntity>().HasData(new
            {
                buyerId = Guid.Parse("2F108769-AAF3-4829-9263-72523BBB223E"),
                buyerType = false,
                area = "155000",
                ban = true,
                banStartingDate = DateTime.Parse("2022-01-01T09:00:00"),
                banLasting = "355",
                banEndingDate = DateTime.Parse("2023-01-01T09:00:00"),
                authorizedPersonId = Guid.Parse("4F22E39E-3E7D-4063-AECE-CB9BF65B37CE"),
                priorityId = Guid.Parse("1BB9CB0A-A2AD-4FF3-BBAA-BA312E968A9B"),
                phoneNumber1 = "2345435675",
                phoneNumber2 = "8974839473",
                emailAddress = "34455@gmail.com",
                addressId = "addresstestno2",
                paymentId = "vvvvvvvvvvvvvvv",
                publicBiddingId = "bidding2",
                accountNumber = "23534234563",
                legalEntityName = "name",
                legalEntityId = "12432434",
                legalEntityFax = "fax",
                contactPerson = Guid.Parse("E1ED563F-E902-4D84-92C9-AE1E066952A2")

            });
            //Legal entity
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace payment.Entities
{
    public class PaymentContext : DbContext

       

    {
        public PaymentContext()
        {

        }

        public readonly IConfiguration? configuration;
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {

        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PaymentDB;Integrated Security=True;Connect Timeout=30;");
 
        }
                

        /// <summary>
        /// Popunjava bazu podataka inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRate>()
              .HasData(new
              {
                  exchangeRateId = Guid.Parse("D45DA337-EFFC-4D60-8D22-79D27F248D7F"),
                  date = DateTime.Parse("2023-2-12"),
                  currency = "RSD",
                  value = float.Parse("1234")
              });
            modelBuilder.Entity<ExchangeRate>()
              .HasData(new
              {
                  exchangeRateId = Guid.Parse("A7170F6A-33A1-431E-9B61-267AAF398297"),
                  date = DateTime.Parse("2023-2-13"),
                  currency = "RSD",
                  value = float.Parse("4321")
              });

            modelBuilder.Entity<Payment>()
                .HasData(new
                {
                    //UplataID = Guid.Parse("608eba57-ec53-4286-b745-b4db269a611c"),
                    paymentId = Guid.Parse("2FB18B50-A4F2-4B06-A060-C8B84E4BC349"),
                    accountNumber = "236541",
                    referenceNumber = "147852",
                    amount = float.Parse("4321"),
                    paymentPurpose = "Uplata javnog nadmetanja",
                    date = DateTime.Parse("2023-2-13"),
                    exchangeRateId = Guid.Parse("D45DA337-EFFC-4D60-8D22-79D27F248D7F"),
                    buyerId = Guid.Parse("9AAB5A84-057F-44A5-A382-8D066C36A342"),
                    publicBiddingId = Guid.Parse("DCC0AEF3-2598-4B54-B3EF-853696F57488")
                });

            modelBuilder.Entity<Payment>()
               .HasData(new
               {
                   //UplataID = Guid.Parse("608eba57-ec53-4286-b745-b4db269a611c"),
                   paymentId = Guid.Parse("2475979C-1AFE-437A-ACC1-42C749F9C900"),
                   accountNumber = "236541",
                   referenceNumber = "147852",
                   amount = float.Parse("4321"),
                   paymentPurpose = "Uplata javnog nadmetanja",
                   date = DateTime.Parse("2022-2-14"),
                   exchangeRateId = Guid.Parse("A7170F6A-33A1-431E-9B61-267AAF398297"),
                   buyerId = Guid.Parse("367DE211-7928-4BB6-8EEA-81A1E77397FE"),
                   publicBiddingId = Guid.Parse("DCC0AEF3-2598-4B54-B3EF-853696F57488")
               });
        }








    }
}

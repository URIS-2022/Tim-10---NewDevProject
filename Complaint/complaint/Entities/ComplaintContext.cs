using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;




namespace complaint.Entities
{
    public class ComplaintContext : DbContext
    {

        public ComplaintContext()
        {

        }
        public ComplaintContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public readonly IConfiguration _configuration;

        
        public DbSet<ComplaintType> ComplaintType { get; set; }
        public DbSet<ComplaintStatus> ComplaintStatus { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<Complaint> Complaint { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ComplaintDB;Integrated Security=SSPI;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplaintType>()
                .HasData(new
                {
                    complaintTypeId = Guid.Parse("F98DE9DC-5A4A-4EE2-BCCC-FBA4134DD97A"),
                    typeName = "Žalba na tok javnog nadmetanja"
                },
                new
                {
                    complaintTypeId = Guid.Parse("53B25384-45C5-4F30-8B27-3DB311E855FB"),
                    typeName = "Žalba na Odluku o davanju u zakup"
                },
                new
                {
                    complaintTypeId = Guid.Parse("071849EB-3561-40FE-9DCF-1F57FA7F6FF8"),
                    typeName = "Žalba na Odluku o davanju na korišćenje"
                }
                );
            modelBuilder.Entity<ComplaintStatus>()
               .HasData(new
               {
                   complaintStatusId = Guid.Parse("5C416D45-715A-4432-B2B6-2DF9046FE828"),
                   statusName = "Usvojena"
               },
               new
               {
                   complaintStatusId = Guid.Parse("436B9E51-057A-404D-AB52-155A2B4D8071"),
                   statusName = "Odbijena"
               },
               new
               {
                   complaintStatusId = Guid.Parse("02B27D09-4958-4245-BE2C-76E434E39351"),
                   statusName = "Otvorena"
               }
               );
            modelBuilder.Entity<Action>()
               .HasData(new
               {
                   actionId = Guid.Parse("0FF49176-03FF-4E8E-9878-038A56E35A5B"),
                   actionName = "JN ide u drugi krug sa novim uslovima"
               },
               new
               {
                   actionId = Guid.Parse("DF859A22-1CE8-466C-B919-F4CFBEA3C7A6"),
                   actionName = "JN ide u drugi krug sa starim uslovima"
               },
               new
               {
                   actionId = Guid.Parse("228C0094-41ED-4455-BD11-0F024DD199E9"),
                   actionName = "JN ne ide u drugi krug"
               }
               );
            modelBuilder.Entity<Complaint>()
              .HasData(new
              {
                  complaintId = Guid.Parse("A6C49AE9-75F8-4685-8671-B74CC94EBFC0"),
                  complaintDate = DateTime.Parse("2023-02-15T11:00:00"),
                  complaintSubmitter = Guid.Parse("702E05D2-AFEA-48B0-A8AE-48AC259915C1"),
                  complaintNumber = "1234",
                  cause = "Krsenje pravilnika za javno nadmetanje",
                  reason = "Neispravnost prilikom dodeljivanja parcele",
                  rescriptDate = DateTime.Parse("2023-03-11T10:00:00"),
                  rescriptNumber = "1035",
                  decisionNumber = "1221",
                  actionId = Guid.Parse("0FF49176-03FF-4E8E-9878-038A56E35A5B"),
                  complaintStatusId = Guid.Parse("5C416D45-715A-4432-B2B6-2DF9046FE828"),
                  complaintTypeId = Guid.Parse("F98DE9DC-5A4A-4EE2-BCCC-FBA4134DD97A")

              },
              new
              {
                  complaintId = Guid.Parse("B136E4A4-0009-4113-AD40-7F3A0483152B"),
                  complaintDate = DateTime.Parse("2023-02-15T11:00:00"),
                  complaintSubmitter = Guid.Parse("301EE496-B6F6-4EE4-B40E-6DD782B7E426"),
                  complaintNumber = "1234",
                  cause = "Krsenje pravilnika za javno nadmetanje",
                  reason = "Neispravnost prilikom dodeljivanja parcele",
                  rescriptDate = DateTime.Parse("2023-03-11T10:00:00"),
                  rescriptNumber = "1035",
                  decisionNumber = "1221",
                  actionId = Guid.Parse("DF859A22-1CE8-466C-B919-F4CFBEA3C7A6"),
                  complaintStatusId = Guid.Parse("5C416D45-715A-4432-B2B6-2DF9046FE828"),
                  complaintTypeId = Guid.Parse("F98DE9DC-5A4A-4EE2-BCCC-FBA4134DD97A")

              }

              );
        }





    }
}

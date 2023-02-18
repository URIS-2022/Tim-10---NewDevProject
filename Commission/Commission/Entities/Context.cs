using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Commission.Entities
{
    public class Context : DbContext
    {
        
        public DbSet<CommissionEntity> Commission { get; set; }
        public DbSet<PresidentEntity> President { get; set; }
        public DbSet<MemberEntity> Member { get; set; }
        public readonly IConfiguration configuration;
        public Context() { }

        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Commission;Integrated Security=True;Connect Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PresidentEntity>()
                .HasData(new PresidentEntity
                {
                    presidentId = Guid.Parse("F5468F83-D3AF-49DF-8136-7D5323CAD68B"),
                    personalityId = Guid.Parse("274D5A86-9E8C-481A-BFAC-7043DB9EF65A")
                },
                new PresidentEntity
                {
                    presidentId = Guid.Parse("2D31D380-627E-4CD8-B29A-76D21C6C65B3"),
                    personalityId = Guid.Parse("916C6F7B-651B-440D-B811-7FE9E35D38AB")
                },
                new PresidentEntity
                {
                    presidentId = Guid.Parse("99832B19-A420-4420-8FC9-10B22A7B0324"),
                    personalityId = Guid.Parse("19E4A45D-0E40-42DC-B502-CD774F606E87")
                }
                );
            modelBuilder.Entity<CommissionEntity>()
                .HasData(new CommissionEntity
                {
                    commissionId = Guid.Parse("03DCF963-9569-4773-AE05-F205A97FFCC7"),
                    nameOfCommission = "First",
                    presidentId = Guid.Parse("F5468F83-D3AF-49DF-8136-7D5323CAD68B")
                },
                new CommissionEntity
                {
                    commissionId = Guid.Parse("C4E35C57-ADDB-4C11-B476-DC91F9FF14A2"),
                    nameOfCommission = "Second",
                    presidentId = Guid.Parse("F5468F83-D3AF-49DF-8136-7D5323CAD68B")
                },
                new CommissionEntity
                {
                    commissionId = Guid.Parse("4B7C8B4D-BCB1-433B-BD88-38FF0032CAAF"),
                    nameOfCommission = "Third",
                    presidentId = Guid.Parse("F5468F83-D3AF-49DF-8136-7D5323CAD68B")
                }
                );
            modelBuilder.Entity<MemberEntity>()
                .HasData(new MemberEntity
                {
                    memberId = Guid.Parse("1084E8C4-C92B-4EA3-9C20-0CA8BC8D5917"),
                    personalityId =Guid.Parse("57029EB1-8C5F-4A55-824E-344A4DF697AD"),
                    commissionId = Guid.Parse("4B7C8B4D-BCB1-433B-BD88-38FF0032CAAF")
                },
                new MemberEntity
                {
                    memberId = Guid.Parse("CF18DA5D-07F3-4E56-90A2-03C8A9BDE369"),
                    personalityId = Guid.Parse("4A664B46-7959-4C8D-B830-618E3BEA1AA2"),
                    commissionId = Guid.Parse("C4E35C57-ADDB-4C11-B476-DC91F9FF14A2")
                }
                );
        }
    }
}


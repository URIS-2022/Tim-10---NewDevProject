using Microsoft.EntityFrameworkCore;

namespace User1.Entities
{
    public class UserContext : DbContext
    {
            public UserContext() { }
            public readonly IConfiguration? configuration;
            public DbSet<User> User { get; set; }
            public DbSet<UserType> UserType { get; set; }
            public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
            {
                this.configuration = configuration;
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30");
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    userId = Guid.Parse("18A38124-3EB4-44DC-941A-1D164661B615"),
                    userTypeId = Guid.Parse("95D791CC-A0C9-4EBD-A598-9CCAD0022A78"),
                    name = "Una",
                    surname = "Obradovic",
                    username = "UUna",
                    password = "123456",
                },
                new User
                {
                    userId = Guid.Parse("90FA6CDE-79E2-4B82-B0A5-28D70C66E2DD"),
                    userTypeId = Guid.Parse("50D0D37E-B01A-4D48-AA12-BE3ACC5CF379"),
                    name = "Dusan",
                    surname = "Markovic",
                    username = "MMarkovic",
                    password = "123456",
                }
                );
            modelBuilder.Entity<UserType>()
                .HasData(
                new UserType
                {
                    userTypeId = Guid.Parse("17F97A34-89B3-48FA-A6C0-265D15A18D3C"),
                    role = "Admin"
                }
                );
        }
    }
  
}

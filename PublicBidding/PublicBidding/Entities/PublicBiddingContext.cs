using Microsoft.EntityFrameworkCore;

namespace PublicBidding.Entities
{
	/// <summary>
	/// Status of public bidding entity
	/// </summary>
	public class PublicBiddingContext : DbContext
	{
		public readonly IConfiguration configuration;
		public PublicBiddingContext() 
		{
		}

		public PublicBiddingContext(DbContextOptions<PublicBiddingContext> options) : base(options)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PublicBiddingDB;Integrated Security=True;Connect Timeout=30");
		}
		

		public DbSet<TypeOfPublicBidding> typesOfPublicBidding { get; set; }

		public DbSet<StatusOfPublicBidding> statusesOfPublicBidding { get; set; }

		public DbSet<PublicBidding> publicBiddings { get; set; }

		public DbSet<Licitation> licitations { get; set; }

		/// <summary>
		/// Fills the database with initial data
		/// </summary>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TypeOfPublicBidding>()
			  .HasData(new
			  {
				  typePublicBiddingId = Guid.Parse("E7AB1800-0064-49FC-9671-82A45DDC53F2"),
				  typePublicBiddingName = "Javna licitacija"
			  });
			modelBuilder.Entity<TypeOfPublicBidding>()
			  .HasData(new
			  {
				  typePublicBiddingId = Guid.Parse("98CE7DCE-7F2A-4142-85EF-C261C76C76C2"),
				  typePublicBiddingName = "Javno otvaranje zatvorenih ponuda"
			  });

			modelBuilder.Entity<StatusOfPublicBidding>()
			   .HasData(new
			   {
				   statusOfPublicBiddingId = Guid.Parse("8587D9BA-BAA5-44D0-9E47-50E0253BFA9B"),
				   statusOfPublicBiddingName = "Prvi krug"
			   });
			modelBuilder.Entity<StatusOfPublicBidding>()
			  .HasData(new
			  {
				  statusOfPublicBiddingId = Guid.Parse("97C0078F-D01D-4D14-8A9C-46B0AF243ED0"),
				  statusOfPublicBiddingName = "Drugi krug sa starim uslovima"
			  });
			modelBuilder.Entity<StatusOfPublicBidding>()
			  .HasData(new
			  {
				  statusOfPublicBiddingId = Guid.Parse("A3FC511F-9D3E-4DA6-82BB-20E289E9702F"),
				  statusOfPublicBiddingName = "Drugi krug sa novim uslovima"
			  });

			modelBuilder.Entity<PublicBidding>()
				.HasData(new
				{
					publicBiddingId = Guid.Parse("35D85C17-47A3-4C2D-831E-D9DC4243A670"),
					date = DateTime.Parse("2023-2-17"),
					timeOfBeginning = DateTime.Parse("2023-2-17T08:00:00"),//godina, mesec, dan, sat, minut, sekunda
					timeOfEnd = DateTime.Parse("2023-2-17T10:00:00"),
					initialPricePerHectare = 5000,
					excepted = false,
					typePublicBiddingId = Guid.Parse("E7AB1800-0064-49FC-9671-82A45DDC53F2"),
					auctionedPrice = 7500,
					leasePeriod = 12,
					numberOfParticipants = 10,
					depositTopUpAmount = 500,
					circle = 1,
					statusOfPublicBiddingId = Guid.Parse("8587D9BA-BAA5-44D0-9E47-50E0253BFA9B"),
					addressId = Guid.Parse("7E96DF4A-2908-4F39-8CC0-BA710615B2AF"),
					authorizedBidderPersonId = Guid.Parse("69FDC285-DD45-4FB9-9BC8-C5E42428C9F4"),
					parcelsId = new List<Guid>() { Guid.Parse("B823F2AC-0022-4758-ABD9-2BDC6A36BF95") },
					buyerId = Guid.Parse("F1053E62-7E19-47CD-AFCB-4D360838793E"),
					buyersId = new List<Guid>() { Guid.Parse("F72EC03D-0B58-4A90-909A-DF79BA497EC1") },
					userId = Guid.Parse("492F1A72-7BA4-4F7B-AA25-CFFB8903ED48")
				});

			modelBuilder.Entity<PublicBidding>()
			   .HasData(new
			   {
				   publicBiddingId = Guid.Parse("CBA217A7-7E8E-48A8-813A-9404CEBF8F56"),
				   date = DateTime.Parse("2023-2-18"),
				   timeOfBeginning = DateTime.Parse("2023-2-18T08:00:00"),
				   timeOfEnd = DateTime.Parse("2023-2-18T10:00:00"),
				   initialPricePerHectare = 4000,
				   excepted = false,
				   typePublicBiddingId = Guid.Parse("98CE7DCE-7F2A-4142-85EF-C261C76C76C2"),
				   auctionedPrice = 6000,
				   leasePeriod = 12,
				   numberOfParticipants = 10,
				   depositTopUpAmount = 400,
				   circle = 1,
				   statusOfPublicBiddingId = Guid.Parse("97C0078F-D01D-4D14-8A9C-46B0AF243ED0"),
				   addressId = Guid.Parse("7E96DF4A-2908-4F39-8CC0-BA710615B2AF"),
				   authorizedBidderPersonId = Guid.Parse("69FDC285-DD45-4FB9-9BC8-C5E42428C9F4"),
				   parcelsId = new List<Guid>() { Guid.Parse("B823F2AC-0022-4758-ABD9-2BDC6A36BF95") },
				   buyerId = Guid.Parse("F1053E62-7E19-47CD-AFCB-4D360838793E"),
				   buyersId = new List<Guid>() { Guid.Parse("F72EC03D-0B58-4A90-909A-DF79BA497EC1") },
				   userId = Guid.Parse("7836E78D-26D4-441D-843F-21062CDA2240")
			   });

			modelBuilder.Entity<Licitation>()
				.HasData(new
				{
					licitationId = Guid.Parse("EB72C2D4-2159-4146-AD8D-11EB02791E8F"),
					number = 1,
					year = 2023,
					date = DateTime.Parse("2023-2-17"),
					restrictions = 1,
					priceDifference = 100,
					listOfDocumentationOfIndividuals = new List<string>() { "dok1_fl", "dok2_fl" },
					listOfDocumentationOfLegalEntities = new List<string>() { "dok1_pl", "dok1_pl" },
					publicBiddingId = Guid.Parse("35D85C17-47A3-4C2D-831E-D9DC4243A670"),
					deadlineForSubmissionOfApplications = DateTime.Parse("2023-2-15")

				});
			modelBuilder.Entity<Licitation>()
			   .HasData(new
			   {
				   licitationId = Guid.Parse("A856FEA4-557F-4923-A5DC-FEBFFD8B7744"),
				   number = 2,
				   year = 2023,
				   date = DateTime.Parse("2023-2-18"),
				   restrictions = 1,
				   priceDifference = 200,
				   listOfDocumentationOfIndividuals = new List<string>() { "dok1_fl", "dok2_fl" },
				   listOfDocumentationOfLegalEntities = new List<string>() { "dok1_pl", "dok1_pl" },
				   publicBiddingId = Guid.Parse("35D85C17-47A3-4C2D-831E-D9DC4243A670"),
				   deadlineForSubmissionOfApplications = DateTime.Parse("2023-2-16")
			   });
		}

	}
}

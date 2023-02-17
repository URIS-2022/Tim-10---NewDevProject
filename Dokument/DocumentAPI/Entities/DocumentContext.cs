using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DocumentAPI.Entities
{
	public class DocumentContext : DbContext
	{
		private readonly IConfiguration configuration;

		public DocumentContext()
		{
	
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DocumentDB;Integrated Security=True;Connect Timeout=30;");
		}
		public DocumentContext(DbContextOptions options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		public DbSet<StatusOfDocumentEntity> StatusOfDocumentEntity { get; set; }
		public DbSet<DocumentEntity> DocumentEntity { get; set; }
		public DbSet<TypeOfDocumentEntity> TypeOfDocumentEntity { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<StatusOfDocumentEntity>()
			   .HasData(new StatusOfDocumentEntity
			   {
				   statusOfDocumentId = Guid.Parse("F7097B00-A82F-4A74-9EA5-9B97D57ADA4A"),
				   adopted = true,
				   rejected = false,
				   opened = false,
				   modified = false
			   });

			builder.Entity<StatusOfDocumentEntity>()
				.HasData(new StatusOfDocumentEntity
				{
					statusOfDocumentId = Guid.Parse("2F73E247-1181-4BE5-BB27-D644BDF97026"),
					adopted = false,
					rejected = false,
					opened = true,
					modified = true
				});

			builder.Entity<DocumentEntity>()
				.HasData(new DocumentEntity
				{
					documentId = Guid.Parse("1391AD03-80D4-4B47-A2FD-79802AA870AA"),
					statusOfDocumentId = Guid.Parse("F7097B00-A82F-4A74-9EA5-9B97D57ADA4A"),
					typeOfDocumentId = Guid.Parse("0E6E43AF-D3E6-463F-89A2-EC35A45413E7"),
					referenceNumber = "15548/RS7",
					date = DateTime.Parse("2021-11-15T09:00:00"),
					dateAdoptionDocument = DateTime.Parse("2021-12-15T09:00:00"),
					userId = Guid.Parse("6CCF941E-B3BB-41A0-BAC4-B11B0F27B4C3")
				});

			builder.Entity<DocumentEntity>()
				.HasData(new DocumentEntity
				{
					documentId = Guid.Parse("cfe84b37-bb6d-498d-a546-5dee8758ed1a"),
					statusOfDocumentId = Guid.Parse("2F73E247-1181-4BE5-BB27-D644BDF97026"),
					typeOfDocumentId = Guid.Parse("94F2C14D-C3A4-4310-9B24-448AFCAA2B81"),
					referenceNumber = "17748/RS7",
					date = DateTime.Parse("2019-11-15T09:00:00"),
					dateAdoptionDocument = DateTime.Parse("2019-12-15T09:00:00"),
					userId = Guid.Parse("6CCF941E-B3BB-41A0-BAC4-B11B0F27B4C3")

				});

			builder.Entity<TypeOfDocumentEntity>()
				.HasData(new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("0E6E43AF-D3E6-463F-89A2-EC35A45413E7"),
					typeOfDocumentName = "Rešenje o obrazovanju stručne komisije"

				});
			builder.Entity<TypeOfDocumentEntity>()
				.HasData(new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("94F2C14D-C3A4-4310-9B24-448AFCAA2B81"),
					typeOfDocumentName = "Predlog godišnjeg Programa zaštite"

				});
			builder.Entity<TypeOfDocumentEntity>()
				.HasData(new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("55F97234-D821-4F3A-89EB-2F8171B302B6"),
					typeOfDocumentName = "Rešenje o obrazovanju Komisije za sprovođenje postupaka davanje poljoprivrednog zemljišta u zakup"

				});
			builder.Entity<TypeOfDocumentEntity>()
				.HasData(new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("D1C95CD9-5018-4B23-85BC-9AF26063F80C"),
					typeOfDocumentName = "Predlog odluke o davanju u zakup"

				});
			builder.Entity<TypeOfDocumentEntity>()
				.HasData(new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("EFE8E9AA-CAF5-4969-8941-D02C05031D07"),
					typeOfDocumentName = "Saglasnost Ministarstva"

				});
		}
	}

}

using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public class TypeOfDocumentMockRepository : ITypeOfDocumentRepository
	{
		public static List<TypeOfDocumentEntity> TypeOfDocumentEntities { get; set; } = new List<TypeOfDocumentEntity>();

		public TypeOfDocumentMockRepository()
		{
			FillData();
		}

		private static void FillData()
		{
			TypeOfDocumentEntities.AddRange(new List<TypeOfDocumentEntity>
			{
				new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("0E6E43AF-D3E6-463F-89A2-EC35A45413E7"),
					typeOfDocumentName = "Rešenje o obrazovanju stručne komisije"

				},
				new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("94F2C14D-C3A4-4310-9B24-448AFCAA2B81"),
					typeOfDocumentName = "Predlog godišnjeg Programa zaštite"

				},
				new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("55F97234-D821-4F3A-89EB-2F8171B302B6"),
					typeOfDocumentName = "Rešenje o obrazovanju Komisije za sprovođenje postupaka davanje poljoprivrednog zemljišta u zakup"

				},
				new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("D1C95CD9-5018-4B23-85BC-9AF26063F80C"),
					typeOfDocumentName = "Predlog odluke o davanju u zakup"

				},
				new TypeOfDocumentEntity
				{
					typeOfDocumentId = Guid.Parse("EFE8E9AA-CAF5-4969-8941-D02C05031D07"),
					typeOfDocumentName = "Saglasnost Ministarstva"
				}
			});
		}
		public TypeOfDocumentEntity GetTypeOfDocumentById(Guid typeOfDocumentId)
		{
			return TypeOfDocumentEntities.FirstOrDefault(D => D.typeOfDocumentId == typeOfDocumentId);
		}

		public List<TypeOfDocumentEntity> GetTypeOfDocumentEntities(string typeOfDocumentName = "type")
		{
			return (from e in TypeOfDocumentEntities
					where e.typeOfDocumentName == typeOfDocumentName
					select e).ToList();
		}

		public TypeOfDocumentConfirmation CreateTypeOfDocument(TypeOfDocumentEntity typeOfDocumentId)
		{
			typeOfDocumentId.typeOfDocumentId = Guid.NewGuid();
			TypeOfDocumentEntities.Add(typeOfDocumentId);
			var d = GetTypeOfDocumentById(typeOfDocumentId.typeOfDocumentId);

			return new TypeOfDocumentConfirmation
			{
				typeOfDocumentId = d.typeOfDocumentId,
				typeOfDocumentName = d.typeOfDocumentName
			};
		}

		public void DeleteTypeOfDocument(Guid typeOfDocumentId)
		{
			TypeOfDocumentEntities.Remove(TypeOfDocumentEntities.FirstOrDefault(e => e.typeOfDocumentId == typeOfDocumentId));
		}

		public void UpdateTypeOfDocument(TypeOfDocumentEntity typeOfDocumentId)
		{
			TypeOfDocumentEntity doc = GetTypeOfDocumentById(typeOfDocumentId.typeOfDocumentId);

			doc.typeOfDocumentId = typeOfDocumentId.typeOfDocumentId;
			doc.typeOfDocumentName = typeOfDocumentId.typeOfDocumentName;

		}

		public bool SaveChanges()
		{
			return true;
		}

	}
}

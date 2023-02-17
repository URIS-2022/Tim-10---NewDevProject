using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public class StatusOfDocumentMockRepository : IStatusOfDocumentRepository
	{
		public static List<StatusOfDocumentEntity> statusOfDocumentEntities { get; set; } = new List<StatusOfDocumentEntity>();

		public StatusOfDocumentMockRepository()
		{
			FillData();
		}

		private static void FillData()
		{
			statusOfDocumentEntities.AddRange(new List<StatusOfDocumentEntity>
			{
				new StatusOfDocumentEntity
				{
					statusOfDocumentId = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
					adopted = true,
					rejected=false,
					opened=false,
					modified=false,

				},
				new StatusOfDocumentEntity
				{
					statusOfDocumentId = Guid.Parse("4ff5c5a0-93d4-443d-bf2c-dc9cf9fa4296"),
					adopted = false,
					rejected=false,
					opened=true,
					modified=false,
				}
			});
		}
		public StatusOfDocumentEntity GetStatusOfDocumentById(Guid statusOfDocumentId)
		{
			return statusOfDocumentEntities.FirstOrDefault(D => D.statusOfDocumentId == statusOfDocumentId);
		}

		public List<StatusOfDocumentEntity> GetStatusOfDocumentEntities(bool adopted = false)
		{
			return (from e in statusOfDocumentEntities
					where adopted == false || adopted != true || e.adopted == adopted
					select e).ToList();
		}

		public StatusOfDocumentConfirmation CreateStatusOfDocument(StatusOfDocumentEntity document)
		{
			document.statusOfDocumentId = Guid.NewGuid();
			statusOfDocumentEntities.Add(document);
			var d = GetStatusOfDocumentById((Guid)document.statusOfDocumentId);

			return new StatusOfDocumentConfirmation
			{
				statusOfDocumentId = (Guid)d.statusOfDocumentId,
				adopted = d.adopted,
				rejected = d.rejected,
				opened = d.opened,
				modified = d.modified
			};
		}

		public void DeleteStatusOfDocument(Guid statusOfDocumentId)
		{
			statusOfDocumentEntities.Remove(statusOfDocumentEntities.FirstOrDefault(e => e.statusOfDocumentId == statusOfDocumentId));
		}

		public void UpdateStatusOfDocument(StatusOfDocumentEntity document)
		{
			StatusOfDocumentEntity doc = GetStatusOfDocumentById((Guid)document.statusOfDocumentId);

			doc.statusOfDocumentId = document.statusOfDocumentId;
			doc.adopted = document.adopted;
			doc.rejected = document.rejected;
			doc.opened = document.opened;
			doc.modified = document.modified;

		}

		public bool SaveChanges()
		{
			return true;
		}

	}
}

using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class LicitationMockRepository : ILicitationRepository
	{
		public static List<Licitation>? licitations { get; set; } = new List<Licitation>();

		public LicitationMockRepository()
		{
			FillData();
		}

		static private void FillData()
		{
			licitations.AddRange(new List<Licitation>
			{
				new Licitation
				{
					licitationId = Guid.Parse("80EE5F47-A842-4F18-B410-D56D6B02CB95"),
					number = 1,
					year = 2023,
					date = DateTime.Parse("2023-2-17"),
					restrictions = 1,
					priceDifference = 100,
					listOfDocumentationOfIndividuals = new List<string>(){ "dok1_fl", "dok2_fl"},
					listOfDocumentationOfLegalEntities = new List<string>(){ "dok1_pl", "dok1_pl"},
					publicBiddingId = Guid.Parse("E0F7001E-5236-48A2-A803-782DF0522026"),
					deadlineForSubmissionOfApplications = DateTime.Parse("2023-2-15")
				},
				new Licitation
				{
					licitationId = Guid.Parse("37ECA58F-47C8-45C9-81A2-198E8F29C300"),
					number = 2,
					year = 2023,
					date = DateTime.Parse("2023-2-18"),
					restrictions = 1,
					priceDifference = 200,
					listOfDocumentationOfIndividuals = new List<string>(){ "dok1_fl", "dok2_fl"},
					listOfDocumentationOfLegalEntities = new List<string>(){ "dok1_pl", "dok1_pl"},
					publicBiddingId = Guid.Parse("CE8B4945-F812-4FE1-9F1C-357900BBFE1F"),
					deadlineForSubmissionOfApplications = DateTime.Parse("2023-2-16")
				}
			});
		}

		public LicitationConfirmationDto CreateLicitation(Licitation licitation)
		{
			licitation.licitationId = Guid.NewGuid();
			licitations.Add(licitation);
			Licitation l = GetLicitationById(licitation.licitationId);

			return new LicitationConfirmationDto
			{
				licitationId = l.licitationId
			};
		}

		public void DeleteLicitation(Guid licitationId)
		{
			licitations.Remove(licitations.FirstOrDefault(l => l.licitationId == licitationId));
		}

		public List<Licitation> GetLicitations()
		{
			return (from l in licitations select l).ToList();
		}

		public Licitation GetLicitationById(Guid licitationId)
		{
			return licitations.FirstOrDefault(l => l.licitationId == licitationId);
		}

		public LicitationConfirmationDto UpdateLicitation(Licitation licitation)
		{
			Licitation l = GetLicitationById(licitation.licitationId);

			l.licitationId = licitation.licitationId;
			l.number = licitation.number;
			l.year = licitation.year;
			l.date = licitation.date;
			l.restrictions = licitation.restrictions;
			l.priceDifference = licitation.priceDifference;
			l.listOfDocumentationOfIndividuals = licitation.listOfDocumentationOfIndividuals;
			l.listOfDocumentationOfLegalEntities = licitation.listOfDocumentationOfLegalEntities;
			l.publicBiddingId = licitation.publicBiddingId;
			l.deadlineForSubmissionOfApplications = licitation.deadlineForSubmissionOfApplications;

			return new LicitationConfirmationDto
			{
				licitationId = l.licitationId
			};
		}

		public bool SaveChanges()
		{
			throw new NotImplementedException();
		}

	}
}

using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class PublicBiddingMockRepository : IPublicBiddingRepository
	{
		public static List<Entities.PublicBidding> publicBiddings { get; set; } = new List<Entities.PublicBidding>();

		public PublicBiddingMockRepository()
		{
			FillData();
		}

		static private void FillData()
		{
			publicBiddings.AddRange(new List<Entities.PublicBidding>
			{
				new Entities.PublicBidding
				{
					publicBiddingId = Guid.Parse("E0F7001E-5236-48A2-A803-782DF0522026"),
					date = DateTime.Parse("2023-2-17"),
					timeOfBeginning = DateTime.Parse("2023-2-17T08:00:00"),//godina, mesec, dan, sat, minut, sekunda
                    timeOfEnd = DateTime.Parse("2023-2-17T10:00:00"),
					initialPricePerHectare = 5000,
					excepted = false,
					typePublicBiddingId = Guid.Parse("A8D6B75D-92DE-4702-8E4E-D026781A2060"),
					auctionedPrice = 7500,
					leasePeriod = 12,
					numberOfParticipants = 10,
					depositTopUpAmount = 500,
					circle = 1,
					statusOfPublicBiddingId = Guid.Parse("E64E321A-A49F-4C34-84D2-0A6120E53E63")
				},
				new Entities.PublicBidding
				{
					publicBiddingId = Guid.Parse("CE8B4945-F812-4FE1-9F1C-357900BBFE1F"),
					date = DateTime.Parse("2023-2-18"),
					timeOfBeginning = DateTime.Parse("2023-2-18T08:00:00"),
					timeOfEnd = DateTime.Parse("2023-2-18T10:00:00"),
					initialPricePerHectare = 4000,
					excepted = false,
					typePublicBiddingId = Guid.Parse("699382E7-872C-4993-A617-6F96362CC64E"),
					auctionedPrice = 6000,
					leasePeriod = 12,
					numberOfParticipants = 10,
					depositTopUpAmount = 400,
					circle = 1,
					statusOfPublicBiddingId = Guid.Parse("A77F9497-462C-4EE2-BDAF-F18FE181E827")
				}
			});
		}

		public PublicBiddingConfirmationDto CreatePublicBidding(Entities.PublicBidding publicBidding)
		{
			publicBidding.publicBiddingId = Guid.NewGuid();
			publicBiddings.Add(publicBidding);
			Entities.PublicBidding j = GetPublicBiddingById(publicBidding.publicBiddingId);

			return new PublicBiddingConfirmationDto
			{
				publicBiddingId = j.publicBiddingId
			};
		}

		public void DeletePublicBidding(Guid publicBiddingId)
		{
			publicBiddings.Remove(publicBiddings.FirstOrDefault(j => j.publicBiddingId == publicBiddingId));
		}

		public List<Entities.PublicBidding> GetPublicBiddings()
		{
			return (from j in publicBiddings select j).ToList();
		}

		public Entities.PublicBidding GetPublicBiddingById(Guid publicBiddingId)
		{
			return publicBiddings.FirstOrDefault(j => j.publicBiddingId == publicBiddingId);
		}

		public PublicBiddingConfirmationDto UpdatePublicBidding(Entities.PublicBidding publicBidding)
		{
			Entities.PublicBidding j = GetPublicBiddingById(publicBidding.publicBiddingId);

			j.publicBiddingId = publicBidding.publicBiddingId;
			j.date = publicBidding.date;
			j.timeOfBeginning = publicBidding.timeOfBeginning;
			j.timeOfEnd = publicBidding.timeOfEnd;
			j.initialPricePerHectare = publicBidding.initialPricePerHectare;
			j.excepted = publicBidding.excepted;
			j.typePublicBiddingId = publicBidding.typePublicBiddingId;
			j.auctionedPrice = publicBidding.auctionedPrice;
			j.leasePeriod = publicBidding.leasePeriod;
			j.numberOfParticipants = publicBidding.numberOfParticipants;
			j.depositTopUpAmount = publicBidding.depositTopUpAmount;
			j.circle = publicBidding.circle;
			j.statusOfPublicBiddingId = publicBidding.statusOfPublicBiddingId;

			return new PublicBiddingConfirmationDto
			{
				publicBiddingId = j.publicBiddingId
			};
		}

		public bool SaveChanges()
		{
			throw new NotImplementedException();
		}

	}
}

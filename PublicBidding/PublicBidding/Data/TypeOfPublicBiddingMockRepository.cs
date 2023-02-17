using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class TypeOfPublicBiddingMockRepository : ITypeOfPublicBiddingRepository
	{
		public static List<TypeOfPublicBidding> typesOfPublicBidding { get; set; } = new List<TypeOfPublicBidding>();

		public TypeOfPublicBiddingMockRepository()
		{
			FillData();
		}

		private void FillData()
		{
			typesOfPublicBidding.AddRange(new List<TypeOfPublicBidding>
			{
				new TypeOfPublicBidding
				{
					typePublicBiddingId = Guid.Parse("A8D6B75D-92DE-4702-8E4E-D026781A2060"),
					typePublicBiddingName = "Licitation"
				},
				new TypeOfPublicBidding
				{
					typePublicBiddingId = Guid.Parse("699382E7-872C-4993-A617-6F96362CC64E"),
					typePublicBiddingName = "Opening of sealed bids"
				}
			});
		}

		public TypeOfPublicBiddingConfirmationDto CreateTypeOfPublicBidding(TypeOfPublicBidding typePublicBidding)
		{
			typePublicBidding.typePublicBiddingId = Guid.NewGuid();
			typesOfPublicBidding.Add(typePublicBidding);
			TypeOfPublicBidding t = GetTypeOfPublicBiddingById(typePublicBidding.typePublicBiddingId);

			return new TypeOfPublicBiddingConfirmationDto
			{
				typePublicBiddingId = t.typePublicBiddingId
			};
		}

		public void DeleteTypeOfPublicBidding(Guid typePublicBiddingId)
		{
			typesOfPublicBidding.Remove(typesOfPublicBidding.FirstOrDefault(t => t.typePublicBiddingId == typePublicBiddingId));
		}

		public List<TypeOfPublicBidding> GetTypesOfPublicBidding()
		{
			return (from t in typesOfPublicBidding select t).ToList();
		}

		public TypeOfPublicBidding GetTypeOfPublicBiddingById(Guid typePublicBiddingId)
		{
			return typesOfPublicBidding.FirstOrDefault(t => t.typePublicBiddingId == typePublicBiddingId);
		}

		public TypeOfPublicBiddingConfirmationDto UpdateTypeOfPublicBidding(TypeOfPublicBidding typePublicBiddingId)
		{
			TypeOfPublicBidding t = GetTypeOfPublicBiddingById(typePublicBiddingId.typePublicBiddingId);

			t.typePublicBiddingId = typePublicBiddingId.typePublicBiddingId;
			t.typePublicBiddingName = typePublicBiddingId.typePublicBiddingName;

			return new TypeOfPublicBiddingConfirmationDto
			{
				typePublicBiddingId = t.typePublicBiddingId
			};
		}

		public bool SaveChanges()
		{
			throw new NotImplementedException();
		}

	}
}

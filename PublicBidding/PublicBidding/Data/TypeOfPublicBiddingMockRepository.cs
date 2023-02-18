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

		static private void FillData()
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

		public TypeOfPublicBiddingConfirmationDto CreateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding)
		{
			typeOfPublicBidding.typePublicBiddingId = Guid.NewGuid();
			typesOfPublicBidding.Add(typeOfPublicBidding);
			TypeOfPublicBidding t = GetTypeOfPublicBiddingById(typeOfPublicBidding.typePublicBiddingId);

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

		public TypeOfPublicBiddingConfirmationDto UpdateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding)
		{
			TypeOfPublicBidding t = GetTypeOfPublicBiddingById(typeOfPublicBidding.typePublicBiddingId);

			t.typePublicBiddingId = typeOfPublicBidding.typePublicBiddingId;
			t.typePublicBiddingName = typeOfPublicBidding.typePublicBiddingName;

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

using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public interface ITypeOfPublicBiddingRepository
	{
		List<TypeOfPublicBidding> GetTypesOfPublicBidding();

		TypeOfPublicBidding GetTypeOfPublicBiddingById(Guid typePublicBiddingId);

		TypeOfPublicBiddingConfirmationDto CreateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding);

		TypeOfPublicBiddingConfirmationDto UpdateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding);

		void DeleteTypeOfPublicBidding(Guid typePublicBiddingId);

		bool SaveChanges();

	}
}

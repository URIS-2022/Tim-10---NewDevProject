using PublicBidding.Models;

namespace PublicBidding.Data
{
	public interface IPublicBiddingRepository
	{
		List<Entities.PublicBidding> GetPublicBiddings();

		Entities.PublicBidding GetPublicBiddingById(Guid publicBiddingId);

		PublicBiddingConfirmationDto CreatePublicBidding(Entities.PublicBidding publicBidding);

		PublicBiddingConfirmationDto UpdatePublicBidding(Entities.PublicBidding publicBidding);

		void DeletePublicBidding(Guid publicBiddingId);

		bool SaveChanges();

	}
}

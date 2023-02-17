using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public interface IStatusOfPublicBiddingRepository
	{
		List<StatusOfPublicBidding> GetStatusesOfPublicBidding();

		StatusOfPublicBidding GetStatusOfPublicBiddingById(Guid statusOfPublicBiddingId);

		StatusOfPublicBiddingConfirmationDto CreateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding);

		StatusOfPublicBiddingConfirmationDto UpdateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding);

        void DeleteStatusOfPublicBidding(Guid statusOfPublicBiddingId);

        bool SaveChanges();

	}
}

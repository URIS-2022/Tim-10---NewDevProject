using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class StatusOfPublicBiddingMockRepository : IStatusOfPublicBiddingRepository
	{
		public static List<StatusOfPublicBidding> statusesOfPublicBidding { get; set; } = new List<StatusOfPublicBidding>();

		public StatusOfPublicBiddingMockRepository()
		{
			FillData();
		}

		static private void FillData()
		{
			statusesOfPublicBidding.AddRange(new List<StatusOfPublicBidding>
			{
				new StatusOfPublicBidding
				{
					statusOfPublicBiddingId = Guid.Parse("E64E321A-A49F-4C34-84D2-0A6120E53E63"),
					statusOfPublicBiddingName = "Prvi krug"
				},
				new StatusOfPublicBidding
				{
					statusOfPublicBiddingId = Guid.Parse("A77F9497-462C-4EE2-BDAF-F18FE181E827"),
					statusOfPublicBiddingName = "Drugi krug sa starim uslovima"
				},
				new StatusOfPublicBidding
				{
					statusOfPublicBiddingId = Guid.Parse("021DD2C8-A20F-4FF9-AE73-7D08BA5621B6"),
					statusOfPublicBiddingName = "Drugi krug sa novim uslovima"
				}
			});
		}

		public StatusOfPublicBiddingConfirmationDto CreateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding)
		{
			statusOfPublicBidding.statusOfPublicBiddingId = Guid.NewGuid();
			statusesOfPublicBidding.Add(statusOfPublicBidding);
			StatusOfPublicBidding s = GetStatusOfPublicBiddingById(statusOfPublicBidding.statusOfPublicBiddingId);

			return new StatusOfPublicBiddingConfirmationDto
			{
				statusOfPublicBiddingId = s.statusOfPublicBiddingId
			};
		}

		public void DeleteStatusOfPublicBidding(Guid statusOfPublicBiddingId)
		{
			statusesOfPublicBidding.Remove(statusesOfPublicBidding.FirstOrDefault(s => s.statusOfPublicBiddingId == statusOfPublicBiddingId));
		}

		public List<StatusOfPublicBidding> GetStatusesOfPublicBidding()
		{
			return (from s in statusesOfPublicBidding select s).ToList();
		}

		public StatusOfPublicBidding GetStatusOfPublicBiddingById(Guid statusOfPublicBiddingId)
		{
			return statusesOfPublicBidding.FirstOrDefault(s => s.statusOfPublicBiddingId == statusOfPublicBiddingId);
		}

		public StatusOfPublicBiddingConfirmationDto UpdateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding)
		{
			StatusOfPublicBidding s = GetStatusOfPublicBiddingById(statusOfPublicBidding.statusOfPublicBiddingId);

			s.statusOfPublicBiddingId = statusOfPublicBidding.statusOfPublicBiddingId;
			s.statusOfPublicBiddingName = statusOfPublicBidding.statusOfPublicBiddingName;

			return new StatusOfPublicBiddingConfirmationDto
			{
				statusOfPublicBiddingId = s.statusOfPublicBiddingId
			};
		}

		public bool SaveChanges()
		{
			throw new NotImplementedException();
		}


	}
}

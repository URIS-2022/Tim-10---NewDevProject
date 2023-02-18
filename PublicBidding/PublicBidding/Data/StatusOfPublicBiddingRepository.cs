using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class StatusOfPublicBiddingRepository : IStatusOfPublicBiddingRepository
	{
		private readonly PublicBiddingContext context;
		private readonly IMapper mapper;

		public StatusOfPublicBiddingRepository(PublicBiddingContext context, IMapper mapper)
		{

			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		public StatusOfPublicBiddingConfirmationDto CreateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding)
		{
			statusOfPublicBidding.statusOfPublicBiddingId = Guid.NewGuid();
			var newEntity = context.statusesOfPublicBidding.Add(statusOfPublicBidding);

			//return mapper.Map<StatusOfPublicBiddingConfirmationDto>(publicBiddingStatus);
			return mapper.Map<StatusOfPublicBiddingConfirmationDto>(newEntity.Entity);
		}

		public void DeleteStatusOfPublicBidding(Guid statusOfPublicBiddingId)
		{
			StatusOfPublicBidding status = GetStatusOfPublicBiddingById(statusOfPublicBiddingId);
			context.statusesOfPublicBidding.Remove(status);
		}

		public List<StatusOfPublicBidding> GetStatusesOfPublicBidding()
		{
			return context.statusesOfPublicBidding.ToList();
		}

		public StatusOfPublicBidding GetStatusOfPublicBiddingById(Guid statusOfPublicBiddingId)
		{
			return context.statusesOfPublicBidding.FirstOrDefault(s => s.statusOfPublicBiddingId == statusOfPublicBiddingId);
		}

		public StatusOfPublicBiddingConfirmationDto UpdateStatusOfPublicBidding(StatusOfPublicBidding statusOfPublicBidding)
		{
			throw new NotImplementedException();
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
		}
	}
}

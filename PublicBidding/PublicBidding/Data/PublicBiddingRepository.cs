using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class PublicBiddingRepository : IPublicBiddingRepository
	{
		private readonly PublicBiddingContext context;
		private readonly IMapper mapper;

		public PublicBiddingRepository(PublicBiddingContext context, IMapper mapper)
		{

			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		public PublicBiddingConfirmationDto CreatePublicBidding(Entities.PublicBidding publicBidding)
		{
			publicBidding.publicBiddingId = Guid.NewGuid();
			var noviEntitet = context.publicBiddings.Add(publicBidding);

			return mapper.Map<PublicBiddingConfirmationDto>(noviEntitet.Entity);
		}

		public void DeletePublicBidding(Guid publicBiddingId)
		{
			Entities.PublicBidding publicBidding = GetPublicBiddingById(publicBiddingId);
			context.publicBiddings.Remove(publicBidding);
		}

		public List<Entities.PublicBidding> GetPublicBiddings()
		{
			return context.publicBiddings.ToList();
		}

		public Entities.PublicBidding GetPublicBiddingById(Guid publicBiddingId)
		{
			return context.publicBiddings.FirstOrDefault(j => j.publicBiddingId == publicBiddingId);
		}

		public PublicBiddingConfirmationDto UpdatePublicBidding(Entities.PublicBidding publicBidding)
		{
			throw new NotImplementedException();
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
		}

	}
}

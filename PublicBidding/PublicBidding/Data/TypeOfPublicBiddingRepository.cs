using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class TypeOfPublicBiddingRepository : ITypeOfPublicBiddingRepository
	{
		private readonly PublicBiddingContext context;
		private readonly IMapper mapper;

		public TypeOfPublicBiddingRepository(PublicBiddingContext context, IMapper mapper)
		{

			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		public TypeOfPublicBiddingConfirmationDto CreateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding)
		{
			typeOfPublicBidding.typePublicBiddingId = Guid.NewGuid();
			var noviEntitet = context.typesOfPublicBidding.Add(typeOfPublicBidding);

			return mapper.Map<TypeOfPublicBiddingConfirmationDto>(noviEntitet.Entity);
		}

		public void DeleteTypeOfPublicBidding(Guid typePublicBiddingId)
		{
			TypeOfPublicBidding type = GetTypeOfPublicBiddingById(typePublicBiddingId);
			context.typesOfPublicBidding.Remove(type);
		}

		public List<TypeOfPublicBidding> GetTypesOfPublicBidding()
		{
			return context.typesOfPublicBidding.ToList();
		}

		public TypeOfPublicBidding GetTypeOfPublicBiddingById(Guid typePublicBiddingId)
		{
			return context.typesOfPublicBidding.FirstOrDefault(t => t.typePublicBiddingId == typePublicBiddingId);
		}

		public TypeOfPublicBiddingConfirmationDto UpdateTypeOfPublicBidding(TypeOfPublicBidding typeOfPublicBidding)
		{
			throw new NotImplementedException();
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
		}

	}
}

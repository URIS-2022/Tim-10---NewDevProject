using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public class LicitationRepository : ILicitationRepository
	{
		private readonly PublicBiddingContext context;
		private readonly IMapper mapper;

		public LicitationRepository(PublicBiddingContext context, IMapper mapper)
		{

			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		public LicitationConfirmationDto CreateLicitation(Licitation licitation)
		{
			licitation.licitationId = Guid.NewGuid();
			var noviEntitet = context.licitations.Add(licitation);

			return mapper.Map<LicitationConfirmationDto>(noviEntitet.Entity);
		}

		public void DeleteLicitation(Guid licitationId)
		{
			Licitation licitation = GetLicitationById(licitationId);
			context.licitations.Remove(licitation);
		}

		public List<Licitation> GetLicitations()
		{
			return context.licitations.ToList();
		}

		public Licitation GetLicitationById(Guid licitationId)
		{
			return context.licitations.FirstOrDefault(l => l.licitationId == licitationId);
		}

		public LicitationConfirmationDto UpdateLicitation(Licitation licitation)
		{
			throw new NotImplementedException();
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
		}

	}
}

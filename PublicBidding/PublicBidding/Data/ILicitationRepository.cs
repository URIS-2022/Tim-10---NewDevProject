using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Data
{
	public interface ILicitationRepository
	{
		List<Licitation> GetLicitations();

		Licitation GetLicitationById(Guid licitationId);

		LicitationConfirmationDto CreateLicitation(Licitation licitation);

		LicitationConfirmationDto UpdateLicitation(Licitation licitation);

		void DeleteLicitation(Guid licitationId);

		bool SaveChanges();

	}
}

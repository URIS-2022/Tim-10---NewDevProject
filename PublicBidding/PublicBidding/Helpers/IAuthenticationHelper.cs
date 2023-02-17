using PublicBidding.Models;

namespace PublicBidding.Helpers
{
	public interface IAuthenticationHelper
	{
		public bool AuthenticatePrincipal(Principal principal);
		public string GenerateJwt(Principal principal);

	}
}

using Microsoft.AspNetCore.Mvc;
using PublicBidding.Helpers;
using PublicBidding.Models;

namespace PublicBidding.Controllers
{
	[ApiController]
	[Route("api/authentication")]
	[Produces("application/json", "application/xml")]
	public class AuthenticationController : ControllerBase

	{
		private readonly IAuthenticationHelper authenticationHelper;

		public AuthenticationController(IAuthenticationHelper authenticationHelper)
		{
			this.authenticationHelper = authenticationHelper;
		}

		/// <summary>
		/// User authentication
		/// </summary>
		/// <param name="principal">Model with data for authentication</param>
		/// <returns></returns>
		[HttpPost("authenticate")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult Authenticate(Principal principal)
		{
			// Authentication attempt
			if (authenticationHelper.AuthenticatePrincipal(principal))
			{
				var tokenString = authenticationHelper.GenerateJwt(principal);
				return Ok(new { token = tokenString });
			}

			// If authentication failed it returns a 401 status
			return Unauthorized();
		}

	}
}

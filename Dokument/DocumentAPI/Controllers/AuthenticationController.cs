using DocumentAPI.Helpers;
using DocumentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAPI.Controllers
{
	[ApiController]
	[Route("api/document")]
	[Route("api/statusOfDocument")]
	[Route("api/typeOfDocument")]

	public class AuthenticationController : ControllerBase
	{

		private readonly IAuthenticationHelper authenticationHelper;

		public AuthenticationController(IAuthenticationHelper authenticationHelper)
		{
			this.authenticationHelper = authenticationHelper;
		}

		/// <summary>
		/// Authentication user
		/// </summary>
		/// <param name="principal">Model with data for authentication</param>
		/// <returns></returns>
		[HttpPost("authenticate")]
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

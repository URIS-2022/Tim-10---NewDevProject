using Buyer.Entities;
using Buyer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Buyer.Controllers
{/// <summary>
/// kontroler autentifikacije
/// </summary>
    [ApiController]
    [Route("api/buyer")]
    [Route("api/contactPerson")]
    [Route("api/priorities")]
    [Produces("application/json", "application/xml")]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;
        /// <summary>
        /// 
        ///authentication
        /// </summary>
        /// <param name="authenticationHelper"></param>
        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public IActionResult Authenticate([FromBody] Principal principal)
        {

            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }


            return Unauthorized();
        }
    }
}

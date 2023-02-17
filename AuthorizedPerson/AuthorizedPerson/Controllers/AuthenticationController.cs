using AuthorizedPerson.Entities;
using AuthorizedPerson.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizedPerson.Controllers
{
    [ApiController]
    [Route("api/authorizedPerson")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
        {
            private readonly IAuthenticationHelper authenticationHelper;

            public AuthenticationController(IAuthenticationHelper authenticationHelper)
            {
                this.authenticationHelper = authenticationHelper;
            }


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

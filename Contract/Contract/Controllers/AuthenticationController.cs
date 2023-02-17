using Contract.Helpers;
using Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract.Controllers
{
    [ApiController]
    [Route("api/contract")]
    [Route("api/guarantee")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Principal principal)
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
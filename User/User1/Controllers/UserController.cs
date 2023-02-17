using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using User1.Data;
using User1.Entities;
using User1.Models;
using User1.ServiceCalls;

namespace User1.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json", "application/xml")]
    public class UserController : ControllerBase
    {
        //dependency injector
        private readonly IUserRespository userRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        private readonly Message message = new Message();

        public UserController(IUserRespository userRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
        }
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserDto>> GetUserList()
        {
            List<User> userList = userRepository.GetUserList();
            message.method = "GET";

            if (userList == null || userList.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.information = "Returned list of users!";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<UserDto>>(userList));
        }
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserDto>> GetUserById(Guid userId)
        {
            User user = userRepository.GetUserById(userId);
            message.method = "GET";

            if (user == null)
            {
                message.information = "Not found";
                message.error = "There is no object of user with identifier: " + userId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.information = user.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<UserDto>(user));
        }
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<UserDto> CreateUser([FromBody] UserCreateDto user) //FromBody uzima iz bodya requesta
        {
            message.method = "POST";

            try
            {
                User createUser = mapper.Map<User>(user);
                User confirmation = userRepository.CreateUser(createUser);
                userRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUser", "User", new { userId = confirmation.userId });

                message.information = createUser.ToString();
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<UserDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation Error" + ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            message.method = "DELETE";

            try
            {
                User deleteUser = userRepository.GetUserById(userId);
                if (deleteUser == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of user with identifier: " + userId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }
                userRepository.DeleteUser(userId);

                userRepository.SaveChanges();
                message.information = "Successfully deleted " + deleteUser.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + deleteUser.ToString());
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<UserDto> UpdateUser(UserUpdateDto user)
        {
            message.method = "PUT";

            try
            {
                User oldUser = userRepository.GetUserById(user.userId);

                //Proveriti da li postoji korisnik
                if (oldUser == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of user with identifier: " + user.userId;
                    loggerService.CreateMessage(message);

                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                User updateUser = mapper.Map<User>(user);

                mapper.Map(user, oldUser);

                userRepository.SaveChanges();
                message.information = updateUser.ToString();
                loggerService.CreateMessage(message);

                return Ok(mapper.Map<User>(updateUser));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("authorize/{token}")]
        public ActionResult Authorize(Principal principal)
        {

            if (userRepository.UserWithCredentialsExists(principal.Username, principal.Password))
            {
                return Ok();

            }

            return Unauthorized();
        }

        [HttpOptions]
        public IActionResult GetKorisnikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");

            return Ok();
        }

    }
}

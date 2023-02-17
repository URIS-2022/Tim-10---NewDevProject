using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using User1.Data;
using User1.Entities;
using User1.Models;
using User1.ServiceCalls;

namespace User1.Controllers
{
    [ApiController]
    [Route("api/userType")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository userTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly Message message = new Message();

        public UserTypeController(IUserTypeRepository typeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.userTypeRepository = typeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }
        [HttpGet]
        public ActionResult<List<UserTypeDto>> GetUserTypeList()
        {
            List<UserType> types = userTypeRepository.GetUserTypeList();
            message.method = "GET";


            if ( types == null || types.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            message.information = "Returned list of types";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<UserTypeDto>>(types));
        }
        [HttpGet("{userTypeId}")]
        public ActionResult<UserTypeDto> GetUserTypeById(Guid userTypeId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            UserType userType = userTypeRepository.GetUserTypeById(userTypeId);
            message.method = "GET";

            if (userType == null)
            {
                message.information = "Not found";
                message.error = "There is no object of type with identifier: " + userTypeId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.information = userType.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<UserTypeDto>(userType));
        }

        [HttpPost]
        public ActionResult<UserTypeDto> CreateUserType([FromBody] UserTypeDto userType)
        {
            message.method = "POST";

            try
            {

                UserType createType = mapper.Map<UserType>(userType);
                UserType confirmation = userTypeRepository.CreateUserType(createType);

                string location = linkGenerator.GetPathByAction("GetCountryList", "Country", new { userTypeId = confirmation.userTypeId });

                message.information = createType.ToString();
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<UserTypeDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{userTypeId}")]
        public IActionResult DeleteUserType(Guid userTypeId)
        {
            message.method = "DELETE";

            try
            {
                UserType userType = userTypeRepository.GetUserTypeById(userTypeId);
                if (userType == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of type with identifier: " + userTypeId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }

                userTypeRepository.DeleteUserType(userTypeId);

                userTypeRepository.SaveChanges();
                message.information = "Successfully deleted " + userType.ToString();

                return NoContent();
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error during the deletion");
            }
        }

        [HttpPut]
        public ActionResult<UserTypeDto> UpdateUserType(UserType userType)
        {
            message.method = "PUT";

            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldUserType = userTypeRepository.GetUserTypeById(userType.userTypeId);
                if (oldUserType == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of type with identifier: " + userType.userTypeId;
                    loggerService.CreateMessage(message);

                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                UserType userTypeEntity = mapper.Map<UserType>(userType);

                mapper.Map(userTypeEntity, oldUserType); //Update objekta koji treba da sačuvamo u bazi                

                userTypeRepository.SaveChanges(); //Perzistiramo promene
                message.information = userTypeEntity.ToString();
                loggerService.CreateMessage(message);

                userTypeRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<UserTypeDto>(oldUserType));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetTipKorisnikaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

using AutoMapper;
using Country.Data;
using Country.Entities;
using Country.Models;
using Country.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Country.Controllers
{
    [ApiController]
    [Route("api/address")]
    [Produces("application/json", "application/xml")]
   // [Authorize]
    public class AddressController : ControllerBase
    {

        private readonly IAddressRepository addressRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "Address";
        private readonly Message message = new Message();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        /// <param name="linkGenerator"></param>
        public AddressController(IAddressRepository addressRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.addressRepository = addressRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AddressDto>> GetAddressList()
        {
            var addresses = addressRepository.GetAddressList();
            message.serviceName = serviceName;
            message.method = "GET";

            if (addresses == null || addresses.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            message.information = "Returned list of Address";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<AddressDto>>(addresses));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{addressId}")]
        public ActionResult<AddressDto> GetAddressById(Guid addressId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var address = addressRepository.GetAddressById(addressId);

            message.serviceName = serviceName;
            message.method = "GET";

            if (address == null)

            {
                message.information = "Not found";
                message.error = "There is no object of Address with identifier: " + addressId;
                loggerService.CreateMessage(message);
                return NotFound();
            }

            message.information = address.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<AddressDto>(address));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<AddressDto> CreateAddress([FromBody] AddressDto address)
        {
            message.serviceName = serviceName;
            message.method = "POST";

            try
            {
                Address a = mapper.Map<Address>(address);
                Address confirmation = addressRepository.CreateAddress(a);

                string? location = linkGenerator.GetPathByAction("GetAddressById", "Address", new { addressId = confirmation.addressId });

                message.information = a.ToString() + " | Address location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<AddressDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Creation error!");
            }
        }

        [HttpDelete("{addressId}")]
        public IActionResult DeleteAddress(Guid addressId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                Address address = addressRepository.GetAddressById(addressId);
                if (address == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Adress with identifier: " + addressId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }


                addressRepository.DeleteAddress(addressId);
                addressRepository.SaveChanges();
                message.information = "Successfully deleted " + address.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + address.ToString());
            }
            catch (Exception ex)
            {

                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting an address!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AddressDto> UpdateAddress(AddressDto address)
        {
            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldAddress = addressRepository.GetAddressById(address.addressId);
                if (oldAddress == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Address with identifier: " + address.addressId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Address addressNew = mapper.Map<Address>(address);

                mapper.Map(addressNew, oldAddress); //Update objekta koji treba da sačuvamo u bazi                

                addressRepository.SaveChanges(); //Perzistiramo promene
                message.information = addressNew.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<AddressDto>(addressNew));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating an address!");
            }
        }
        [HttpOptions]
        public IActionResult GetAddressOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }

    }
}

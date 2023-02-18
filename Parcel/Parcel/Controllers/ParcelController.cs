using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;
using Parcel.ServiceCalls;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/parcel")]
    [Produces("application/json")]
    public class ParcelController :ControllerBase
    {

        private readonly IParcelRepository parcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly IBuyerService buyerService;

        private readonly ILoggerService loggerService;
        private readonly string serviceName = "Parcel";
        private readonly Message message = new Message();


        public ParcelController(IParcelRepository parcelRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IBuyerService buyerService)
        {
            this.parcelRepository = parcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.buyerService = buyerService;
        }


        [HttpGet]
        [HttpHead]
        public ActionResult<List<ParcelDto>>? GetParcelList()
        {
            List<Entities.Parcel> parcel = parcelRepository.GetParcelList();


            message.serviceName = serviceName;
            message.method = "GET";
            if (parcel == null || parcel.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            try
            {
                foreach (Entities.Parcel p in parcel)
                {
                    BuyerDto buyer = buyerService.GetBuyerById(p.userOfParcelId).Result;
                    if (buyer != null)
                    {
                        p.BuyerDto = buyer;
                    }
                }
            }
            catch
            {
                return default; 
            }
            message.information = "Returned list of Parcel";
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<List<ParcelDto>>(parcel));
        }

        [HttpGet("{parcelId}")]
        public ActionResult<ParcelDto> GetParcelById(Guid parcelId)
        {
            Entities.Parcel parcel = parcelRepository.GetParcelById(parcelId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (parcel == null)
            {
                message.information = "Not found";
                message.error = "There is no object of Parcela with identifier: " + parcelId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.information = parcel.ToString();
            loggerService.CreateMessage(message);

            return Ok(mapper.Map<ParcelDto>(parcel));
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<ParcelDto> CreateParcel([FromBody] ParcelDto parcel)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {

                Entities.Parcel p = mapper.Map<Entities.Parcel>(parcel);
                Entities.Parcel confirmation = parcelRepository.CreateParcel(p);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi

                string? location = linkGenerator.GetPathByAction("GetParcelById", "Parcel", new { parcelId = confirmation.parcelId });

                message.information = parcel.ToString() + " | Parcel location: " + location;
                loggerService.CreateMessage(message);

                return Created(location, mapper.Map<ParcelDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{parcelId}")]
        public IActionResult DeleteParcel(Guid parcelId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                Entities.Parcel p = parcelRepository.GetParcelById(parcelId);
                if (p == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Parcel with identifier: " + parcelId;
                    loggerService.CreateMessage(message);

                    return NotFound();
                }


                parcelRepository.DeleteParcel(parcelId);
                parcelRepository.SaveChanges();

                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                message.information = "Successfully deleted " + p.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + p.ToString());
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Deletion error!");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ParcelDto> UpdateParcel(ParcelDto parcel)
        {
            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldParcel = parcelRepository.GetParcelById(parcel.parcelId);
                if (oldParcel == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Parcel with identifier: " + parcel.parcelId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Entities.Parcel newParcel = mapper.Map<Entities.Parcel>(parcel);

                mapper.Map(newParcel, oldParcel); //Update objekta koji treba da sačuvamo u bazi                

                parcelRepository.SaveChanges(); //Perzistiramo promene
                message.information = oldParcel.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ParcelDto>(oldParcel));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting");
            }
        }

        [HttpOptions]
        public IActionResult GetParcelOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

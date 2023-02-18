using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using payment.Models;
using payment.ServiceCalls;
using payment.Data;
using payment.Entities;

namespace payment.Controllers
{

    //Omogucava dodavanje dodatnih stvari kao sto su statusni kodovi
    [ApiController]
    [Route("api/payments")]
    [Produces("application/json", "application/xml")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository paymentRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "PaymentService";
        private readonly Message message = new Message();
        private readonly IPublicBiddingService publicBiddingService;
        private readonly IBuyerService buyerService;

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public PaymentController(IPaymentRepository paymentRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IPublicBiddingService publicBiddingService, IBuyerService buyerService)
        {
            this.paymentRepository = paymentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.publicBiddingService = publicBiddingService;
            this.buyerService = buyerService;
        }

        
        [HttpGet]
        [HttpHead] //Vraca samo zaglavlje u odgovoru
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PaymentConfirmationDto>> GetPayments()
        {
            List<Entities.Payment> payments = paymentRepository.GetPayments();
            message.serviceName = serviceName;
            message.method = "GET";
            //ukoliko nije pronadjen ni jedna uplata vratiti status 204(NoContent)
            if (payments == null || payments.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }

            List<PaymentDto> paymentDto = mapper.Map<List<PaymentDto>>(payments);

            foreach (PaymentDto u in paymentDto)
            {
                u.publicBidding = publicBiddingService.GetPublicBiddings(u.publicBiddingId).Result;
                u.buyer = buyerService.GetBuyer(u.buyerId).Result;
            }

            message.information = "Returned list of Uplata";
            loggerService.CreateMessage(message);

            //ukoliko smo pronasli neku uplatu vratiti status 200 i listu uplata
            return Ok(paymentDto);
        }

        [HttpGet("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PaymentDto> GetUplata(Guid paymentId)
        {
            Entities.Payment payment = paymentRepository.GetPaymentById(paymentId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (payment == null)
            {
                message.information = "Not found";
                message.error = "There is no object of Uplata with identifier: " + paymentId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            PaymentDto paymentDto = mapper.Map<PaymentDto>(payment);
            paymentDto.publicBidding = publicBiddingService.GetPublicBiddings(payment.publicBiddingId).Result;
            paymentDto.buyer =  buyerService.GetBuyer(payment.buyerId).Result;

            message.information = payment.ToString();
            loggerService.CreateMessage(message);
            return Ok(paymentDto);
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PaymentConfirmationDto> CreatePayment([FromBody] PaymentCreationDto paymentDto)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {
                Entities.Payment payment = mapper.Map<Entities.Payment>(paymentDto);
                Payment u = paymentRepository.CreatePayment(payment);
                paymentRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetPaymentById", "Payment", new { paymentId = u.paymentId });
                message.information = paymentDto.ToString() + " | Payment location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<PaymentConfirmationDto>(u));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

      
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PaymentConfirmationDto> UpdatePayment(PaymentUpdateDto paymentDto)
        {
            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                var starije = paymentRepository.GetPaymentById(paymentDto.paymentId);
                //provera da li postoji nadmetanje koje hocemo da azuriramo
                if (starije == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Uplata with identifier: " + paymentDto.paymentId;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                Entities.Payment payment = mapper.Map<Entities.Payment>(paymentDto);
                mapper.Map(payment, starije);
                paymentRepository.SaveChanges(); //Perzistiramo promene
                message.information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<PaymentConfirmationDto>(starije));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        
        [HttpDelete("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePayment(Guid paymentId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                Entities.Payment payment = paymentRepository.GetPaymentById(paymentId);
                if (payment == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of Uplata with identifier: " + paymentId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                paymentRepository.DeletePayment(paymentId);
                paymentRepository.SaveChanges(); //Perzistiramo promene
                message.information = "Successfully deleted " + payment.ToString();
                //Status kod tipa 2xx koji se koristi kada se ne vraca nikakav sadrzaj, ali naglasava da je sve uredu
                return NoContent();
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vraca opcije dostupne za rad sa uplatama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetPaymentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}

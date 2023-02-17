using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using payment.Data;
using payment.ServiceCalls;
using payment.Models;
using payment.Entities;


namespace payment.Controllers
{
    [ApiController]
    [Route("api/exchangeRates")]
    [Produces("application/json", "application/xml")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly LinkGenerator linkGenerator; //sluzi za generisanje putanje do neke akcije
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "PaymentService";
        private readonly Message message = new Message();

        //Pomocu dependency injection-a dodajemo potrebne zavisnosti
        public ExchangeRateController(IExchangeRateRepository exchangeRateRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.exchangeRateRepository = exchangeRateRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Vraca sve kursne liste.
        /// </summary>
        /// <returns> Lista kursnih lista</returns>
        /// <response code="200">Vraca listu kursnih lista</response>
        /// <response code="404">Nije pronadjena ni jedna kursna lista</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ExchangeRateDto>> GetExchangeRates()
        {
            var rate = exchangeRateRepository.GetExchangeRates();


            if (rate == null || rate.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ExchangeRateDto>>(rate));
        }

        /// <summary>
        /// Vraca jednu kursnu listu na osnovu ID-ja.
        /// </summary>
        /// // <param name="kursnaListaID">ID kursne liste</param>
        /// <returns>Trazena kursna lista</returns>
        /// <response code="200">Vraca trazenu kursnu listu</response>
        /// <response code="404">Trazena kursna lista nije pronadjena</response>
        [HttpGet("{exchangeRateId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ExchangeRateDto> GetExchangeRates(Guid exchangeRateId)
        {
            ExchangeRate exchangeRate = exchangeRateRepository.GetExchangeRateById(exchangeRateId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (exchangeRate == null)
            {
                message.information = "Not found";
                message.error = "There is no object of KursnaLista with identifier: " + exchangeRateId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.information = exchangeRate.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ExchangeRateDto>(exchangeRate));
        }

        /// <summary>
        /// Kreira novu kursnu listu.
        /// </summary>
        /// /// <param name="kursnaListaDto">Model kursne liste</param>
        /// <returns>Potvrdu o kreiranoj kursnoj listi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove kursne liste \
        /// POST /api/kursneListe \
        /// {   \
        ///    "datum": "2022-02-10T00:00:00", \
        ///    "valuta": "RSD", \
        ///    "vrednost": 5555 \
        /// }
        /// </remarks>
        /// <response code="201">Vraca kreiranu kursnu listu</response>
        /// <response code="500">Doslo je do greske na serveru prilikom kreiranja kursne liste</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ExchangeRateConfirmationDto> CreateExchangeRate([FromBody] ExchangeRateCreationDto exchangeRateDto)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {
                ExchangeRate lista = mapper.Map<ExchangeRate>(exchangeRateDto);
                ExchangeRateConfirmationDto k = exchangeRateRepository.CreateExchangeRate(lista);
                exchangeRateRepository.SaveChanges(); //Perzistiramo promene
                //generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetExchangeRates", "ExchangeRate", new { exchangeRateId = k.exchangeRateId });
                message.information = exchangeRateDto.ToString() + " | ExchangeRate location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<ExchangeRateConfirmationDto>(k));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Azurira jednu kursnu listu.
        /// </summary>
        /// <param name="kursnaListaDto">Model kursneListe koji se azurira</param>
        /// <returns>Potvrdu o modifikovanoj kursnoj listi</returns>
        /// <response code="200">Vraca azuriranu kursnu listu</response>
        /// <response code="400">Kursna lista koja se azurira nije pronadjena</response>
        /// <response code="500">Doslo je do greske prilikom azuriranja kursne liste</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ExchangeRateConfirmationDto> UpdateExchangeRate(ExchangeRateUpdateDto exchangeRateDto)
        {
            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                var starije = exchangeRateRepository.GetExchangeRateById(exchangeRateDto.exchangeRateId);
                //provera da li postoji lista koju hocemo da azuriramo
                if (starije == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of KursnaLista with identifier: " + exchangeRateDto.exchangeRateId;
                    loggerService.CreateMessage(message);
                    return NotFound(); //ukoliko ne postoji vraca se status 404 (NotFoud)
                }
                ExchangeRate exchangeRate = mapper.Map<ExchangeRate>(exchangeRateDto);
                mapper.Map(exchangeRate, starije);
                exchangeRateRepository.SaveChanges(); //Perzistiramo promene
                message.information = starije.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ExchangeRateConfirmationDto>(starije));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrsi brisanje jedne kursne liste na osnovu ID-ja.
        /// </summary>
        /// <param name="kursnaListaID">ID kursne liste</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kursna lista uspesno obrisana</response>
        /// <response code="404">Nije pronadjena kursna lista za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prilikom brisanja kursne liste</response>
        [HttpDelete("{exchangeRateId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteExchangeRate(Guid exchangeRateId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                ExchangeRate exchangeRate = exchangeRateRepository.GetExchangeRateById(exchangeRateId);
                if (exchangeRate == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object of KursnaLista with identifier: " + exchangeRateId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                exchangeRateRepository.DeleteExchangeRate(exchangeRateId);
                exchangeRateRepository.SaveChanges(); //Perzistiramo promene
                message.information = "Successfully deleted " + exchangeRate.ToString();
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
        /// Vraca opcije dostupne za rad sa kursnim listama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetExchangeRateOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }



    }
}

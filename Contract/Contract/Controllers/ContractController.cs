using AutoMapper;
using Contract.Data;
using Contract.Entities;
using Contract.Models;
using Contract.ServiceCalls;
using Microsoft.AspNetCore.Mvc;

namespace Contract.Controllers
{
    [ApiController]
    [Route("api/contract")]
    [Produces("application/json", "application/xml")]
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository contractRepository;
        private readonly ILoggerService loggerService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly string serviceName = "ContractService";
        private readonly Message message = new Message();
        private readonly IDocumentService documentService;
        private readonly IBuyerService buyerService;
        private readonly IPublicBiddingService publicBiddingService;


        public ContractController(IContractRepository contractRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkGenerator, IDocumentService documentService, IBuyerService buyerService, IPublicBiddingService publicBiddingService)
        {
            this.contractRepository = contractRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.linkGenerator = linkGenerator;
            this.documentService = documentService;
            this.buyerService = buyerService;
            this.publicBiddingService = publicBiddingService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ContractEntity>>? GetContracts(string? referenceNumber = null)
        {

            message.serviceName = serviceName;
            message.method = "GET";
            List<ContractEntity> contracts = contractRepository.GetContracts(referenceNumber);
            if (contracts == null || contracts.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            try
            {
                foreach (ContractEntity c in contracts)
                {

                    DocumentDto document = documentService.GetDocumentById(c.documentId).Result;
                    if (document != null)
                    {
                        c.documentDto = document;
                    }
                }
                foreach (ContractEntity c in contracts)
                {
                    BuyerDto buyer = buyerService.GetBuyerById(c.buyerId).Result;
                    if (buyer != null)
                    {
                        c.buyerDto = buyer;
                    }
                }
                foreach (ContractEntity c in contracts)
                {
                    PublicBiddingDto bidding = publicBiddingService.GetPublicBiddingById(c.publicBiddingId).Result;
                    if (bidding != null)
                    {
                        c.publicBiddingDto = bidding;
                    }
                }
            }
            catch
            {
                return default;
            }

            message.information = "Returned list of Contract";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<ContractDto>>(contracts));
        }

        [HttpGet("{contractId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContractEntity> GetContractById(Guid contractId)
        {
            message.serviceName = serviceName;
            message.method = "GET";
            ContractEntity contract = contractRepository.GetContractById(contractId);
            if (contract == null)
            {
                message.information = "Not found";
                message.error = "There is no object with identifier: " + contractId;
                loggerService.CreateMessage(message);
                return NotFound();

            }
            message.information = contract.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ContractDto>(contract));

        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContractDto> CreateContract([FromBody] ContractDto contract)
        {

            message.serviceName = serviceName;
            message.method = "POST";
            try
            {

                ContractEntity cont = mapper.Map<ContractEntity>(contract);

                ContractEntity confirmation = contractRepository.CreateContract(cont);
                contractRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetContractById", "Contract", new { contractId = confirmation.contractId });
                message.information = cont.ToString() + " | Contract location: " + location;
                loggerService.CreateMessage(message);
                return Created(location, mapper.Map<ContractDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during creating");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContractDto> UpdateContract(ContractEntity contract)
        {

            message.serviceName = serviceName;
            message.method = "PUT";
            try
            {
                var upd = contractRepository.GetContractById(contract.contractId);

                if (upd == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + contract.contractId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                ContractEntity cont = mapper.Map<ContractEntity>(contract);

                mapper.Map(cont, upd);

                contractRepository.SaveChanges();

                message.information = cont.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ContractEntity>(cont));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{contractId}")]
        public IActionResult DeleteContract(Guid contractId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                ContractEntity cont = contractRepository.GetContractById(contractId);
                if (cont == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + contractId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }

                ContractEntity contract= contractRepository.GetContractById(contractId);
                contractRepository.DeleteContract(contractId);
                contractRepository.SaveChanges();

                message.information = "Successfully deleted " + contract.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + contract.ToString());
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
        public IActionResult GetContractOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
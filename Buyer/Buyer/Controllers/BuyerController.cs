using AutoMapper;
using Buyer.Data;
using Buyer.Entities;
using Buyer.Models;
using Buyer.ServiceCalls;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Buyer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/buyer")]
    [Produces("application/json", "application/xml")]
    public class BuyerController : ControllerBase
    {
        private readonly IIndividialRepository individialRepository;
        private readonly ILegalEntityRepository legalEntityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IAuthorizedPersonService authorizedPersonService;
        private readonly IPaymentService paymentService;
        private readonly IAddressService addressService;
        private readonly Message message = new Message();
        private readonly string serviceName = "BuyerService";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="individualRepository"></param>
        /// <param name="legalEntityRepository"></param>
        /// <param name="loggerService"></param>
        /// <param name="paymentService"></param>
        /// <param name="addressService"></param>
        /// <param name="authorizedPersonService"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public BuyerController(IIndividialRepository individualRepository, ILegalEntityRepository legalEntityRepository, ILoggerService loggerService, IPaymentService paymentService, IAddressService addressService, IAuthorizedPersonService authorizedPersonService, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.individialRepository = individualRepository;
            this.legalEntityRepository = legalEntityRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
            this.authorizedPersonService = authorizedPersonService;
            this.paymentService = paymentService;
            this.addressService = addressService;
            this.mapper = mapper;
        }
        /// <summary>
        /// List of all the buyers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        public ActionResult<List<BuyerModelDto>> GetBuyer()
        {
            List<Individual> individuals = individialRepository.GetIndividual();
            List<LegalEntity> legalEntities = legalEntityRepository.GetLegalEntity();
            message.ServiceName = serviceName;
            message.Method= "GET";
            List<BuyerModel> allBuyers = individuals.ConvertAll(i => (BuyerModel)i);
            List<BuyerModel> temp = legalEntities.ConvertAll(i => (BuyerModel)i);

            allBuyers.AddRange(temp); //Put together all buyers, individuals and legal entities

            try
            {
                foreach(BuyerModel b in allBuyers)
                {
                    AuthorizedPersonDto authorized = authorizedPersonService.GetAuthorizedPersonById(b.authorizedPersonId).Result;
                    if(authorized != null)
                    {
                        b.authorizedPersonDto = authorized;
                    }
                }
            }catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return default;
            }
            try
            {
                foreach(BuyerModel b in allBuyers)
                {
                    if(!((b.paymentId).Equals("string") || (b.paymentId).Length < 25))
                    {
                        PaymentDto paymentDto = paymentService.GetPaymentById(Guid.Parse((b.paymentId))).Result;
                        if(paymentDto != null)
                        {
                            b.paymentDto= paymentDto;
                        }
                    }
                }
            }catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return default;
            }
            try
            {
                foreach(BuyerModel b in allBuyers)
                {
                    if(!((b.addressId).Equals("string") || (b.addressId).Length < 25))
                    {
                        AddressDto address = addressService.GetAddressById(Guid.Parse((b.addressId))).Result;
                        Console.WriteLine(address);
                        if(address != null)
                        {
                            b.addressDto= address;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return default;

            }
            message.Information = "Returned list of kupci";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<BuyerModelDto>>(allBuyers));
        }

        ///<summary>
        ///Buyer by ID
        /// </summary>
        /// <param name="buyerId">Enter valid Id</param>
        /// <returns></returns>
        [HttpGet("{buyerId}")]
        public ActionResult<BuyerModelDto> GetBuyerById(Guid buyerId)
        {
            BuyerModel buyer;


            message.ServiceName = serviceName;
            message.Method = "GET";

            buyer = (BuyerModel)individialRepository.GetIndividualById(buyerId);

            if (buyer == null) buyer = (BuyerModel)legalEntityRepository.GetLegalEntityById(buyerId);
            if (buyer == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of buyer with identifier: " + buyerId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = buyer.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<BuyerModelDto>(buyer));

        }
        ///<summary>
        ///Deleting buyer
        /// </summary>
        /// <param name="buyerId">Enter buyer Id</param>
        /// <returns></returns>
        [HttpDelete("{buyerId}")]
        public IActionResult DeleteBuyer(Guid buyerId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                BuyerModel buyer;
                buyer = (BuyerModel)individialRepository.GetIndividualById(buyerId);
                if (buyer == null) buyer = (BuyerModel)legalEntityRepository.GetLegalEntityById(buyerId);
                if (buyer == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of ovlasceno lice with identifier: " + buyerId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                if (buyer.buyerType)
                {
                    individialRepository.DeleteIndividual(buyerId);
                    individialRepository.SaveChanges();
                }
                else
                {
                    legalEntityRepository.DeleteLegalEntity(buyerId);
                    legalEntityRepository.SaveChanges();
                }
                message.Information = "Successfully deleted";
                return NoContent();
            }
            catch(Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
       
        [HttpPut("individual")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BuyerConformationDto> UpdateIndividualBuyer(IndividualUpdateDto individual)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            try
            {
                var oldPerson = individialRepository.GetIndividualById(individual.buyerId);
                if (oldPerson == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of authorized person with identifier: " + individual.buyerId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Individual indi = mapper.Map<Individual>(individual);
                indi.individualId = individual.individualId;
                indi.individualName = individual.individualName;
                indi.individualSurname = individual.individualSurname;
                mapper.Map(indi, oldPerson);
                individialRepository.SaveChanges();
                message.Information = oldPerson.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<BuyerConformationDto>(oldPerson));

            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut("legalEntities")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BuyerConformationDto> UpdateLegalEntities(LegalEntityUpdateDto legalentity)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";

            try
            {
                var oldPerson = legalEntityRepository.GetLegalEntityById(legalentity.buyerId);
                if (oldPerson == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of authorized person with identifier: " + legalentity.buyerId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                LegalEntity legalEntity = mapper.Map<LegalEntity>(legalentity);
                legalEntity.legalEntityId = legalentity.legalEntityId;
                legalEntity.legalEntityName = legalentity.legalEntityName;
                legalEntity.legalEntityFax = legalentity.legalEntityFax;
                legalEntity.contactPerson = legalentity.contactPerson;
                mapper.Map(legalEntity, oldPerson);
                individialRepository.SaveChanges();
                message.Information = oldPerson.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<BuyerConformationDto>(oldPerson));

            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("individual")]
        [Produces("application/json")]
        public ActionResult<BuyerIndividualCreationDto> CreateBuyer([FromBody] BuyerIndividualCreationDto buyer)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            Individual b = mapper.Map<Individual>(buyer);

            
                Individual individualCreated = new Individual(b);
                individualCreated.individualId = buyer.individualId;
                individualCreated.individualName = buyer.individualName;
                individualCreated.individualSurname = buyer.individualSurname;
            
                Individual buyer1 = individialRepository.CreateIndividual(individualCreated);
                individialRepository.SaveChanges();

            string location = linkGenerator.GetPathByAction("GetBuyer", "Buyer", new { buyerId = b.buyerId });

            message.Information = buyer1.ToString() + " | buyer location: " + location;
            loggerService.CreateMessage(message);
            return Created(location, mapper.Map<Individual>(buyer1));

        }
        [HttpPost("legalEntity")]
        [Produces("application/json")]
        public ActionResult<BuyerLegalEntitiesCreationDto> CreateCreateLEBuyer([FromBody] BuyerLegalEntitiesCreationDto buyer)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";

            LegalEntity le = mapper.Map<LegalEntity>(buyer);


            LegalEntity legalEntity = new LegalEntity(le);
            legalEntity.legalEntityId = buyer.legalEntityId;
            legalEntity.legalEntityName = buyer.legalEntityName;
            legalEntity.legalEntityFax = buyer.legalEntityFax;
            legalEntity.contactPerson = buyer.contactPerson;
            LegalEntity legalEntity1 = legalEntityRepository.CreateLegalEntity(legalEntity);
            legalEntityRepository.SaveChanges();

            string? location = linkGenerator.GetPathByAction("GetBuyer", "Buyer", new { buyerId = le.buyerId });

            message.Information = legalEntity1.ToString() + " | buyer location: " + location;
            loggerService.CreateMessage(message);
            return Created(location, mapper.Map<LegalEntity>(legalEntity1));

        }

        /// <summary>
        /// Options for buyer
        /// </summary>
        /// <returns></returns>

        [HttpOptions]
        public IActionResult GetKupacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

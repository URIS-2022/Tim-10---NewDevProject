using AutoMapper;
using complaint.Models;
using complaint.ServiceCalls;
using Microsoft.AspNetCore.Mvc;
using complaint.Data;
using complaint.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Contracts;

namespace complaint.Controllers
{

    [ApiController]
    [Route("api/complaints")]
    [Produces("application/json", "application/xml")]


    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly string serviceName = "ZalbaService";
        private readonly Message message = new Message();
        private readonly LinkGenerator linkGenerator;
        private readonly IBuyerService buyerService;

        public ComplaintController(IComplaintRepository complaintRepository, IMapper mapper, ILoggerService loggerService, LinkGenerator linkgenerator, IBuyerService buyerService)
        {
            this.complaintRepository = complaintRepository;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.linkGenerator = linkgenerator;
            this.buyerService = buyerService;
        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Complaint>> GetAllComplaints()
        {
           

            message.ServiceName = serviceName;
            message.Method = "GET";
            List<Complaint> complaint = complaintRepository.GetAllComplaints();
            if (complaint == null || complaint.Count == 0)
            {
                message.Information = "No content";
                message.Error = "There is no content in database!";
                loggerService.CreateMessage(message);
                return NoContent();
            }
            try
            {


                foreach (Complaint p in complaint)
                {
                    BuyerDto buyer = buyerService.GetComplaintSubmitter(p.complaintSubmitter).Result;
                    if (buyer != null)
                    {
                        p.buyer = buyer;
                    }

                }
            }
            catch
            {
                return default;
            }

            message.Information = "Returned list of Contract";
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<List<ComplaintDto>>(complaint));

        }
        [HttpGet("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Complaint> GetComplaintById(Guid complaintId)
        {
            Complaint complaint = complaintRepository.GetComplaintById(complaintId);
            message.ServiceName = serviceName;
            message.Method = "GET";
            if (complaint == null)
            {
                message.Information = "Not found";
                message.Error = "There is no object of Zalba with identifier: " + complaintId;
                loggerService.CreateMessage(message);
                return NotFound();
            }
            message.Information = complaint.ToString();
            loggerService.CreateMessage(message);
            return Ok(mapper.Map<ComplaintDto>(complaint));

        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Complaint> CreateComplaint([FromBody] ComplaintDto complaint)
        {
            message.ServiceName = serviceName;
            message.Method = "POST";
            try
            {
                Complaint _complaint = mapper.Map<Complaint>(complaint);
                Complaint confirmation = complaintRepository.CreateComplaint(_complaint);
                complaintRepository.SaveChanges();

                string  ?lokacija = linkGenerator.GetPathByAction("GetComplaintById", "Complaint", new { complaintId = confirmation.complaintId });
                message.Information = complaint.ToString() + " | Zalba location: " + lokacija;
                loggerService.CreateMessage(message);

                return Created(lokacija, mapper.Map<ComplaintDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja zalbe!");
            }



        }
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintDto> UpdateComplaint(ComplaintDto complaint)
        {
            message.ServiceName = serviceName;
            message.Method = "PUT";
            try
            {
                var oldComplaint = complaintRepository.GetComplaintById(complaint.complaintId);
                if (oldComplaint == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + complaint.complaintId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                Complaint newComplaint = mapper.Map<Complaint>(complaint);
                mapper.Map(newComplaint, oldComplaint);
                complaintRepository.SaveChanges();
                message.Information = oldComplaint.ToString();
                loggerService.CreateMessage(message);
                return Ok(mapper.Map<ComplaintDto>(oldComplaint));
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom izmene zalbe!");
            }
        }

        [HttpDelete("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteComplaint(Guid complaintId)
        {
            message.ServiceName = serviceName;
            message.Method = "DELETE";
            try
            {
                var complaint = complaintRepository.GetComplaintById(complaintId);
                if (complaint == null)
                {
                    message.Information = "Not found";
                    message.Error = "There is no object of Zalba with identifier: " + complaintId;
                    loggerService.CreateMessage(message);
                    return NotFound();
                }
                complaintRepository.DeleteComplaint(complaintId);
                complaintRepository.SaveChanges();
                message.Information = "Successfully deleted " + complaint.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + complaint.ToString());
            }
            catch (Exception ex)
            {
                message.Information = "Server error";
                message.Error = ex.Message;
                loggerService.CreateMessage(message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja zalbe!");
            }
        }
        /// <summary>
        /// Prikazuje metode koje je moguće koristiti
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }




}

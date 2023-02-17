using AutoMapper;
using complaint.Data;
using complaint.Entities;
using complaint.Models;
using Microsoft.AspNetCore.Mvc;

namespace complaint.Controllers
{


    [ApiController]
    [Route("api/complaintStatuses")]
    [Produces("application/json", "application/xml")]
    public class ComplaintStatusController : ControllerBase
    {
        private readonly IComplaintStatusRepository complaintStatusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public ComplaintStatusController(IComplaintStatusRepository complaintStatusRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintStatusRepository = complaintStatusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ComplaintStatusDto>> GetStatusList()
        {
            var complaintStatuses = complaintStatusRepository.GetStatusList();


            if (complaintStatuses == null || complaintStatuses.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ComplaintStatusDto>>(complaintStatuses));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{statusId}")]
        public ActionResult<ComplaintStatusDto> GetStatusById(Guid statusId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var complaintStatus = complaintStatusRepository.GetStatusById(statusId);

            if (complaintStatus == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintStatusDto>(complaintStatus));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<ComplaintStatusDto> CreateStatus([FromBody] ComplaintStatusDto complaintStatus)
        {
            try
            {

                ComplaintStatus d = mapper.Map<ComplaintStatus>(complaintStatus);
                ComplaintStatus complaintStatus1 = complaintStatusRepository.CreateStatus(d);

                string location = linkGenerator.GetPathByAction("GetStatusList", "ComplaintStatus", new { statusId = complaintStatus1.complaintStatusId });
                return Created(location, mapper.Map<ComplaintStatusDto>(complaintStatus1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{statusId}")]
        public IActionResult DeleteStatus(Guid statusId)
        {
            try
            {
                ComplaintStatus complaintStatus = complaintStatusRepository.GetStatusById(statusId);
                if (complaintStatus == null)
                {
                    return NotFound();
                }

                complaintStatusRepository.DeleteStatus(statusId);
                complaintStatusRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja statusa!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<ComplaintStatusDto> UpdateStatus(ComplaintStatus complaintStatus)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldStatus = complaintStatusRepository.GetStatusById(complaintStatus.complaintStatusId);
                if (oldStatus == null)
                {
                    return NotFound();
                }
                ComplaintStatus d = mapper.Map<ComplaintStatus>(complaintStatus);

                mapper.Map(d, oldStatus); //Update objekta koji treba da sačuvamo u bazi                

                complaintStatusRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ComplaintStatusDto>(d));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetDrzavaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }






    }




}

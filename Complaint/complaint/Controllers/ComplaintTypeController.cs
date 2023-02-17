using AutoMapper;
using Azure;
using complaint.Data;
using complaint.Entities;
using complaint.Models;
using Microsoft.AspNetCore.Mvc;

namespace complaint.Controllers
{

    [ApiController]
    [Route("api/complaintTypes")]
    [Produces("application/json", "application/xml")]
    public class ComplaintTypeController : ControllerBase
    {

        private readonly IComplaintTypeRepository complaintTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public ComplaintTypeController(IComplaintTypeRepository complaintTypeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintTypeRepository = complaintTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ComplaintTypeDto>> GetTypeList()
        {
            var complaintTypes = complaintTypeRepository.GetTypeList();


            if (complaintTypes == null || complaintTypes.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ComplaintTypeDto>>(complaintTypes));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{complaintTypeId}")]
        public ActionResult<ComplaintTypeDto> GetTypeById(Guid complaintTypeId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var complaintType = complaintTypeRepository.GetTypeById(complaintTypeId);

            if (complaintType == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintTypeDto>(complaintType));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<ComplaintTypeDto> CreateType([FromBody] ComplaintTypeDto complaintType)
        {
            try
            {

                ComplaintType d = mapper.Map<ComplaintType>(complaintType);
                ComplaintType complaintType1 = complaintTypeRepository.CreateType(d);

                string location = linkGenerator.GetPathByAction("GetTypeList", "ComplaintType", new { complaintTypeId = complaintType1.complaintTypeId });
                return Created(location, mapper.Map<ComplaintTypeDto>(complaintType1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{complaintTypeId}")]
        public IActionResult DeleteType(Guid complaintTypeId)
        {
            try
            {
                ComplaintType complaintType = complaintTypeRepository.GetTypeById(complaintTypeId);
                if (complaintType == null)
                {
                    return NotFound();
                }

                complaintTypeRepository.DeleteType(complaintTypeId);
                complaintTypeRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja statusa!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<ComplaintTypeDto> UpdateType(ComplaintType complaintType)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldType = complaintTypeRepository.GetTypeById(complaintType.complaintTypeId);
                if (oldType == null)
                {
                    return NotFound();
                }
                ComplaintType d = mapper.Map<ComplaintType>(complaintType);

                mapper.Map(d, oldType); //Update objekta koji treba da sačuvamo u bazi                

                complaintTypeRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ComplaintTypeDto>(d));
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

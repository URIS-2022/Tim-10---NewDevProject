using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/workability")]
    [Produces("application/json", "application/xml")]
    public class WorkabilityController : ControllerBase
    {
        private readonly IWorkabilityRepository workabilityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public WorkabilityController(IWorkabilityRepository workabilityRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.workabilityRepository = workabilityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<WorkabilityDto>> GetWorkabilityList()
        {
            var workabilities = workabilityRepository.GetWorkabilityList();


            if (workabilities == null || workabilities.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<WorkabilityDto>>(workabilities));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{workabilityId}")]
        public ActionResult<WorkabilityDto> GetWorkabilityById(Guid workabilityId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var workability = workabilityRepository.GetWorkabilityById(workabilityId);

            if (workability == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WorkabilityDto>(workability));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<WorkabilityDto> CreateWorkability([FromBody] WorkabilityDto workability)
        {
            try
            {

                Workability w = mapper.Map<Workability>(workability);
                Workability workability1 = workabilityRepository.CreateWorkability(w);

                string? location = linkGenerator.GetPathByAction("GetWorkabilityList", "Workability", new { workabilityId = workability1.workabilityId });
                return Created(location, mapper.Map<WorkabilityDto>(workability1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{workabilityId}")]
        public IActionResult DeleteWorkability(Guid workabilityId)
        {
            try
            {
                Workability workability = workabilityRepository.GetWorkabilityById(workabilityId);
                if (workability == null)
                {
                    return NotFound();
                }

                workabilityRepository.DeleteWorkability(workabilityId);
                workabilityRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a workability!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<WorkabilityDto> UpdateWorkability(Workability workability)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldWorkability = workabilityRepository.GetWorkabilityById(workability.workabilityId);
                if (oldWorkability == null)
                {
                    return NotFound();
                }
                Workability w = mapper.Map<Workability>(workability);

                mapper.Map(w, oldWorkability); //Update objekta koji treba da sačuvamo u bazi                

                workabilityRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<WorkabilityDto>(w));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetWorkabilityOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }


    }
}

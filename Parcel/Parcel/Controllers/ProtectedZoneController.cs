using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/protectedZone")]
    [Produces("application/json", "application/xml")]
    public class ProtectedZoneController : ControllerBase
    {

        private readonly IProtectedZoneRepository protectedZoneRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public ProtectedZoneController(IProtectedZoneRepository protectedZoneRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.protectedZoneRepository = protectedZoneRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ProtectedZoneDto>> GetProtectedZoneList()
        {
            var protectedZones = protectedZoneRepository.GetProtectedZoneList();


            if (protectedZones == null || protectedZones.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ProtectedZoneDto>>(protectedZones));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{protectedZoneId}")]
        public ActionResult<ProtectedZoneDto> GetProtectedZoneById(Guid protectedZoneId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var protectedZone = protectedZoneRepository.GetProtectedZoneById(protectedZoneId);

            if (protectedZone == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProtectedZoneDto>(protectedZone));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<ProtectedZoneDto> CreateProtectedZone([FromBody] ProtectedZoneDto protectedZone)
        {
            try
            {

                ProtectedZone p = mapper.Map<ProtectedZone>(protectedZone);
                ProtectedZone protectedZone1 = protectedZoneRepository.CreateProtectedZone(p);

                string? location = linkGenerator.GetPathByAction("GetProtectedZoneList", "ProtectedZone", new { protectedZoneId = protectedZone1.protectedZoneId });
                return Created(location, mapper.Map<ProtectedZoneDto>(protectedZone1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{protectedZoneId}")]
        public IActionResult DeleteProtectedZone(Guid protectedZoneId)
        {
            try
            {
                ProtectedZone protectedZone = protectedZoneRepository.GetProtectedZoneById(protectedZoneId);
                if (protectedZone == null)
                {
                    return NotFound();
                }

                protectedZoneRepository.DeleteProtectedZone(protectedZoneId);
                protectedZoneRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a protectedZone!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<ProtectedZoneDto> UpdateProtectedZone(ProtectedZone protectedZone)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldProtectedZone = protectedZoneRepository.GetProtectedZoneById(protectedZone.protectedZoneId);
                if (oldProtectedZone == null)
                {
                    return NotFound();
                }
                ProtectedZone p = mapper.Map<ProtectedZone>(protectedZone);

                mapper.Map(p, oldProtectedZone); //Update objekta koji treba da sačuvamo u bazi                

                protectedZoneRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ProtectedZoneDto>(p));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetProtectedZoneOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }

    }
}

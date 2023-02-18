using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/drainage")]
    [Produces("application/json", "application/xml")]
    public class DrainageController : ControllerBase
    {
        private readonly IDrainageRepository drainageRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public DrainageController(IDrainageRepository drainageRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.drainageRepository = drainageRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DrainageDto>> GetDrainageList()
        {
            var drainages = drainageRepository.GetDrainageList();


            if (drainages == null || drainages.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<DrainageDto>>(drainages));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{drainageId}")]
        public ActionResult<DrainageDto> GetDrainageById(Guid drainageId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var drainage = drainageRepository.GetDrainageById(drainageId);

            if (drainage == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DrainageDto>(drainage));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<DrainageDto> CreateDrainage([FromBody] DrainageDto drainage)
        {
            try
            {

                Drainage d = mapper.Map<Drainage>(drainage);
                Drainage drainage1 = drainageRepository.CreateDrainage(d);

                string? location = linkGenerator.GetPathByAction("GetDrainageList", "Drainage", new { drainageId = drainage1.drainageId });
                return Created(location, mapper.Map<DrainageDto>(drainage1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{drainageId}")]
        public IActionResult DeleteDrainage(Guid drainageId)
        {
            try
            {
                Drainage drainage = drainageRepository.GetDrainageById(drainageId);
                if (drainage == null)
                {
                    return NotFound();
                }

                drainageRepository.DeleteDrainage(drainageId);
                drainageRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a drainage!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<DrainageDto> UpdateDrainage(Drainage drainage)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldDrainage = drainageRepository.GetDrainageById(drainage.drainageId);
                if (oldDrainage == null)
                {
                    return NotFound();
                }
                Drainage d = mapper.Map<Drainage>(drainage);

                mapper.Map(d, oldDrainage); //Update objekta koji treba da sačuvamo u bazi                

                drainageRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<DrainageDto>(d));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetDrainageOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

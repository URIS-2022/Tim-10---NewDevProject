using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/culture")]
    [Produces("application/json", "application/xml")]
    public class CultureController : ControllerBase
    {

        private readonly ICultureRepository cultureRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public CultureController(ICultureRepository cultureRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cultureRepository = cultureRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CultureDto>> GetCultureList()
        {
            var cultures = cultureRepository.GetCultureList();


            if (cultures == null || cultures.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CultureDto>>(cultures));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{cultureId}")]
        public ActionResult<CultureDto> GetCultureById(Guid cultureId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var culture = cultureRepository.GetCultureById(cultureId);

            if (culture == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CultureDto>(culture));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CultureDto> CreateCulture([FromBody] CultureDto culture)
        {
            try
            {

                Culture c = mapper.Map<Culture>(culture);
                Culture culture1 = cultureRepository.CreateCulture(c);

                string? location = linkGenerator.GetPathByAction("GetCultureList", "Culture", new { cultureId = culture1.cultureId });
                return Created(location, mapper.Map<CultureDto>(culture1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{cultureId}")]
        public IActionResult DeleteCulture(Guid cultureId)
        {
            try
            {
                Culture culture = cultureRepository.GetCultureById(cultureId);
                if (culture == null)
                {
                    return NotFound();
                }

                cultureRepository.DeleteCulture(cultureId);
                cultureRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a culture!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<CultureDto> UpdateCulture(Culture culture)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldCulture = cultureRepository.GetCultureById(culture.cultureId);
                if (oldCulture == null)
                {
                    return NotFound();
                }
                Culture c = mapper.Map<Culture>(culture);

                mapper.Map(c, oldCulture); //Update objekta koji treba da sačuvamo u bazi                

                cultureRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<CultureDto>(c));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetCultureOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }


    }
}

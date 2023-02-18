using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/cadastralMunicipality")]
    [Produces("application/json")]
    public class CadastralMunicipalityController: ControllerBase
    {

        private readonly ICadastralMunicipalityRepository cadastralMunicipalityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public CadastralMunicipalityController(ICadastralMunicipalityRepository cadastralMunicipalityRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.cadastralMunicipalityRepository = cadastralMunicipalityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CadastralMunicipalityDto>> GetCadastralMunicipalityList()
        {
            var cadastralMunicipalities = cadastralMunicipalityRepository.GetCadastralMunicipalityList();

            if (cadastralMunicipalities == null || cadastralMunicipalities.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CadastralMunicipalityDto>>(cadastralMunicipalities));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{cadastralMunicipalityId}")]
        public ActionResult<CadastralMunicipalityDto> GetCadastralMunicipalityById(Guid cadastralMunicipalityId)
        {
            var cadastralMunicipality = cadastralMunicipalityRepository.GetCadastralMunicipalityById(cadastralMunicipalityId);

            if (cadastralMunicipality == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CadastralMunicipalityDto>(cadastralMunicipality));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CadastralMunicipalityDto> CreateCadastralMunicipality([FromBody] CadastralMunicipalityDto cadastralMunicipality)
        {
            try
            {
                CadastralMunicipality c = mapper.Map<CadastralMunicipality>(cadastralMunicipality);
                CadastralMunicipality cadastralMunicipality1 = cadastralMunicipalityRepository.CreateCadastralMunicipality(c);

                string? location = linkGenerator.GetPathByAction("GetCadastralMunicipalityList", "CadastralMunicipality", new { cadastralMunicipalityId = cadastralMunicipality1.cadastralMunicipalityId });
                return Created(location, mapper.Map<CadastralMunicipalityDto>(cadastralMunicipality1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{cadastralMunicipalityId}")]
        public IActionResult DeleteCadastralMunicipality(Guid cadastralMunicipalityId)
        {
            try
            {
                CadastralMunicipality cadastralMunicipality = cadastralMunicipalityRepository.GetCadastralMunicipalityById(cadastralMunicipalityId);
                if (cadastralMunicipality == null)
                {
                    return NotFound();
                }

                cadastralMunicipalityRepository.DeleteCadastralMunicipality(cadastralMunicipalityId);
                cadastralMunicipalityRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a cadastralMunicipality!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<CadastralMunicipalityDto> UpdateCadastralMunicipality(CadastralMunicipality cadastralMunicipality)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldCadastralMunicipality = cadastralMunicipalityRepository.GetCadastralMunicipalityById(cadastralMunicipality.cadastralMunicipalityId);
                if (oldCadastralMunicipality == null)
                {
                    return NotFound();
                }
                CadastralMunicipality c = mapper.Map<CadastralMunicipality>(cadastralMunicipality);

                mapper.Map(c, oldCadastralMunicipality); //Update objekta koji treba da sačuvamo u bazi                

                cadastralMunicipalityRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<CadastralMunicipalityDto>(c));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetCadastralMunicipalityOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

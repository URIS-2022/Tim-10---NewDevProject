using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/formOfProperty")]
    [Produces("application/json", "application/xml")]
    public class FormOfPropertyController : ControllerBase
    {

        private readonly IFormOfPropertyRepository formOfPropertyRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public FormOfPropertyController(IFormOfPropertyRepository formOfPropertyRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.formOfPropertyRepository = formOfPropertyRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<FormOfPropertyDto>> GetFormOfPropertyList()
        {
            var formOfProperties = formOfPropertyRepository.GetFormOfPropertyList();


            if (formOfProperties == null || formOfProperties.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<FormOfPropertyDto>>(formOfProperties));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{formOfPropertyId}")]
        public ActionResult<FormOfPropertyDto> GetFormOfPropertyById(Guid formOfPropertyId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var formOfProperty = formOfPropertyRepository.GetFormOfPropertyById(formOfPropertyId);

            if (formOfProperty == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<FormOfPropertyDto>(formOfProperty));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<FormOfPropertyDto> CreateFormOfProperty([FromBody] FormOfPropertyDto formOfProperty)
        {
            try
            {

                FormOfProperty f = mapper.Map<FormOfProperty>(formOfProperty);
                FormOfProperty formOfProperty1 = formOfPropertyRepository.CreateFormOfProperty(f);

                string? location = linkGenerator.GetPathByAction("GetFormOfPropertyList", "FormOfProperty", new { formOfPropertyyId = formOfProperty1.formOfPropertyId });
                return Created(location, mapper.Map<FormOfPropertyDto>(formOfProperty1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{formOfPropertyId}")]
        public IActionResult DeleteFormOfProperty(Guid formOfPropertyId)
        {
            try
            {
                FormOfProperty formOfProperty = formOfPropertyRepository.GetFormOfPropertyById(formOfPropertyId);
                if (formOfProperty == null)
                {
                    return NotFound();
                }

                formOfPropertyRepository.DeleteFormOfProperty(formOfPropertyId);
                formOfPropertyRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a formOfProperty!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<FormOfPropertyDto> UpdateFormOfProperty(FormOfProperty formOfProperty)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldFormOfProperty = formOfPropertyRepository.GetFormOfPropertyById(formOfProperty.formOfPropertyId);
                if (oldFormOfProperty == null)
                {
                    return NotFound();
                }
                FormOfProperty f = mapper.Map<FormOfProperty>(formOfProperty);

                mapper.Map(f, oldFormOfProperty); //Update objekta koji treba da sačuvamo u bazi                

                formOfPropertyRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<FormOfPropertyDto>(f));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetFormOfPropertyOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

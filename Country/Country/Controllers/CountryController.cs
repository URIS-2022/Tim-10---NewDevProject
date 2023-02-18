using AutoMapper;
using Country.Data;
using Country.Entities;
using Country.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Country.Controllers
{
    [ApiController]
    [Route("api/country")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public CountryController(ICountryRepository countryRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CountryDto>> GetCountryList()
        {
            var countryes = countryRepository.GetCountryList();


            if (countryes == null || countryes.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CountryDto>>(countryes));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{countryId}")]
        public ActionResult<CountryDto> GetCountryById(Guid countryId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var country = countryRepository.GetCountryById(countryId);

            if (country == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CountryDto>(country));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CountryDto> CreateCountry([FromBody] CountryDto country)
        {
            try
            {

                Country1 c = mapper.Map<Country1>(country);
                Country1 country1 = countryRepository.CreateCountry(c);

                string? location = linkGenerator.GetPathByAction("GetCountryList", "Country", new { countryId = country1.countryId });
                return Created(location, mapper.Map<CountryDto>(country1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(Guid countryId)
        {
            try
            {
                Country1 country = countryRepository.GetCountryById(countryId);
                if (country == null)
                {
                    return NotFound();
                }

                countryRepository.DeleteCountry(countryId);
                countryRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a country!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<CountryDto> UpdateCountry(Country1 country)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldCountry = countryRepository.GetCountryById(country.countryId);
                if (oldCountry == null)
                {
                    return NotFound();
                }
                Country1 c = mapper.Map<Country1>(country);

                mapper.Map(c, oldCountry); //Update objekta koji treba da sačuvamo u bazi                

                countryRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<CountryDto>(c));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpOptions]
        public IActionResult GetCountryOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }


    }
}

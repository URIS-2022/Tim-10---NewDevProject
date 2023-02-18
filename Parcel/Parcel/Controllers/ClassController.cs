using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parcel.Data;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Controllers
{
    [ApiController]
    [Route("api/class")]
    [Produces("application/json")]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository classRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ClassController(IClassRepository classRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.classRepository = classRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ClassDto>> GetClassList()
        {
            var classes = classRepository.GetClassList();

            if (classes == null || classes .Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ClassDto>>(classes));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{classId}")]
        public ActionResult<ClassDto> GetClassById(Guid classId)
        {
            Class classs = classRepository.GetClassById(classId);

            if (classs == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClassDto>(classs));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<ClassDto> CreateClass([FromBody] ClassDto classs)
        {
            try
            {

                Class c = mapper.Map<Class>(classs);
                Class class1 = classRepository.CreateClass(c);

                string? location = linkGenerator.GetPathByAction("GetClassList", "Class", new { classId = class1.classId });
                return Created(location, mapper.Map<ClassDto>(class1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{classId}")]
        public IActionResult DeleteClass(Guid classId)
        {
            try
            {
                Class classs = classRepository.GetClassById(classId);
                if (classs == null)
                {
                    return NotFound();
                }

                classRepository.DeleteClass(classId);
                classRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting a class!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<ClassDto> UpdateClass(Class classs)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldClass = classRepository.GetClassById(classs.classId);
                if (oldClass == null)
                {
                    return NotFound();
                }
                Class c = mapper.Map<Class>(classs);

                mapper.Map(c, oldClass); //Update objekta koji treba da sačuvamo u bazi                

                classRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ClassDto>(c));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetClassOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }

    }
}

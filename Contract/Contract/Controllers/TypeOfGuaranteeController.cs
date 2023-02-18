using AutoMapper;
using Contract.Data;
using Contract.Entities;
using Contract.Models;
using Contract.ServiceCalls;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.Contracts;

namespace Contract.Controllers
{
    [ApiController]
    [Route("api/guarantee")]
    [Produces("application/json", "application/xml")]
    public class TypeOfGuaranteeController : ControllerBase
    {
        private readonly ITypeOfGuaranteeRepository typeOfGuaranteeRepository;
        private readonly Message message = new Message();
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public TypeOfGuaranteeController(ITypeOfGuaranteeRepository typeOfGuaranteeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.typeOfGuaranteeRepository = typeOfGuaranteeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TypeOfGuaranteeEntity>> GetGuarantees(string? type = null)
        {
            List<TypeOfGuaranteeEntity> guarantees = typeOfGuaranteeRepository.GetGuarantees(type);
            if (guarantees == null || guarantees.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TypeOfGuaranteeDto>>(guarantees));
        }

        [HttpGet("{typeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeOfGuaranteeEntity> GetGuaranteeById(Guid typeId)
        {
            TypeOfGuaranteeEntity contract = typeOfGuaranteeRepository.GetGuaranteeById(typeId);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TypeOfGuaranteeDto>(contract));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TypeOfGuaranteeDto> CreateGuarantee([FromBody] TypeOfGuaranteeDto g)
        {
            try
            {
                TypeOfGuaranteeEntity guarantee = mapper.Map<TypeOfGuaranteeEntity>(g);
                TypeOfGuaranteeEntity confirmation = typeOfGuaranteeRepository.CreateGuarantee(guarantee);
                typeOfGuaranteeRepository.SaveChanges();
                string? location = linkGenerator.GetPathByAction("GetGuaranteeById", "TypeOfGuarantee", new { TypeId = g.typeId });
                return Created(location, mapper.Map<TypeOfGuaranteeDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during creating");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TypeOfGuaranteeDto> UpdateGuarantee(TypeOfGuaranteeEntity guarantee)
        {
            try
            {
                var upd = typeOfGuaranteeRepository.GetGuaranteeById(guarantee.typeId);

                if (upd == null)
                {
                    return NotFound();
                }
                TypeOfGuaranteeEntity g = mapper.Map<TypeOfGuaranteeEntity>(guarantee);

                mapper.Map(g, upd);

                typeOfGuaranteeRepository.SaveChanges();
                return Ok(mapper.Map<TypeOfGuaranteeEntity>(g));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{typeId}")]
        public IActionResult DeleteGuarantee(Guid typeId)
        {
            try
            {
                TypeOfGuaranteeEntity type = typeOfGuaranteeRepository.GetGuaranteeById(typeId);
                if (type == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + typeId;
                    return NotFound();
                }
                typeOfGuaranteeRepository.DeleteGuarantee(typeId);
                TypeOfGuaranteeEntity t = typeOfGuaranteeRepository.GetGuaranteeById(typeId);
                typeOfGuaranteeRepository.SaveChanges();
                message.information = "Successfully deleted " + t.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + t.ToString());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting");
            }
        }

        [HttpOptions]
        public IActionResult GetGuaranteeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }

}

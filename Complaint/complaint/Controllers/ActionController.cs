using AutoMapper;
using complaint.Data;
using complaint.Entities;
using complaint.Models;
using Microsoft.AspNetCore.Mvc;

namespace complaint.Controllers
{


    [ApiController]
    [Route("api/actions")]
    [Produces("application/json", "application/xml")]
    public class ActionController : ControllerBase
    {
        private readonly IActionRepository actionRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public ActionController(IActionRepository actionRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.actionRepository = actionRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ActionDto>> GetActionList()
        {
            var action = actionRepository.GetActionList();


            if (action == null || action.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ActionDto>>(action));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{actionId}")]
        public ActionResult<ActionDto> GetActionById(Guid actionId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var action = actionRepository.GetActionById(actionId);

            if (action == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ActionDto>(action));
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<ActionDto> CreateAction([FromBody] ActionDto action)
        {
            try
            {

                Entities.Action d = mapper.Map<Entities.Action>(action);
                Entities.Action action1 = actionRepository.CreateAction(d);

                string location = linkGenerator.GetPathByAction("GetActionList", "Action", new { actionId = action1.actionId });
                return Created(location, mapper.Map<ActionDto>(action1));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{actionId}")]
        public IActionResult DeleteAction(Guid actionId)
        {
            try
            {
                Entities.Action action = actionRepository.GetActionById(actionId);
                if (action == null)
                {
                    return NotFound();
                }

                actionRepository.DeleteAction(actionId);
                actionRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja radnje!");
            }
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<ActionDto> UpdateAction(Entities.Action action)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldAction = actionRepository.GetActionById(action.actionId);
                if (oldAction == null)
                {
                    return NotFound();
                }
                Entities.Action d = mapper.Map<Entities.Action>(action);

                mapper.Map(d, oldAction); //Update objekta koji treba da sačuvamo u bazi                

                actionRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ActionDto>(d));
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

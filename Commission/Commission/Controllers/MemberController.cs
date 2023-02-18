using AutoMapper;
using AutoMapper.Execution;
using Azure;
using Commission.Data;
using Commission.Entities;
using Commission.Models;
using Commission.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commission.Controllers
{
    [Route("api/member")]
    [ApiController]
    //[Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository memberRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly string serviceName = "Commission";
        private readonly Message message = new Message();
        private readonly IPersonalityService personalityService;

        public MemberController(IMemberRepository memberRepository, LinkGenerator linkGenerator, IMapper mapper, IPersonalityService personalityService)
        {
            this.memberRepository = memberRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.personalityService = personalityService;
        }
        /// <summary>
        /// Returns all the members of the commission.
        /// </summary>
        /// <returns>A list of members</returns>
        /// <response code="200">Returns a list of members</response>
        /// <response code="204">There are no members to show</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<MemberDto>> GetAllMembers(Guid? commissionId)
        {

            message.serviceName = serviceName;
            message.method = "GET";
            List<MemberEntity> member = memberRepository.GetAllMembers();
            if (member == null || member.Count == 0)
            {
                message.information = "No content";
                message.error = "There is no content in database!";
                return NoContent();
            }
            List<MemberDto> memberDto = mapper.Map<List<MemberDto>>(member);

            foreach (MemberDto p in memberDto)
            {

                p.personality = personalityService.GetPersonality(p.memberId).Result;

            }
            message.information = "Returned list of Members";
            return Ok(memberDto);

        }
        /// <summary>
        /// Returns a member with the passed ID.
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>Member</returns>
        /// <response code="200">Returned member</response>
        /// <response code="204">There is no member with the passed ID</response>
        [HttpGet("{memberId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<MemberDto> GetMember(Guid memberId)
        {
            MemberEntity member = memberRepository.GetMemberById(memberId);
            message.serviceName = serviceName;
            message.method = "GET";
            if (member == null)
            {
                message.information = "Not found";
                message.error = "There is no object with identifier: " + memberId;
                return NotFound();
            }
            MemberDto memberDto = mapper.Map<MemberDto>(member);
            memberDto.personality = personalityService.GetPersonality(member.memberId).Result;
            message.information = member.ToString();
            return Ok(memberDto);

        }
        /// <summary>
        /// Adds a member.
        /// </summary>
        /// <param name="memberDto">Model of member</param>
        /// <returns>Data about the added member</returns>
        /// <remarks>
        /// EXAMPLE \
        /// POST /api/member \
        /// {
        ///     "memberId": "54a107-684d0d5-205-f724-08d9f3dcf86e",
        ///     "commissionId": "54a107-684d0d5-205-f724-08d9f3dcf86e"
        ///     
        /// }
        /// </remarks>
        /// <response code="201">Returns data about the added member</response>
        /// <response code="500">An error occurred</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MemberDto> CreateMember([FromBody] MemberDto member)
        {
            message.serviceName = serviceName;
            message.method = "POST";
            try
            {
                MemberEntity _member = mapper.Map<MemberEntity>(member);
                MemberDto confirmation = memberRepository.CreateMember(_member);
                memberRepository.SaveChanges();

                string? location = linkGenerator.GetPathByAction("GetMember", "Member", new { memberId = confirmation.memberId });
                message.information = member.ToString() + " | Member location: " + location;

                return Created(location, mapper.Map<MemberDto>(confirmation));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred durring adding");
            }
        }
        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="memberDto">Model of a member</param>
        /// <returns>Data about the member</returns>
        ///     /// <remarks>
        /// EXAMPLE \
        /// POST /api/member \
        /// {
        ///     "memberId": "54a107-684d0d5-205-f724-08d9f3dcf86e",
        ///     "commisionId": "54a107-684d0d5-205-f724-08d9f3dcf86e"
        /// }
        /// </remarks>
        /// <response code="200">Returns data about the updated member</response>
        /// <response code="404">There is no member which you tried to update</response>
        /// <response code="500">An error occurred during updating</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MemberDto> UpdateMember(MemberDto member)
        {
            message.serviceName = serviceName;
            message.method = "PUT";

            try
            {
                var old = memberRepository.GetMemberById(member.memberId);
                if (old == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + member.memberId;
                    return NotFound();
                }
                MemberEntity neww = mapper.Map<MemberEntity>(member);
                mapper.Map(neww, old);
                memberRepository.SaveChanges();
                message.information = old.ToString();
                return Ok(mapper.Map<MemberDto>(member));
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }
        /// <summary>
        /// Deletes a member with the passed ID
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>string</returns>
        /// <response code="204">A message about a successful deletion</response>
        /// <response code="404">There is no member with that ID</response>
        /// <response code="500">An error occurred durring deleting</response>
        [HttpDelete("{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMember(Guid memberId)
        {
            message.serviceName = serviceName;
            message.method = "DELETE";
            try
            {
                var member = memberRepository.GetMemberById(memberId);
                if (member == null)
                {
                    message.information = "Not found";
                    message.error = "There is no object with identifier: " + memberId;
                    return NotFound();
                }
                memberRepository.DeleteMember(memberId);
                memberRepository.SaveChanges();
                message.information = "Successfully deleted " + member.ToString();
                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted " + member.ToString());
            }
            catch (Exception ex)
            {
                message.information = "Server error";
                message.error = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting");
            }
        }
        /// <summary>
        /// The methods you can use
        /// </summary>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetMemberOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}

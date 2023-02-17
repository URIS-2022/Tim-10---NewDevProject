using Commission.Models;

namespace Commission.ServiceCalls
{
    public interface IPersonalityService
    {
        Task<PersonalityDto> GetPersonality(Guid personalityId);
    }
}

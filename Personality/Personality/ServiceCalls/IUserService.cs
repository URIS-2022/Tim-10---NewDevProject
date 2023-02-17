using Personality.Models;

namespace Personality.ServiceCalls
{
    public interface IUserService
    {
        public bool validateUser(Principal principal);

    }
}

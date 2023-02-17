using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface IAuthorizedPersonService
    {
        public Task<AuthorizedPersonDto> GetAuthorizedPersonById(Guid? APID);
    }
}

using complaint.Models;
namespace complaint.ServiceCalls
{
    public interface IBuyerService
    {
        Task<BuyerDto> GetComplaintSubmitter(Guid buyerId);

    }
}

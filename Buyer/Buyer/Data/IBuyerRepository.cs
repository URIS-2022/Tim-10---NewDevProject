using Buyer.Entities;

namespace Buyer.Data
{
    public interface IBuyerRepository
    {
        List<BuyerModel> GetBuyer();
        BuyerModel GetBuyerById(Guid? bid);
        BuyerModel CreateBuyer(BuyerModel buyer);
        BuyerModel UpdateBuyer(BuyerModel buyer);
        void DeleteBuyer(Guid bid);
        bool SaveChanges();
    }
}

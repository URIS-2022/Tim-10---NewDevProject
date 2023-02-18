using AutoMapper;
using Buyer.Entities;
using System;

namespace Buyer.Data
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly BuyerContext context;
        private readonly IMapper mapper;

        public static List<BuyerModel> buyers { get; set; } = new List<BuyerModel>();

        public BuyerRepository(BuyerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public BuyerModel CreateBuyer(BuyerModel buyer)
        {
            var newEntity = context.Add(buyer);
            return mapper.Map<BuyerModel>(newEntity.Entity);
        }

        public void DeleteBuyer(Guid bid)
        {
            var buyer = GetBuyerById(bid);
            context.Remove(buyer);
        }

        public List<BuyerModel> GetBuyer()
        {
            return buyers.ToList();
        }

        public BuyerModel GetBuyerById(Guid? bid)
        {
            return buyers.FirstOrDefault(e => e.buyerId == bid);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public BuyerModel UpdateBuyer(BuyerModel buyer)
        {
            throw new NotImplementedException();
        }

    }
}


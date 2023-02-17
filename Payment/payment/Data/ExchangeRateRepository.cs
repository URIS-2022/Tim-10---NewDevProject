using AutoMapper;
using payment.Entities;
using payment.Models;

namespace payment.Data
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly PaymentContext context;
        private readonly IMapper mapper;

        public ExchangeRateRepository(PaymentContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ExchangeRateConfirmationDto CreateExchangeRate(ExchangeRate exchangeRate)
        {
            exchangeRate.exchangeRateId = Guid.NewGuid();
            var noviEntitet = context.ExchangeRates.Add(exchangeRate);

            //return mapper.Map<KursnaListaConfirmationDto>(kursnaLista);
            return mapper.Map<ExchangeRateConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteExchangeRate(Guid exchangeRateId)
        {
            ExchangeRate rate = GetExchangeRateById(exchangeRateId);
            context.ExchangeRates.Remove(rate);
        }

        public List<ExchangeRate> GetExchangeRates()
        {
            return context.ExchangeRates.ToList();
        }

        public ExchangeRate GetExchangeRateById(Guid exchangeRateId)
        {
            return context.ExchangeRates.FirstOrDefault(k => k.exchangeRateId == exchangeRateId);
        }

        public ExchangeRateConfirmationDto UpdateExchangeRate(ExchangeRate exchangeRate)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }

    }
}

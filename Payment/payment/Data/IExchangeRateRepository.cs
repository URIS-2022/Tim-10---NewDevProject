using payment.Entities;
using payment.Models;

namespace payment.Data
{
    public interface IExchangeRateRepository
    {
        List<ExchangeRate> GetExchangeRates();

        ExchangeRate GetExchangeRateById(Guid exchangeRateId);

        ExchangeRateConfirmationDto CreateExchangeRate(ExchangeRate exchangeRate);

        ExchangeRateConfirmationDto UpdateExchangeRate(ExchangeRate exchangeRate);

        void DeleteExchangeRate(Guid exchangeRateId);

        bool SaveChanges();


    }
}

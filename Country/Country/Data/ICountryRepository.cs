using Country.Entities;

namespace Country.Data
{
    public interface ICountryRepository
    {
        List<Country1> GetCountryList();

        Country1 GetCountryById(Guid countryId);

        Country1 CreateCountry(Country1 country);

        void UpdateCountry(Country1 country);

        void DeleteCountry(Guid countryId);
        bool SaveChanges();
    }
}

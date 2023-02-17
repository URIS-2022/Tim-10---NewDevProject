using AutoMapper;
using Country.Entities;


namespace Country.Data
{
    public class CountryRepository : ICountryRepository
    {

        private readonly CountryContext context;
        private readonly IMapper mapper;

        public CountryRepository(CountryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Country1 CreateCountry(Country1 country)
        {
            var createdEntity = context.Add(country);
            context.SaveChanges();
            return mapper.Map<Country1>(createdEntity.Entity);
        }

        public void DeleteCountry(Guid countryId)
        {
            var country = GetCountryById(countryId);
            context.Remove(country);
            context.SaveChanges();
        }

        public Country1 GetCountryById(Guid countryId)
        {
            return context.Country1.FirstOrDefault(c => c.countryId == countryId);
        }

        public List<Country1> GetCountryList()
        {
            return context.Country1.ToList();
        }

        public void UpdateCountry(Country1 country)
        {
            //nije potrebno posebno implementirati update
        }

    }
}

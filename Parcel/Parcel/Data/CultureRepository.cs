using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class CultureRepository : ICultureRepository
    {
        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public CultureRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Culture CreateCulture(Culture culture)
        {
            var createdEntity = context.Add(culture);
            context.SaveChanges();
            return mapper.Map<Culture>(createdEntity.Entity);
        }

        public void DeleteCulture(Guid cultureId)
        {
            var culture = GetCultureById(cultureId);
            context.Remove(culture);
            context.SaveChanges();
        }
        public Culture GetCultureById(Guid cultureId)
        {
            return context.Culture.FirstOrDefault(c => c.cultureId == cultureId);
        }

        public List<Culture> GetCultureList()
        {
            return context.Culture.ToList();
        }

        public void UpdateCulture(Culture culture)
        {
            //nije potrebno posebno implementirati update
        }
    }
}

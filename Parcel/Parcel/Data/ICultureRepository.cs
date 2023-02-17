using Parcel.Entities;

namespace Parcel.Data
{
    public interface ICultureRepository
    {
            List<Culture> GetCultureList();
            Culture GetCultureById(Guid cultureId);
            Culture CreateCulture(Culture culture);

            void UpdateCulture(Culture culture);

            void DeleteCulture(Guid cultureId);
            bool SaveChanges();
    }
}

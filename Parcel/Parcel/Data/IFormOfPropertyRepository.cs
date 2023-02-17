using Parcel.Entities;

namespace Parcel.Data
{
    public interface IFormOfPropertyRepository
    {

        List<FormOfProperty> GetFormOfPropertyList();
        FormOfProperty GetFormOfPropertyById(Guid formOfPropertyId);
        FormOfProperty CreateFormOfProperty(FormOfProperty formOfProperty);

        void UpdateFormOfProperty(FormOfProperty formOfProperty);

        void DeleteFormOfProperty(Guid formOfPropertyId);
        bool SaveChanges();
    }
}

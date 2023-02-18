using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class FormOfPropertyRepository : IFormOfPropertyRepository
    {

        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public FormOfPropertyRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public FormOfProperty CreateFormOfProperty(FormOfProperty formOfProperty)
        { 
            var createdEntity = context.Add(formOfProperty);
            context.SaveChanges();
            return mapper.Map<FormOfProperty>(createdEntity.Entity);
        }

        public void DeleteFormOfProperty(Guid formOfPropertyId)
        {
            var formOfProperty = GetFormOfPropertyById(formOfPropertyId);
            context.Remove(formOfProperty);
            context.SaveChanges();
        }
        public FormOfProperty GetFormOfPropertyById(Guid formOfPropertyId)
        {
            return context.FormOfProperty.FirstOrDefault(o => o.formOfPropertyId == formOfPropertyId);
        }

        public List<FormOfProperty> GetFormOfPropertyList()
        {
            return context.FormOfProperty.ToList();
        }
        public void UpdateFormOfProperty(FormOfProperty formOfProperty)
        {
            //nije potrebno posebno implementirati update
        }
    }
}

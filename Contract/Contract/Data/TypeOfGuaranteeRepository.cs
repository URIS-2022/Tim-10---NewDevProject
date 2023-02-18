using AutoMapper;
using Contract.Entities;

namespace Contract.Data
{
    public class TypeOfGuaranteeRepository : ITypeOfGuaranteeRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public TypeOfGuaranteeRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public TypeOfGuaranteeEntity CreateGuarantee(TypeOfGuaranteeEntity guarantee)
        {
            var createdEntity = context.Add(guarantee);
            return mapper.Map<TypeOfGuaranteeEntity>(createdEntity.Entity);
        }

        public void DeleteGuarantee(Guid typeId)
        {
            var document = GetGuaranteeById(typeId);
            context.Remove(document);
        }

        public List<TypeOfGuaranteeEntity> GetGuarantees(string? type = null)
        {
            return context.TypeOfGuaranteeEntity.Where(e => (type == null)).ToList();
        }

        public TypeOfGuaranteeEntity GetGuaranteeById(Guid typeId)
        {
            return context.TypeOfGuaranteeEntity.FirstOrDefault(e => e.typeId == typeId);
        }


        public void UpdateGuarantee(TypeOfGuaranteeEntity guarantee)
        {
           //does not need to be implemented
        }


    }
}

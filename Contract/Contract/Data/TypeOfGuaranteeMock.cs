using Contract.Entities;

namespace Contract.Data
{
    public class TypeOfGuaranteeMock : ITypeOfGuaranteeRepository
    {
        public static List<TypeOfGuaranteeEntity> TypeOfGuarantee { get; set; } = new List<TypeOfGuaranteeEntity>();
        public TypeOfGuaranteeMock()
        {
            FillData();
        }

        private static void FillData()
        {
            TypeOfGuarantee.AddRange(new List<TypeOfGuaranteeEntity>
            {
                new TypeOfGuaranteeEntity
                {
                    typeId = Guid.Parse("06CFBBBF-39E1-485C-BD54-3CB336F25242"),
                   type = "Monthly"

                },
                new TypeOfGuaranteeEntity
                {
                    typeId = Guid.Parse("5E6F0201-B31A-4767-8087-910E3C91DCC4"),
                    type = "Quarterly"
                }
            });
        }

        public void DeleteGuarantee(Guid typeId)
        {
            TypeOfGuarantee.Remove(TypeOfGuarantee.FirstOrDefault(e => e.typeId == typeId));
        }

        public TypeOfGuaranteeEntity GetGuaranteeById(Guid typeId)
        {
            return TypeOfGuarantee.FirstOrDefault(e => e.typeId == typeId);
        }

        public List<TypeOfGuaranteeEntity> GetGuarantees(string? type = null)
        {
            return (from e in TypeOfGuarantee
                    where string.IsNullOrEmpty(type) || e.type == type
                    select e).ToList();
        }

        public void UpdateGuarantee(TypeOfGuaranteeEntity guarantee)
        {
            TypeOfGuaranteeEntity ugo = GetGuaranteeById(guarantee.typeId);

            ugo.typeId = guarantee.typeId;
            ugo.type = guarantee.type;

        }

        public TypeOfGuaranteeEntity CreateGuarantee(TypeOfGuaranteeEntity guarantee)
        {
            guarantee.typeId = Guid.NewGuid();
            TypeOfGuarantee.Add(guarantee);
            TypeOfGuaranteeEntity cont = GetGuaranteeById(guarantee.typeId);

            return new TypeOfGuaranteeEntity
            {
                typeId = cont.typeId,
                type = cont.type
            };
        }
        public bool SaveChanges()
        {
            return true;
        }
    }
}

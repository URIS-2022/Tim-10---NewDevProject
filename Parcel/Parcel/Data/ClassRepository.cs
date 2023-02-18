using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class ClassRepository : IClassRepository
    {

        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public ClassRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Class CreateClass(Class classs)
        {
            var createdEntity = context.Add(classs);
            context.SaveChanges();
            return mapper.Map<Class>(createdEntity.Entity);
        }

        public void DeleteClass(Guid classId)
        {
            var classs = GetClassById(classId);
            context.Remove(classs);
            context.SaveChanges();
        }
        public Class GetClassById(Guid classId)
        {
            return context.Class.FirstOrDefault(c => c.classId == classId);
        }

        public List<Class> GetClassList()
        {
            return context.Class.ToList();
        }
        public void UpdateClass(Class classs)
        {
            //nije potrebno posebno implementirati update
        }
    }
}

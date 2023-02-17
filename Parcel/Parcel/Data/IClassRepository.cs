using Parcel.Entities;

namespace Parcel.Data
{
    public interface IClassRepository
    {
        List<Class> GetClassList();
        Class GetClassById(Guid classId);
        Class CreateClass(Class classs);

        void UpdateClass(Class classs);

        void DeleteClass(Guid classId);
        bool SaveChanges();
    }
    
}

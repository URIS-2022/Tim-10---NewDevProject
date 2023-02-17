using complaint.Entities;
namespace complaint.Data
{
    public interface IComplaintTypeRepository
    {
        List<ComplaintType> GetTypeList();

        ComplaintType GetTypeById(Guid complaintTypeId);

        ComplaintType CreateType(ComplaintType complaintType);

        void UpdateType(ComplaintType complaintType);

        void DeleteType(Guid complaintTypeId);
        bool SaveChanges();
    }
}

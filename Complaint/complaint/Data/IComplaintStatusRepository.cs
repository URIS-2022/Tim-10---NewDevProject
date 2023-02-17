using complaint.Entities;
namespace complaint.Data
{
    public interface IComplaintStatusRepository
    {
        List<ComplaintStatus> GetStatusList();

        ComplaintStatus GetStatusById(Guid statusId);

        ComplaintStatus CreateStatus(ComplaintStatus complaintStatus);

        void UpdateStatus(ComplaintStatus complaintStatus);

        void DeleteStatus(Guid statusId);
        bool SaveChanges();

    }
}

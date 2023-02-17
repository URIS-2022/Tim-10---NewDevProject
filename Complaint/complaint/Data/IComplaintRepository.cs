using complaint.Entities;
using complaint.Models;
namespace complaint.Data
{
    public interface IComplaintRepository
    {
        List<Complaint> GetAllComplaints();
        Complaint GetComplaintById(Guid complaintId);
        Complaint CreateComplaint(Complaint complaint);
        void UpdateComplaint(Complaint complaint);
        void DeleteComplaint(Guid complaintId);
        bool SaveChanges();


    }
}

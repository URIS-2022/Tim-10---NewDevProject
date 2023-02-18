using complaint.Entities;
using complaint.Models;
using AutoMapper;

namespace complaint.Data
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Complaint CreateComplaint(Complaint complaint)
        {
            var createdEntity = context.Add(complaint);
            context.SaveChanges();
            return mapper.Map<Complaint>(createdEntity.Entity);
        }

        public void DeleteComplaint(Guid complaintId)
        {
            var complaint = GetComplaintById(complaintId);
            context.Remove(complaint);
            context.SaveChanges();
        }

        public List<Complaint> GetAllComplaints()
        {
            return context.Complaint.ToList();
        }

        public Complaint GetComplaintById(Guid complaintId)
        {
            return context.Complaint.FirstOrDefault(z => z.complaintId == complaintId);
        }

        public void UpdateComplaint(Complaint complaint)
        {
            //Entity framework core prati entitet pa nema potrebe za implementacijom
        }




    }
}

using AutoMapper;
using complaint.Entities;
namespace complaint.Data
{
    public class ComplaintStatusRepository : IComplaintStatusRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;
        public  ComplaintStatusRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ComplaintStatus CreateStatus(ComplaintStatus complaintStatus)
        {
            var createdEntity = context.Add(complaintStatus);
            context.SaveChanges();
            return mapper.Map<ComplaintStatus>(createdEntity.Entity);
        }

        public void DeleteStatus(Guid statusId)
        {
            var complaintStatus = GetStatusById(statusId);
            context.Remove(complaintStatus);
            context.SaveChanges();
        }

        public ComplaintStatus GetStatusById(Guid statusId)
        {
            return context.ComplaintStatus.FirstOrDefault(d => d.complaintStatusId == statusId);
        }

        public List<ComplaintStatus> GetStatusList()
        {
            return context.ComplaintStatus.ToList();
        }

        public void UpdateStatus(ComplaintStatus complaintStatus)
        {
            //nije potrebno posebno implementirati update
        }
    }



}

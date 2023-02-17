using AutoMapper;
using complaint.Entities;



namespace complaint.Data
{
    public class ComplaintTypeRepository : IComplaintTypeRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;
        public ComplaintTypeRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ComplaintType CreateType(ComplaintType complaintType)
        {
            var createdEntity = context.Add(complaintType);
            context.SaveChanges();
            return mapper.Map<ComplaintType>(createdEntity.Entity);
        }

        public void DeleteType(Guid complaintTypeId)
        {
            var complaintType = GetTypeById(complaintTypeId);
            context.Remove(complaintType);
            context.SaveChanges();
        }

        public ComplaintType GetTypeById(Guid complaintTypeId)
        {
            return context.ComplaintType.FirstOrDefault(d => d.complaintTypeId == complaintTypeId);
        }

        public List<ComplaintType> GetTypeList()
        {
            return context.ComplaintType.ToList();
        }

        public void UpdateType(ComplaintType complaintType)
        {
            //nije potrebno posebno implementirati update
        }


    }
}

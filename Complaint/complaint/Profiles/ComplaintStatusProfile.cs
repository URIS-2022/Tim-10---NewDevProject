using AutoMapper;
using complaint.Entities;
using complaint.Models;

namespace complaint.Profiles
{
    public class ComplaintStatusProfile : Profile
    {
        public ComplaintStatusProfile()
        {
            CreateMap<ComplaintStatus, ComplaintStatusDto>();
            CreateMap<ComplaintStatusDto, ComplaintStatus>();
            CreateMap<ComplaintStatus, ComplaintStatus>();
        }


    }
}

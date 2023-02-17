using AutoMapper;
using complaint.Entities;
using complaint.Models;

namespace complaint.Profiles
{
    public class ComplaintProfile : Profile
    {


        public ComplaintProfile()
        {
            CreateMap<Complaint, ComplaintDto>();
            CreateMap<ComplaintDto, Complaint>();
        
            CreateMap<Complaint, Complaint>();
        }


    }
}

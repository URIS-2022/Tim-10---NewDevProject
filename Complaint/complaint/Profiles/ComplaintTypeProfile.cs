using AutoMapper;
using complaint.Entities;
using complaint.Models;


namespace complaint.Profiles
{
    public class ComplaintTypeProfile : Profile
    {
        public ComplaintTypeProfile()
        {
            CreateMap<ComplaintType, ComplaintTypeDto>();
            CreateMap<ComplaintTypeDto, ComplaintType>();
            CreateMap<ComplaintType, ComplaintType>();
        }

    }
}

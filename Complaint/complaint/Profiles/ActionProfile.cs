using AutoMapper;
using complaint.Entities;
using complaint.Models;


namespace complaint.Profiles
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Entities.Action, ActionDto>();
            CreateMap<ActionDto, Entities.Action>();
            CreateMap<Entities.Action, Entities.Action>();
        }

    }
}

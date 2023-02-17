using payment.Models;
using AutoMapper;

namespace payment.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Entities.Payment, PaymentDto>(); //prvo izvor pa destinacija
            CreateMap<PaymentCreationDto, Entities.Payment>(); //koristi se u post metodi u kontroleru
            CreateMap<PaymentUpdateDto, Entities.Payment>(); //koristi se u put metodi u kontroleru
            CreateMap<Entities.Payment, Entities.Payment>(); //koristi se u kontroleru
            CreateMap<Entities.Payment, PaymentConfirmationDto>(); //koristi se u kontroleru
            CreateMap<PaymentConfirmationDto, PaymentConfirmationDto>(); //koristi se u kontroleru
        }


    }
}

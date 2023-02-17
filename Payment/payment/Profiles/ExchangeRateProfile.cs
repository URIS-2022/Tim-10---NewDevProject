using payment.Entities;
using payment.Models;
using AutoMapper;

namespace payment.Profiles
{
    public class ExchangeRateProfile : Profile
    {
        public ExchangeRateProfile()
        {
            CreateMap<ExchangeRate, ExchangeRateDto>(); //prvo izvor pa destinacija
            CreateMap<ExchangeRateCreationDto, ExchangeRate>(); //koristi se u post metodi u kontroleru
            CreateMap<ExchangeRateUpdateDto, ExchangeRate>(); //koristi se u put metodi u kontroleru
            CreateMap<ExchangeRate, ExchangeRate>(); //koristi se u kontroleru
            CreateMap<ExchangeRate, ExchangeRateConfirmationDto>(); //koristi se u kontroleru
            CreateMap<ExchangeRateConfirmationDto, ExchangeRateConfirmationDto>(); //koristi se u kontroleru
        }


    }
}

using Account.Api.Entities;
using Account.Api.Models;
using AutoMapper;

namespace Account.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountSummaryEntity, AccountResult>()
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(o => o.CustomerNumber))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(o => o.AccountNumber))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Currency))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.CustomerNumber > 0));
                
            
            CreateMap<AccountResult, AccountResultModel>()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(o => o.AccountNumber))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(o => o.CustomerNumber))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Currency))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.IsSuccessful)); 
            
        }
    }
}
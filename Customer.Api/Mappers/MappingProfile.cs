using AutoMapper;
using Customer.Api.Entities;
using Customer.Api.Models;

namespace Customer.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerResult, CustomerResultModel>()
                .ForAllMembers(opts => opts.Ignore());
            
            CreateMap<CustomerEntity, CustomerResult>()
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(o => o.CustomerNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(o => o.Surname))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(o => o.Username))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.CustomerNumber > 0));
            
            CreateMap<CustomerResult, CustomerResultModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(o => o.Surname))
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(o => o.CustomerNumber))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(o => o.Username))
                .ForMember(dest => dest.IsSuccessful, opt => opt.MapFrom(o => o.IsSuccessful)); 


        }
        
    }
}
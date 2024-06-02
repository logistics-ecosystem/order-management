using AutoMapper;
using Logistics.Models;
using Logistics.Protos;

namespace Logistics.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Available, Order>()
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.AddressFrom, opt => opt.MapFrom(src => src.AddressFrom))
                .ForMember(dest => dest.AddressTo, opt => opt.MapFrom(src => src.AddressTo))
                .ForMember(dest => dest.DateTimeFrom, opt => opt.MapFrom(src => src.DateTimeFrom))
                .ForMember(dest => dest.DateTimeTo, opt => opt.MapFrom(src => src.DateTimeTo))
                .ForMember(dest => dest.FrachtType, opt => opt.MapFrom(src => src.FrachtType))
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
                .ForMember(dest => dest.TrunkType, opt => opt.MapFrom(src => src.TrunkType))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.LoadingMetre, opt => opt.MapFrom(src => src.LoadingMetre))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.LoadingType, opt => opt.MapFrom(src => src.LoadingType))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));

            CreateMap<Order, OrderInfoResponse>()                
                .ForMember(dest => dest.AddressFrom, opt => opt.MapFrom(src => src.AddressFrom))
                .ForMember(dest => dest.AddressTo, opt => opt.MapFrom(src => src.AddressTo))
                .ForMember(dest => dest.DateTimeFrom, opt => opt.MapFrom(src => src.DateTimeFrom))
                .ForMember(dest => dest.DateTimeTo, opt => opt.MapFrom(src => src.DateTimeTo))
                .ForMember(dest => dest.FrachtType, opt => opt.MapFrom(src => src.FrachtType))
                .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
                .ForMember(dest => dest.TrunkType, opt => opt.MapFrom(src => src.TrunkType))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.LoadingMetre, opt => opt.MapFrom(src => src.LoadingMetre))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.LoadingType, opt => opt.MapFrom(src => src.LoadingType))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));
        }
    }
}

using AutoMapper;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Models;
using System.Data;

namespace DataHarbor.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProcessRequest, Declaration>()
                .ForMember(dest => dest.RawData, opt => opt.MapFrom(src => src.RawData.ToDictionaryList()))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDictionaryList()));

            CreateMap<Declaration, ProcessRequest>()
                .ForMember(dest => dest.RawData, opt => opt.MapFrom(src => src.RawData.ToDataTable()))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDataTable()));

            CreateMap<ProcessResult, Account>()
               .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDictionaryList()));

            CreateMap<ProcessMessage, Anchored>()
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.RecievedOn, opt => opt.MapFrom(src => src.RecievedOn))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ProcessMessage, Docked>()
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.RecievedOn, opt => opt.MapFrom(src => src.RecievedOn))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ProcessMessage, Adrifted>()
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.RecievedOn, opt => opt.MapFrom(src => src.RecievedOn))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}

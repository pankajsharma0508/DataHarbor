using AutoMapper;
using DataHarbor.Common.Messaging;
using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Models;

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

            CreateMap<Account, ProcessResult>()
               .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDataTable()));

            CreateMap<ProcessMessage, Anchored>()
                .ForMember(dest => dest.DeclarationId, opt => opt.MapFrom(src => src.DeclarationId));

            CreateMap<ProcessMessage, Docked>()
                .ForMember(dest => dest.DeclarationId, opt => opt.MapFrom(src => src.DeclarationId));

            CreateMap<ProcessMessage, Adrifted>()
                .ForMember(dest => dest.DeclarationId, opt => opt.MapFrom(src => src.DeclarationId));
        }
    }
}

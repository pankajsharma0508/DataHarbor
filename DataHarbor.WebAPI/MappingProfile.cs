using AutoMapper;
using DataHarbor.Common.Models;
using DataHarbor.WebAPI.Models;
using System.Data;

namespace DataHarbor.WebAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ProcessRequest, Declaration>()
                .ForMember(dest => dest.RawData, opt => opt.MapFrom(src => src.RawData.ToDictionaryList()))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDictionaryList()));
       
            CreateMap<Declaration, ProcessRequest>()
                .ForMember(dest => dest.RawData, opt => opt.MapFrom(src => src.RawData.ToDataTable()))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.ToDataTable()));
        }


    }
}

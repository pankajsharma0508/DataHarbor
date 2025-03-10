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
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.RawData.ToDictionaryList()));

            CreateMap<Declaration, ProcessRequest>();
        }

        
    }
}

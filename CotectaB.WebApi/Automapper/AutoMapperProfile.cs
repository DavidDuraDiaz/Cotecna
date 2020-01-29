using AutoMapper;
using CotecnaB.Core.DTOs;
using CotecnaB.Core.Entities;
using System.Linq;

namespace CotecnaB.Services.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inspector, InspectorDTO>().ReverseMap();
            CreateMap<Inspection, InspectionDTO>()
                .ForMember(dto => dto.Inspectors, opt => opt.MapFrom(x => x.InspectionInspector.Select(y => y.Inspector).ToList()))
                .ForMember(dto => dto.InspectionDate, opt => opt.MapFrom(x => x.InspectionInspector.Select(y => y.InspectionDate).FirstOrDefault())).ReverseMap();

        }
    }
}

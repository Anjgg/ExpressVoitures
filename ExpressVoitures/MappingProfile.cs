using AutoMapper;
using ExpressVoitures.Dto;
using ExpressVoitures.Models;

namespace ExpressVoitures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VoitureDto, VoitureModel>().ReverseMap();
            CreateMap<ReparationDto, ReparationModel>().ReverseMap();
            CreateMap<PrixDto, PrixModel>().ReverseMap();
            CreateMap<DateDto, DateModel>().ReverseMap();
            CreateMap<TypeDto, TypeModel>().ReverseMap();
        }
    }
}

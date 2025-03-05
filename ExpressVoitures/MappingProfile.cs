using AutoMapper;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;

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

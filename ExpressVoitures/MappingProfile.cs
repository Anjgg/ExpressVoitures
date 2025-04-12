using AutoMapper;
using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Dto;
using ExpressVoitures.Data.Models;

namespace ExpressVoitures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<List<Car>, List<CarDto>>().ReverseMap();
            CreateMap<EventHistory, EventHistoryDto>().ReverseMap();
            CreateMap<Price, PriceDto>().ReverseMap();
            CreateMap<Repair, RepairDto>().ReverseMap();
        }
    }
}

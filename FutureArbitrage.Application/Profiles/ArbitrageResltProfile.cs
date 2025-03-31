using AutoMapper;
using FutureArbitrage.Application.Models.Response;
using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Application.Profiles
{
    public class ArbitrageResltProfile : Profile
    {
        public ArbitrageResltProfile()
        {
            CreateMap<ArbitrageResult, GetArbitrageResults>()
            .ForMember(dest => dest.Symbol1, opt => opt.MapFrom(src => src.FuturesContract1.Symbol))
            .ForMember(dest => dest.Symbol2, opt => opt.MapFrom(src => src.FuturesContract2.Symbol));
        }
    }
}

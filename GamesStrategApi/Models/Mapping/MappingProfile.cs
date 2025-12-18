using AutoMapper;
using GamesStrategApi.Models.DTOss;
using GamesStrategApi.Models.Request;

namespace GamesStrategApi.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Race маппинг
            CreateMap<Race, RaceDto>();
            CreateMap<CreateRaceRequest, Race>();
            CreateMap<UpdateRaceRequest, Race>();

            // Tech маппинг
            CreateMap<Tech, TechDto>();
            CreateMap<CreateTechRequest, Tech>();
            CreateMap<UpdateTechRequest, Tech>();

            // CelestialBody маппинг
            CreateMap<CelestialBody, CelestialBodyDto>();
            CreateMap<CreateCelestialBodyRequest, CelestialBody>();
            CreateMap<UpdateCelestialBodyRequest, CelestialBody>();

            // Unit маппинг
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateUnitRequest, Unit>();
            CreateMap<UpdateUnitRequest, Unit>();

            // Building маппинг
            CreateMap<Building, BuildDto>();
            CreateMap<CreateBuildRequest, Building>();
            CreateMap<UpdateBuildRequest, Building>();
        }
    }
}


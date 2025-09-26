using perla_metro_stations_service.src.Dto;
using perla_metro_stations_service.src.Model;

namespace perla_metro_stations_service.src.Mapper
{
    public static class StationMapper
    {
        // From Station Model -> to Get Dto
        public static GetStation ToGetStation(this Station stationModel)
        {
            return new GetStation
            {
                Id = stationModel.Id,
                Name = stationModel.Name,
                Address = stationModel.Address,
                Type = stationModel.Type,
                IsActive = stationModel.IsActive
            };
        }

        // From Post Dto -> to Station Model
        public static Station ToStationModel(this CreateStation createStationDto)
        {
            return new Station
            {
                Id = Guid.NewGuid(),
                Name = createStationDto.Name,
                Address = createStationDto.Address,
                Type = createStationDto.Type,
                IsActive = true
            };
        }
        // From Update Dto -> to Station Modelo
        public static Station ToStationModel(this UpdateStation updateStationDto, Station existingStation)
        {
            existingStation.Name = updateStationDto?.Name ?? existingStation.Name;
            existingStation.Address = updateStationDto?.Address ?? existingStation.Address;
            existingStation.Type = updateStationDto?.Type ?? existingStation.Type;
            return existingStation;
        }
    }
}
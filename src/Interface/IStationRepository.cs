using perla_metro_stations_service.src.Dto;
using perla_metro_stations_service.src.Helpers;
using perla_metro_stations_service.src.Model;

namespace perla_metro_stations_service.src.Interface
{
    public interface IStationRepository
    {
        Task<(List<Station>, int)> GetAll(StationQuery query);
        Task<Station?> GetById(Guid id);
        Task<Station> Create(Station station);
        Task<Station?> Update(Guid id, Station station);
        Task<bool> Delete(Guid id);
        Task<bool> Exists(string address);
    }
}
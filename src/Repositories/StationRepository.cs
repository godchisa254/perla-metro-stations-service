using Microsoft.EntityFrameworkCore;
using perla_metro_stations_service.src.Data;
using perla_metro_stations_service.src.Helpers;
using perla_metro_stations_service.src.Interface;
using perla_metro_stations_service.src.Model;

namespace perla_metro_stations_service.src.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly ApplicationDBContext _context;
        public StationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(List<Station>, int)> GetAll(StationQuery query)
        {
            var pageNumber = query.PageNumber > 0 ? query.PageNumber : 1;
            var pageSize = query.PageSize > 0 ? query.PageSize : 10;
            var stations = _context.Stations.AsQueryable();

            if (!string.IsNullOrEmpty(query.StationType))
            {
                stations = stations.Where(s => s.Type == query.StationType);
            }

            if (query.StationIsActive.HasValue)
            {
                stations = stations.Where(s => s.IsActive == query.StationIsActive.Value);
            }

            int totalCount = await stations.CountAsync();

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    stations = query.IsDescending ? stations.OrderByDescending(s => s.Name) : stations.OrderBy(s => s.Name);
                }
                else if (query.SortBy.Equals("Type", StringComparison.OrdinalIgnoreCase))
                {
                    stations = query.IsDescending ? stations.OrderByDescending(s => s.Type) : stations.OrderBy(s => s.Type);
                }
            }
            else
            {
                stations = stations.OrderBy(s => s.Name);
            }

            var skipNumber = (pageNumber - 1) * pageSize;
            var stationsModels = await stations.Skip(skipNumber).Take(pageSize).ToListAsync();
            return (stationsModels, totalCount);
        }

        public async Task<Station?> GetById(Guid id)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(s => s.Id == id);

            if (station == null)
            {
                throw new KeyNotFoundException($"Station with Id {id} not found.");
            }

            return station;
        }

        public async Task<Station> Create(Station station)
        {
            await _context.Stations.AddAsync(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<Station?> Update(Guid id, Station station)
        {
            var existingStation = _context.Stations.FirstOrDefault(s => s.Id == id);
            if (existingStation == null)
            {
                throw new KeyNotFoundException($"Station with Id {id} not found.");
            }

            existingStation.Name = station.Name ?? existingStation.Name;
            existingStation.Address = station.Address ?? existingStation.Address;
            existingStation.Type = station.Type ?? existingStation.Type;
            await _context.SaveChangesAsync();

            return existingStation;
        }

        public async Task<bool> Delete(Guid id)
        {
            var existingStation = _context.Stations.FirstOrDefault(s => s.Id == id);
            if (existingStation == null)
            {
                throw new KeyNotFoundException($"Station with Id {id} not found.");
            }
            existingStation.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> Exists(string address)
        {
            return _context.Stations.AnyAsync(s => s.Address == address);
        }

    }
}
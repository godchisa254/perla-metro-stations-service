using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using perla_metro_stations_service.src.Dto;
using perla_metro_stations_service.src.Helpers;
using perla_metro_stations_service.src.Interface;
using perla_metro_stations_service.src.Mapper;

namespace perla_metro_stations_service.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;
        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] StationQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                var validSortProperties = new[] { "Type", "IsActive" };
                if (!validSortProperties.Contains(query.SortBy))
                {
                    return BadRequest($"Invalid SortBy property: {query.SortBy}. Use one of the following: {string.Join(", ", validSortProperties)}");
                }
            }

            var (stations, totalCount) = await _stationRepository.GetAll(query);
            var stationDtos = stations.Select(s => s.ToGetStation()).ToList();
            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);

            var response = new
            {
                Items = stationDtos,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var station = await _stationRepository.GetById(id);
            if (station == null)
            {
                return NotFound();
            }
            var stationDto = station.ToGetStation();

            return Ok(stationDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStation createStation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var station = createStation.ToStationModel();
            bool exists = await _stationRepository.Exists(station.Address);
            if (exists)
            {
                return Conflict("A station with the same address already exists.");
            }
            var createdStation = await _stationRepository.Create(station);
            var stationDto = createdStation.ToGetStation();

            return CreatedAtAction(nameof(GetById), new { id = stationDto.Id }, stationDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStation updateStation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStation = await _stationRepository.GetById(id);
            if (existingStation == null)
            {
                return NotFound();
            }

            var updatedStationModel = updateStation.ToStationModel(existingStation);
            var updatedStation = await _stationRepository.Update(id, updatedStationModel);
            if (updatedStation == null)
            {
                return NotFound();
            }

            var stationDto = updatedStation.ToGetStation();
            return Ok(stationDto);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStation = await _stationRepository.GetById(id);
            if (existingStation == null)
            {
                return NotFound();
            }

            await _stationRepository.Delete(id);
            return NoContent();
        }
    }
}

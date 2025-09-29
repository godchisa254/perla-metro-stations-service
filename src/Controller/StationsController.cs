using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using perla_metro_stations_service.src.Dto;
using perla_metro_stations_service.src.Helpers;
using perla_metro_stations_service.src.Interface;
using perla_metro_stations_service.src.Mapper;

namespace perla_metro_stations_service.src.Controller
{
    /// <summary>
    /// Controller for managing metro stations operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;
        /// <summary>
        /// Initializes a new instance of the StationsController
        /// </summary>
        /// <param name="stationRepository">The station repository for data access</param>
        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        /// <summary>
        /// Retrieves all stations with pagination, filtering, and sorting
        /// </summary>
        /// <param name="query">Query parameters for pagination, filtering, and sorting</param>
        /// <returns>A paginated list of stations</returns>
        /// <response code="200">Returns the paginated list of stations</response>
        /// <response code="400">If the query parameters are invalid</response>
        [HttpGet]
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

        /// <summary>
        /// Retrieves a specific station by its unique identifier
        /// </summary>
        /// <param name="id">The station GUID</param>
        /// <returns>The station details</returns>
        /// <response code="200">Returns the requested station</response>
        /// <response code="400">If the ID format is invalid</response>
        /// <response code="404">If the station is not found</response>
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

        /// <summary>
        /// Creates a new metro station
        /// </summary>
        /// <param name="createStation">The station creation data</param>
        /// <returns>The newly created station</returns>
        /// <response code="201">Returns the newly created station</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="409">If a station with the same address already exists</response>
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

        /// <summary>
        /// Updates an existing station
        /// </summary>
        /// <param name="id">The station GUID to update</param>
        /// <param name="updateStation">The station update data</param>
        /// <returns>The updated station</returns>
        /// <response code="200">Returns the updated station</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If the station is not found</response>
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

        /// <summary>
        /// Deletes a station
        /// </summary>
        /// <param name="id">The station GUID to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the station was successfully deleted</response>
        /// <response code="400">If the ID format is invalid</response>
        /// <response code="404">If the station is not found</response>
        [HttpDelete("{id:guid}")]
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

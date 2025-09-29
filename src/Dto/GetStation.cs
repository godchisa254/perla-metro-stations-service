namespace perla_metro_stations_service.src.Dto
{
    /// <summary>
    /// Represents a station returned from the database or API.
    /// Contains the unique identifier and basic station details.
    /// </summary>
    public class GetStation
    {
        /// <summary>
        /// Gets or sets the unique identifier of the station.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the station.
        /// Default is an empty string.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the address of the station.
        /// Default is an empty string.
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the type of the station.
        /// Default is an empty string.
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets a value indicating whether the station is active.
        /// Defaults to true.
        /// </summary>
        public bool IsActive { get; set; } = true;

    }
}
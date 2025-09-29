using System.ComponentModel.DataAnnotations;

namespace perla_metro_stations_service.src.Dto
{
    /// <summary>
    /// Represents the data required to create a new station.
    /// </summary>
    public class CreateStation
    {
        /// <summary>
        /// Gets or sets the name of the station.
        /// This field is required and must be between 3 and 100 characters long.
        /// </summary>
        [Required(ErrorMessage = "Station name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the station.
        /// This field is required and must be between 3 and 100 characters long.
        /// </summary>
        [Required(ErrorMessage = "Station address is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 100 characters.")]
        public required string Address { get; set; }


        /// <summary>
        /// Gets or sets the type of the station.
        /// This field is required and must be one of the following values: "Origen", "Destino", or "Intermedio".
        /// </summary>
        [Required(ErrorMessage = "Station type is required.")]
        [RegularExpression("^(Origen|Destino|Intermedio)$", ErrorMessage = "Type must be 'Origen', 'Destino' or 'Intermedio'.")]
        public required string Type { get; set; }

    }
}
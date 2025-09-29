using System.ComponentModel.DataAnnotations;

namespace perla_metro_stations_service.src.Dto
{
    /// <summary>
    /// Data Transfer Object for updating existing metro station information
    /// </summary>
    /// <remarks>
    /// This DTO is used for partial updates (PATCH-like behavior). 
    /// Only include the properties that need to be updated.
    /// </remarks>
    /// <example>
    /// Example request to update only the station name:
    /// <code>
    /// {
    ///     "name": "Updated Station Name"
    /// }
    /// </code>
    /// </example>
    public class UpdateStation
    {
        /// <summary>
        /// Gets or sets the updated display name of the metro station
        /// </summary>
        /// <value>
        /// A string containing the station name, must be between 3 and 100 characters
        /// </value>
        /// <example>"Estación Central Actualizada"</example>
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the updated physical location address of the station
        /// </summary>
        /// <value>
        /// A string containing the full address, maximum 100 characters
        /// </value>
        /// <example>"Avenida Principal 456, Colonia Centro"</example>
        [StringLength(100, ErrorMessage = "El nombre de la dirección no puede superar los 100 caracteres.")]
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the updated station type in the metro system
        /// </summary>
        /// <value>
        /// Must be one of the predefined values: Origen (Origin), Destino (Destination), or Intermedio (Intermediate)
        /// </value>
        /// <example>"Intermedio</example>
        [RegularExpression("^(Origen|Destino|Intermedio)$", ErrorMessage = "El tipo debe ser 'Origen', 'Destino' o 'Intermedio'.")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the updated operational status of the station
        /// </summary>
        /// <value>
        /// True if the station is active and operational, false if temporarily closed or inactive
        /// </value>
        /// <example>true</example>
        public bool? IsActive { get; set; }

    }
}
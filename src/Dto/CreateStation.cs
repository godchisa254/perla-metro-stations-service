using System.ComponentModel.DataAnnotations;

namespace perla_metro_stations_service.src.Dto
{
    public class CreateStation
    {
        [Required(ErrorMessage = "Station name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Station address is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 100 characters.")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "Station type is required.")]
        [RegularExpression("^(Origen|Destino|Intermedio)$", ErrorMessage = "Type must be 'Origen', 'Destino' or 'Intermedio'.")]
        public required string Type { get; set; }

    }
}
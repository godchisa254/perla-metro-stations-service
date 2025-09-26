using System.ComponentModel.DataAnnotations;

namespace perla_metro_stations_service.src.Dto
{
    public class UpdateStation
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "El nombre de la dirección no puede superar los 100 caracteres.")]
        public string? Address { get; set; }

        [RegularExpression("^(Origen|Destino|Intermedio)$", ErrorMessage = "El tipo debe ser 'Origen', 'Destino' o 'Intermedio'.")]
        public string? Type { get; set; }

        public bool? IsActive { get; set; }

    }
}
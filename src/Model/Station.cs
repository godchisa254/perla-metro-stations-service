using System.ComponentModel.DataAnnotations;

namespace perla_metro_stations_service.src.Model
{
    public class Station
    {
        [Key]
        public Guid Id { get; set; }                      // UUID V4
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Origen, Destino, Intermedio
        public bool IsActive { get; set; } = true;       // Soft delete flag

    }

}
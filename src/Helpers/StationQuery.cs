namespace perla_metro_stations_service.src.Helpers
{
    public class StationQuery : BaseQuery
    {
        public string? StationType { get; set; }
        public bool? StationIsActive { get; set; }
    }
}
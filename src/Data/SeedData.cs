using perla_metro_stations_service.src.Model;

namespace perla_metro_stations_service.src.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDBContext context)
        {
            if (context.Stations.Any())
                return;

            context.Stations.AddRange(
                new Station
                {
                    Id = Guid.NewGuid(),
                    Name = "Estación Central",
                    Address = "Av. Alameda 1111",
                    Type = "Origen",
                    IsActive = true
                },
                new Station
                {
                    Id = Guid.NewGuid(),
                    Name = "Estación Norte",
                    Address = "Av. Independencia 2020",
                    Type = "Destino",
                    IsActive = true
                },
                new Station
                {
                    Id = Guid.NewGuid(),
                    Name = "Estación Sur",
                    Address = "Av. O'Higgins 3030",
                    Type = "Intermedio",
                    IsActive = true
                }
            );

            context.SaveChanges();
        }

    }
}
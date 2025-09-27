using perla_metro_stations_service.src.Data;

namespace perla_metro_stations_service.src.Extensions
{
    public static class SeederExtensions
    {
        public static IApplicationBuilder UseSeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDBContext>();
                context.Database.EnsureCreated();
                SeedData.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            return app;
        }
    }
}
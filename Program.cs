using perla_metro_stations_service.src.Data;
using perla_metro_stations_service.src.Interface;
using perla_metro_stations_service.src.Repositories;
using perla_metro_stations_service.src.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration); 
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSeedData(); 
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
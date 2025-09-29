using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using perla_metro_stations_service.src.Model;

namespace perla_metro_stations_service.src.Data
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the Perla Metro Stations service.
    /// Manages the database connection and provides access to the <see cref="Station"/> entities.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDBContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext, such as connection string and provider configuration.</param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Station}"/> representing the Stations table in the database.
        /// Enables querying and saving instances of <see cref="Station"/>.
        /// </summary>
        public DbSet<Station> Stations { get; set; } = null!;
    }
}
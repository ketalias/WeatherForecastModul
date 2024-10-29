using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecastModul
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=WeatherDB;Username=postgres;Password=07042006");
        }
    }

}

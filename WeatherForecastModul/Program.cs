using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecastModul
{
 
      class Program
        {
            static void Main(string[] args)
            {
                using (var context = new WeatherContext())
                {
                    context.Database.EnsureCreated();

                    
                        var data = new List<WeatherForecast>
                    {
                        new WeatherForecast { Date = 20230101, Temperature = -5, PrecipitationLevel = 0, WindSpeed = 10 },
                        new WeatherForecast { Date = 20230102, Temperature = -10, PrecipitationLevel = 5, WindSpeed = 15 },
                        new WeatherForecast { Date = 20230101, Temperature = 0, PrecipitationLevel = 0, WindSpeed = 10 },
                        new WeatherForecast { Date = 20230102, Temperature = -1, PrecipitationLevel = 10, WindSpeed = 55 },
                    };
                        context.WeatherForecasts.AddRange(data);
                        context.SaveChanges();

                    var startDate = 20230101;
                    var endDate = 20231231;

                    var dataForPeriod = context.WeatherForecasts
                        .Where(w => w.Date >= startDate && w.Date <= endDate)
                        .ToList(); 

                    if (dataForPeriod.Any()) 
                    {
                        var averageTemperature = dataForPeriod.Average(w => w.Temperature);
                        var totalPrecipitation = dataForPeriod.Sum(w => w.PrecipitationLevel);

                        Console.WriteLine($"Середня температура: {averageTemperature}");
                        Console.WriteLine($"Загальний рівень опадів: {totalPrecipitation}");
                    }
                    else
                    {
                        Console.WriteLine("Немає даних для заданого періоду.");
                    }

                    ExportToXml(context.WeatherForecasts.ToList());
                }
            }

            static void ExportToXml(List<WeatherForecast> forecasts)
            {
                string filePath = "C:\\Users\\Леся\\source\\repos\\WeatherForecastModul\\WeatherForecastModul\\WeatherForecasts.xml";

                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "<WeatherForecasts></WeatherForecasts>");
                }

                var serializer = new XmlSerializer(typeof(List<WeatherForecast>));

                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, forecasts);
                }

                Console.WriteLine("Дані успішно збережені у WeatherForecasts.xml");
            }
    }
}
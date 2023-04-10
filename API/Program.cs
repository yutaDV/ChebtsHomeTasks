﻿using System.Text.Json;

namespace API;
public class Coord
{
    public double? lat { get; set; }
    public double? lon { get; set; }
}

public class City
{
    public int? id { get; set; }
    public string? name { get; set; }
    public Coord? coord { get; set; }
    public string? country { get; set; }
    public int? population { get; set; }
    public int? timezone { get; set; }
    public int? sunrise { get; set; }
    public int? sunset { get; set; }
}

public class WeatherForecast
{
    public string? cod { get; set; }
    public int? message { get; set; }
    public int? cnt { get; set; }
    public City? city { get; set; }
}

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string url = "https://api.openweathermap.org/data/2.5/forecast?appid=143f8bc8e5fbe961c1054045a441bbe0&q=Cherkasy&cnt=5";

    async static Task Main(string[] args)
    {
        await getWeather();
        Console.WriteLine("Hello, World!");

        async Task getWeather()
        {
            Console.WriteLine("Getting JSON...");
            var responseString = await client.GetStringAsync(url);
            Console.WriteLine("Parsing JSON...");
            WeatherForecast? weatherForecast =
               JsonSerializer.Deserialize<WeatherForecast>(responseString);
            Console.WriteLine($"cod: {weatherForecast?.cod}");
            Console.WriteLine($"City: {weatherForecast?.city?.name}");
        }
    }
}


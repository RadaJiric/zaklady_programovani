
using System.Text.Json;

namespace Homework_Lecture12;

class Program
{
    private const string ApiKey = "a5057b9ed5474531b6c213148250501";
    private const string BaseUrl = "http://api.weatherapi.com/v1/current.json";

    static async Task Main(string[] args)
    {
        Console.Write("Zadejte název města: ");
        string city = Console.ReadLine();

        try
        {
            WeatherResponse weather = await GetWeatherAsync(city);
            if (weather != null)
            {
                DisplayWeather(weather);
            }
            else
            {
                Console.WriteLine("Nepodařilo se načíst počasí.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Chyba HTTP: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Neočekávaná chyba: {ex.Message}");
        }
    }

    private static async Task<WeatherResponse> GetWeatherAsync(string city)
    {
        using HttpClient client = new HttpClient();
        string url = $"{BaseUrl}?key={ApiKey}&q={city}&aqi=no";

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("API Response:");
        Console.WriteLine(responseBody);

        WeatherResponse weather = JsonSerializer.Deserialize<WeatherResponse>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return weather;
    }

    private static void DisplayWeather(WeatherResponse weather)
    {
        if (weather?.Location == null || weather.Current == null)
        {
            Console.WriteLine("Incomplete weather data.");
            return;
        }

        Console.WriteLine("\n--- Aktuální počasí ---");
        Console.WriteLine($"Město: {weather.Location.Name}, {weather.Location.Country}");
        Console.WriteLine($"Čas: {weather.Location.Localtime}");
        Console.WriteLine($"Teplota: {weather.Current.Temp_C} °C");
        Console.WriteLine($"Vítr: {weather.Current.Wind_Kph} km/h");
        Console.WriteLine($"Vlhkost: {weather.Current.Humidity}%");
        Console.WriteLine($"Podmínky: {weather.Current.Condition.Text}");
    }
}



using System.Text.Json;

namespace Homework_Lecture12;

class Program

{
    private const string ApiKey = "a5057b9ed5474531b6c213148250501";
    private const string BaseUrl = "http://api.weatherapi.com/v1/current.json";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Zadejte město pro získání počasí:");
        string city = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("Neplatné zadání města.");
            return;
        }

        try
        {
            string weatherData = await FetchWeatherDataAsync(city);
            if (!string.IsNullOrEmpty(weatherData))
            {
                ProcessWeatherData(weatherData);
            }
            else
            {
                Console.WriteLine("Nepodařilo se získat data o počasí.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Došlo k chybě: {ex.Message}");
        }
    }



    private static async Task<string> FetchWeatherDataAsync(string city)
    {
        using HttpClient client = new HttpClient();
        string url = $"{BaseUrl}?key={ApiKey}&q={city}&aqi=no";

        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        Console.WriteLine($"Chyba při stahování dat: {response.StatusCode}");
        return null;
    }

    private static void ProcessWeatherData(string jsonData)
    {
        try
        {


            var weather = JsonSerializer.Deserialize<WeatherResponse>(jsonData);

            if (weather != null && weather.Current != null && weather.Location != null)
            {
                Console.WriteLine("=== Počasí ===");
                Console.WriteLine($"Místo: {weather.Location.Name}, {weather.Location.Country}");
                Console.WriteLine($"Teplota: {weather.Current.TempC} °C");
                Console.WriteLine($"Pocitová teplota: {weather.Current.FeelsLikeC} °C");
                Console.WriteLine($"Podmínky: {weather.Current.Condition.Text}");
                Console.WriteLine("================");
            }
            else
            {
                Console.WriteLine("Nepodařilo se zpracovat data o počasí.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při zpracování dat: {ex.Message}");
        }
    }
}


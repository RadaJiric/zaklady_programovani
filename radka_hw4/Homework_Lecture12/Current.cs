

using System.Text.Json.Serialization;

namespace Homework_Lecture12
{
    public class Current
    {
        
        
            [JsonPropertyName("temp_c")]
            public double TempC { get; set; }

            [JsonPropertyName("feelslike_c")]
            public double FeelsLikeC { get; set; }

            [JsonPropertyName("condition")]
            public Condition Condition { get; set; }

            [JsonPropertyName("wind_kph")]
            public double WindKph { get; set; }

            [JsonPropertyName("wind_dir")]
            public string WindDir { get; set; }

            [JsonPropertyName("pressure_mb")]
            public double PressureMb { get; set; }

           [JsonPropertyName("humidity")]
           public int Humidity { get; set; }

    }
}

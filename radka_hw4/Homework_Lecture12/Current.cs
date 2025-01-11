

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

            
        
    }
}

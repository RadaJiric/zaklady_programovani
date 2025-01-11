

using System.Text.Json.Serialization;

namespace Homework_Lecture12
{
   

  
    
        public class WeatherResponse
        {



            [JsonPropertyName("location")]
            public Location Location { get; set; }

            [JsonPropertyName("current")]
            public Current Current { get; set; }

        }
    
}



    

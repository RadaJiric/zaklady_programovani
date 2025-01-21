
using System.Text.Json.Serialization;

namespace Homework_Lecture12
{

    public class Condition
    {
        [JsonPropertyName("text")]

        public string Text { get; set; }
    }

}


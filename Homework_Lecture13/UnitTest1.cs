using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Homework_Lecture13_Tests
{
    public class UnitTests
    {
        [Fact]
        public void Test_PositiveScenario()
        {
            var mockInterface = new Mock<IMyInterface>();
            mockInterface.Setup(m => m.DoSomething()).Returns("ExpectedResult");

            var myClass = new MyClass(mockInterface.Object);

            var result = myClass.PerformAction();

            Assert.Equal("ExpectedResult", result);
        }

        [Fact]
        public void Test_NegativeScenario()
        {
            var mockInterface = new Mock<IMyInterface>();
            mockInterface.Setup(m => m.DoSomething()).Returns("UnexpectedResult");

            var myClass = new MyClass(mockInterface.Object);

            var result = myClass.PerformAction();

            Assert.NotEqual("ExpectedResult", result);
        }

        [Fact]
        public void Test_ExceptionScenario()
        {
            var mockInterface = new Mock<IMyInterface>();
            mockInterface.Setup(m => m.DoSomething()).Throws(new InvalidOperationException());

            var myClass = new MyClass(mockInterface.Object);

            Assert.Throws<InvalidOperationException>(() => myClass.PerformAction());
        }

        [Fact]
        public void Test_SerializationOfCurrent()
        {
            var current = new Current
            {
                TempC = 25.0,
                FeelsLikeC = 27.0,
                WindKph = 15.5,
                WindDir = "NE",
                PressureMb = 1013.25,
                Humidity = 60,
                Condition = new Condition { Text = "Sunny" }
            };

            var json = JsonSerializer.Serialize(current);
            var deserialized = JsonSerializer.Deserialize<Current>(json);

            Assert.NotNull(deserialized);
            Assert.Equal(25.0, deserialized.TempC);
            Assert.Equal("Sunny", deserialized.Condition.Text);
        }

        [Fact]
        public void Test_SerializationOfLocation()
        {
            var location = new Location
            {
                Name = "Prague",
                Region = "Central Bohemia",
                Country = "Czech Republic"
            };

            var json = JsonSerializer.Serialize(location);
            var deserialized = JsonSerializer.Deserialize<Location>(json);

            Assert.NotNull(deserialized);
            Assert.Equal("Prague", deserialized.Name);
            Assert.Equal("Czech Republic", deserialized.Country);
        }

        [Fact]
        public async Task Test_FetchWeatherDataAsync_ValidCity()
        {
            var city = "Prague";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Setup(m => m.Send(It.IsAny<HttpRequestMessage>()))
                                   .Returns(new HttpResponseMessage
                                   {
                                       StatusCode = System.Net.HttpStatusCode.OK,
                                       Content = new StringContent("{\"location\":{\"name\":\"Prague\"},\"current\":{\"temp_c\":25.0}}")
                                   });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            var result = await FetchWeatherDataAsync(city, client);

            Assert.NotNull(result);
            Assert.Contains("Prague", result);
        }

        private async Task<string> FetchWeatherDataAsync(string city, HttpClient client)
        {
            var url = $"http://api.weatherapi.com/v1/current.json?key=dummy_key&q={city}&aqi=no";
            var response = await client.GetAsync(url);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }

        [Fact]
        public void Test_InvalidWeatherResponse()
        {
            var json = "{ \"location\": null, \"current\": null }";

            var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(json);

            Assert.NotNull(weatherResponse);
            Assert.Null(weatherResponse.Location);
            Assert.Null(weatherResponse.Current);
        }
    }

    public interface IMyInterface
    {
        string DoSomething();
    }

    public class MyClass
    {
        private readonly IMyInterface _myInterface;

        public MyClass(IMyInterface myInterface)
        {
            _myInterface = myInterface;
        }

        public string PerformAction()
        {
            return _myInterface.DoSomething();
        }
    }

    public class Condition
    {
        public string Text { get; set; }
    }

    public class Current
    {
        public double TempC { get; set; }
        public double FeelsLikeC { get; set; }
        public double WindKph { get; set; }
        public string WindDir { get; set; }
        public double PressureMb { get; set; }
        public int Humidity { get; set; }
        public Condition Condition { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }

    public class WeatherResponse
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}

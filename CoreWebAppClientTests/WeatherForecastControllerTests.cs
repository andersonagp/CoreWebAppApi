using CoreWebAppApi;
using CoreWebAppApi.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreWebAppClientTests
{
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _weatherForecastController;

        [SetUp]
        public void Setup()
        {
            var moq = new Mock<ILogger<WeatherForecastController>>();

            _weatherForecastController = new WeatherForecastController(moq.Object);
        }

        [Test]
        public void Get_Should_Return_Forecasts()
        {
            //arrange
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast> { new WeatherForecast() };

            //act
            var result = _weatherForecastController.Get();

            //assert
            Assert.IsNotNull(result);

            result.Should().HaveCountGreaterThan(0);

        }
    }
}
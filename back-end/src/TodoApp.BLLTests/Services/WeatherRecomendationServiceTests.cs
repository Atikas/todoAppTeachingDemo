using TodoApp.BLL.Models;
using Xunit;

namespace TodoApp.BLL.Services.Tests
{
    

    /// <summary>
    /// Tests using the Equivalence Class Testing (ECT) approach.
    /// </summary>
    public class WeatherRecomendationServiceTests
    {
/*
     Equivalence Class Testing Weaknesses:
     - Misses Boundary Conditions: ECT divides input data into equivalence classes where each member of a class is expected to be treated the same by the software. It assumes that testing one value from each class is as effective as testing all values. However, this approach might miss errors that occur at the boundaries of these classes, which RBVT explicitly targets, given that boundaries are often where errors occur.
     - Less Focus on Edge Cases: By concentrating on representative values, ECT might overlook edge cases that fall just outside defined input ranges or at the extreme ends of these ranges. RBVT, on the other hand, emphasizes these areas by including tests for values just outside the valid boundaries, catching issues that might arise with unexpected or extreme inputs.
     - Does Not Guarantee Detection of All Defects: While ECT can efficiently cover a broad range of inputs by testing only representative values, it does not guarantee the detection of defects that specifically arise due to boundary conditions or exceptional cases. RBVT complements this by ensuring that such boundary-related defects are less likely to be missed.
*/

        private WeatherRecomendationService _service;

        public WeatherRecomendationServiceTests()
        {
            _service = new WeatherRecomendationService();
        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureMaxMinus90()// Invalid value test
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(-90, 0, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Empty(recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureMaxMinus20()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(-20, 0, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear a very warm down jacket; it's going to be very cold.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureMax15()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(15, 0, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear a jacket; it's cool outside.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureMax25()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(25, 0, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear light clothing; it's warm.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureMax45()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(45, 0, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Stay indoors; it's extremely hot outside.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenPrecipitation2()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(0, 2, 0, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Consider taking an umbrella; there might be light rain.", recommendations);

        }


        [Fact]
        public void GenerateRecommendations_WhenWind30()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(0, 0, 30, 0, 0);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("It's windy, consider wearing something that won't get blown around.", recommendations);

        }
    }
}
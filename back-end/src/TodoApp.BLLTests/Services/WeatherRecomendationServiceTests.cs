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

        /*
      Temperature-Based Clothing Recommendations
        Divide the temperature conditions into Valid Equivalence Classes (VEC):
          VEC1: (Very Cold): Temperature < 10°C
          VEC2: (Cold): 10°C ≤ Temperature < 20°C
          VEC3: (Warm): 20°C ≤ Temperature < 30°C
          VEC4: (Hot): 30°C ≤ Temperature < 40°C
          VEC5: (Very Hot): Temperature ≥ 40°C
        Divide the temperature conditions into Invalid  Equivalence Classes (IEC):
          IEC1: Temperature = null
          IEC2: Temperature < -273.15°C (Absolute zero)
        
        From each of these classes, select at least one temperature value to create test cases:
          TC1 for VEC1: Test with Temperature = 5°C, Precipitation = 1.0 mm
          TC2 for VEC2: Test with Temperature = 15°C, Precipitation = 1.0 mm
          TC3 for VEC3: Test with Temperature = 25°C, Precipitation = 1.0 mm
          TC4 for VEC4: Test with Temperature = 35°C, Precipitation = 1.0 mm
          TC5 for VEC5: Test with Temperature = 45°C, Precipitation = 1.0 mm
          TC6 for IEC1: Test with Temperature = null, Precipitation = 1.0 mm
          TC7 for IEC2: Test with Temperature = -300°C, Precipitation = 1.0 mm
         */

        [Fact]
        public void GenerateRecommendations_WhenTemperature5()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(5, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear a coat; it's going to be cold.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperature15()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(15, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear a jacket; it's cool outside.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperature25()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(25, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear light clothing; it's warm.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperature35()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(35, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Wear very light clothing; it's hot outside.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperature45()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(45, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Stay indoors; it's extremely hot outside.", recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureNull()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(null, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Empty(recommendations);

        }

        [Fact]
        public void GenerateRecommendations_WhenTemperatureNegative()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(-300, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Empty(recommendations);

        }

       



        /*
        Precipitation-Based Recommendations
         Divide the precipitation conditions into Valid Equivalence Classes (VEC):
           VEC1: (Light): 0 < Precipitation ≤ 2.5 mm
           VEC2: (Moderate): 2.5 < Precipitation ≤ 7.6 mm
           VEC3: (Heavy): Precipitation > 7.6 mm
        Divide the precipitation conditions into Invalid  Equivalence Classes (IEC):
           IEC1: Precipitation = null
           IEC2: Precipitation < 0 mm
        From each of these classes, select at least one precipitation value to create test cases:
           TC8 for VEC1: Test with Temperature = 5°C, Precipitation = 1.0 mm
           TC9 for VEC2: Test with Temperature = 15°C, Precipitation = 5.0 mm
           TC10 for VEC3: Test with Temperature = 25°C, Precipitation = 10.0 mm
           TC11 for IEC1: Test with Temperature = 10°C, Precipitation = null
           TC12 for IEC2: Test with Temperature = 35°C, Precipitation = -1.0 mm
         */


        [Fact]
        public void GenerateRecommendations_WhenPrecipitation1()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(5, 1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.Contains("Light rain: Consider taking an umbrella;", recommendations);
            Assert.DoesNotContain("Moderate rain: Take an umbrella;", recommendations);
            Assert.DoesNotContain("Heavy rain: Take an umbrella and wear waterproof shoes.", recommendations);
        }

        [Fact]
        public void GenerateRecommendations_WhenPrecipitation5()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(15, 5);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.DoesNotContain("Light rain: Consider taking an umbrella;", recommendations);
            Assert.Contains("Moderate rain: Take an umbrella;", recommendations);
            Assert.DoesNotContain("Heavy rain: Take an umbrella and wear waterproof shoes.", recommendations);
        }

        [Fact]
        public void GenerateRecommendations_WhenPrecipitation10()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(25, 10);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.DoesNotContain("Light rain: Consider taking an umbrella;", recommendations);
            Assert.DoesNotContain("Moderate rain: Take an umbrella;", recommendations);
            Assert.Contains("Heavy rain: Take an umbrella and wear waterproof shoes.", recommendations);
          
        }

        [Fact]
        public void GenerateRecommendations_WhenPrecipitationNull()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(10, null);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.DoesNotContain("Light rain: Consider taking an umbrella;", recommendations);
            Assert.DoesNotContain("Moderate rain: Take an umbrella;", recommendations);
            Assert.DoesNotContain("Heavy rain: Take an umbrella and wear waterproof shoes.", recommendations);
        }

        [Fact]
        public void GenerateRecommendations_WhenPrecipitationNegative()
        {
            // Arrange
            var weather = new WeatherRecomendationRequest(35, -1);

            // Act
            var recommendations = _service.GenerateRecommendations(weather);

            // Assert
            Assert.DoesNotContain("Light rain: Consider taking an umbrella;", recommendations);
            Assert.DoesNotContain("Moderate rain: Take an umbrella;", recommendations);
            Assert.DoesNotContain("Heavy rain: Take an umbrella and wear waterproof shoes.", recommendations);
        }

        


       
    }
}
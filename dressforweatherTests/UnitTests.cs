using Microsoft.Extensions.Configuration;
using weather;
namespace dressforweatherTests;

public class Tests
{

    [TestCase("Melbourne")]
    public void Get_Geocode_returnsCoordinates(string city)
    {
        //Validates get_geocode function fetches lat, lon from openWeatherMap geocode API 
        WeatherAPI weatherAPI = new WeatherAPI();
        (double, double) coordinates = weatherAPI.get_geocode(city);
        Assert.IsNotNull(coordinates.Item1);
        Assert.IsNotNull(coordinates.Item2);
        Assert.Pass();
    }

    [TestCase(37.8, 144.9)]
    public void Get_temperature_returnsCurrentTemperature(double lat, double lon)
    {
        //Validates get_temperature function fetches feels_like from openWeatherMap weather API 
        WeatherAPI weatherAPI = new WeatherAPI();
        var temperature = weatherAPI.Get_temperature(lat, lon);
        Assert.IsTrue(temperature.GetType() == typeof(double));
        Assert.Pass();
    }

    [TestCase(19, "Jumper"),TestCase(9, "Jacket"), TestCase(29, "Casual")]
    public void Get_Outfit_returnsOutfit(double temperature, string expectedOutfit)
    {
        //Validates Get_Outfit function returns recommended outfit based on Get_temperature function

        WeatherAPI weatherAPI = new WeatherAPI();
        string recommendedoutfit = weatherAPI.Get_Outfit(temperature);
        Assert.That(recommendedoutfit, Is.EqualTo(expectedOutfit));
        Assert.Pass();
    }

     [TestCase("Melbourne", "Jumper"),TestCase("Alaska", "Jacket"), TestCase("Mumbai", "Casual")]
    public void Get_CityOutfit_returnsOutfitforCityName(string city, string expectedOutfit)
    {
        //Validates Get_CityOutfit function returns recommended outfit based on city name

        WeatherAPI weatherAPI = new WeatherAPI();
        (double,string) recommendedoutfit = weatherAPI.Get_CityOutfit(city);
        Assert.IsTrue(recommendedoutfit.Item1.GetType() == typeof(double));
        Assert.That(expectedOutfit, Is.EqualTo(recommendedoutfit.Item2));
        Assert.Pass();
    }

}


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

    [TestCase("Melbourne")]
    public void Get_CityOutfit_returnsCurrentTemperature(string city)
    {
        List<string> outfits = new List<string>{"Jacket", "Casual", "Jumper"};
        //Validates Get_CityDress function returns recommended outfit based on Get_temperature function
        WeatherAPI weatherAPI = new WeatherAPI();
        string recommendedoutfit = weatherAPI.Get_CityOutfit(city);
        CollectionAssert.Contains(outfits, recommendedoutfit);
        Assert.Pass();
    }

}


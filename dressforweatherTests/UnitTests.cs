using weather;
namespace dressforweatherTests;

public class Tests
{
    [TestCase("Melbourne")]
    public void Get_Geocode_returnsCoordinates(string city)
    {
        WeatherAPI weatherAPI = new WeatherAPI();
        (double, double) coordinates = weatherAPI.get_geocode(city);
        Assert.IsNotNull(coordinates.Item1);
        Assert.IsNotNull(coordinates.Item2);
        Assert.Pass();
    }
}


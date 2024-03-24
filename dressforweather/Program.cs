using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
namespace weather
{
    public class WeatherAPI
    {
        private string apiKey = null;
        private HttpClient client;

        public WeatherAPI()
        {
            client = new HttpClient();
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<WeatherAPI>()
            .Build();
            SetApiKey(config["OpenWeatherMap:ApiKey"]);
        }
        public string GetApiKey()
        {
            return apiKey;
        }

        public void SetApiKey(String value)
        {
            apiKey = value;
        }

        public (double, double) get_geocode(string city)
        {
            string response = client.GetStringAsync($"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}").Result;
            List<Location> locations = JsonSerializer.Deserialize<List<Location>>(response.ToString());
            return (locations[0].lat, locations[0].lon);
        }

        public double Get_temperature(double lat, double lon)
        {
            string response = client.GetStringAsync($"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&units=metric&exclude=hourly,daily&appid={apiKey}").Result;
            Weather weather = JsonSerializer.Deserialize<Weather>(response.ToString());
            return weather.current.feels_like;
        }

         public static void Main(string[] args)
        {

        }

        public string Get_Outfit(double temperature)
        {
            if (temperature < 10)
                    return "Jacket";
                else if (temperature < 20)
                    return "Jumper";
                else
                    return "Casual";
        }
    }
       
      
}
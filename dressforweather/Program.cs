using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
namespace weather
{
    public class WeatherAPI {
        private string apiKey = null;
        public WeatherAPI(){
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<WeatherAPI>()
            .Build();
            SetApiKey(config["OpenWeatherMap:ApiKey"]);
        }
        public string GetApiKey() {
            return apiKey;
        }

        public void SetApiKey(String value){
            apiKey = value;
        }

        public (double, double) get_geocode(string city)
            {
      
                HttpClient client = new HttpClient();
                string response = client.GetStringAsync($"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}").Result;
                List<Location> locations = JsonSerializer.Deserialize<List<Location>>(response.ToString());
                return (locations[0].lat, locations[0].lon);
            }

        public static void Main(string[] args)
        {

        }
    }
}
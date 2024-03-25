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
            apiKey = Environment.GetEnvironmentVariable("APPSETTING_OPENWEATHER_API_KEY");
            System.Diagnostics.Trace.TraceError(apiKey + "found apikey");
            client = new HttpClient();
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
            var builder = WebApplication.CreateBuilder();
            string port = Environment.GetEnvironmentVariable("APPSETTING_PORT");


            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddSwaggerGen();;


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();

            app.MapGet("/dressforweather/{page}", (string page) =>
            {
                WeatherAPI weather = new WeatherAPI();
                var data = weather.Get_CityOutfit(page);
                var outfitJson = new 
                    {
                        TemperatureCelsius = data.Item1,
                        Outfit = data.Item2
                    };
                return outfitJson;
            })
            .WithName("GetDressforWeather")
            .WithOpenApi();
            app.Run();
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

        public (double,string) Get_CityOutfit(string city)
        {
            (double, double) coordinates = get_geocode(city);
            double temperature = Get_temperature(coordinates.Item1, coordinates.Item2);
            string outfit = Get_Outfit(temperature);
            return (temperature, outfit);
        }
    }
       
      
}
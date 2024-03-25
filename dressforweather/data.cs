using System.Text.Json.Serialization;

public class Location
    {
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }

public class Weather
{
    public CurrentTemperature current { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }

}

public class CurrentTemperature
{
    [JsonPropertyName("feels_like")]
    public double feels_like { get; set; }
    [JsonPropertyName("temp")]
    public double temp { get; set; }
   
}

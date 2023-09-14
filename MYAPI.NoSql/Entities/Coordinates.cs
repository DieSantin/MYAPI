using Newtonsoft.Json;

namespace MYAPI.NoSql.Entities;

public class Coordinates
{
    public int Id { get; set; }

    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lng")]
    public double Lng { get; set; }
}

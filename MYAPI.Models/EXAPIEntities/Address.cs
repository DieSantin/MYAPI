using Newtonsoft.Json;

namespace MYAPI.Models.EXAPIEntities;

public class Address
{
    public int Id { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("street_name")]
    public string StreetName { get; set; }

    [JsonProperty("street_address")]
    public string StreetAddress { get; set; }

    [JsonProperty("zip_code")]
    public string ZipCode { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("coordinates")]
    public Coordinates Coordinates { get; set; }

    public DateTime CreationDate { get; set; }
}

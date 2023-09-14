using Newtonsoft.Json;

namespace MYAPI.ExApi.Models;

public class Subscription
{
    public int Id { get; set; }

    [JsonProperty("plan")]
    public string Plan { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("payment_method")]
    public string PaymentMethod { get; set; }

    [JsonProperty("term")]
    public string Term { get; set; }
}

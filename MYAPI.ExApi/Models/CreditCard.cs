using Newtonsoft.Json;

namespace MYAPI.ExApi.Models;

public class CreditCard
{
    public int Id { get; set; }

    [JsonProperty("cc_number")]
    public string CcNumber { get; set; }
}

using Newtonsoft.Json;

namespace MYAPI.Models.EXAPIEntities;

public class CreditCard
{
    public int Id { get; set; }

    [JsonProperty("cc_number")]
    public string CcNumber { get; set; }
}

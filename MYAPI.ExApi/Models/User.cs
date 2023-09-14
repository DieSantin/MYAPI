using Newtonsoft.Json;

namespace MYAPI.ExApi.Models;

public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("uid")]
    public Guid Uid { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("avatar")]
    public Uri Avatar { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonProperty("social_insurance_number")]
    public long SocialInsuranceNumber { get; set; }

    [JsonProperty("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonProperty("employment")]
    public Employment Employment { get; set; }

    [JsonProperty("address")]
    public Address Address { get; set; }

    [JsonProperty("credit_card")]
    public CreditCard CreditCard { get; set; }

    [JsonProperty("subscription")]
    public Subscription Subscription { get; set; }

    public DateTime CreationDate { get; set; }
}

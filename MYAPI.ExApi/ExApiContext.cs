using Newtonsoft.Json;

namespace MYAPI.ExApi;

public class ExApiContext
{
    private const string HOST = "https://random-data-api.com/api/";

    private readonly IHttpClientFactory _httpClientFactory;
    public ExApiContext(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<T> Get<T>(string url)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(HOST);

            using var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Ex api bad response: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ex api error: {ex.Message}");
        }
    }
}

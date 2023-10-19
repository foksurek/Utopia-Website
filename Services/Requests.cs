using Utopia.Interfaces;

namespace Utopia.Services;

public class Requests : IRequests
{
    private readonly HttpClient _httpClient;

    public Requests(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAsync(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();

        }
        catch (Exception e)
        {
            return "";
        }
    }
}
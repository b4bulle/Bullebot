using System.Net.Http.Json;
using Babulle.Bullebot.Core;

namespace Babulle.Bullebot.Twitch.Infrastructure.OAuth;

public class TwitchOAuthTokenProvider(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(HttpClientNames.TwitchAuth);

    public async Task<string> GetAuthToken()
    {
        var payload = new List<KeyValuePair<string, string>>()
        {
            new("client_id", Environment.GetEnvironmentVariable("TWITCH_BULLEBOT_ID", EnvironmentVariableTarget.Process)?? ""),
            new("client_secret", Environment.GetEnvironmentVariable("TWITCH_BULLEBOT_SECRET", EnvironmentVariableTarget.Process)?? ""),
            new("grant_type", "client_credentials")
        };

        var content = new FormUrlEncodedContent(payload);

        var httpResponse = await _httpClient.PostAsync("oauth2/token", content);

        if (!httpResponse.IsSuccessStatusCode) throw new HttpRequestException();
        
        var dao = await httpResponse.Content.ReadFromJsonAsync<TwitchOAuthTokenDao>();

        return dao?.AccessToken?? "";
    }
}
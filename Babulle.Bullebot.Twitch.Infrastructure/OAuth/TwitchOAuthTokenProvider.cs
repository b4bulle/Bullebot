using Babulle.Bullebot.Core;
using Babulle.Bullebot.Twitch.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Babulle.Bullebot.Twitch.Infrastructure.OAuth;

public class TwitchOAuthTokenProvider(IHttpClientFactory clientFactory, IOptions<TwitchConfiguration> twitchConfiguration)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(HttpClientNames.TwitchAuth);

    public async Task<string> GetAuthToken()
    {
        var payload = new List<KeyValuePair<string, string>>()
        {
            new("client_id", twitchConfiguration.Value.ClientId),
            new("client_secret", twitchConfiguration.Value.ClientSecret),
            new("grant_type", "client_credentials")
        };

        var content = new FormUrlEncodedContent(payload);

        var httpResponse = await _httpClient.PostAsync("oauth2/token", content);

        if (!httpResponse.IsSuccessStatusCode) throw new HttpRequestException();
        
        var dao = await httpResponse.Content.ReadFromJsonAsync<TwitchOAuthTokenDao>();

        return dao?.AccessToken?? "";
    }
}
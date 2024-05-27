using Babulle.Bullebot.Core;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.Stream;

public class TwitchStreamInformationProvider(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(HttpClientNames.TwitchApi);

    public async Task GetStreamInformation(string userId)
    {
        var uri = "helix/streams";

        var queryString = QueryString.Create("user_id", userId);

        var responseMessage = await _httpClient.GetAsync(uri + queryString.Value);
    }
}
using Babulle.Bullebot.Core;
using Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub.Dto;
using Babulle.Bullebot.Twitch.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub;

public class TwitchEventSubService(IHttpClientFactory clientFactory, IOptions<TwitchConfiguration> twitchConfiguration)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(HttpClientNames.TwitchApi);

    public async Task SubscribeWebhook()
    {
        const string uri = "helix/eventsub/subscriptions";

        var twitchEventSubDto = new TwitchEventSubDto("stream.online", "1",
            new TwitchEventSubConditionDto(twitchConfiguration.Value.ChannelId),
            new TwitchEventSubTransportDto("webhook", twitchConfiguration.Value.StreamUpWebhookUri,
                twitchConfiguration.Value.WebhookSecret));
        var content = JsonContent.Create(twitchEventSubDto);
        
        _httpClient.DefaultRequestHeaders.Add("Client-Id", twitchConfiguration.Value.ClientId);
        
        var responseMessage = await _httpClient.PostAsync(uri, content);
    }
}
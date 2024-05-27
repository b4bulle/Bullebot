using System.Net.Http.Headers;
using Babulle.Bullebot.Twitch.Infrastructure.OAuth;

namespace Babulle.Bullebot.Twitch.Infrastructure;

public class TwitchTokenMessageHandler(TwitchOAuthTokenProvider tokenProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await tokenProvider.GetAuthToken());
        
        return await base.SendAsync(request, cancellationToken);
    }
}
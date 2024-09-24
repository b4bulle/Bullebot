using Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub;
using Microsoft.Azure.Functions.Worker;

namespace Babulle.Bullebot.TwitchFunctions;

public class WebhooksSubscription(TwitchEventSubService twitchEventSubService)
{
    [Function("WebhooksSubscription")]
    public async Task CreateTwitchWebhookSubscription([TimerTrigger("0 0 */10 * *", RunOnStartup = true)]TimerInfo myTimer, FunctionContext context)
    {
        await twitchEventSubService.SubscribeWebhook();
    }
}
using Babulle.Bullebot.TwitchFunctions.Commands;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Babulle.Bullebot.TwitchFunctions
{
    public class WebhookSubscription(ILoggerFactory loggerFactory, IMediator mediator)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<WebhookSubscription>();

        [Function("WebhookSubscription")]
        public void Run([TimerTrigger("0 0 12 */20 * *")] TimerInfo myTimer)
        {
            mediator.Send(new TwitchWebHookStreamUpSubscribeCommand());
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Reset webhook subscription at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}

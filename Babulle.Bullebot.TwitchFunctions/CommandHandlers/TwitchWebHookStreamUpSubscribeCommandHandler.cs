using Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub;
using Babulle.Bullebot.TwitchFunctions.Commands;
using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;

namespace Babulle.Bullebot.TwitchFunctions.CommandHandlers;

public class TwitchWebHookStreamUpSubscribeCommandHandler(TwitchEventSubService eventSubService): IRequestHandler<TwitchWebHookStreamUpSubscribeCommand, TwitchWebHookStreamUpSubscribeResponse>
{
    public async Task<TwitchWebHookStreamUpSubscribeResponse> Handle(TwitchWebHookStreamUpSubscribeCommand request, CancellationToken cancellationToken)
    {
        await eventSubService.SubscribeWebhook();

        return new TwitchWebHookStreamUpSubscribeResponse(true);
    }
}
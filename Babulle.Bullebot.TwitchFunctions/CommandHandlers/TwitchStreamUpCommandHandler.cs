using System.Net;
using Babulle.Bullebot.DiscordActions;
using Babulle.Bullebot.TwitchFunctions.Commands;
using Babulle.Bullebot.TwitchFunctions.Configuration;
using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;
using Microsoft.Extensions.Options;

namespace Babulle.Bullebot.TwitchFunctions.CommandHandlers;

public class TwitchStreamUpCommandHandler(SendMessageService sendMessageService, IOptions<DiscordStreamUpConfiguration> streamUpOptions): IRequestHandler<TwitchStreamUpCommand, TwitchEventResponse>
{
    public async Task<TwitchEventResponse> Handle(TwitchStreamUpCommand request, CancellationToken cancellationToken)
    {
        await sendMessageService.ExecuteAsync(new SendMessageCommand(streamUpOptions.Value.Channel, $"{request.Event.BroadCasterUserName} est live sur https://twitch/tv/{request.Event.BroadCasterUserName} ! Venez jeter un oeil !", streamUpOptions.Value.NotifiedRoles));
        
        return new TwitchEventResponse(HttpStatusCode.OK, string.Empty);
    }
}
using System.Net;
using Babulle.Bullebot.DiscordActions;
using Babulle.Bullebot.TwitchFunctions.Commands;
using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;

namespace Babulle.Bullebot.TwitchFunctions.CommandHandlers;

public class TwitchStreamUpCommandHandler(SendMessageService sendMessageService): IRequestHandler<TwitchStreamUpCommand, TwitchEventResponse>
{
    public async Task<TwitchEventResponse> Handle(TwitchStreamUpCommand request, CancellationToken cancellationToken)
    {
        await sendMessageService.ExecuteAsync(new SendMessageCommand("1243650501220761803", $"{request.Event.BroadCasterUserName} est live ! Trop bien !"));
        
        return new TwitchEventResponse(HttpStatusCode.OK, string.Empty);
    }
}
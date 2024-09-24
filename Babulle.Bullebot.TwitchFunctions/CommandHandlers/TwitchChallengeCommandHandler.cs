using System.Net;
using Babulle.Bullebot.TwitchFunctions.Commands;
using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;

namespace Babulle.Bullebot.TwitchFunctions.CommandHandlers;

public class TwitchChallengeCommandHandler: IRequestHandler<TwitchChallengeCommand, TwitchEventResponse>
{
    public Task<TwitchEventResponse> Handle(TwitchChallengeCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new TwitchEventResponse(HttpStatusCode.OK, request.Challenge));
    }
}
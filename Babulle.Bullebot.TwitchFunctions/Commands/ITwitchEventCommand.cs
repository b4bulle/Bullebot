using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public interface ITwitchEventCommand : IRequest<TwitchEventResponse>
{
    
}
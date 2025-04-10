using System.Text.Json.Serialization;
using Babulle.Bullebot.TwitchFunctions.Responses;
using MediatR;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public class TwitchWebHookStreamUpSubscribeCommand : IRequest<TwitchWebHookStreamUpSubscribeResponse>
{
}
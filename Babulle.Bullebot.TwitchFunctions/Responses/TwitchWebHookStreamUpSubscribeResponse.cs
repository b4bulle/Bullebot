using System.Net;

namespace Babulle.Bullebot.TwitchFunctions.Responses;

public class TwitchWebHookStreamUpSubscribeResponse(bool success)
{
    public bool Success { get; set; } = success;
}
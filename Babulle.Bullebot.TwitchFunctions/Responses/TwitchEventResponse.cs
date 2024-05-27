using System.Net;

namespace Babulle.Bullebot.TwitchFunctions.Responses;

public class TwitchEventResponse(HttpStatusCode statusCode, string response)
{
    public HttpStatusCode StatusCode { get; set; } = statusCode;

    public string Response { get; set; } = response;
}
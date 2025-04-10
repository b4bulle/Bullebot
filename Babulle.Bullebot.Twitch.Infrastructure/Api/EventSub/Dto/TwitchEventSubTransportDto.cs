using System.Text.Json.Serialization;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub.Dto;

public class TwitchEventSubTransportDto(string method, Uri callback, string secret)
{
    [JsonPropertyName("method")]
    public string Method { get; set; } = method;

    [JsonPropertyName("callback")]
    public Uri Callback { get; set; } = callback;

    [JsonPropertyName("secret")]
    public string Secret { get; set; } = secret;
}
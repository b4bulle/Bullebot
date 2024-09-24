using System.Text.Json.Serialization;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub.Dto;

public class TwitchEventSubDto(
    string type,
    string version,
    TwitchEventSubConditionDto condition,
    TwitchEventSubTransportDto transport)
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = type;

    [JsonPropertyName("version")]
    public string Version { get; set; } = version;

    [JsonPropertyName("condition")]
    public TwitchEventSubConditionDto Condition { get; set; } = condition;

    [JsonPropertyName("transport")]
    public TwitchEventSubTransportDto Transport { get; set; } = transport;
}
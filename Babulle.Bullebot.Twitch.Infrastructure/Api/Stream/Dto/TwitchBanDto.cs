using System.Text.Json.Serialization;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.Stream.Dto;

public class TwitchBanDto(string userId, int duration, string reason)
{
    [JsonPropertyName("user_id")]
    public string UserId { get; } = userId;

    [JsonPropertyName("duration")]
    public int Duration { get; } = duration;

    [JsonPropertyName("reason")]
    public string Reason { get; } = reason;
}
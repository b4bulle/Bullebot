using System.Text.Json.Serialization;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.Stream.DTO;

public class TwitchBanDto
{
    [JsonPropertyName("user_id")]
    public string UserId { get; }

    [JsonPropertyName("duration")]
    public int Duration { get; }

    [JsonPropertyName("reason")]
    public string Reason { get; }

    public TwitchBanDto(string userId, int duration, string reason)
    {
        UserId = userId;
        Duration = duration;
        Reason = reason;
    }
}
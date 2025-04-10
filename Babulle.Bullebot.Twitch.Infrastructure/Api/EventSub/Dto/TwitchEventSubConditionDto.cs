using System.Text.Json.Serialization;

namespace Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub.Dto;

public class TwitchEventSubConditionDto(string broadcasterUserId)
{
    [JsonPropertyName("broadcaster_user_id")]
    public string BroadcasterUserId { get; set; } = broadcasterUserId;
}
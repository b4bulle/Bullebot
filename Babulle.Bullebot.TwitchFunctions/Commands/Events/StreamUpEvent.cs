using System.Text.Json.Serialization;

namespace Babulle.Bullebot.TwitchFunctions.Commands.Events;

public class StreamUpEvent
{
    [JsonPropertyName("id")] 
    public string StreamId { get; set; } = string.Empty;
    
    [JsonPropertyName("broadcaster_user_id")] 
    public string BroadCasterUserId { get; set; } = string.Empty;
    
    [JsonPropertyName("broadcaster_user_login")] 
    public string BroadCasterUserLogin { get; set; } = string.Empty;
    
    [JsonPropertyName("broadcaster_user_name")] 
    public string BroadCasterUserName { get; set; } = string.Empty;
    
    [JsonPropertyName("type")] 
    public BroadcastType Type { get; set; }
    
    [JsonPropertyName("started_at")] 
    public DateTime StartedAt { get; set; }
}
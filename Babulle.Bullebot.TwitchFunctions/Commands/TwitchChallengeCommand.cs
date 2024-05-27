using System.Text.Json.Serialization;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public class TwitchChallengeCommand : ITwitchEventCommand
{
    [JsonPropertyName("challenge")] 
    public string Challenge { get; set; } = string.Empty;
}
using System.Text.Json.Serialization;
using Babulle.Bullebot.TwitchFunctions.Commands.Events;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public class TwitchStreamUpCommand : ITwitchEventCommand
{
    [JsonPropertyName("event")] 
    public required StreamUpEvent Event { get; set; }
}
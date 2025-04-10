using System.Text.Json.Serialization;
using Babulle.Bullebot.TwitchFunctions.Commands.Events;
using JetBrains.Annotations;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public class TwitchStreamUpCommand : ITwitchEventCommand
{
    [JsonPropertyName("event")] 
    public required StreamUpEvent Event { get; [UsedImplicitly] set; }
}
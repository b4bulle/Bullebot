using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BroadcastType
{
    [EnumMember(Value = "live")]
    Live,
    [EnumMember(Value = "playlist")]
    Playlist,
    [EnumMember(Value = "watch_party")]
    WatchParty,
    [EnumMember(Value = "premiere")]
    Premiere,
    [EnumMember(Value = "rerun")]
    Rerun
}
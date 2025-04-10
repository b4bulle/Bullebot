using System.Collections.ObjectModel;

namespace Babulle.Bullebot.TwitchFunctions.Configuration;

public class DiscordStreamUpConfiguration
{
    public ulong Channel { get; init; }
    
    public IReadOnlyList<ulong> NotifiedRoles { get; init; } = []; 
}
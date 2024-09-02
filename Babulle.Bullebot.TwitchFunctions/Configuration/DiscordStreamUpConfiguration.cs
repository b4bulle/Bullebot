namespace Babulle.Bullebot.TwitchFunctions.Configuration;

public class DiscordStreamUpConfiguration
{
    public ulong Channel { get; set; }
    
    public List<ulong> NotifiedRoles { get; set; } = []; 
}
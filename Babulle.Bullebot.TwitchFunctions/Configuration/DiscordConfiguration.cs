namespace Babulle.Bullebot.TwitchFunctions.Configuration;

public class DiscordConfiguration
{
    public required Uri BaseUri { get; set; }
    
    public string BotToken { get; set; } = string.Empty;
    
    public required DiscordStreamUpConfiguration StreamUp { get; set; }
}
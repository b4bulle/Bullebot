namespace Babulle.Bullebot.TwitchFunctions.Configuration;

public class DiscordConfiguration
{
    public required Uri? BaseUri { get; set; }
    
    public string BotToken { get; init; } = string.Empty;
    
    public required DiscordStreamUpConfiguration StreamUp { get; init; }
}
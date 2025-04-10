namespace Babulle.Bullebot.Twitch.Infrastructure.Configuration;

public class TwitchConfiguration
{
    public string ClientId { get; init; } = string.Empty;
    
    public string ClientSecret { get; init; } = string.Empty;
    
    public string ChannelId { get; init; } = string.Empty;

    public required Uri StreamUpWebhookUri { get; set; }
    
    public string WebhookSecret { get; init; } = string.Empty;
}
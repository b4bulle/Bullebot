namespace Babulle.Bullebot.DiscordActions;

public record SendMessageCommand(ulong ChannelId, string Message);
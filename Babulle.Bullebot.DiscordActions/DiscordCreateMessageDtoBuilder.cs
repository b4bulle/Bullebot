namespace Babulle.Bullebot.DiscordActions;

public class DiscordCreateMessageDtoBuilder
{
    private readonly string _content;
    private bool _isTts = false;

    private DiscordCreateMessageDtoBuilder(string content)
    {
         _content = content;
    }
    
    public static DiscordCreateMessageDtoBuilder CreateDiscordMessageBuilder(string content)
    {
        return new DiscordCreateMessageDtoBuilder(content);
    }

    public DiscordCreateMessageDtoBuilder SetTts(bool isTts)
    {
        _isTts = isTts;
        return this;
    }
    
    public DiscordCreateMessageDto Build()
    {
        return new DiscordCreateMessageDto(_content, _isTts);
    }
}
using System.Text.Json.Serialization;

namespace Babulle.Bullebot.DiscordActions;

public class DiscordCreateMessageDto(string content, bool textToSpeech)
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = content;

    [JsonPropertyName("tts")]
    public bool TextToSpeech { get; set; } = textToSpeech;
}
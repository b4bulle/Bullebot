using System.Net.Http.Json;
using Babulle.Bullebot.Core;
using Microsoft.Extensions.Logging;

namespace Babulle.Bullebot.DiscordActions;

public class SendMessageService(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
{
    private const string DiscordCreateMessageApiEndpoint = "api/v10/channels/{0}/messages";
    
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.Discord);
    private readonly ILogger _logger = loggerFactory.CreateLogger<SendMessageService>();

    public async Task ExecuteAsync(SendMessageCommand sendMessageCommand)
    {
        var dto = new DiscordCreateMessageDto(sendMessageCommand.Message, false);

        var content = JsonContent.Create(dto);

        var responseMessage = await _httpClient.PostAsync(string.Format(DiscordCreateMessageApiEndpoint, sendMessageCommand.ChannelId), content);

        if (!responseMessage.IsSuccessStatusCode)
        {
            _logger.LogError(new EventId(99999, "DISCORD_MESSAGE_ERROR"), "Error sending Discord message");
        }
    }
}
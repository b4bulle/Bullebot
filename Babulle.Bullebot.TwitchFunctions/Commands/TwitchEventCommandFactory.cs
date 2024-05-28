using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public static class TwitchEventCommandFactory
{
    public static async Task<ITwitchEventCommand> CreateEventCommandAsync(HttpRequest requestData)
    {
        return requestData.Headers["Twitch-Eventsub-Message-Type"].Single() switch
        {
            "webhook_callback_verification" => await ParseEventCommand<TwitchChallengeCommand>(requestData.Body),
            "notification" => await ParseEventCommand<TwitchStreamUpCommand>(requestData.Body),
            _ => throw new ArgumentException()
        };
    }

    private static async Task<T> ParseEventCommand<T>(Stream requestBody) where T : ITwitchEventCommand
    {
        using var streamReader = new StreamReader(requestBody);
        var json = await streamReader.ReadToEndAsync();
        var command = JsonSerializer.Deserialize<T>(json);

        if (command == null)
        {
            throw new FormatException();
        }

        return command;
    }
}
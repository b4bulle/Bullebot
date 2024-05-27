using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;

namespace Babulle.Bullebot.TwitchFunctions.Commands;

public static class TwitchEventCommandFactory
{
    public static ITwitchEventCommand CreateEventCommand(HttpRequestData requestData)
    {
        return requestData.Headers.GetValues("Twitch-Eventsub-Message-Type").Single() switch
        {
            "webhook_callback_verification" => ParseEventCommand<TwitchChallengeCommand>(requestData.Body),
            "notification" => ParseEventCommand<TwitchStreamUpCommand>(requestData.Body),
            _ => throw new ArgumentException()
        };
    }

    private static T ParseEventCommand<T>(Stream requestBody) where T : ITwitchEventCommand
    {
        using var streamReader = new StreamReader(requestBody);
        var json = streamReader.ReadToEnd();
        var command = JsonSerializer.Deserialize<T>(json);

        if (command == null)
        {
            throw new FormatException();
        }

        return command;
    }
}
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Babulle.Bullebot.TwitchFunctions.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Babulle.Bullebot.TwitchFunctions;

public class TwitchEvent(ILoggerFactory loggerFactory, IMediator mediator)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TwitchEvent>();

    [Function("TwitchEvent")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        FunctionContext executionContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var command = await TwitchEventCommandFactory.CreateEventCommandAsync(req);

        _logger.LogInformation(JsonSerializer.Serialize(command));

        var commandResult = await mediator.Send(command, cancellationToken);

        if (commandResult.StatusCode == HttpStatusCode.OK)
        {
            return new ContentResult()
            {
                Content = commandResult.Response,
                StatusCode = (int)commandResult.StatusCode,
                ContentType = MediaTypeNames.Text.Plain
            };
        }

        return new StatusCodeResult((int)commandResult.StatusCode);
    }
}
using System.Net.Mime;
using Babulle.Bullebot.TwitchFunctions.Commands;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Babulle.Bullebot.TwitchFunctions;

public class TwitchEvent(ILoggerFactory loggerFactory, IMediator mediator)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TwitchEvent>();

    [Function("TwitchEvent")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var command = TwitchEventCommandFactory.CreateEventCommand(req);

        var commandResult = await mediator.Send(command);
        
        var response = req.CreateResponse(commandResult.StatusCode);
        await response.WriteStringAsync(commandResult.Response);
        response.Headers.Add("Content-Type", MediaTypeNames.Text.Plain);

        return response;
    }
}
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Babulle.Bullebot.TwitchFunctions.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Babulle.Bullebot.TwitchFunctions;

public class ConfigFunction(ILoggerFactory loggerFactory, IConfiguration configuration)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<TwitchEvent>();

    [Function("ConfigFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        FunctionContext executionContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        // Read configuration data
        var keyName = "Bullebot:Discord:SendStreamUpNotification:Channel";
        var message = configuration[keyName];

        return message != null
            ? (ActionResult)new OkObjectResult(message)
            : new BadRequestObjectResult($"Please create a key-value with the key '{keyName}' in App Configuration.");
    }
}
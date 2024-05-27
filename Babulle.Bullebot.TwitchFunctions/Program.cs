using System.Net.Http.Headers;
using Babulle.Bullebot.Core;
using Babulle.Bullebot.DiscordActions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });
        
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient(HttpClientNames.Discord, client =>
        {
            client.BaseAddress = new Uri("https://discord.com");
            var token = Environment.GetEnvironmentVariable("DISCORD_BULLEBOT_TOKEN", EnvironmentVariableTarget.User);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);
        });

        services.AddTransient<SendMessageService>();
    })
    .Build();

host.Run();
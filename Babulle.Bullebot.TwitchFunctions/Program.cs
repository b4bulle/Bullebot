using System.Net.Http.Headers;
using Babulle.Bullebot.Core;
using Babulle.Bullebot.DiscordActions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
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

        services.Configure<LoggerFilterOptions>(options =>
        {
            LoggerFilterRule? toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                                                                              == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

            if (toRemove is not null)
            {
                options.Rules.Remove(toRemove);
            }
        });

        services.AddTransient<SendMessageService>();
    })
    .Build();

await host.RunAsync();
using System.Configuration;
using System.Net.Http.Headers;
using Babulle.Bullebot.Core;
using Babulle.Bullebot.DiscordActions;
using Babulle.Bullebot.Twitch.Infrastructure;
using Babulle.Bullebot.Twitch.Infrastructure.Api.EventSub;
using Babulle.Bullebot.Twitch.Infrastructure.Configuration;
using Babulle.Bullebot.Twitch.Infrastructure.OAuth;
using Babulle.Bullebot.TwitchFunctions.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(configure =>
    {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_APPCONFIG_CONNECTION_STRING");
        configure.AddAzureAppConfiguration(connectionString);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var hostConfiguration = hostContext.Configuration;
        services.Configure<DiscordConfiguration>(
            hostConfiguration.GetSection("Bullebot:Discord")
        ).PostConfigure<DiscordConfiguration>(options =>
        {
            if (Uri.TryCreate(options.BaseUri!.ToString(), UriKind.Absolute, out var uriResult))
            {
                options.BaseUri = uriResult;
            }
            else
            {
                throw new ConfigurationErrorsException("The base URI is not valid.");
            }
        });

        services.Configure<TwitchConfiguration>(
            hostConfiguration.GetSection("Bullebot:Twitch")
        ).PostConfigure<TwitchConfiguration>(options =>
        {
            if (Uri.TryCreate(options.StreamUpWebhookUri.ToString(), UriKind.Absolute, out var uriResult))
            {
                options.StreamUpWebhookUri = uriResult;
            }
            else
            {
                throw new ConfigurationErrorsException("The streamup webhook URI is not valid.");
            }
        });

        services.Configure<DiscordStreamUpConfiguration>(
            hostConfiguration.GetSection("Bullebot:Discord").GetSection("StreamUp")
        );
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddAzureAppConfiguration();
        
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient(HttpClientNames.Discord, (serviceProvider, client) =>
        {
            var discordConfiguration = serviceProvider.GetRequiredService<IOptions<DiscordConfiguration>>();
            
            client.BaseAddress = discordConfiguration.Value.BaseUri;
            var token = discordConfiguration.Value.BotToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);
        });
        
        services.AddSingleton<TwitchTokenMessageHandler>();
        services.AddSingleton<TwitchOAuthTokenProvider>();
        
        services.AddHttpClient(HttpClientNames.TwitchAuth,
            client => { client.BaseAddress = new Uri("https://id.twitch.tv"); });

        services.AddHttpClient(HttpClientNames.TwitchApi,
                client => { client.BaseAddress = new Uri("https://api.twitch.tv"); })
            .AddHttpMessageHandler<TwitchTokenMessageHandler>();

        services.Configure<LoggerFilterOptions>(options =>
        {
            var toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                                                                == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

            if (toRemove is not null)
            {
                options.Rules.Remove(toRemove);
            }
        });

        services.AddTransient<SendMessageService>();
        services.AddTransient<TwitchEventSubService>();
    })
    .Build();

await host.RunAsync();
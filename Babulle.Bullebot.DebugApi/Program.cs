using Babulle.Bullebot.Core;
using Babulle.Bullebot.Twitch.Infrastructure;
using Babulle.Bullebot.Twitch.Infrastructure.Api.Stream;
using Babulle.Bullebot.Twitch.Infrastructure.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TwitchTokenMessageHandler>();
builder.Services.AddSingleton<TwitchOAuthTokenProvider>();
builder.Services.AddSingleton<TwitchStreamInformationProvider>();

builder.Services.AddHttpClient(HttpClientNames.TwitchAuth,
    client => { client.BaseAddress = new Uri("https://id.twitch.tv"); });

builder.Services.AddHttpClient(HttpClientNames.TwitchApi,
        client => { client.BaseAddress = new Uri("https://api.twitch.tv"); })
    .AddHttpMessageHandler<TwitchTokenMessageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("oauth2/token", (TwitchOAuthTokenProvider tokenProvider) => tokenProvider.GetAuthToken());
app.MapGet("stream",
    (TwitchStreamInformationProvider streamInformationProvider) =>
        streamInformationProvider.GetStreamInformation("62823957"));

app.Run();
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TGBot;
using TGBot.Configuration;
using TGBot.Controllers;
using TGBot.Services.CountOfCharacters;
using TGBot.Services.Logger;
using TGBot.Services.Storage;
using TGBot.Services.SumOfNumbers;

var apikey = "";
Parser.Default.ParseArguments<StartArgsOptions>(args)
    .WithParsed(o =>
    {
        apikey = o.ApiKey;
    });
if(string.IsNullOrEmpty(apikey))return;
var host = new HostBuilder()
    .ConfigureServices((_, services) => ConfigureServices(services))
    .UseConsoleLifetime()
    .Build();
await host.RunAsync();
return;

AppSettings BuildAppSettings()
{
    return new AppSettings
    {
        BotToken = apikey
    };
}

void ConfigureServices(IServiceCollection services)
{
    var appSettings = BuildAppSettings();
    services.AddSingleton(appSettings);
    services.AddSingleton<ILogger, Logger>();
    services.AddSingleton<IStorage, MemoryStorage>();
    services.AddTransient<ISumOfNumbers, SumOfNumbers>();
    services.AddTransient<ICountOfCharacters, CountOfCharacters>();
    services.AddTransient<DefaultMessageController>();
    services.AddTransient<TextMessageController>();
    services.AddTransient<InlineKeyboardController>();
    services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(appSettings.BotToken!));
    services.AddHostedService<Bot>();
}
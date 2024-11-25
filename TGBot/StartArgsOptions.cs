using CommandLine;

namespace TGBot;

internal class StartArgsOptions
{
    [Option('a', "apikey", Required = true, HelpText = "Set telegram bot api key")]
    public string? ApiKey { get; set; }
}
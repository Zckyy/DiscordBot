using DiscordBotTemplateNet8.Commands;
using DiscordBotTemplateNet8.Commands.Slash;
using DiscordBotTemplateNet8.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DiscordBotTemplateNet8
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            // Read info from config JSON file
            var botConfig = new BotConfig();
            await botConfig.ReadJSONAsync();

            // Creating a new configuration for the Lavalink client
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1", // From your server configuration.
                Port = 2333 // From your server configuration
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass", // From your server configuration.
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };

            // Creating a new configuration for the Discord client
            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = botConfig.DiscordBotToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);

            Client.Ready += Client_Ready;

            // Creating a new Lavalink client
            var lavalink = Client.UseLavalink();

            // Creating a new configuration for the commands
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { botConfig.DiscordBotPrefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // Enabling slash commands
            var slashCommandsConfig = Client.UseSlashCommands();

            // Registering commands
            //slashCommandsConfig.RegisterCommands<SlashCommands>();
            slashCommandsConfig.RegisterCommands<SlashCommands>(814256164069441567); // This is the guild ID where the slash commands will be registered, if you want to register them globally, remove the guild ID

            // Connect the client to the Discord gateway
            await Client.ConnectAsync();

            await lavalink.ConnectAsync(lavalinkConfig); // Make sure this is after Discord.ConnectAsync().

            // Keep the program running
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}

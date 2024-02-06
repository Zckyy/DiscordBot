using DiscordBotTemplateNet8.Commands;
using DiscordBotTemplateNet8.Commands.Slash;
using DiscordBotTemplateNet8.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;

namespace DiscordBotTemplateNet8
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            var botConfig = new BotConfig();
            await botConfig.ReadJSON();

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
            slashCommandsConfig.RegisterCommands<SlashCommands>(814256164069441567);
            Commands.RegisterCommands<Basic>();

            // Connect the client to the Discord gateway
            await Client.ConnectAsync();
            // Keep the program running
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}

using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotTemplateNet8.Helper;

namespace DiscordBotTemplateNet8.Commands.Slash
{
    // Every Slash Command class must be public and inherit from ApplicationCommandModule
    public class SlashCommands : ApplicationCommandModule
    {

        private readonly CommandHelper _commandHelper;

        // Constructor
        public SlashCommands()
        {
            _commandHelper = new CommandHelper();
        }

        [SlashCommand("Ping", "Replies with pong!")]
        public async Task Ping(InteractionContext ctx)
        {
            await _commandHelper.BuildMessage(ctx, "Pong! 🏓", "23ms Europe Datacentre 📶", DiscordColor.SpringGreen);
        }

        [SlashCommand("UploadFile", "Choose a user and upload a file")]
        public async Task UploadFile(InteractionContext ctx, [Option("DiscordUser", "Choose a discord user")] DiscordUser discordUser, [Option("File", "Upload a file")] DiscordAttachment discordAttachment)
        {
            await _commandHelper.BuildMessage(ctx, "File Uploaded", $"File Name is:{discordAttachment.FileName} - Selected user is: {discordUser.Username}", DiscordColor.SpringGreen);
        }
    }
}

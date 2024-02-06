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
            // get response from an API
            var apiResult = await WebHelper.GetJsonFromApi("https://jsonplaceholder.typicode.com/todos/1");

            // Check if the response contains the userId
            if (apiResult.TryGetValue("userId", out var userIdValue))
            {
                await _commandHelper.BuildMessage(ctx, $"Pong! 🏓 \n User is: {userIdValue}", "", DiscordColor.SpringGreen);
            }
            else
            {
                await _commandHelper.BuildMessage(ctx, $"Pong! 🏓 \n - Failed to get user", "", DiscordColor.SpringGreen);
            }
        }



        [SlashCommand("UploadFile", "Choose a user and upload a file")]
        public async Task UploadFile(InteractionContext ctx, [Option("DiscordUser", "Choose a discord user")] DiscordUser discordUser, [Option("File", "Upload a file")] DiscordAttachment discordAttachment)
        {
            await _commandHelper.BuildMessage(ctx, "File Uploaded", $"File Name is:{discordAttachment.FileName} - Selected user is: {discordUser.Username}", DiscordColor.SpringGreen);
        }
    }
}

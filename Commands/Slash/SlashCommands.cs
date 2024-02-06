using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotTemplateNet8.Helper;
using System.Drawing;

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

        // Example of simple command that replies to slash command (this one goes off and gets data from API too)
        [SlashCommand("Ping", "Replies with pong!")]
        public async Task Ping(InteractionContext ctx)
        {
            // get response from an API
            var apiResult = await WebHelper.GetJsonFromApi("https://jsonplaceholder.typicode.com/todos/1");

            // Check if the response contains the userId
            if (apiResult.TryGetValue("userId", out var userIdValue))
            {
                await _commandHelper.BuildMessage(ctx, $"Pong! 🏓 \n Placeholder User is: {userIdValue}", "", DiscordColor.SpringGreen);
            }
            else
            {
                await _commandHelper.BuildMessage(ctx, $"Pong! 🏓 \n - Failed to get user", "", DiscordColor.SpringGreen);
            }
        }

        // Example of a command with that lets you upload a file and choose a user
        [SlashCommand("UploadFile", "Choose a user and upload a file")]
        public async Task UploadFile(InteractionContext ctx, [Option("DiscordUser", "Choose a discord user")] DiscordUser discordUser, [Option("File", "Upload a file")] DiscordAttachment discordAttachment)
        {
            await _commandHelper.BuildMessage(ctx, "File Uploaded", $"File Name is:{discordAttachment.FileName} - Selected user is: {discordUser.Username}", DiscordColor.SpringGreen);
        }


        // Example of a command with 3 required string options
        [SlashCommand("SpinTheWheel", "Type in multiple options and ill choose for you")]
        public async Task SpinTheWheel(InteractionContext ctx, [Option("OptionOne", "First choice")] string Option1, [Option("OptionTwo", "Second choice")] string Option2, [Option("OptionThree", "Third choice")] string Option3)
        {
            string[] options = { Option1, Option2, Option3 };

            string randomChoice = options[new Random().Next(options.Length)];

            await _commandHelper.BuildMessage(ctx, $"{randomChoice}", $"{randomChoice} has been chosen!", DiscordColor.SpringGreen);
        }

        // Example of a command that displays the presence of a user
        [SlashCommand("UserStatus", "Print the status of a discord member in this server")]
        public async Task GetUserStatus(InteractionContext ctx, [Option("DiscordUser", "Choose a discord user on this server")] DiscordUser discordUser)
        {
            var member = await ctx.Guild.GetMemberAsync(discordUser.Id);
            var status = member.Presence.Status.ToString();
            var activityType = member.Presence.Activity.ActivityType.ToString();
            string? activityName = member.Presence.Activities.FirstOrDefault(x => x.ActivityType == ActivityType.Playing)?.Name ?? "Nothing";


            // Get the platform of the user using the helper method
            var platform = _commandHelper.GetPlatform(member);

            var description = $"Status: {status}\n Activity: {activityType} {activityName}\n Platform: {platform}";

            await _commandHelper.BuildMessage(ctx, "User Status", description, DiscordColor.SpringGreen);
        }

        // Example of how to get discord user profile picutre
        [SlashCommand("ProfilePicture", "Get the profile picture of a user")]
        public async Task ProfilePicture(InteractionContext ctx, [Option("DiscordUser", "Choose a discord user")] DiscordUser discordUser)
        {
            // Get the member object from the discord user
            // This would be the member object of the guild the command was used in
            var member = await ctx.Guild.GetMemberAsync(discordUser.Id);

            // Defer the response to avoid the "This interaction failed" message
            await ctx.DeferAsync();

            // Create the embed message
            DiscordEmbedBuilder embedMessage = new DiscordEmbedBuilder()
                .WithTitle("Profile Picture")
                .WithDescription($"Profile Picture of {member.DisplayName}")
                .WithImageUrl(discordUser.AvatarUrl)
                .WithThumbnail(discordUser.BannerUrl)
                .WithColor(DiscordColor.SpringGreen);


            // Edit the temporary response from the Defer code above with the embed message
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }
    }
}

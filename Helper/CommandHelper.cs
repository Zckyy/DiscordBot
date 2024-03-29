﻿using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplateNet8.Helper
{
    public class CommandHelper
    {
        public string GetPlatform(DiscordMember member)
        {
            if (member.Presence.ClientStatus.Desktop.HasValue)
                return "Desktop";
            if (member.Presence.ClientStatus.Mobile.HasValue)
                return "Mobile";
            if (member.Presence.ClientStatus.Web.HasValue)
                return "Web";
            return "Unknown";
        }

        public async Task BuildMessage(InteractionContext ctx, string title, string description, DiscordColor color)
        {
            // Defer the response to avoid the "This interaction failed" message
            await ctx.DeferAsync();

            // Create the embed message
            DiscordEmbedBuilder embedMessage = new DiscordEmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color);

            // Edit the temporary response from the Defer code above with the embed message
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }
    }
}

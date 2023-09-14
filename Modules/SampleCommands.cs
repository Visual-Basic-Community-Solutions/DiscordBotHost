using Disqord;
using Disqord.Bot.Commands;
using Disqord.Bot.Commands.Application;
using Disqord.Gateway;
using Qmmands;

namespace DiscordBotHost.Modules
{
    public sealed class SampleCommands : DiscordApplicationModuleBase
    {
        [SlashCommand("ping")]
        [Description("Test application responsiveness.")]
        [RateLimit(1, 5, RateLimitMeasure.Seconds, RateLimitBucketType.Guild)]
        public async Task Ping() => await Response("pong");

        [UserCommand("Show Color")]
        public IResult GetColor(IMember member)
        {
            var memberColor = member.GetRoles().Values.Where(x => x.Color != null).OrderByDescending(x => x.Position).FirstOrDefault()?.Color;
            var embed = new LocalEmbed()
                .WithAuthor(member)
                .WithDescription(memberColor?.ToString() ?? "Member has no color");

            if (memberColor != null)
                embed.WithColor(memberColor.Value);

            return Response(embed);
        }
    }
}

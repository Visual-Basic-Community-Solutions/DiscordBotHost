using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DiscordBotHost.Extensions
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder ConfigureSettings(this MauiAppBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream($"{nameof(DiscordBotHost)}.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(config);

            return builder;
        }
    }
}

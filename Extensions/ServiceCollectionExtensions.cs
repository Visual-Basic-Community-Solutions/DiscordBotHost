using Disqord;
using Disqord.Bot;
using Disqord.Bot.Commands.Application.Default;
using Disqord.Bot.Hosting;
using Disqord.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DiscordBotHost.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDisqord(this IServiceCollection services, IConfiguration config, Action<IConfiguration, DiscordBotHostingContext> configure = null)
        {
            var botContext = new DiscordBotHostingContext();

            configure?.Invoke(config, botContext);

            services.Configure<DefaultApplicationCommandCacheProviderConfiguration>(options => options.DirectoryPath = Path.Combine(FileSystem.Current.AppDataDirectory, options.DirectoryPath));
            services.Configure<DefaultApplicationCommandLocalizerConfiguration>(options => options.DirectoryPath = Path.Combine(FileSystem.Current.AppDataDirectory, options.DirectoryPath));

            services.AddDiscordBot<DisqordBot>();
            services.TryAddSingleton<DiscordBotSetupService>();
            services.AddHostedService(x => x.GetRequiredService<DiscordBotSetupService>());
            services.ConfigureDiscordBot<DiscordBotConfiguration>(null, botContext);

            services.AddDiscordClient();
            services.TryAddSingleton<DiscordClientSetupService>();
            services.AddHostedService(x => x.GetRequiredService<DiscordClientSetupService>());

            services.TryAddSingleton<DiscordClientRunnerService>();
            services.AddHostedService(x => x.GetRequiredService<DiscordClientRunnerService>());

            services.ConfigureDiscordClient(null, botContext);
            return services;
        }
    }
}

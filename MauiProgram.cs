using DiscordBotHost.Extensions;
using DiscordBotHost.Platforms.Android;
using DiscordBotHost.Services;
using DiscordBotHost.Shared;
using Disqord;
using Disqord.Gateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System.Reflection;

namespace DiscordBotHost
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>()
                   .ConfigureSettings()
                   .ConfigureFonts(fonts => fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"));

            builder.Services.UseDisqord(builder.Configuration, (config, bot) => 
            {
                bot.UseMentionPrefix = false;
                bot.Status = UserStatus.Online;
                bot.Intents = GatewayIntents.Guilds;
                bot.ReadyEventDelayMode = ReadyEventDelayMode.Guilds;
                bot.Token = config.GetRequiredSection("Discord").Get<AppSettings>().Token;
                bot.ServiceAssemblies = new List<Assembly> { Assembly.GetExecutingAssembly()! };
                bot.Activities = new[] { new LocalActivity($"via {nameof(DiscordBotHost)} on Android", ActivityType.Playing) };
            });
            
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		    builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(typeof(ILogger<>), typeof(AndroidLogger<>));
            builder.Services.AddSingleton<LoggingService>();
            builder.Services.AddSingleton<ResourceService>();

            return builder.Build();
        }
    }
}
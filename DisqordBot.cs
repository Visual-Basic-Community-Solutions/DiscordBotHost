using Disqord;
using Disqord.Bot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DiscordBotHost
{
    internal class DisqordBot : DiscordBot
    {
        public DisqordBot(IOptions<DiscordBotConfiguration> options, ILogger<DiscordBot> logger, IServiceProvider services, DiscordClient client) 
            : base(options, logger, services, client) { }

        protected override IEnumerable<Assembly> GetModuleAssemblies()
        {
            var assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };

            return assemblies;
        }
    }
}

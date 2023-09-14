using System.Threading.Channels;

namespace DiscordBotHost.Services
{
    public sealed class LoggingService
    {
        private Channel<string> LogChannel { get; set; } = Channel.CreateUnbounded<string>();

        public ChannelReader<string> Reader => LogChannel.Reader;
     
        public bool TryWrite(string message) => LogChannel.Writer.TryWrite(message);

    }
}

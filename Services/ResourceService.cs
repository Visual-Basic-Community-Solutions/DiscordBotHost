using Android.App;
using Android.Content;

namespace DiscordBotHost.Services
{
    public sealed class ResourceService
    {
        public double GetMemoryUsage()
        {
            var activityManager = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);

            ActivityManager.MemoryInfo memoryInfo = new();
            activityManager.GetMemoryInfo(memoryInfo);
            var nativeHeapSize = memoryInfo.TotalMem;
            var nativeHeapFreeSize = memoryInfo.AvailMem;
            var usedMemInBytes = nativeHeapSize - nativeHeapFreeSize;
            var usedMemInPercentage = usedMemInBytes * 100d / nativeHeapSize;

            return usedMemInPercentage;
        }
    }
}

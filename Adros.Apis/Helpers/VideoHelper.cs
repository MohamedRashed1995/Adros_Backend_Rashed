using System;
using System.IO;
using MediaInfo.DotNetWrapper;

namespace Adros.Apis.Helpers
{
    public static class VideoHelper
    {
        public static string GetVideoDuration(string physicalVideoPath)
        {
            if (!File.Exists(physicalVideoPath))
                return "00:00:00";

            var mediaInfo = new MediaInfoWrapper(physicalVideoPath);
            var durationMs = mediaInfo.Duration;

            if (durationMs <= 0)
                return "00:00:00";

            return TimeSpan
                .FromMilliseconds(durationMs)
                .ToString(@"hh\:mm\:ss");
        }
    }
}

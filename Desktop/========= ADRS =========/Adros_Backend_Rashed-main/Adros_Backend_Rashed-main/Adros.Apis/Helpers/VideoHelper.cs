// ================= VideoHelper =================
using System;
using System.Diagnostics;

public static class VideoHelper
{
    /// <summary>
    /// يحسب مدة الفيديو بصيغة "HH:mm:ss"
    /// </summary>
    public static string? GetVideoDuration(string videoPath, string ffprobePath)
    {
        try
        {
            if (!System.IO.File.Exists(videoPath) || !System.IO.File.Exists(ffprobePath))
                return null;

            var startInfo = new ProcessStartInfo
            {
                FileName = ffprobePath,
                Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{videoPath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (double.TryParse(output.Trim(), out double seconds))
            {
                TimeSpan time = TimeSpan.FromSeconds(seconds);
                return time.ToString(@"hh\:mm\:ss");
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}

//using System;
//using System.Diagnostics;
//using System.Drawing;
//using System.IO;

//public class VideoInfo
//{
//    public string? Duration { get; set; }          // "hh:mm:ss"
//    public string? Resolution { get; set; }        // "1920x1080"
//    public string? Codec { get; set; }             // "h264"
//    public double? FrameRate { get; set; }         // 30.0
//    public string? ThumbnailPath { get; set; }     // مسار الصورة المصغرة
//}

//public static class VideoHelper
//{
//    /// <summary>
//    /// يحلل الفيديو باستخدام ffprobe و ffmpeg
//    /// </summary>
//    public static VideoInfo AnalyzeVideo(string videoPath, string ffprobePath, string ffmpegPath, string thumbnailFolder)
//    {
//        var info = new VideoInfo();

//        if (!File.Exists(videoPath) || !File.Exists(ffprobePath))
//            return info;

//        // ===== 1. Duration, Resolution, Codec, FrameRate =====
//        try
//        {
//            var args = $"-v error -select_streams v:0 -show_entries stream=width,height,r_frame_rate,codec_name -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{videoPath}\"";
//            var output = RunProcess(ffprobePath, args);

//            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

//            if (lines.Length >= 5)
//            {
//                if (double.TryParse(lines[4], out double seconds))
//                {
//                    info.Duration = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
//                }

//                info.Resolution = $"{lines[0]}x{lines[1]}";
//                info.FrameRate = ParseFrameRate(lines[2]);
//                info.Codec = lines[3];
//            }
//        }
//        catch
//        {
//            // Ignore errors
//        }

//        // ===== 2. Generate Thumbnail =====
//        try
//        {
//            if (!Directory.Exists(thumbnailFolder))
//                Directory.CreateDirectory(thumbnailFolder);

//            var thumbnailFile = Path.Combine(thumbnailFolder, Guid.NewGuid() + ".jpg");

//            if (File.Exists(ffmpegPath))
//            {
//                var args = $"-i \"{videoPath}\" -ss 00:00:01 -vframes 1 \"{thumbnailFile}\"";
//                RunProcess(ffmpegPath, args);

//                if (File.Exists(thumbnailFile))
//                    info.ThumbnailPath = thumbnailFile;
//            }
//        }
//        catch
//        {
//            // Ignore errors
//        }

//        return info;
//    }

//    private static string RunProcess(string fileName, string arguments)
//    {
//        var startInfo = new ProcessStartInfo
//        {
//            FileName = fileName,
//            Arguments = arguments,
//            RedirectStandardOutput = true,
//            RedirectStandardError = true,
//            UseShellExecute = false,
//            CreateNoWindow = true
//        };

//        using var process = new Process { StartInfo = startInfo };
//        process.Start();
//        string output = process.StandardOutput.ReadToEnd();
//        string error = process.StandardError.ReadToEnd();
//        process.WaitForExit();

//        if (!string.IsNullOrWhiteSpace(error))
//            Console.WriteLine($"[VideoHelper Error] {error}");

//        return output;
//    }

//    private static double? ParseFrameRate(string frameRateStr)
//    {
//        if (string.IsNullOrWhiteSpace(frameRateStr)) return null;

//        var parts = frameRateStr.Split('/');
//        if (parts.Length == 2 && double.TryParse(parts[0], out double num) && double.TryParse(parts[1], out double den))
//            return den != 0 ? num / den : (double?)null;

//        return null;
//    }
//}


namespace Adros.Shared.Settings
{
    public class VideoUploadResponse
    {
        // The unique GUID identifier from Bunny.net
        public Guid Guid { get; set; }

        // ID of the video library
        public long LibraryId { get; set; }

        // Title of the video
        public string Title { get; set; } = default!;

        // Date when the video was created
        public DateTime DateCreated { get; set; }

        // Current video status (e.g., "Uploading")
        public string Status { get; set; } = default!;

        // Indicates if thumbnail capture is enabled
        public bool ThumbnailCaptureEnabled { get; set; }

        // Array of available resolutions
        public string[] AvailableResolutions { get; set; } = [];

        // Length of the video in seconds
        public decimal Length { get; set; }

        // Storage size in bytes
        public long StorageSize { get; set; }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}

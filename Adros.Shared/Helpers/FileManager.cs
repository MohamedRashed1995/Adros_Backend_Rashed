using Microsoft.AspNetCore.Http;
namespace Adros.Shared.Helpers
{
    public static class FileManager
    {
        public static async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty.", nameof(file));

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folderName);


            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        // Delete File

        public static void DeleteFile(string fileName, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folderName);
            string filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}

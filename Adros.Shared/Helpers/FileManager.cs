<<<<<<< HEAD
﻿////using Microsoft.AspNetCore.Http;
////using System;
////using System.IO;
////using System.Linq;
////using System.Threading.Tasks;

////namespace Adros.Shared.Helpers
////{
////    public static class FileManager
////    {
////        // root = wwwroot
////        private static readonly string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

////        // base folders
////        private static readonly string UploadsPath = Path.Combine(WebRootPath, "Uploads");
////        private static readonly string ImagesPath = Path.Combine(UploadsPath, "Images");
////        private static readonly string VideosPath = Path.Combine(UploadsPath, "Videos");
////        private static readonly string PdfPath = Path.Combine(UploadsPath, "Attachments");

////        // allowed extensions
////        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
////        private static readonly string[] VideoExtensions = { ".mp4", ".mov", ".avi" };
////        private static readonly string[] PdfExtensions = { ".pdf" };

////        private const long MaxPdfSize = 10 * 1024 * 1024; // 10 MB

////        static FileManager()
////        {
////            Directory.CreateDirectory(UploadsPath);
////            Directory.CreateDirectory(ImagesPath);
////            Directory.CreateDirectory(VideosPath);
////            Directory.CreateDirectory(PdfPath);
////        }

////        // ==================== IMAGE ====================
////        public static async Task<string> UploadImageAsync(IFormFile file)
////        {
////            ValidateFile(file);
////            var ext = Path.GetExtension(file.FileName).ToLower();
////            if (!ImageExtensions.Contains(ext))
////                throw new ArgumentException("Only image files are allowed.");

////            return await SaveFileAsync(file, ImagesPath);
////        }

////        // ==================== VIDEO ====================
////        public static async Task<string> UploadVideoAsync(IFormFile file)
////        {
////            ValidateFile(file);
////            var ext = Path.GetExtension(file.FileName).ToLower();
////            if (!VideoExtensions.Contains(ext))
////                throw new ArgumentException("Only video files are allowed.");

////            return await SaveFileAsync(file, VideosPath);
////        }

////        // ==================== PDF ====================
////        public static async Task<string> UploadFilePDFAsync(IFormFile file)
////        {
////            ValidateFile(file);
////            var ext = Path.GetExtension(file.FileName).ToLower();
////            if (!PdfExtensions.Contains(ext))
////                throw new ArgumentException("Only PDF files are allowed.");

////            if (file.Length > MaxPdfSize)
////                throw new ArgumentException("PDF size must not exceed 10 MB.");

////            return await SaveFileAsync(file, PdfPath);
////        }
////        // ================== ENSURE FOLDER ==================
////        public static void EnsureFolderExists(string relativePath)
////        {
////            var fullPath = Path.Combine(
////                UploadsPath,
////                relativePath.Replace("/", Path.DirectorySeparatorChar.ToString())
////            );

////            if (!Directory.Exists(fullPath))
////                Directory.CreateDirectory(fullPath);
////        }

////        // ================== DELETE FILE ==================
////        public static void DeleteFile(string folderName, string fileName)
////        {
////            if (string.IsNullOrWhiteSpace(fileName)) return;

////            var filePath = Path.Combine(
////                UploadsPath,
////                folderName.Replace("/", Path.DirectorySeparatorChar.ToString()),
////                fileName
////            );

////            if (File.Exists(filePath))
////                File.Delete(filePath);
////        }

////        // ================== FLEXIBLE UPLOAD (GENERIC) ==================
////        public static async Task<string> UploadFileAsync(IFormFile file, string folderName)
////        {
////            ValidateFile(file);

////            var ext = Path.GetExtension(file.FileName).ToLower();
////            if (!ImageExtensions.Concat(VideoExtensions).Concat(PdfExtensions).Contains(ext))
////                throw new ArgumentException("File type not allowed.");

////            var targetFolder = Path.Combine(
////                UploadsPath,
////                folderName.Replace("/", Path.DirectorySeparatorChar.ToString())
////            );

////            Directory.CreateDirectory(targetFolder);

////            return await SaveFileAsync(file, targetFolder);
////        }

////        // ==================== HELPERS ====================
////        private static async Task<string> SaveFileAsync(IFormFile file, string folderPath)
////        {
////            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
////            var filePath = Path.Combine(folderPath, fileName);

////            using var stream = new FileStream(filePath, FileMode.Create);
////            await file.CopyToAsync(stream);

////            return fileName;
////        }

////        private static void ValidateFile(IFormFile file)
////        {
////            if (file == null || file.Length == 0)
////                throw new ArgumentException("File is null or empty.");
////        }
////    }
////}
//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Adros.Shared.Helpers
//{
//    public static class FileManager
//    {
//        // ================== ROOT ==================
//        // المسار الأساسي لwwwroot على السيرفر
//        private static readonly string WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

//        // ================== BASE FOLDERS ==================
//        private static readonly string UploadsPath = Path.Combine(WebRootPath, "Uploads");
//        private static readonly string ImagesPath = Path.Combine(UploadsPath, "Images");
//        private static readonly string VideosPath = Path.Combine(UploadsPath, "Videos");
//        private static readonly string PdfPath = Path.Combine(UploadsPath, "Attachments");

//        // ================== ALLOWED ==================
//        private static readonly string[] AllowedExtensions =
//        {
//            ".jpg", ".jpeg", ".png", ".gif",
//            ".pdf",
//            ".mp4", ".mov", ".avi"
//        };

//        private static readonly string[] AllowedPdfTypes = { "application/pdf" };
//        private const long MaxPdfSize = 10 * 1024 * 1024; // 10 MB

//        // ================== INIT ==================
//        static FileManager()
//        {
//            Directory.CreateDirectory(UploadsPath);
//            Directory.CreateDirectory(ImagesPath);
//            Directory.CreateDirectory(VideosPath);
//            Directory.CreateDirectory(PdfPath);
//        }

//        // ================== ENSURE FOLDER ==================
//        public static void EnsureFolderExists(string relativePath)
//        {
//            var fullPath = Path.Combine(WebRootPath, "Uploads", relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
//            if (!Directory.Exists(fullPath))
//                Directory.CreateDirectory(fullPath);
//        }

//        // ================== UPLOAD METHODS ==================

//        public static async Task<string> UploadFileAsync(IFormFile file, string folderName)
//        {
//            ValidateFile(file);

//            var ext = Path.GetExtension(file.FileName).ToLower();
//            if (!AllowedExtensions.Contains(ext))
//                throw new ArgumentException("File type not allowed.");

//            var targetFolder = Path.Combine(WebRootPath, "Uploads", folderName.Replace("/", Path.DirectorySeparatorChar.ToString()));
//            Directory.CreateDirectory(targetFolder);

//            return await SaveFileAsync(file, targetFolder);
//        }

//        public static async Task<string> UploadImageAsync(IFormFile file)
//            => await UploadGenericAsync(file, ImagesPath);

//        public static async Task<string> UploadVideoAsync(IFormFile file)
//            => await UploadGenericAsync(file, VideosPath);

//        public static async Task<string> UploadFilePDFAsync(IFormFile file)
//        {
//            ValidateFile(file);

//            if (!AllowedPdfTypes.Contains(file.ContentType))
//                throw new ArgumentException("Only PDF files are allowed.");

//            if (file.Length > MaxPdfSize)
//                throw new ArgumentException("PDF size must not exceed 10 MB.");

//            return await SaveFileAsync(file, PdfPath);
//        }

//        // ================== DELETE ==================
//        public static void DeleteFile(string folderName, string fileName)
//        {
//            if (string.IsNullOrWhiteSpace(fileName)) return;

//            var filePath = Path.Combine(WebRootPath, "Uploads", folderName.Replace("/", Path.DirectorySeparatorChar.ToString()), fileName);
//            if (File.Exists(filePath))
//                File.Delete(filePath);
//        }

//        // ================== HELPERS ==================
//        private static async Task<string> UploadGenericAsync(IFormFile file, string folderPath)
//        {
//            ValidateFile(file);
//            var ext = Path.GetExtension(file.FileName).ToLower();
//            if (!AllowedExtensions.Contains(ext))
//                throw new ArgumentException("File type not allowed.");

//            return await SaveFileAsync(file, folderPath);
//        }

//        private static async Task<string> SaveFileAsync(IFormFile file, string folderPath)
//        {
//            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
//            var filePath = Path.Combine(folderPath, fileName);

//            using var stream = new FileStream(filePath, FileMode.Create);
//            await file.CopyToAsync(stream);

//            return fileName;
//        }

//        private static void ValidateFile(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                throw new ArgumentException("File is null or empty.");
//        }
//    }
//}


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
namespace Adros.Shared.Helpers
{
    public static class FileManager
    {
<<<<<<< HEAD
        private static string _webRootPath = string.Empty;

        // Base folders relative to wwwroot/Uploads
        private const string ImagesFolder = "Images";
        private const string VideosFolder = "Videos";
        private const string PdfFolder = "Attachments";

        // Allowed extensions
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private static readonly string[] VideoExtensions = { ".mp4", ".mov", ".avi" };
        private static readonly string[] PdfExtensions = { ".pdf" };

        private static readonly string[] AllowedExtensions = ImageExtensions.Concat(VideoExtensions).Concat(PdfExtensions).ToArray();

        private const long MaxPdfSize = 10 * 1024 * 1024; // 10 MB

        // ================== INIT ==================
        public static void Init(IWebHostEnvironment env)
        {
            _webRootPath = env.WebRootPath;

            // لو في wwwroot/wwwroot بالغلط، نقص واحدة
            if (Path.GetFileName(_webRootPath).Equals("wwwroot", StringComparison.OrdinalIgnoreCase) &&
                Path.GetFileName(Path.GetDirectoryName(_webRootPath)).Equals("wwwroot", StringComparison.OrdinalIgnoreCase))
            {
                _webRootPath = Path.GetDirectoryName(_webRootPath)!;
            }

            EnsureFoldersExist();
        }


        public static void EnsureFoldersExist()
        {
            Directory.CreateDirectory(Path.Combine(_webRootPath, "Uploads"));
            Directory.CreateDirectory(Path.Combine(_webRootPath, "Uploads", ImagesFolder));
            Directory.CreateDirectory(Path.Combine(_webRootPath, "Uploads", VideosFolder));
            Directory.CreateDirectory(Path.Combine(_webRootPath, "Uploads", PdfFolder));
        }

        // ================== UPLOAD ==================
        public static async Task<string> UploadFileAsync(IFormFile file, string relativeFolder)
        {
            ValidateFile(file);

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(ext))
                throw new ArgumentException("File type not allowed.");

            var folderPath = Path.Combine(_webRootPath, "Uploads", relativeFolder);
            Directory.CreateDirectory(folderPath);

            return await SaveFileAsync(file, folderPath);
        }

        public static async Task<string> UploadImageAsync(IFormFile file)
        {
            ValidateFile(file);
            return await SaveFileAsync(file, Path.Combine(_webRootPath, "Uploads", ImagesFolder));
        }

        public static async Task<string> UploadVideoAsync(IFormFile file)
        {
            ValidateFile(file);
            return await SaveFileAsync(file, Path.Combine(_webRootPath, "Uploads", VideosFolder));
        }

        public static async Task<string> UploadFilePDFAsync(IFormFile file)
        {
            ValidateFile(file);

            if (!PdfExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                throw new ArgumentException("Only PDF files are allowed.");

            if (file.Length > MaxPdfSize)
                throw new ArgumentException("PDF size must not exceed 10 MB.");

            return await SaveFileAsync(file, Path.Combine(_webRootPath, "Uploads", PdfFolder));
        }

        // ================== DELETE ==================
        public static void DeleteFile(string relativeFolder, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            var filePath = Path.Combine(_webRootPath, "Uploads", relativeFolder, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        // ================== HELPERS ==================
        private static async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }


        private static void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty.");
        }

        // ================== ENSURE SPECIFIC FOLDER ==================
        public static void EnsureFolderExists(string relativeFolder)
        {
            var folderPath = Path.Combine(_webRootPath, "Uploads", relativeFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
    }
}
=======
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
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

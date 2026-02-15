using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            IFormFile? file = value as IFormFile;

           
            if (file == null)
                return ValidationResult.Success;

           
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            
            if (string.IsNullOrEmpty(extension) || !_extensions.Contains(extension))
            {
               
                return new ValidationResult($"This photo extension is not allowed! Allowed extensions are: {string.Join(", ", _extensions)}.");
            }

            
            return ValidationResult.Success;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Adros.Application.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
    {
        private readonly int _maxFileSize = maxFileSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            IFormFile? file = value as IFormFile;


            if (file == null)
                return ValidationResult.Success;

            if (file.Length > _maxFileSize)
            {

                string maxSizeInMB = (_maxFileSize / (1024 * 1024)).ToString();
                return new ValidationResult($"Maximum allowed file size is {maxSizeInMB} MB.");
            }

            return ValidationResult.Success;
        }
    }
}

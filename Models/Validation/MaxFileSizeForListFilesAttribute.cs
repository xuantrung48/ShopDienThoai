using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models.Validation
{
    public class MaxFileSizeForListFilesAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeForListFilesAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var files = value as IList<IFormFile>;
            foreach (var file in files)
            {
                if (file != null)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }


            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Kích thước của tập tin là quá lớn, kích cỡ cho phép là { _maxFileSize / 1024 / 1024} MB.";
        }
    }
}

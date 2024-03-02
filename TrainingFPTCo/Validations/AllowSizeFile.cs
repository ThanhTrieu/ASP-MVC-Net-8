using System.ComponentModel.DataAnnotations;

namespace TrainingFPTCo.Validations
{
    public class AllowSizeFile : ValidationAttribute
    {
        private readonly int _maxSizeFile;
        public AllowSizeFile(int maxSizeFile)
        {
            _maxSizeFile = maxSizeFile;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxSizeFile)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return $"Maximum a allowed file size is {_maxSizeFile} bytes. ";
        }
    }
}

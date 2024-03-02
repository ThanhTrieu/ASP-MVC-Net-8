using System.ComponentModel.DataAnnotations;

namespace TrainingFPTCo.Validations
{
    public class AllowExtensionFile : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowExtensionFile(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return "This photo extension is not allowed !";
        }
    }
}

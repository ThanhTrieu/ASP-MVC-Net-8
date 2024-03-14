using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TrainingFPTCo.Validations;

namespace TrainingFPTCo.Models
{
    public class CategoryViewModel
    {
        public List<CategoryDetail> CategoryDetailList { get; set; }
    }
    
    public class CategoryDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name's Category, please")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage ="Choose Status, please")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Choose file, please")]
        [AllowExtensionFile(new string[] { ".png", ".jpg", ".jpeg" })]
        [AllowSizeFile(5 * 1024 * 1024)]
        public IFormFile? PosterImage { get; set; }

        // view ten anh
        [AllowNull]
        public string? PosterNameImage { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

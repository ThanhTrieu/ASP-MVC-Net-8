using System.ComponentModel.DataAnnotations;
using TrainingFPTCo.Validations;

namespace TrainingFPTCo.Models
{
    public class CourseViewModel
    {
        public List<CourseDetail> CourseDetailsList { get; set; }
    }

    public class CourseDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose Category, please")]
        public int CategoryId { get; set; }

        public string? ViewCategoryName { get; set; }

        [Required(ErrorMessage = "Enter name's course, please")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string? ViewStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string? ViewEndDate { get; set; }

        [Required(ErrorMessage = "Choose file image, please")]
        [AllowExtensionFile(new string[] {".png", ".jpg", ".jpeg"})]
        [AllowSizeFile(5 * 1024 * 1024)]
        public IFormFile Image { get; set; }

        public string? ViewImageCouser {  get; set; }

        public int? LikeCourse {  get; set; }

        public int? StarCourse { get; set; }

        [Required(ErrorMessage = "Choose Status, please")]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}

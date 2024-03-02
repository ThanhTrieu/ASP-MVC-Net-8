using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TrainingFPTCo.DataDBContext
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column("Name", TypeName ="Varchar(50)"), Required]
        public string Name { get; set; }

        [Column("Description", TypeName="Varchar(200)"), AllowNull]
        public string? Description { get; set; }

        [Column("ParentId", TypeName ="integer"), Required]
        public int ParentId { get; set; }

        [Column("PosterImage", TypeName="Varchar(200)"), Required]
        public string PosterImage { get; set; }

        [Column("Status", TypeName = "Varchar(20)"), Required]
        public string Status { get; set; }

        [AllowNull]
        public DateTime? CreatedAt { get; set; }

        [AllowNull]
        public DateTime? UpdatedAt { get; set; }

        [AllowNull]
        public DateTime? DeletedAt { get; set; }
    }
}

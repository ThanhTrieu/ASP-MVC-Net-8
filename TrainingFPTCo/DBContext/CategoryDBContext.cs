using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TrainingFPTCo.DBContext
{
    public class CategoryDBContext
    {
        [Key] 
        public int Id { get; set; }

        [Column("Name", TypeName ="Varchar(100)")]
        public string? Name { get; set; }

        [Column("Description", TypeName ="Varchar(200)")]
        public string? Description { get; set; }

        [Column("PosterImage", TypeName ="Varchar(200)")]
        public string? PosterImage { get; set; }

        [Column("ParentId", TypeName ="Integer")]
        public int? ParentId { get; set; }

        [Column("Status", TypeName = "Varchar(20)")]
        public string? Status { get; set; }

        [AllowNull]
        public DateTime? CreatedAt { get; set; }
        [AllowNull]
        public DateTime? UpdatedAt { get; set; }
        [AllowNull]
        public DateTime? DeletedAt { get; set; }
    }
}

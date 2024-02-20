using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingFPTCo.DataDBContext
{
    public class Users
    {
        [Key] 
        public int Id { get; set; }

        [ForeignKey("RolesId"), Required]
        public required Roles Roles { get; set; }

        [Column("Username", TypeName="Varchar(100)"), Required]
        public required string UserName { get; set; }

        [Column("Password", TypeName = "Varchar(100)"), Required]
        public required string Password { get; set; }

        [Column("Extracode", TypeName = "Varchar(100)"), Required]
        public required string Extracode { get; set; }

        [Column("Email", TypeName = "Varchar(100)"), Required]
        public required string Email { get; set; }

        [Column("Phone", TypeName = "Varchar(20)"), Required]
        public required string Phone { get; set; }
    }
}

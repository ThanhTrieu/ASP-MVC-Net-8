namespace TrainingFPTCo.Models
{
    public class LoginViewModel
    {
        public required int Id { get; set; }
        public required int RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set;}

    }
}

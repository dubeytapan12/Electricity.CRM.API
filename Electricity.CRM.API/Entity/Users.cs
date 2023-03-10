using System.ComponentModel.DataAnnotations;

namespace Electricity.CRM.API.Entity
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        public string? ForgotPasswordToken { get; set; }
    }
}

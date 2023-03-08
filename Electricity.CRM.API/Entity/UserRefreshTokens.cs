using System.ComponentModel.DataAnnotations;

namespace Electricity.CRM.API.Entity
{
    public class UserRefreshTokens
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

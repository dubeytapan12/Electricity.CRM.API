using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electricity.CRM.API.Entity
{
    public class ForgotPassword
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public DateTime expiry { get; set; }

        public virtual User User { get; set; }
    }
}

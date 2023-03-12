using System.ComponentModel.DataAnnotations;
using System;

namespace Electricity.CRM.API.Entity
{
    public class ElectricityUserCommercial
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FName { get; set; }
        [Required]
        [StringLength(100)]
        public string LName { get; set; }
        [Required]
        public DateTime ConnectionDate { get; set; }
        [Required]
        [StringLength(30)]
        public string Mobile { get; set; }
        public bool IsUsingNewMeeter { get; set; }
        [StringLength(100)]
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        [StringLength(100)]
        public string AadharNumber { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        [StringLength(100)]
        public string state { get; set; }
        [Required]
        public int Pincode { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
    }
}

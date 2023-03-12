using System.ComponentModel.DataAnnotations;
using System;

namespace Electricity.CRM.API.Dtos
{
    public class ElectricityUserDtos
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime ConnectionDate { get; set; }
        public string Mobile { get; set; }
        public bool IsUsingNewMeeter { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string AadharNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string state { get; set; }
        public int Pincode { get; set; }
        public string Notes { get; set; }
    }
}

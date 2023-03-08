using System.ComponentModel.DataAnnotations;

namespace Electricity.CRM.API.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

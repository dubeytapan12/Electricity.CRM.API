namespace Electricity.CRM.API.Dtos
{
    public class ResetPasswordDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}

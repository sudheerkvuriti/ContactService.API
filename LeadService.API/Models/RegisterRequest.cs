namespace LeadService.API.Models
{
    public class RegisterRequest
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User"; // default
    }
}

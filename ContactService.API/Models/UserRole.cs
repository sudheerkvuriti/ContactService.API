namespace ContactServices.API.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; }   // Admin, HR, Recruiter, User, etc.

        // Navigation
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

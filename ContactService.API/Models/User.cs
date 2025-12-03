namespace ContactServices.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        // Store ONLY the hashed password — NEVER store plain text password
        public string PasswordHash { get; set; }

        // A user can have multiple roles (Admin, HR, Recruiter)
        public List<UserRole> Roles { get; set; } = new List<UserRole>();

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

using ContactServices.API.Models;

namespace ContactServices.API.Services
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string email, string password);
    }
}

using Entities;

namespace Services
{
    public interface IUserService
    {
        int CheckPasword(string password);
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<User> GetUserToLogin(string email, string password);
        Task<User> UpDateUser(int id, User userToUpdate);
    }
}
//change from Nechami
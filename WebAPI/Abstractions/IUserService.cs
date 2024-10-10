using WebAPI.Entities;

namespace WebAPI.Abstractions
{
    public interface IUserService
    {
        IEnumerable<User>? GetAllUsers();
        User CreateUser(User user);
        User? UpdateUser(User user);
        void DeleteUser(int id);
    }
}

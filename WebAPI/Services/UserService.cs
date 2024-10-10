using WebAPI.Abstractions;
using WebAPI.Entities;
using WebAPI.Repositories;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User>? GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User CreateUser(User user)
        {
            return _userRepository.Create(user);
        }

        public User? UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Remove(id);
        }
    }
}

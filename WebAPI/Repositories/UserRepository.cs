using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Entities;

namespace WebAPI.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? Get(int id)
        {
            return _context.Users.Include(x => x.Company).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User>? GetAll()
        {
            return _context.Users.Include(x => x.Company);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Update(User user)
        {
            User? userFromDb = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (userFromDb == null)
                return null;
            userFromDb.Name = user.Name;
            userFromDb.Age = user.Age;
            userFromDb.CompanyId = user.CompanyId;
            _context.Entry(userFromDb).State = EntityState.Modified;
            _context.SaveChanges();
            return userFromDb;
        }
        public void Remove(int id)
        {
            _context.Users.Remove(_context.Users.First(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}

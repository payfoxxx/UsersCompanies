using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Entities;

namespace WebAPI.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public Company? Get(int id)
        {
            return _context.Companies.Include(x => x.Users).FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Company>? GetAll()
        {
            return _context.Companies.Include(x => x.Users);
        }

        public Company Create(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public Company? Update(Company company)
        {
            Company? companyFromDb = _context.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (companyFromDb == null)
                return null;
            companyFromDb.NameCompany = company.NameCompany;
            _context.Entry(companyFromDb).State = EntityState.Modified;
            _context.SaveChanges();
            return companyFromDb;
        }
        public void Remove(int id)
        {
            _context.Remove(_context.Companies.First(x => x.Id == id));
            _context.SaveChanges();
        }
    }
}

using WebAPI.Entities;
using WebAPI.Repositories;

namespace WebAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;

        public CompanyService(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IEnumerable<Company>? GetAllCompanies()
        {
            return _companyRepository.GetAll();
        }

        public Company CreateCompany(Company company)
        {
            return _companyRepository.Create(company);
        }

        public Company? UpdateCompany(Company company)
        {
            return _companyRepository.Update(company);
        }

        public void DeleteCompany(int id)
        {
            _companyRepository.Remove(id);
        }
    }
}

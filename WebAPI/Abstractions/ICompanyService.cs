using WebAPI.Entities;

namespace WebAPI.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company>? GetAllCompanies();
        Company CreateCompany(Company company);
        Company? UpdateCompany(Company company);
        void DeleteCompany(int id);
    }
}

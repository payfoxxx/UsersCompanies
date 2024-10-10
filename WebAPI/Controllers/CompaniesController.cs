using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<Company>? GetAllCompanies()
        {
            return _companyService.GetAllCompanies();
        }

        [HttpPost]
        public Company AddCompany([FromBody] Company company)
        {
            return _companyService.CreateCompany(company);
        }

        [HttpPut]
        public IActionResult EditCompany([FromBody] Company company)
        {
            _companyService.UpdateCompany(company);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveCompany([FromRoute] int id)
        {
            try
            {
                _companyService.DeleteCompany(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}

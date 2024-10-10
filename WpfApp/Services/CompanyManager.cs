using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class CompanyManager
    {
        private readonly ApiClient _client;
        public CompanyManager()
        {
            _client = new ApiClient("http://localhost:5011/companies/");
        }

        public async Task<List<Company>?> GetAllCompanies()
        {
            var response = await _client.Get();
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return DesserializeListFromJson(responseBody);
            }
            else
            {
                var responseError = await response.Content.ReadAsStringAsync();
                throw new Exception(responseError.ToString());
            }
        }

        public async Task<Company?> CreateCompany(Company company)
        {
            string? jsonObject = SerializeToJson(company);
            if (jsonObject == null)
                throw new ArgumentNullException("Компания не может быть пустой");
            HttpContent content = ConfigureContent(jsonObject);
            HttpResponseMessage responseMessage = await _client.Create(content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                return DeserializeObjectFromJson(responseBody);
            }
            else
            {
                var responseError = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception(responseError.ToString());
            }
        }

        public async Task<Company?> UpdateCompany(Company company)
        {
            string? jsonObject = SerializeToJson(company);
            if (jsonObject == null)
                throw new ArgumentNullException("Компания не может быть пустой");
            HttpContent content = ConfigureContent(jsonObject);
            HttpResponseMessage responseMessage = await _client.Update(content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                return DeserializeObjectFromJson(responseBody);
            }
            else
            {
                var responseError = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception(responseError.ToString());
            }
        }

        public async Task<bool> RemoveCompany(int companyId)
        {
            HttpResponseMessage responseMessage = await _client.Delete(companyId);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private HttpContent ConfigureContent(string jsonObject)
        {
            HttpContent content = new StringContent(jsonObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return content;
        }

        private string? SerializeToJson(Company company)
        {
            return JsonConvert.SerializeObject(company);
        }

        private Company? DeserializeObjectFromJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<Company>(jsonObject);
        }

        private List<Company>? DesserializeListFromJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<List<Company>>(jsonObject);
        }
    }
}

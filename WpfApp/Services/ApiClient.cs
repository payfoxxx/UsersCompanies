using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<HttpResponseMessage> Get()
        {
            return await _client.GetAsync(_client.BaseAddress);
        }

        public async Task<HttpResponseMessage> Create(HttpContent content)
        {
            return await _client.PostAsync(_client.BaseAddress, content);
        }

        public async Task<HttpResponseMessage> Update(HttpContent content)
        {
            return await _client.PutAsync(_client.BaseAddress, content);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await _client.DeleteAsync(id.ToString());
        }
    }
}

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
    public class UserManager
    {
        private readonly ApiClient _client;
        public UserManager()
        {
            _client = new ApiClient("http://localhost:5011/users/");
        }

        public async Task<List<User>?> GetAllUsers()
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

        public async Task<User?> CreateUser(User user)
        {
            string? jsonObject = SerializeToJson(user);
            if (jsonObject == null)
                throw new ArgumentNullException("Пользователь не может быть пустым");
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

        public async Task<User?> UpdateUser(User user)
        {
            string? jsonObject = SerializeToJson(user);
            if (jsonObject == null)
                throw new ArgumentNullException("Пользователь не может быть пустым");
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

        public async Task<bool> RemoveUser(int userId)
        {
            HttpResponseMessage responseMessage = await _client.Delete(userId);
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

        private string? SerializeToJson(User user)
        {
            return JsonConvert.SerializeObject(user);
        }

        private User? DeserializeObjectFromJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<User>(jsonObject);
        }

        private List<User>? DesserializeListFromJson(string jsonObject)
        {
            return JsonConvert.DeserializeObject<List<User>>(jsonObject);
        }
    }
}

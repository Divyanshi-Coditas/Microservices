using EmployeeApiConsumer.Models;
using System.Net.Http.Headers;

namespace EmployeeApiConsumer.Services
{
    public class AccountServices
    {

        readonly HttpClient _client;
        readonly string baseUrl = "https://localhost:7181/";
        readonly IHttpContextAccessor _HttpContextAccessor;
        public AccountServices(HttpClient client, IHttpContextAccessor HttpContextAccessor)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseUrl);
            _HttpContextAccessor = HttpContextAccessor;
            var token = _HttpContextAccessor!.HttpContext!.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<bool> RegisterNewUser(User RegisterUser)
        {
            var result = await _client.PostAsJsonAsync($"Register", RegisterNewUser);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

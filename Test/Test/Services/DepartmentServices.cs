
using EmployeeApiConsumer.Models;
using System.Net.Http.Headers;

namespace EmployeeApiConsumer.Services
{
    public class DepartmentServices
    {
        readonly HttpClient _client;
        readonly string baseUrl = "https://localhost:7181/";
        readonly IHttpContextAccessor _HttpContextAccessor;
        public DepartmentServices(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _HttpContextAccessor = httpContextAccessor;
            var token = _HttpContextAccessor!.HttpContext!.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var departments = await _client.GetFromJsonAsync<List<Department>>($"{baseUrl}Getalldepartments");
            if (departments == null)
            {
                throw new Exception("There are no Departments");
            }
            return departments;
        }
      
    }
}

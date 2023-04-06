using EmployeeApiConsumer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace EmployeeApiConsumer.Services
{
    public class EmployeeServices
    {
        readonly HttpClient _client;
        readonly string baseUrl = "https://localhost:7181/";
        readonly IHttpContextAccessor _HttpContextAccessor;
        public EmployeeServices(HttpClient client, IHttpContextAccessor HttpContextAccessor) 
        {
            _client =client;
            _client.BaseAddress = new Uri(baseUrl);
            _HttpContextAccessor = HttpContextAccessor;
            var token = _HttpContextAccessor!.HttpContext!.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = await _client.GetFromJsonAsync<List<Employee>>($"GetAllEmployees");
            if(employees == null )
            {
                throw new Exception("There are no Employee records");
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee= await _client.GetFromJsonAsync<Employee>($"GetEmployeesById/" + employeeId);
            if (employee == null)
            {
                throw new Exception($"There are no Employee record found with id{employeeId}");
            }
            return employee;

        }

        public async Task<bool> AddEmployeeAsync(Employee employee) 
        {
            var result = await _client.PostAsJsonAsync($"CreateEmployee", employee);
            if(result.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditEmployeeAsync(Employee employee)
        {
            var respose = await _client.PutAsJsonAsync<Employee>($"UpdateEmployee", employee);
            if (respose.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeletEmployeeAsync(int id)
        {
            var respose = await _client.DeleteAsync($"DeleteEmployee?id=" + id);
            if (respose.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> PatchEmployee(JsonPatchDocument patchDocument,int id)
        {
            var serializeDoc = JsonConvert.SerializeObject(patchDocument);
            var request = new StringContent(serializeDoc, Encoding.UTF8, "application/json-patch+json");
            var PatchUpdate = await _client.PatchAsync("PatchEmployee/"+id, request);
            return PatchUpdate.IsSuccessStatusCode;
        }
    }
}

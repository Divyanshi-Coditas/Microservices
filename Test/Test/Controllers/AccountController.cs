using EmployeeApiConsumer.Models;
using EmployeeApiConsumer.Services;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeApiConsumer.Controllers
{
    public class AccountController : Controller
    {
        readonly HttpClient _client;
        readonly string baseUrl = "https://localhost:7181/";
        readonly AccountServices _accountServices;

        public AccountController(HttpClient client, AccountServices accountServices)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseUrl);
            _accountServices = accountServices;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> UserLogin(LoginEntity entity)
        {
            try
            {
                entity.IPAddress = GetIp();
                var result = await _client.PostAsJsonAsync($"Login", entity);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseModel = await result.Content.ReadAsAsync<AuthenticationResponse>();
                    if (responseModel!=null)
                    {
                        HttpContext.Session.SetString("token", responseModel.Token);
                        HttpContext.Session.SetString("UserName", entity.UserName.ToUpper());                       
                        return RedirectToAction("Navigation", "Comman");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    TempData["CredentialFailed"] = "Login Failed! Incorrect Username or password";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult RegisterUser()
        {
            return View();
        }
        public async Task<IActionResult> Register(User RegisterModel)
        {
            try
            {
                    var result = await  _accountServices.RegisterNewUser(RegisterModel);
                    if (result)
                    {
                        return View(result);
                    }

                return RedirectToAction("Login");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public  IActionResult UserLogout()
        {
            try
            {
                HttpContext.Session.Remove("token");
                return RedirectToAction("Login");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetIp()
        {
            try
            {
                string ipAddress = Request.Headers["X-Forwarded-For"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
                }
                return ipAddress;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

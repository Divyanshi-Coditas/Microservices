using EmployeeApiConsumer.Entities.Models.EntityModels;
using EmployeeManagement.Api.Helper;
using EmployeeManagement.Entities.Models.EntityModels;
using EmployeeManagment.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly JwtTokenGenrator _tokenGenrator;
        private readonly Hashing _hash;
        public AccountController(Hashing hash, JwtTokenGenrator tokenGenrator, AccountService accountService)
        {
            _hash = hash;
            _tokenGenrator = tokenGenrator;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(LoginModel entity)
        {           
            var audit = new LoginAudit();
            audit.Auditype = "Login";
            audit.CreatedOn=DateTime.Now;
            audit.Ipaddress = entity.IPAddress;
            var user = _accountService.GetUser(entity);
            if (user == null)
            {
                audit.Username = entity.UserName;
                audit.Auditstatus = "Login Failed";
                _accountService.LoginAudit(audit);
                return BadRequest();
            }
          
            var isValid = _hash.VerifyHash(user.Password, entity.Password);
            if(!isValid)
            {
                audit.Username = entity.UserName;
                audit.Auditstatus = "Login Failed";
                _accountService.LoginAudit(audit);
                return Unauthorized();
            }
            audit.Username = user.UserName;
            audit.Auditstatus = "Login Success";
            _accountService.LoginAudit(audit);
            var response = _tokenGenrator.GenerateJSONWebToken(user.UserName);
            return Ok(response);
        }

        [Route("/Register")]
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.Password = _hash.OneWayHash(user.Password);
            var res = _accountService.CreateUser(user);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

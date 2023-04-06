using EmployeeApiConsumer.Entities.Models.EntityModels;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Models.EntityModels;
using Serilog;

namespace EmployeeManagment.Services.Account
{
    public class AccountService
    {
        private readonly EmployeeManagementContext _employeeManagementContext;
        private readonly ILogger _logger;
        public AccountService(EmployeeManagementContext employeeManagementContext)
        {
            _employeeManagementContext = employeeManagementContext;
            _logger = Log.ForContext<AccountService>();
        }

        public User GetUser(LoginModel entity)
        {

            _logger.Information("Attempt for Logn In");
            var res = _employeeManagementContext.Users.ToList();
            var user = (from users in res
                        where users.UserName == entity.UserName.ToLower() || users.EmailId == entity.UserName
                        select users).SingleOrDefault();

            if (user == null)
            {
                _logger.Information("invalid Login attempt!!");
            }
            else
            {
                _logger.Information("\"" + entity.UserName + "\" logged in successfully");

            }
            return user!;
        }

        public void LoginAudit(LoginAudit audit)
        {
            _employeeManagementContext.LoginAudits.Add(audit);
            _employeeManagementContext.SaveChanges();
        }

        public User CreateUser(User user)
        {
            _logger.Information("Creating new User.");
            user.UserName = user.UserName.ToLower();
            var res = _employeeManagementContext.Users.Add(user);
            _employeeManagementContext.SaveChanges();
            _logger.Information("user created sucessfully with ID: " + user.Id);
            return res.Entity;
        }
    }
}

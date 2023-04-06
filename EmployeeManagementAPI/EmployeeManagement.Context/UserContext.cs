using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using System.Threading.Tasks;

namespace EmployeeManagement.Context
{

    public class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }


        public string GetUserNameFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault();

            token = token!.Split(" ")[1];
            if (token != null)
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                return userId?.Value!;
            }

            return null!;
        }

    }

}

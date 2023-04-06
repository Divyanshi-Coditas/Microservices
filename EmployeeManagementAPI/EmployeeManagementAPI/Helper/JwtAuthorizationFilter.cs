using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Api.Helper
{
    public class JwtAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public JwtAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult("Unauthorized")
                {
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized
                };

                return;
            }

            var token = authHeader.Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(ex.Message)
                {
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized
                };

                return;
            }
        }
    }
}

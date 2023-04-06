using EmployeeManagement.Context;
using EmployeeManagement.Entities.Models.DTOModels;
using EmployeeManagement.Entities.Models.EntityModels;
using EmployeeManagement.Repositories;
using EmployeeManagment.Services.Account;
using EmployeeManagment.Services.CorrelationId;
using EmployeeManagment.Services.Departments;
using EmployeeManagment.Services.Employees;
using EmployeeManagment.Services.RedisCache;
using EmployeeManagment.Services.Services;


namespace EmployeeManagement.Api.Helper
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDbEmployeeService<Employee, EmployeeDTO, int>, EmployessService>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<EmployessService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<Hashing>();
            services.AddScoped<JwtTokenGenrator>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<AccountService>();
            services.AddScoped<JwtAuthorizationFilter>();
            services.AddHttpContextAccessor();
            services.AddScoped<UserContext>();
            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
                        
            return services;
        }
       

    }
}

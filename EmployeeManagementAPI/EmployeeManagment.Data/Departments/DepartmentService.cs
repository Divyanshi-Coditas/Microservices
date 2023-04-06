using EmployeeManagement.Context;
using EmployeeManagement.Entities.Models.DTOModels;
using EmployeeManagement.Entities.Models.EntityModels;
using EmployeeManagement.Repositories;
using EmployeeManagment.Services;
using EmployeeManagment.Services.Services;
using Microsoft.AspNetCore.JsonPatch;
using Serilog;
using StackExchange.Redis;

namespace EmployeeManagment.Services.Departments
{
    public class DepartmentService : IDbDepartmentAccess<Department , DepartmentDTO, int>
    {
        private readonly DepartmentRepository _repository;
        private readonly ICacheService _cacheService;

        private readonly ILogger _logger;

        public DepartmentService(DepartmentRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;

            _logger = Log.ForContext<DepartmentService>();
        }

        public IEnumerable<Department> GetAll()
        {
            _logger.Information("Attempt for Getting all Departments..");
            var cacheData = _cacheService.GetData<IEnumerable<Department>>("DepartmentKey");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = _repository.GetAllDepartments();
            _cacheService.SetData("DepartmentKey", cacheData, expirationTime);
            return cacheData;
        }

    }
}

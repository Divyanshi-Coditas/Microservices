using AutoMapper;
using EmployeeManagement.Context;
using EmployeeManagement.Entities.Models.DTOModels;
using EmployeeManagement.Entities.Models.EntityModels;
using EmployeeManagement.Repositories;
using EmployeeManagment.Services.Services;
using Microsoft.AspNetCore.JsonPatch;
using Serilog;


namespace EmployeeManagment.Services.Employees
{
    public class EmployessService : IDbEmployeeService<Employee, EmployeeDTO, int>
    {
        #region Globals
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly UserContext _userContext;
        private readonly ILogger _logger;
        private readonly string _redisKey;

        #endregion

        #region Constructor
        public EmployessService(EmployeeRepository employeeRepository, IMapper mapper, ICacheService cacheService, UserContext userContext)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _userContext = userContext;
            _logger = Log.ForContext<EmployessService>();
            _redisKey = _userContext.GetUserNameFromToken();
        }

        #endregion

        #region Public methods

        public IEnumerable<Employee> GetAll()
        {
            try
            {
                _logger.Information("Attempt for Getting all employees.");
                var cacheData = _cacheService.GetData<IEnumerable<Employee>>(_redisKey);
                if (cacheData != null)
                {
                    _logger.Information($"Retrieved {cacheData.Count()} employees from cache.");
                    return cacheData;
                }

                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheData = _employeeRepository.GetAllEmployees();
                _cacheService.SetData(_redisKey, cacheData, expirationTime);
                _logger.Information($"Retrieved {cacheData.Count()} employees from database.");
                return cacheData;
            }
            catch (Exception ex)
            {
                _logger.Information($"Something went wrong: {ex}");
                return null!;
            }
        }
        public Employee Get(int id)
        {
            Employee filteredData;
            var cacheData = _cacheService.GetData<IEnumerable<Employee>>(_redisKey);
            if (cacheData != null)
            {
                _logger.Information($"Fetching employees with id: {id}", id);
                filteredData = cacheData.Where(x => x.Id == id).FirstOrDefault()!;
                return filteredData;
            }
            _logger.Information($"Fetching employees with id: {id}", id);
            filteredData = _employeeRepository.GetEmployeeById(id);
            return filteredData;
        }

        public Employee Create(EmployeeDTO entity)
        {
            _logger.Information("Attempt for Creating New Employee.");
            var employeeEnitity = _mapper.Map<Employee>(entity);
            var result = _employeeRepository.CreateEmployee(employeeEnitity);
            _logger.Information("Sucessfully Created Employee with Name : " + "'" + employeeEnitity.Name + "'");
            _cacheService.RemoveData(_redisKey);
            _logger.Information("fetching updated recored form database.");
            return result;
        }

        public Employee Update(EmployeeDTO entity)
        {
            _logger.Information($"Attempt for Updating Employee with ID: {0}", entity.Id);
            var employeeEnitity = _mapper.Map<Employee>(entity);
            var result = _employeeRepository.UpdateEmployee(employeeEnitity);
            _logger.Information("Sucessfully Updated EMployee.");
            _cacheService.RemoveData(_redisKey);
            _logger.Information("fetching updated recored form database.");
            return result;
        }

        public Employee Delete(int id)
        {
            _logger.Information($"Attempt for Deleting Employee with ID: {id}", id);

            var result = _employeeRepository.DeleteEmployee(id);
            _logger.Information("EMployee deleted Sucessfully. ");
            _cacheService.RemoveData(_redisKey);
            return result;
        }

        public Employee Patch(JsonPatchDocument entity, int id)
        {
            _logger.Information(" Attempt for Updating Employee with ID: {id}", id);
            var response = _employeeRepository.PatchEmployee(id, entity);
            _logger.Information("Sucessfully Updated EMployee.");
            _cacheService.RemoveData(_redisKey);
            _logger.Information("fetching updated recored form database.");
            return response;
        }


        #endregion
    }
}

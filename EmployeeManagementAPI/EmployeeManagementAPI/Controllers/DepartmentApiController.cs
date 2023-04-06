
namespace EmployeeAPI.Controllers
{
    using EmployeeManagement.Api.Helper;

    #region References
    using EmployeeManagement.Entities.Models.EntityModels;
    using EmployeeManagment.Services;
    using EmployeeManagment.Services.Departments;
    using Microsoft.AspNetCore.Mvc;
    #endregion

    #region Department Controller

    #region Routes
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizationFilter))]
    #endregion

    public class DepartmentApiController : ControllerBase
    {
        #region Globle Varibles
      DepartmentService _departmentService;
        #endregion

        #region Constructors
        public DepartmentApiController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion

        #region Public Methods
        [Route("/Getalldepartments")]
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var AllDepartment = _departmentService.GetAll().ToList();
            if (AllDepartment.Count > 0)
            {
                return Ok(AllDepartment);
            }
           return BadRequest();
        }
        #endregion

    }
    #endregion

}

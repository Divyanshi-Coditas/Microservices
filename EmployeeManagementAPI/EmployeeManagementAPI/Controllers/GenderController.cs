using EmployeeManagement.Api.Helper;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Models.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizationFilter))]
    public class GenderController : Controller
    {

        EmployeeManagementContext _context;
        public GenderController(EmployeeManagementContext context)
        {
            _context = context;
        }

        [Route("/GetallGender")]
        [HttpGet]
        public List<Gender> GetallGender()
        {
            var AllGender = _context.Genders.ToList();
            return AllGender;
        }


    }

}

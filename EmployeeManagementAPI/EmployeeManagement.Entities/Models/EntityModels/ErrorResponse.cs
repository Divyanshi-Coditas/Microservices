using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Models.EntityModels
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}

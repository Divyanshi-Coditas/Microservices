using Microsoft.Build.Framework;

using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace EmployeeApiConsumer.Models
{
    public class LoginEntity
    {

        public string UserName { get; set; } = string.Empty;
       
        public string Password { get; set; }= string.Empty;
        public string IPAddress { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;

namespace EmployeeManagement.Api.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Salary { get; set; }
        public string Designation { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int GenderId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Gender Gender { get; set; } = null!;
    }
}

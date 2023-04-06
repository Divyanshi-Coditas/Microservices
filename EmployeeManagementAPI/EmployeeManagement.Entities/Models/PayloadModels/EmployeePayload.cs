using System;
using System.Collections.Generic;

namespace EmployeeManagement.Entities.Models.PayloadModel
{
    public partial class Employeepayload
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
    }
}

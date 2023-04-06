using System;
using System.Collections.Generic;

namespace EmployeeManagement.Entities.Models.EntityModels
{
    public partial class LoginAudit
    {

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Ipaddress { get; set; }
        public string? Auditstatus { get; set; }
        public string? Auditype { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Description { get; set; }
    }
}

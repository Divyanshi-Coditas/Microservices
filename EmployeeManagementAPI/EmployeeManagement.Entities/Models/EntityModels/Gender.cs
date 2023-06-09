﻿using EmployeeManagement.Entities.Models.EntityModels;
using System;
using System.Collections.Generic;
namespace EmployeeManagement.Entities.Models.EntityModels
{
    public partial class Gender
    {
        public Gender()
        {
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Gender1 { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

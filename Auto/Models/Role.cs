﻿using System;
using System.Collections.Generic;

namespace Auto.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public decimal RoleSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}

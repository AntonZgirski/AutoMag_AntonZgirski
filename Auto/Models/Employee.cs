using System;
using System.Collections.Generic;

namespace Auto.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Magazine> Magazines { get; } = new List<Magazine>();

    public virtual Role Role { get; set; } = null!;
}

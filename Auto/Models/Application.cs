using System;
using System.Collections.Generic;

namespace Auto.Models;

public partial class Application
{
    public int ApplicId { get; set; }

    public int UserId { get; set; }

    public int AutoId { get; set; }

    public string Status { get; set; } = null!;
}

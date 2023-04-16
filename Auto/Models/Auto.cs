using System;
using System.Collections.Generic;

namespace Auto.Models;

public partial class Auto
{
    public int AutoId { get; set; }

    public string AutoModel { get; set; } = null!;

    public int Year { get; set; }

    public decimal Price { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Magazine> Magazines { get; } = new List<Magazine>();
}

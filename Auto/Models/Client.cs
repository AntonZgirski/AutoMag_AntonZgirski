using System;
using System.Collections.Generic;

namespace Auto.Models;

public partial class Client
{
    public int ClaentId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientSname { get; set; } = null!;

    public virtual ICollection<Magazine> Magazines { get; } = new List<Magazine>();
}

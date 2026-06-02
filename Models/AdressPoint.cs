using System;
using System.Collections.Generic;

namespace DemoText12.Models;

public partial class AdressPoint
{
    public int Id { get; set; }

    public int? Index { get; set; }

    public string? City { get; set; }

    public string? Streat { get; set; }

    public string? Number { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

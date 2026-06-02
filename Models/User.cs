using System;
using System.Collections.Generic;

namespace DemoText12.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public int? IdRole { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

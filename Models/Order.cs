using System;
using System.Collections.Generic;

namespace DemoText12.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int? IdAdressPoint { get; set; }

    public string? Kod { get; set; }

    public int? IdUser { get; set; }

    public int? IdOrderStatus { get; set; }

    public virtual AdressPoint? IdAdressPointNavigation { get; set; }

    public virtual OrderStatus? IdOrderStatusNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}

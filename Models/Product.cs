using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoText12.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public int? IdProductName { get; set; }

    public int? IdSupplier { get; set; }

    public string? Unit { get; set; }

    public float? Price { get; set; }

    public int? IdMaker { get; set; }

    public int? IdProductCategory { get; set; }

    public int Discount { get; set; }

    public int? CountOnStock { get; set; }

    public string? Discription { get; set; }

    public string? Image { get; set; }

    public virtual Maker? IdMakerNavigation { get; set; }

    public virtual ProductCategory? IdProductCategoryNavigation { get; set; }

    public virtual ProductName? IdProductNameNavigation { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    [NotMapped]
    public string Thine => Discount > 15 ? "#2E8B57" : CountOnStock <= 0 ? "LightBlue" : "Transparent";
    [NotMapped]
    public bool HasDiscount => Discount > 0;
    [NotMapped]
    public bool HasDiscount1 => Discount <= 0;

    [NotMapped]
    public double? PriceResult => Price - (Price * Discount / 100);
}

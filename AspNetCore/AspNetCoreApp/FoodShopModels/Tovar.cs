using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class Tovar
{
    public int CodTovara { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateExpiration { get; set; }

    public string Category { get; set; } = null!;

    public double Price { get; set; }

    public int? Count { get; set; }

    public virtual CategoryTovara CategoryNavigation { get; set; } = null!;

    public virtual ICollection<LineOrder> LineOrders { get; } = new List<LineOrder>();

    public virtual ICollection<LineWrite> LineWrites { get; } = new List<LineWrite>();
}

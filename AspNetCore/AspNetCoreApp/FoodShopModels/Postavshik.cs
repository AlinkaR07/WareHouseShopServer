using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class Postavshik
{
    public string NameOrganization { get; set; } = null!;

    public string Fiodirector { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string NumberAccount { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}

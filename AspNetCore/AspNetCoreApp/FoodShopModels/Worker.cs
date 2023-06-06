using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class Worker
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fio { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Write> Writes { get; } = new List<Write>();
}

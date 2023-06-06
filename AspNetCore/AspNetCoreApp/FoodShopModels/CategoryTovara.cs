using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class CategoryTovara
{
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Tovar> Tovars { get; } = new List<Tovar>();
}

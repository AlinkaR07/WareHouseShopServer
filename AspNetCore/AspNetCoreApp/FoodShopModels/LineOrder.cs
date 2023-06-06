using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class LineOrder
{
    public int CountOrder { get; set; }

    public string? PurchasePrice { get; set; }

    public int? CountShipment { get; set; }

    public int CodTovaraFk { get; set; }

    public int NumberOrderFk { get; set; }

    public DateTime? DataManuf { get; set; }

    public int Id { get; set; }

    public virtual Tovar CodTovaraFkNavigation { get; set; } = null!;

    public virtual Order NumberOrderFkNavigation { get; set; } = null!;
}

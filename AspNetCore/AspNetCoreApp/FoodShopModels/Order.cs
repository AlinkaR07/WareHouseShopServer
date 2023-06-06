using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class Order
{
    public int Number { get; set; }

    public DateTime DataOrder { get; set; }

    public DateTime? DataShipment { get; set; }

    public string StatusOrder { get; set; } = null!;

    public string NameOrganizationPostavshikFk { get; set; } = null!;

    public string FioworkerFk { get; set; } = null!;

    public virtual Worker FioworkerFkNavigation { get; set; } = null!;

    public virtual ICollection<LineOrder> LineOrders { get; } = new List<LineOrder>();

    public virtual Postavshik NameOrganizationPostavshikFkNavigation { get; set; } = null!;
}

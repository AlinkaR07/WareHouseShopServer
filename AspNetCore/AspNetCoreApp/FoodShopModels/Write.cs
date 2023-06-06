using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class Write
{
    public DateTime DataWrite { get; set; }

    public int NumberAct { get; set; }

    public string FioworkerFk { get; set; } = null!;

    public virtual Worker FioworkerFkNavigation { get; set; } = null!;

    public virtual ICollection<LineWrite> LineWrites { get; } = new List<LineWrite>();
}

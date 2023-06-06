using System;
using System.Collections.Generic;

namespace AspNetCoreApp.FoodShopModels;

public partial class LineWrite
{
    public double Summa { get; set; }

    public int Count { get; set; }

    public int NumberActWriteFk { get; set; }

    public int CodTovaraFk { get; set; }

    public int Id { get; set; }

    public virtual Tovar CodTovaraFkNavigation { get; set; } = null!;

    public virtual Write NumberActWriteFkNavigation { get; set; } = null!;
}

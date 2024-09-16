using System;
using System.Collections.Generic;

namespace PointOfSale.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Nombre { get; set; }

    public int Cantidad { get; set; }
}

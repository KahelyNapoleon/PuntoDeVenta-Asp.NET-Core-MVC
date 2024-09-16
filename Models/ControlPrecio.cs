using System;
using System.Collections.Generic;

namespace PointOfSale.Models;

public partial class ControlPrecio
{
    public int ControlPrecio1 { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Nombre { get; set; }

    public decimal? Costo { get; set; }

    public decimal? PrecioVenta { get; set; }
}

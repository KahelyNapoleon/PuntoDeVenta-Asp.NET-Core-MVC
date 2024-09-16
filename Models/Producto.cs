using System;
using System.Collections.Generic;

namespace PointOfSale.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string CodigoBarra { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int PesoNeto { get; set; }

    public decimal? Costo { get; set; }

    public decimal? PrecioVenta { get; set; }

    public int Cantidad { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }
}

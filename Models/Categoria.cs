using System;
using System.Collections.Generic;

namespace PointOfSale.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

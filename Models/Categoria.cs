using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }
    [Required(ErrorMessage ="Obligatorio Ingresar un Valor")]
    [StringLength(50)]
    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models;

public partial class Producto
{
    public int ProductoId { get; set; }
    [Display(Name ="Codigo de Barra")]
    [Required(ErrorMessage = "Valor Obligatorio")]
    [StringLength(20)]
    public string CodigoBarra { get; set; } = null!;
    [Required(ErrorMessage = "Valor Obligatorio")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public int PesoNeto { get; set; }
    [Required(ErrorMessage = "Valor Obligatorio, Ingrese Valor")]
    [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
    public decimal? Costo { get; set; }
    [Required(ErrorMessage = "Valor Obligatorio, Ingrese Valor")]
    [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
    public decimal? PrecioVenta { get; set; }
    [Required(ErrorMessage = "Valor Obligatorio, Ingrese una Cantidad")]
    public int Cantidad { get; set; }
    [Display(Name ="Categoria")]
    [Required(ErrorMessage = "Valor Obligatorio, Ingrese una Categoria")]
    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }
}

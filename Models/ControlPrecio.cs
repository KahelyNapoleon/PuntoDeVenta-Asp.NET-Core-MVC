using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models;

public partial class ControlPrecio
{
    public int ControlPrecio1 { get; set; }
    [Required(ErrorMessage ="Falta Inngresar Codigo de Barra")]
    [Display(Name ="Codigo de Barra")]
    [StringLength(20)]
    public string? CodigoBarra { get; set; }
    [StringLength(50)]
    public string? Nombre { get; set; }
    [Required(ErrorMessage ="Valor Obligatorio")]
    [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
    public decimal? Costo { get; set; }
    [Required(ErrorMessage = "Valor Obligatorio")]
    [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
    public decimal? PrecioVenta { get; set; }
}

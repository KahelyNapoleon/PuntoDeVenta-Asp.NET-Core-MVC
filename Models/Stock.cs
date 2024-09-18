using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Nombre { get; set; }
    [Required(ErrorMessage = "Valor Obligatorio, Ingrese Cantidad")]
    public int Cantidad { get; set; }
}

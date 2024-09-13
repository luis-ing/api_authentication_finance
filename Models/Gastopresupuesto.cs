using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Gastopresupuesto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Conceptogastopresupuesto> Conceptogastopresupuestos { get; set; } = new List<Conceptogastopresupuesto>();
}

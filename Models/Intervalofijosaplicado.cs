using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Intervalofijosaplicado
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Conceptogastopresupuesto> Conceptogastopresupuestos { get; set; } = new List<Conceptogastopresupuesto>();
}

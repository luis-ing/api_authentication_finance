using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Categorium
{
    public int Idcategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Conceptogastopresupuesto> Conceptogastopresupuestos { get; set; } = new List<Conceptogastopresupuesto>();

    public virtual Usuario? Usuario { get; set; }
}

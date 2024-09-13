using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PresupuestoDisponible { get; set; }

    public bool? Activo { get; set; }

    public int UsuarioCreadorId { get; set; }

    public virtual ICollection<Conceptogastopresupuesto> Conceptogastopresupuestos { get; set; } = new List<Conceptogastopresupuesto>();

    public virtual ICollection<Cuentasusuario> Cuentasusuarios { get; set; } = new List<Cuentasusuario>();

    public virtual ICollection<Hitorialpresupuesto> Hitorialpresupuestos { get; set; } = new List<Hitorialpresupuesto>();

    public virtual Usuario UsuarioCreador { get; set; } = null!;
}

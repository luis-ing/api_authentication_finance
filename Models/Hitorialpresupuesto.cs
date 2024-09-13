using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Hitorialpresupuesto
{
    public int Id { get; set; }

    public decimal? Monto { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int ConceptoGastoPresupuestoId { get; set; }

    public int UsuarioCreadorId { get; set; }

    public int? UsuarioActualizacionId { get; set; }

    public bool? Activo { get; set; }

    public int CuentasId { get; set; }

    public DateTime? FechaMontoAplicado { get; set; }

    public virtual Conceptogastopresupuesto ConceptoGastoPresupuesto { get; set; } = null!;

    public virtual Cuenta Cuentas { get; set; } = null!;

    public virtual Usuario? UsuarioActualizacion { get; set; }

    public virtual Usuario UsuarioCreador { get; set; } = null!;
}

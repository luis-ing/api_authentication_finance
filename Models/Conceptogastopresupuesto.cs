using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Conceptogastopresupuesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int UsuarioCreadorId { get; set; }

    public int? UsuarioActualizacionId { get; set; }

    public int GastoPresupuestoId { get; set; }

    public int? IntervaloFijosAplicadoId { get; set; }

    public int CuentasId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public int FijovariableId { get; set; }

    public int? Idcategoria { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaFinal { get; set; }

    public virtual Cuenta Cuentas { get; set; } = null!;

    public virtual Fijovariable Fijovariable { get; set; } = null!;

    public virtual Gastopresupuesto GastoPresupuesto { get; set; } = null!;

    public virtual ICollection<Hitorialpresupuesto> Hitorialpresupuestos { get; set; } = new List<Hitorialpresupuesto>();

    public virtual Categorium? IdcategoriaNavigation { get; set; }

    public virtual Intervalofijosaplicado? IntervaloFijosAplicado { get; set; }

    public virtual Usuario? UsuarioActualizacion { get; set; }

    public virtual Usuario UsuarioCreador { get; set; } = null!;
}

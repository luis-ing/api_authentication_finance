using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Cuentasusuario
{
    public int CuentasId { get; set; }

    public int UsuarioId { get; set; }

    public bool? Activo { get; set; }

    public bool InvitacionAceptada { get; set; }

    public virtual Cuenta Cuentas { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}

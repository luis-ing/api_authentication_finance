using System;
using System.Collections.Generic;

namespace api_finance.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string? Email { get; set; }

    public string Contrasena { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }

    public string? ImgUrl { get; set; }

    public bool TemaOscuro { get; set; }

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<Conceptogastopresupuesto> ConceptogastopresupuestoUsuarioActualizacions { get; set; } = new List<Conceptogastopresupuesto>();

    public virtual ICollection<Conceptogastopresupuesto> ConceptogastopresupuestoUsuarioCreadors { get; set; } = new List<Conceptogastopresupuesto>();

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Cuentasusuario> Cuentasusuarios { get; set; } = new List<Cuentasusuario>();

    public virtual ICollection<Hitorialpresupuesto> HitorialpresupuestoUsuarioActualizacions { get; set; } = new List<Hitorialpresupuesto>();

    public virtual ICollection<Hitorialpresupuesto> HitorialpresupuestoUsuarioCreadors { get; set; } = new List<Hitorialpresupuesto>();
}

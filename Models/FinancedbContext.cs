using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace api_finance.Models;

public partial class FinancedbContext : DbContext
{
    public FinancedbContext()
    {
    }

    public FinancedbContext(DbContextOptions<FinancedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Conceptogastopresupuesto> Conceptogastopresupuestos { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Cuentasusuario> Cuentasusuarios { get; set; }

    public virtual DbSet<Fijovariable> Fijovariables { get; set; }

    public virtual DbSet<Gastopresupuesto> Gastopresupuestos { get; set; }

    public virtual DbSet<Hitorialpresupuesto> Hitorialpresupuestos { get; set; }

    public virtual DbSet<Intervalofijosaplicado> Intervalofijosaplicados { get; set; }

    public virtual DbSet<PrismaMigration> PrismaMigrations { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=financedb;user=root;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PRIMARY");

            entity
                .ToTable("categoria")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UsuarioId, "fk_categoria_usuario1_idx");

            entity.Property(e => e.Idcategoria)
                .HasColumnType("int(11)")
                .HasColumnName("idcategoria");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("fk_categoria_usuario1");
        });

        modelBuilder.Entity<Conceptogastopresupuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("conceptogastopresupuesto")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.GastoPresupuestoId, "fk_ConceptoGastoPresupuesto_GastoPresupuesto1_idx");

            entity.HasIndex(e => e.IntervaloFijosAplicadoId, "fk_ConceptoGastoPresupuesto_IntervaloFijosAplicado1_idx");

            entity.HasIndex(e => e.UsuarioCreadorId, "fk_ConceptoGastoPresupuesto_Usuario1_idx");

            entity.HasIndex(e => e.UsuarioActualizacionId, "fk_ConceptoGastoPresupuesto_Usuario2_idx");

            entity.HasIndex(e => e.Idcategoria, "fk_conceptogastopresupuesto_categoria1_idx");

            entity.HasIndex(e => e.CuentasId, "fk_conceptogastopresupuesto_cuentas1_idx");

            entity.HasIndex(e => e.FijovariableId, "fk_conceptogastopresupuesto_fijovariable1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.CuentasId)
                .HasColumnType("int(11)")
                .HasColumnName("cuentas_id");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaFinal)
                .HasColumnType("datetime")
                .HasColumnName("fechaFinal");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.FijovariableId)
                .HasColumnType("int(11)")
                .HasColumnName("fijovariable_id");
            entity.Property(e => e.GastoPresupuestoId)
                .HasColumnType("int(11)")
                .HasColumnName("GastoPresupuesto_id");
            entity.Property(e => e.Idcategoria)
                .HasColumnType("int(11)")
                .HasColumnName("idcategoria");
            entity.Property(e => e.IntervaloFijosAplicadoId)
                .HasColumnType("int(11)")
                .HasColumnName("IntervaloFijosAplicado_id");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioActualizacionId)
                .HasColumnType("int(11)")
                .HasColumnName("UsuarioActualizacion_id");
            entity.Property(e => e.UsuarioCreadorId)
                .HasColumnType("int(11)")
                .HasColumnName("UsuarioCreador_id");

            entity.HasOne(d => d.Cuentas).WithMany(p => p.Conceptogastopresupuestos)
                .HasForeignKey(d => d.CuentasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_conceptogastopresupuesto_cuentas1");

            entity.HasOne(d => d.Fijovariable).WithMany(p => p.Conceptogastopresupuestos)
                .HasForeignKey(d => d.FijovariableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_conceptogastopresupuesto_fijovariable1");

            entity.HasOne(d => d.GastoPresupuesto).WithMany(p => p.Conceptogastopresupuestos)
                .HasForeignKey(d => d.GastoPresupuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ConceptoGastoPresupuesto_GastoPresupuesto1");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Conceptogastopresupuestos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("fk_conceptogastopresupuesto_categoria1");

            entity.HasOne(d => d.IntervaloFijosAplicado).WithMany(p => p.Conceptogastopresupuestos)
                .HasForeignKey(d => d.IntervaloFijosAplicadoId)
                .HasConstraintName("fk_ConceptoGastoPresupuesto_IntervaloFijosAplicado1");

            entity.HasOne(d => d.UsuarioActualizacion).WithMany(p => p.ConceptogastopresupuestoUsuarioActualizacions)
                .HasForeignKey(d => d.UsuarioActualizacionId)
                .HasConstraintName("fk_ConceptoGastoPresupuesto_Usuario2");

            entity.HasOne(d => d.UsuarioCreador).WithMany(p => p.ConceptogastopresupuestoUsuarioCreadors)
                .HasForeignKey(d => d.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ConceptoGastoPresupuesto_Usuario1");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("cuentas")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.UsuarioCreadorId, "fk_cuentas_usuario1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.PresupuestoDisponible)
                .HasPrecision(10, 2)
                .HasColumnName("presupuestoDisponible");
            entity.Property(e => e.UsuarioCreadorId)
                .HasColumnType("int(11)")
                .HasColumnName("usuarioCreador_id");

            entity.HasOne(d => d.UsuarioCreador).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentas_usuario1");
        });

        modelBuilder.Entity<Cuentasusuario>(entity =>
        {
            entity.HasKey(e => new { e.CuentasId, e.UsuarioId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("cuentasusuario")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CuentasId, "fk_cuentas_has_usuario_cuentas1_idx");

            entity.HasIndex(e => e.UsuarioId, "fk_cuentas_has_usuario_usuario1_idx");

            entity.Property(e => e.CuentasId)
                .HasColumnType("int(11)")
                .HasColumnName("cuentas_id");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.InvitacionAceptada).HasColumnName("invitacionAceptada");

            entity.HasOne(d => d.Cuentas).WithMany(p => p.Cuentasusuarios)
                .HasForeignKey(d => d.CuentasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentas_has_usuario_cuentas1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Cuentasusuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentas_has_usuario_usuario1");
        });

        modelBuilder.Entity<Fijovariable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("fijovariable")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Gastopresupuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("gastopresupuesto")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Hitorialpresupuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("hitorialpresupuesto")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ConceptoGastoPresupuestoId, "fk_HitorialPresupuesto_ConceptoGastoPresupuesto");

            entity.HasIndex(e => e.UsuarioCreadorId, "fk_HitorialPresupuesto_Usuario1_idx");

            entity.HasIndex(e => e.UsuarioActualizacionId, "fk_HitorialPresupuesto_Usuario2_idx");

            entity.HasIndex(e => e.CuentasId, "fk_hitorialpresupuesto_cuentas1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.ConceptoGastoPresupuestoId)
                .HasColumnType("int(11)")
                .HasColumnName("ConceptoGastoPresupuesto_id");
            entity.Property(e => e.CuentasId)
                .HasColumnType("int(11)")
                .HasColumnName("cuentas_id");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaMontoAplicado)
                .HasColumnType("datetime")
                .HasColumnName("fechaMontoAplicado");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.UsuarioActualizacionId)
                .HasColumnType("int(11)")
                .HasColumnName("UsuarioActualizacion_id");
            entity.Property(e => e.UsuarioCreadorId)
                .HasColumnType("int(11)")
                .HasColumnName("UsuarioCreador_id");

            entity.HasOne(d => d.ConceptoGastoPresupuesto).WithMany(p => p.Hitorialpresupuestos)
                .HasForeignKey(d => d.ConceptoGastoPresupuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HitorialPresupuesto_ConceptoGastoPresupuesto");

            entity.HasOne(d => d.Cuentas).WithMany(p => p.Hitorialpresupuestos)
                .HasForeignKey(d => d.CuentasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_hitorialpresupuesto_cuentas1");

            entity.HasOne(d => d.UsuarioActualizacion).WithMany(p => p.HitorialpresupuestoUsuarioActualizacions)
                .HasForeignKey(d => d.UsuarioActualizacionId)
                .HasConstraintName("fk_HitorialPresupuesto_Usuario2");

            entity.HasOne(d => d.UsuarioCreador).WithMany(p => p.HitorialpresupuestoUsuarioCreadors)
                .HasForeignKey(d => d.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HitorialPresupuesto_Usuario1");
        });

        modelBuilder.Entity<Intervalofijosaplicado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("intervalofijosaplicado")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(55)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PrismaMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("_prisma_migrations")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.AppliedStepsCount)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("applied_steps_count");
            entity.Property(e => e.Checksum)
                .HasMaxLength(64)
                .HasColumnName("checksum");
            entity.Property(e => e.FinishedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("finished_at");
            entity.Property(e => e.Logs)
                .HasColumnType("text")
                .HasColumnName("logs");
            entity.Property(e => e.MigrationName)
                .HasMaxLength(255)
                .HasColumnName("migration_name");
            entity.Property(e => e.RolledBackAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("rolled_back_at");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("current_timestamp(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("started_at");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("usuario")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(80)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(300)
                .HasColumnName("imgURL");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(60)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.TemaOscuro).HasColumnName("temaOscuro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

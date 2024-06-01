using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvancedeProyectoFinal.Data;

public partial class TurismoGoContext : DbContext
{
    public TurismoGoContext()
    {
    }

    public TurismoGoContext(DbContextOptions<TurismoGoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividades> Actividades { get; set; }

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<EstadisticasActividades> EstadisticasActividades { get; set; }

    public virtual DbSet<Reservas> Reservas { get; set; }

    public virtual DbSet<Reseñas> Reseñas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-N0O4F0F1;Database=TurismoGo;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividades>(entity =>
        {
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__3AC1AB44005EB61B");

            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.DescripcionLarga).HasColumnName("descripcion_larga");
            entity.Property(e => e.DescripcionPequeña).HasColumnName("descripcion_pequeña");
            entity.Property(e => e.Destino)
                .HasMaxLength(100)
                .HasColumnName("destino");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.ImagenActividad).HasColumnName("imagen_actividad");
            entity.Property(e => e.Itinerario).HasColumnName("itinerario");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK__Actividad__empre__3E52440B");
        });

        modelBuilder.Entity<Empresas>(entity =>
        {
            entity.HasKey(e => e.EmpresaId).HasName("PK__Empresas__536BE4A26AF15C5D");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Empresas__5B8A0682BBB86052").IsUnique();

            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.ImagenEmpresa).HasColumnName("imagen_empresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<EstadisticasActividades>(entity =>
        {
            entity.HasKey(e => e.EstadisticaId).HasName("PK__Estadist__828E5E3114FC9F52");

            entity.ToTable("Estadisticas_Actividades");

            entity.Property(e => e.EstadisticaId).HasColumnName("estadistica_id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.PromedioValoracion)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("promedio_valoracion");
            entity.Property(e => e.TotalReservas).HasColumnName("total_reservas");

            entity.HasOne(d => d.Actividad).WithMany(p => p.EstadisticasActividades)
                .HasForeignKey(d => d.ActividadId)
                .HasConstraintName("FK__Estadisti__activ__4BAC3F29");
        });

        modelBuilder.Entity<Reservas>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__Reservas__F1437E48E8DDA5E2");

            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.CantidadPersonas).HasColumnName("cantidad_personas");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_reserva");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Actividad).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ActividadId)
                .HasConstraintName("FK__Reservas__activi__4316F928");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reservas__usuari__4222D4EF");
        });

        modelBuilder.Entity<Reseñas>(entity =>
        {
            entity.HasKey(e => e.ResenaId).HasName("PK__Reseñas__642724508B158F25");

            entity.Property(e => e.ResenaId).HasColumnName("resena_id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.FechaResena)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_resena");
            entity.Property(e => e.ImagenResena).HasColumnName("imagen_resena");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Valoracion).HasColumnName("valoracion");

            entity.HasOne(d => d.Actividad).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ActividadId)
                .HasConstraintName("FK__Reseñas__activid__48CFD27E");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reseñas__usuario__47DBAE45");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2ED7D2AFFE86B3BF");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__5B8A06822216FE9E").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.ImagenUsuario).HasColumnName("imagen_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

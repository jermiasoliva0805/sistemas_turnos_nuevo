using Microsoft.EntityFrameworkCore;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ✅ DbSets (tablas)
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<HorarioEmpleado> Horarios_Empleados { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔹 Empleados
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id_Empleados);
            entity.Property(e => e.Nombre_Completo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        // 🔹 Horarios de empleados
        modelBuilder.Entity<HorarioEmpleado>(entity =>
        {
            entity.HasKey(h => h.Id_Horario);
            entity.Property(h => h.Dia_Semana).IsRequired().HasMaxLength(20);

            entity.HasOne(h => h.Empleado)
                  .WithMany(e => e.Horarios)
                  .HasForeignKey(h => h.Id_Empleados);
        });

        // 🔹 Servicios
        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(s => s.Id_Servicios);
            entity.Property(s => s.Nombre).IsRequired().HasMaxLength(150);
            entity.Property(s => s.Descripcion).HasMaxLength(500);
            entity.Property(s => s.Precio).HasColumnType("decimal(18,2)");
        });

        // 🔹 Usuarios
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id_Usuarios);
            entity.Property(u => u.Nombre_Completo).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Password_Hash).IsRequired();
            entity.Property(u => u.Rol).IsRequired().HasMaxLength(50);
            entity.Property(u => u.Fecha_Creacion).HasDefaultValueSql("GETDATE()");
        });

        // 🔹 Turnos
        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(t => t.Id_Turno);

            entity.Property(t => t.Estado).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Fecha_Creacion).HasDefaultValueSql("GETDATE()");

            entity.HasOne(t => t.Usuario)
                  .WithMany(u => u.Turnos)
                  .HasForeignKey(t => t.Id_Usuarios);

            entity.HasOne(t => t.Empleado)
                  .WithMany(e => e.Turnos)
                  .HasForeignKey(t => t.Id_Empleados);

            entity.HasOne(t => t.Servicio)
                  .WithMany(s => s.Turnos)
                  .HasForeignKey(t => t.Id_Servicios);
        });
    }
}

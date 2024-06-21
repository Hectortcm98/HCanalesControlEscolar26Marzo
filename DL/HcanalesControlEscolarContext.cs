using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class HcanalesControlEscolarContext : DbContext
{
    public HcanalesControlEscolarContext()
    {
    }

    public HcanalesControlEscolarContext(DbContextOptions<HcanalesControlEscolarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnoMaterium> AlumnoMateria { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= HCanalesControlEscolar; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumnos__460B4740AEAE3414");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AlumnoMaterium>(entity =>
        {
            entity.HasKey(e => e.IdalumnoMateria).HasName("PK__Alumno_M__E69049F1E493CD5B");

            entity.ToTable("Alumno_Materia");

            entity.Property(e => e.IdalumnoMateria).HasColumnName("IDalumnoMateria");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnoMateria)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK__Alumno_Ma__IdMat__145C0A3F");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.AlumnoMateria)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("FK__Alumno_Ma__IdMat__15502E78");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materias__EC1746701A3F974D");

            entity.Property(e => e.Costo).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

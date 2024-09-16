using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<ControlPrecio> ControlPrecios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E50529F531");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ControlPrecio>(entity =>
        {
            entity.HasKey(e => e.ControlPrecio1).HasName("PK__ControlP__3E7512CABBF05FBE");

            entity.ToTable("ControlPrecio");

            entity.Property(e => e.ControlPrecio1)
                .ValueGeneratedNever()
                .HasColumnName("ControlPrecio");
            entity.Property(e => e.CodigoBarra)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Costo).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(20, 2)");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3FA8F7AF9");

            entity.ToTable("Producto");

            entity.Property(e => e.CodigoBarra)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Costo).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Producto__Catego__398D8EEE");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__2C83A9C298E9F233");

            entity.ToTable("Stock");

            entity.Property(e => e.StockId).ValueGeneratedNever();
            entity.Property(e => e.CodigoBarra)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

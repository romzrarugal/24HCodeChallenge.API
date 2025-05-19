using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Entities;

namespace MyProject.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaType> PizzaTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__4659622934C1BE47");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A4080ADCAFC8E");

            entity.ToTable("order_details");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pizza_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__order__3E52440B");

            entity.HasOne(d => d.Pizza).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__pizza__3F466844");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId).HasName("PK__pizzas__52B89DE3385EE0DE");

            entity.ToTable("pizzas");

            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pizza_id");
            entity.Property(e => e.PizzaTypeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pizza_type_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("size");

            entity.HasOne(d => d.PizzaType).WithMany(p => p.Pizzas)
                .HasForeignKey(d => d.PizzaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__pizzas__pizza_ty__398D8EEE");
        });

        modelBuilder.Entity<PizzaType>(entity =>
        {
            entity.HasKey(e => e.PizzaTypeId).HasName("PK__pizza_ty__5AB4DED5345145B5");

            entity.ToTable("pizza_types");

            entity.Property(e => e.PizzaTypeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pizza_type_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Ingredients).HasColumnName("ingredients");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

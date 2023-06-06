using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp.FoodShopModels;

public partial class FoodshopContext : DbContext
{
    public FoodshopContext()
    {
    }

    public FoodshopContext(DbContextOptions<FoodshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryTovara> CategoryTovaras { get; set; }

    public virtual DbSet<LineOrder> LineOrders { get; set; }

    public virtual DbSet<LineWrite> LineWrites { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Postavshik> Postavshiks { get; set; }

    public virtual DbSet<Tovar> Tovars { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<Write> Writes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-5C0RSPHK\\SQLEXPRESS;Database=foodshop;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryTovara>(entity =>
        {
            entity.HasKey(e => e.CategoryName).HasName("PK_Категория товара");

            entity.ToTable("CategoryTovara");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<LineOrder>(entity =>
        {
            entity.ToTable("LineOrder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CodTovaraFk).HasColumnName("CodTovara(FK)");
            entity.Property(e => e.DataManuf).HasColumnType("date");
            entity.Property(e => e.NumberOrderFk).HasColumnName("NumberOrder(FK)");
            entity.Property(e => e.PurchasePrice).HasMaxLength(50);

            entity.HasOne(d => d.CodTovaraFkNavigation).WithMany(p => p.LineOrders)
                .HasForeignKey(d => d.CodTovaraFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Строка заказа_Товар");

            entity.HasOne(d => d.NumberOrderFkNavigation).WithMany(p => p.LineOrders)
                .HasForeignKey(d => d.NumberOrderFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Строка заказа_Заказ");
        });

        modelBuilder.Entity<LineWrite>(entity =>
        {
            entity.ToTable("LineWrite");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CodTovaraFk).HasColumnName("CodTovara(FK)");
            entity.Property(e => e.NumberActWriteFk).HasColumnName("NumberActWrite(FK)");

            entity.HasOne(d => d.CodTovaraFkNavigation).WithMany(p => p.LineWrites)
                .HasForeignKey(d => d.CodTovaraFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Строка акта списания_Товар");

            entity.HasOne(d => d.NumberActWriteFkNavigation).WithMany(p => p.LineWrites)
                .HasForeignKey(d => d.NumberActWriteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Строка акта списания_Списание");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK_Заказ");

            entity.ToTable("Order");

            entity.Property(e => e.DataOrder).HasColumnType("date");
            entity.Property(e => e.DataShipment).HasColumnType("date");
            entity.Property(e => e.FioworkerFk)
                .HasMaxLength(50)
                .HasColumnName("FIOworker(FK)");
            entity.Property(e => e.NameOrganizationPostavshikFk)
                .HasMaxLength(50)
                .HasColumnName("NameOrganizationPostavshik(FK)");
            entity.Property(e => e.StatusOrder).HasMaxLength(50);

            entity.HasOne(d => d.FioworkerFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FioworkerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Worker");

            entity.HasOne(d => d.NameOrganizationPostavshikFkNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.NameOrganizationPostavshikFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Заказ_Поставщик");
        });

        modelBuilder.Entity<Postavshik>(entity =>
        {
            entity.HasKey(e => e.NameOrganization).HasName("PK_Поставщик");

            entity.ToTable("Postavshik");

            entity.Property(e => e.NameOrganization).HasMaxLength(50);
            entity.Property(e => e.Fiodirector)
                .HasMaxLength(50)
                .HasColumnName("FIOdirector");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("INN");
            entity.Property(e => e.NumberAccount).HasMaxLength(50);
        });

        modelBuilder.Entity<Tovar>(entity =>
        {
            entity.HasKey(e => e.CodTovara).HasName("PK_Товар");

            entity.ToTable("Tovar");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DateExpiration).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Tovars)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Товар_Категория товара");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Fio).HasName("PK_Работник склада");

            entity.ToTable("Worker");

            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("FIO");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Write>(entity =>
        {
            entity.HasKey(e => e.NumberAct).HasName("PK_Списание");

            entity.ToTable("Write");

            entity.Property(e => e.DataWrite).HasColumnType("date");
            entity.Property(e => e.FioworkerFk)
                .HasMaxLength(50)
                .HasColumnName("FIOworker(FK)");

            entity.HasOne(d => d.FioworkerFkNavigation).WithMany(p => p.Writes)
                .HasForeignKey(d => d.FioworkerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Списание_Работник склада");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

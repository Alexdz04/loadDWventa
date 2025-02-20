﻿using LoadDwhVenta.Data.Entities.Northwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDwhVenta.Data.Contexts.Nortwind;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ClientesAtendidosPorEmpleado> ClientesAtendidosPorEmpleados { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<NumeroOrdenesPorCliente> NumeroOrdenesPorClientes { get; set; }

    public DbSet<NumeroOrdenesPorTransportistum> NumeroOrdenesPorTransportista { get; set; }

    public DbSet<NumeroPedidosPorEmpleado> NumeroPedidosPorEmpleados { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductosMasVendido> ProductosMasVendidos { get; set; }

    public DbSet<ProductosMasVendidosPorCategorium> ProductosMasVendidosPorCategoria { get; set; }

    public DbSet<Shipper> Shippers { get; set; }

    public DbSet<TotalVentasPorCliente> TotalVentasPorClientes { get; set; }

    public DbSet<TotalVentasPorTransportistum> TotalVentasPorTransportista { get; set; }

    public DbSet<VentasPorCategorium> VentasPorCategoria { get; set; }

    public DbSet<VentasPorEmpleado> VentasPorEmpleados { get; set; }

    public DbSet<VentasTotalesPorPeriodo> VentasTotalesPorPeriodos { get; set; }

    public DbSet<ViewClient> ViewClients { get; set; }

    public DbSet<ViewProducto> ViewProductos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "CategoryName");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<ClientesAtendidosPorEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClientesAtendidosPorEmpleado", "DWH");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NombreEmpleado)
                .IsRequired()
                .HasMaxLength(31);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.City, "City");

            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.HasIndex(e => e.Region, "Region");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.LastName, "LastName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Extension).HasMaxLength(4);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.HomePhone).HasMaxLength(24);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Notes).HasColumnType("ntext");
            entity.Property(e => e.PhotoPath).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

        });

        modelBuilder.Entity<NumeroOrdenesPorCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NumeroOrdenesPorCliente", "DWH");

            entity.Property(e => e.Cliente)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
        });

        modelBuilder.Entity<NumeroOrdenesPorTransportistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NumeroOrdenesPorTransportista", "DWH");

            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.Transportista)
                .IsRequired()
                .HasMaxLength(40);
        });

        modelBuilder.Entity<NumeroPedidosPorEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NumeroPedidosPorEmpleado", "DWH");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NombreEmpleado)
                .IsRequired()
                .HasMaxLength(31);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

            entity.HasIndex(e => e.CategoryId, "CategoryID");

            entity.HasIndex(e => e.ProductName, "ProductName");

            entity.HasIndex(e => e.SupplierId, "SupplierID");

            entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.ReorderLevel).HasDefaultValue((short)0);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.UnitsInStock).HasDefaultValue((short)0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue((short)0);

        });

        modelBuilder.Entity<ProductosMasVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosMasVendidos", "DWH");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);
        });

        modelBuilder.Entity<ProductosMasVendidosPorCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosMasVendidosPorCategoria", "DWH");

            entity.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.Phone).HasMaxLength(24);
        });

        modelBuilder.Entity<TotalVentasPorCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TotalVentasPorCliente", "DWH");

            entity.Property(e => e.Cliente)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.TotalVentas).HasColumnType("money");
        });

        modelBuilder.Entity<TotalVentasPorTransportistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TotalVentasPorTransportista", "DWH");

            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.TotalVentas).HasColumnType("money");
            entity.Property(e => e.Transportista)
                .IsRequired()
                .HasMaxLength(40);
        });

        modelBuilder.Entity<VentasPorCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasPorCategoria", "DWH");

            entity.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.VentasTotales).HasColumnType("money");
        });

        modelBuilder.Entity<VentasPorEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasPorEmpleado", "DWH");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NombreEmpleado)
                .IsRequired()
                .HasMaxLength(31);
            entity.Property(e => e.VentasTotales).HasColumnType("money");
        });

        modelBuilder.Entity<VentasTotalesPorPeriodo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasTotalesPorPeriodo", "DWH");

            entity.Property(e => e.VentasTotales).HasColumnType("money");
        });

        modelBuilder.Entity<ViewClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewClients", "DWH");

            entity.Property(e => e.Direccion).HasMaxLength(60);
            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<ViewProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewProductos", "DWH");

            entity.Property(e => e.Costo).HasColumnType("money");
            entity.Property(e => e.Descripcion).HasMaxLength(20);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.PreciVenta).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
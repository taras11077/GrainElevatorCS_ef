﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrainElevatorCS_ef.Models;

public partial class Db : DbContext
{
    public Db()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CompletionReport> CompletionReports { get; set; }

    public virtual DbSet<DepotItem> DepotItems { get; set; }

    public virtual DbSet<InputInvoice> InputInvoices { get; set; }

    public virtual DbSet<LaboratoryCard> LaboratoryCards { get; set; }

    public virtual DbSet<OutputInvoice> OutputInvoices { get; set; }

    public virtual DbSet<PriceByOperation> PriceByOperations { get; set; }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<ProductTitle> ProductTitles { get; set; }

    public virtual DbSet<ProductionBatch> ProductionBatches { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TechnologicalOperation> TechnologicalOperations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GrainElevatorDB_7;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F76BD246A");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryTitle)
                .HasMaxLength(50)
                .HasColumnName("categoryTitle");
            entity.Property(e => e.CategoryValue).HasColumnName("categoryValue");
            entity.Property(e => e.DepotItemId).HasColumnName("depotItem_id");

            entity.HasOne(d => d.DepotItem).WithMany(p => p.Categories)
                .HasForeignKey(d => d.DepotItemId)
                .HasConstraintName("FK_categories_depotItem");
        });

        modelBuilder.Entity<CompletionReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__completi__3213E83FC24FF82B");

            entity.ToTable("completionReports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.IsFinalized).HasColumnName("isFinalized");
            entity.Property(e => e.PhysicalWeightReport).HasColumnName("physicalWeightReport");
            entity.Property(e => e.PriceListId).HasColumnName("priceList_id");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.QuantityesDrying).HasColumnName("quantityesDrying");
            entity.Property(e => e.ReportDate)
                .HasColumnType("datetime")
                .HasColumnName("reportDate");
            entity.Property(e => e.ReportNumber).HasColumnName("reportNumber");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CompletionReports)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_completionReports_users");

            entity.HasOne(d => d.PriceList).WithMany(p => p.CompletionReports)
                .HasForeignKey(d => d.PriceListId)
                .HasConstraintName("FK_completionReports_priceList");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.CompletionReports)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_completionReports_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.CompletionReports)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_completionReports_suppliers");
        });

        modelBuilder.Entity<DepotItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__depotIte__3213E83F20E8B059");

            entity.ToTable("depotItems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.DepotItems)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_depotItems_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.DepotItems)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_depotItems_suppliers");
        });

        modelBuilder.Entity<InputInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__inputInv__3213E83F55A1A217");

            entity.ToTable("inputInvoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrivalDate)
                .HasColumnType("datetime")
                .HasColumnName("arrivalDate");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.InvNumber)
                .HasMaxLength(8)
                .HasColumnName("invNumber");
            entity.Property(e => e.PhysicalWeight).HasColumnName("physicalWeight");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(8)
                .HasColumnName("vehicleNumber");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InputInvoices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_inputInvoices_users");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.InputInvoices)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inputInvoices_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.InputInvoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inputInvoices_suppliers");
        });

        modelBuilder.Entity<LaboratoryCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__laborato__3213E83F44550D48");

            entity.ToTable("laboratoryCards");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ArrivalDate)
                .HasColumnType("datetime")
                .HasColumnName("arrivalDate");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.GrainImpurity).HasColumnName("grainImpurity");
            entity.Property(e => e.InvNumber)
                .HasMaxLength(8)
                .HasColumnName("invNumber");
            entity.Property(e => e.IsProduction).HasColumnName("isProduction");
            entity.Property(e => e.LabCardNumber).HasColumnName("labCardNumber");
            entity.Property(e => e.Moisture).HasColumnName("moisture");
            entity.Property(e => e.PhysicalWeight).HasColumnName("physicalWeight");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.SpecialNotes)
                .HasMaxLength(50)
                .HasColumnName("specialNotes");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Weediness).HasColumnName("weediness");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LaboratoryCards)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_LaboratoryCards_users");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LaboratoryCard)
                .HasForeignKey<LaboratoryCard>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_laboratoryCards_inputInvoice");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.LaboratoryCards)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_laboratoryCards_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.LaboratoryCards)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_laboratoryCards_suppliers");
        });

        modelBuilder.Entity<OutputInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__outputIn__3213E83F32DDA9EF");

            entity.ToTable("outputInvoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.DepotItemId).HasColumnName("depotItem_id");
            entity.Property(e => e.OutInvNumber)
                .HasMaxLength(8)
                .HasColumnName("outInvNumber");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.ProductWeight).HasColumnName("productWeight");
            entity.Property(e => e.ShipmentDate)
                .HasColumnType("datetime")
                .HasColumnName("shipmentDate");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(8)
                .HasColumnName("vehicleNumber");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OutputInvoices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_outputInvoices_users");

            entity.HasOne(d => d.DepotItem).WithMany(p => p.OutputInvoices)
                .HasForeignKey(d => d.DepotItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_outputInvoices_depotItems");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.OutputInvoices)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_outputInvoices_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.OutputInvoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_outputInvoices_suppliers");
        });

        modelBuilder.Entity<PriceByOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__priceByO__3213E83FE07032BB");

            entity.ToTable("priceByOperations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OperationPrice).HasColumnName("operationPrice");
            entity.Property(e => e.OperationTitle)
                .HasMaxLength(50)
                .HasColumnName("operationTitle");
            entity.Property(e => e.PriceListId).HasColumnName("priceList_id");

            entity.HasOne(d => d.PriceList).WithMany(p => p.PriceByOperations)
                .HasForeignKey(d => d.PriceListId)
                .HasConstraintName("FK_operationPrices_priceList");
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__priceLis__3213E83FA5A83BE4");

            entity.ToTable("priceLists");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.ProductTitle)
                .HasMaxLength(50)
                .HasColumnName("productTitle");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PriceLists)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_priceLists_users");
        });

        modelBuilder.Entity<ProductTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__productT__3213E83FEAC0D5A8");

            entity.ToTable("productTitles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producti__3213E83F49BC279D");

            entity.ToTable("productionBatches");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountWeight).HasColumnName("accountWeight");
            entity.Property(e => e.ArrivalDate)
                .HasColumnType("datetime")
                .HasColumnName("arrivalDate");
            entity.Property(e => e.InvNumber)
                .HasMaxLength(8)
                .HasColumnName("invNumber");
            entity.Property(e => e.LabCardNumber).HasColumnName("labCardNumber");
            entity.Property(e => e.Moisture).HasColumnName("moisture");
            entity.Property(e => e.MoistureBase).HasColumnName("moistureBase");
            entity.Property(e => e.PhysicalWeight).HasColumnName("physicalWeight");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.RegisterId).HasColumnName("register_id");
            entity.Property(e => e.Shrinkage).HasColumnName("shrinkage");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Waste).HasColumnName("waste");
            entity.Property(e => e.Weediness).HasColumnName("weediness");
            entity.Property(e => e.WeedinessBase).HasColumnName("weedinessBase");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ProductionBatch)
                .HasForeignKey<ProductionBatch>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productionBatches_laboratoryCard");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productionBatches_productTitles");

            entity.HasOne(d => d.Register).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.RegisterId)
                .HasConstraintName("FK_productionBatches_register");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productionBatches_suppliers");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__register__3213E83F04CF8E18");

            entity.ToTable("registers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccWeightReg).HasColumnName("accWeightReg");
            entity.Property(e => e.ArrivalDate)
                .HasColumnType("datetime")
                .HasColumnName("arrivalDate");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.PhysicalWeightReg).HasColumnName("physicalWeightReg");
            entity.Property(e => e.ProductTitleId).HasColumnName("productTitle_id");
            entity.Property(e => e.RegisterNumber).HasColumnName("registerNumber");
            entity.Property(e => e.ShrinkageReg).HasColumnName("shrinkageReg");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.WasteReg).HasColumnName("wasteReg");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Registers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_registers_users");

            entity.HasOne(d => d.ProductTitle).WithMany(p => p.Registers)
                .HasForeignKey(d => d.ProductTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_registers_productTitles");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Registers)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_registers_suppliers");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplier__3213E83F0337A3B9");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TechnologicalOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__technolo__3213E83FFD310135");

            entity.ToTable("technologicalOperations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CompletionReportId).HasColumnName("completionReport_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.TotalCost).HasColumnName("totalCost");

            entity.HasOne(d => d.CompletionReport).WithMany(p => p.TechnologicalOperations)
                .HasForeignKey(d => d.CompletionReportId)
                .HasConstraintName("FK_technologicalOperations_completionReport");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FCAE8DCA0");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birthDate");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(30)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .HasColumnName("lastName");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

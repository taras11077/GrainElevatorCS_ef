using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class CompletionReport
{
    public int Id { get; set; }

    public int ReportNumber { get; set; }

    public DateTime ReportDate { get; set; }

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public int? PriceListId { get; set; }

    public double QuantityesDrying { get; set; }

    public double PhysicalWeightReport { get; set; }

    public bool IsFinalized { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual PriceList? PriceList { get; set; }

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<TechnologicalOperation> TechnologicalOperations { get; set; } = new List<TechnologicalOperation>();


    public CompletionReport() { }

    public CompletionReport(int reportNum, DateTime date, params Register[] registers)
    {
        if (registers != null)
        {
            ReportNumber = reportNum;
            ReportDate = date;
            SupplierId = registers[0].SupplierId;
            ProductTitleId = registers[0].ProductTitleId;

            PhysicalWeightReport = CalcSumWeightReport(registers);
            QuantityesDrying = CalcDryingQuantity(registers);
        }

        TechnologicalOperations = new List<TechnologicalOperation>()
            {
                new TechnologicalOperation("Приемка"),
                new TechnologicalOperation("Первичная очистка"),
                new TechnologicalOperation("Сушка в шахтной сушилке"),
            };

        initOperationsValue();
    }

    // присвоение технологическим операциям переменних количественних значений
    private void initOperationsValue()
    {
        try
        {
            (TechnologicalOperations as List<TechnologicalOperation>)?.ForEach(op =>
            {
                switch (op.Title)
                {
                    case "Приемка":
                        op.Amount = PhysicalWeightReport;
                        break;

                    case "Первичная очистка":
                        op.Amount = PhysicalWeightReport;
                        break;

                    case "Сушка в шахтной сушилке":
                        op.Amount = QuantityesDrying;
                        break;

                }
            });
        }
        catch (Exception)
        {
            // TODO
            throw;
        }

    }


    //рассчет Акта доработка по заданному Прайсу
    public void CalcByPrice(PriceList pl)
    {
        if (pl.PriceByOperations == null)
            return;

        try
        {
            (TechnologicalOperations as List<TechnologicalOperation>)?.ForEach(op =>
            {
                foreach (var p in pl.PriceByOperations)
                    if (op.Title == p.OperationTitle)
                    {
                        op.Price = p.OperationPrice;
                        op.TotalCost = op.Amount * op.Price;
                    }
            });

            IsFinalized = true;
            PriceListId = pl.Id;
        }
        catch (Exception)
        {
            // TODO
            throw;
        }
    }


    // добавление в Акт доработки Технологических операций по одной
    private void addOperations(TechnologicalOperation operation)
    {
        try
        {
            (TechnologicalOperations as List<TechnologicalOperation>)?.Add(operation);
        }
        catch (Exception)
        {
            // TODO
            throw;
        }
    }
    // добавление в Акт доработки Технологических операций перечнем
    private void addOperationsRange(params TechnologicalOperation[] operation)
    {
        try
        {
            (TechnologicalOperations as List<TechnologicalOperation>)?.AddRange(operation);
        }
        catch (Exception)
        {
            // TODO
            throw;
        }
    }



    // рассчет общего Сумми Физического веса всех Реестров
    private double CalcSumWeightReport(params Register[] registers)
    {
        try
        {
            foreach (Register reg in registers)
                PhysicalWeightReport += (double)reg.PhysicalWeightReg / 1000;

            return PhysicalWeightReport;
        }
        catch (Exception)
        {
            // TODO
            throw;
        }
    }

    // расчет тонно/процентов сушки по каждой ППП всех Реестров Акта
    private double CalcDryingQuantity(params Register[] registers)
    {
        if (registers == null)
            return 0.0;

        try
        {
            foreach (Register reg in registers)
            {
                if ((reg.ProductionBatches as List<ProductionBatch>) is null)
                    return 0.0;

                (reg.ProductionBatches as List<ProductionBatch>)?.ForEach(p =>
                {
                    if (p.Shrinkage != 0)
                        QuantityesDrying += ((p.PhysicalWeight - p.Waste) * (p.Moisture - p.MoistureBase) / 1000);
                });
            }
            return QuantityesDrying;
        }
        catch (Exception)
        {
            // TODO
            throw;
        }
    }

}

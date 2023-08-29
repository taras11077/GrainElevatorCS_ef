using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class Register
{
    public int Id { get; set; }

    public int RegisterNumber { get; set; }

    public DateTime ArrivalDate { get; set; }

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public int PhysicalWeightReg { get; set; }

    public int ShrinkageReg { get; set; }

    public int WasteReg { get; set; }

    public int AccWeightReg { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual Supplier Supplier { get; set; } = null!;


    public Register()
    { }

    public Register(int regNum, double weedinessBase, double moistureBase, params LaboratoryCard[] labCards)
    {
        if (labCards != null)
        {
            ProductionBatches = new List<ProductionBatch>();

            RegisterNumber = regNum;
            ArrivalDate = labCards[0].ArrivalDate;
            SupplierId = labCards[0].SupplierId;
            ProductTitleId = labCards[0].ProductTitleId;

            InitProductionBatches(weedinessBase, moistureBase, labCards);
        }
    }

    //public void InitRegister(int regNum, double weedinessBase, double moistureBase, params LaboratoryCard[] labCards)
    //{
    //    if (labCards != null)
    //    {
    //        RegisterNumber = regNum;
    //        ArrivalDate = labCards[0].ArrivalDate;
    //        SupplierId = labCards[0].SupplierId;
    //        ProductTitleId = labCards[0].ProductTitleId;

    //        InitProductionBatches(weedinessBase, moistureBase, labCards);
    //    }
    //}

    private void InitProductionBatches(double weedinessBase, double moistureBase, params LaboratoryCard[] labCards)
    {
        foreach (var lc in labCards!)
        {
            ProductionBatch pb = new ProductionBatch(lc, weedinessBase, moistureBase);
            ProductionBatches.Add(pb);

            PhysicalWeightReg += pb.PhysicalWeight;
            AccWeightReg += pb.AccountWeight;
            WasteReg += pb.Waste;
            ShrinkageReg += pb.Shrinkage;
        }
    }


    /*
        public Register(int regNum, List<ProductionBatch>? prodBatches)
        {
            if (prodBatches != null)
            {
                ProductionBatches = new List<ProductionBatch>(prodBatches);

                RegisterNumber = regNum;
                ArrivalDate = prodBatches[0].ArrivalDate;
                SupplierId = prodBatches[0].SupplierId;
                ProductTitleId = prodBatches[0].ProductTitleId;

                foreach (var pb in prodBatches)
                {
                    PhysicalWeightReg += pb.PhysicalWeight;
                    AccWeightReg += pb.AccountWeight;
                    WasteReg += pb.Waste;
                    ShrinkageReg += pb.Shrinkage;
                }
            }
        }

        public Register(int regNum, params ProductionBatch[] prodBatches)
        {
            if (prodBatches != null)
            {
                ProductionBatches = new List<ProductionBatch>(prodBatches);

                RegisterNumber = regNum;
                ArrivalDate = prodBatches[0].ArrivalDate;
                SupplierId = prodBatches[0].SupplierId;
                ProductTitleId = prodBatches[0].ProductTitleId;

                foreach (var pb in prodBatches)
                {
                    PhysicalWeightReg += pb.PhysicalWeight;
                    AccWeightReg += pb.AccountWeight;
                    WasteReg += pb.Waste;
                    ShrinkageReg += pb.Shrinkage;
                }
            }
        }

        public void AddToRegister(ProductionBatch pb)
        {
            if (pb != null)
            {
                ArrivalDate = pb.ArrivalDate;
                SupplierId = pb.SupplierId;
                ProductTitleId = pb.ProductTitleId;
                ProductionBatches?.Add(pb);

                PhysicalWeightReg += pb.PhysicalWeight;
                AccWeightReg += pb.AccountWeight;
                WasteReg += pb.Waste;
                ShrinkageReg += pb.Shrinkage;
            }
        }
    */

    // ТЕСТ ДЛЯ КОНСОЛИ ==============================================================================================================
    public void PrintReg()
    {
        Console.WriteLine($"Реестр:          №{RegisterNumber}");
        Console.WriteLine($"Поставщик:       {Supplier}");
        Console.WriteLine($"Наименование:    {ProductTitle}");
        Console.WriteLine(new string('=', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));
        Console.WriteLine("|{0,8}|{1,10}|{2,15}|{3,10}|{4,10}|{5,10}|{6,10}|{7,15}|", "Дата прихода", "Номер ТТН", "Физический вес", "Влажность", "Усушка", "Сорность", "Отход", "Зачетный вес");
        Console.WriteLine(new string('=', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));

        if (ProductionBatches != null)
        {
            foreach (var pb in ProductionBatches)
                pb.PrintProductionBatch();
        }

        Console.WriteLine(new string('=', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));
        Console.WriteLine("|{0,12}|{1,10}|{2,15}|{3,10}|{4,10}|{5,10}|{6,10}|{7,15}|", "Итого", " ", PhysicalWeightReg, " ", ShrinkageReg, " ", WasteReg, AccWeightReg);
        Console.WriteLine(new string('=', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));
        Console.WriteLine("\n");
    }
}

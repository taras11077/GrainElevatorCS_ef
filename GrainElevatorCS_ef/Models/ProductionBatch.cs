using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class ProductionBatch
{
    public int Id { get; set; }

    public DateTime ArrivalDate { get; set; }

    public int LabCardNumber { get; set; }

    public string InvNumber { get; set; } = null!;

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public int PhysicalWeight { get; set; }

    public double Weediness { get; set; }

    public double Moisture { get; set; }

    public double WeedinessBase { get; set; }

    public double MoistureBase { get; set; }

    public int Waste { get; set; }

    public int Shrinkage { get; set; }

    public int AccountWeight { get; set; }

    public int RegisterId { get; set; }

    public virtual LaboratoryCard IdNavigation { get; set; } = null!;

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual Register Register { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;


    public ProductionBatch() { }

    public ProductionBatch(LaboratoryCard lc, double weedinessBase, double moistureBase)
    {
        Id = lc.Id;
        LabCardNumber = lc.LabCardNumber;
        ArrivalDate = lc.ArrivalDate;
        InvNumber = lc.InvNumber;
        SupplierId = lc.SupplierId;
        ProductTitleId = lc.ProductTitleId;
        PhysicalWeight = lc.PhysicalWeight;
        Weediness = lc.Weediness;
        Moisture = lc.Moisture;

        WeedinessBase = weedinessBase;
        MoistureBase = moistureBase;

        CalcResultProduction();
    }

    public void CalcResultProduction()
    {
        if (Weediness <= WeedinessBase)
            Waste = 0;
        else
            Waste = (int)(PhysicalWeight * (1 - (100 - Weediness) / (100 - WeedinessBase)));

        if (Moisture <= MoistureBase)
            Shrinkage = 0;
        else
            Shrinkage = (int)((PhysicalWeight - Waste) * (1 - (100 - Moisture) / (100 - MoistureBase)));

        AccountWeight = PhysicalWeight - Waste - Shrinkage;
    }







    // вывод информации (ДЛЯ ТЕСТА НА КОНСОЛИ)
    public override string ToString()
    {
        return $"Дата прихода:              {ArrivalDate.ToString("dd.MM.yyyy")}\n" +
               $"Номер Карточки анализа:    {LabCardNumber}\n" +
               $"Поставщик:                 {Supplier}\n" +
               $"Наименование:              {ProductTitle}\n" +
               $"физический вес:            {PhysicalWeight} кг\n" +

               $"Сорная примесь:    {Weediness} %\n" +
               $"Влажность:         {Moisture} %\n\n" +

               $"Базовая сорность:  {WeedinessBase} %\n" +
               $"Базовая Влажность: {MoistureBase} %\n\n" +

               $"Усушка:            {Shrinkage} кг\n" +
               $"Сорная убыль:      {Waste} кг\n" +
               $"Зачетный вес:      {AccountWeight} кг\n";
    }


    public void PrintProductionBatch()
    {
        if (this != null)
        {
            Console.WriteLine(new string('-', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));
            Console.WriteLine("|{0,12}|{1,10}|{2,15}|{3,10}|{4,10}|{5,10}|{6,10}|{7,15}|", ArrivalDate.ToString("dd.MM.yyyy"), InvNumber, PhysicalWeight, Moisture, Shrinkage, Weediness, Waste, AccountWeight);
            Console.WriteLine(new string('-', 12 + 10 + 15 + 10 + 10 + 10 + 10 + 15 + 9));
        }
    }
}

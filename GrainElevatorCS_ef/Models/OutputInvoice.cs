using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GrainElevatorCS_ef.Models;

public partial class OutputInvoice
{
    public int Id { get; set; }

    public string OutInvNumber { get; set; } = null!;

    public DateTime ShipmentDate { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public int DepotItemId { get; set; }

    public string Category { get; set; } = null!;

    public int ProductWeight { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual DepotItem DepotItem { get; set; } = null!;

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;


    public OutputInvoice() { }

    public OutputInvoice(string outInvNumber, DateTime date, string venicleNumber, int supplierId, int productTitleId, string category, int productWeight)
    {
        OutInvNumber = outInvNumber;
        ShipmentDate = date;
        VehicleNumber = venicleNumber;
        SupplierId = supplierId;
        ProductTitleId = productTitleId;
        Category = category;
        ProductWeight = productWeight;
    }

    public OutputInvoice(string outInvNumber, DateTime date, string vehicleNumber, DepotItem depotItem, string category, int productWeight)
    {
        OutInvNumber = outInvNumber;
        ShipmentDate = date;
        VehicleNumber = vehicleNumber;
        SupplierId = depotItem.SupplierId;
        ProductTitleId = depotItem.ProductTitleId;
        DepotItemId = depotItem.Id;
        Category = category;
        ProductWeight = productWeight;

        Shipment(depotItem);
    }

    private void Shipment(DepotItem depotItem)
    {
        foreach (var c in depotItem.Categories)
        {
            if (c.CategoryTitle == Category)
                c.CategoryValue -= ProductWeight;
        }
    }


    // вивод на консоль
    public override string ToString()
    {
        return $"\nРасходная накладная №{OutInvNumber}.\n" +
               $"---------------------------\n" +
               $"Дата отгрузки:             {ShipmentDate.ToString("dd.MM.yyyy")}\n" +
               $"Номер ТС:                  {VehicleNumber}\n" +
               $"Поставщик:                 {Supplier}\n" +
               $"Наименование продукции:    {ProductTitle}\n" +
               $"Категория продукции:       {Category}\n" +
               $"Вес нетто:                 {ProductWeight} кг\n\n";
    }
}

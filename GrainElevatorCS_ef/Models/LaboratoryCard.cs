using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class LaboratoryCard
{
    public int Id { get; set; }

    public int LabCardNumber { get; set; }

    public string InvNumber { get; set; } = null!;

    public DateTime ArrivalDate { get; set; }

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public int PhysicalWeight { get; set; }

    public double Weediness { get; set; }

    public double Moisture { get; set; }

    public double? GrainImpurity { get; set; }

    public string? SpecialNotes { get; set; }

    public bool IsProduction { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual InputInvoice IdNavigation { get; set; } = null!;

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual ProductionBatch? ProductionBatch { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;

    public LaboratoryCard() { }

    public LaboratoryCard(InputInvoice inv, int labCardNumber, double weediness, double moisture, double grainImpurity = 0, string specialNotes = "")
    {
        Id = inv.Id;
        ArrivalDate = inv.ArrivalDate;
        InvNumber = inv.InvNumber;
        SupplierId = inv.SupplierId;
        ProductTitleId = inv.ProductTitleId;
        PhysicalWeight = inv.PhysicalWeight;

        LabCardNumber = labCardNumber;
        Weediness = weediness;
        Moisture = moisture;
        GrainImpurity = grainImpurity;
        SpecialNotes = specialNotes;
    }
}

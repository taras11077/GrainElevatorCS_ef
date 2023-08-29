﻿using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class ProductTitle
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<CompletionReport> CompletionReports { get; set; } = new List<CompletionReport>();

    public virtual ICollection<DepotItem> DepotItems { get; set; } = new List<DepotItem>();

    public virtual ICollection<InputInvoice> InputInvoices { get; set; } = new List<InputInvoice>();

    public virtual ICollection<LaboratoryCard> LaboratoryCards { get; set; } = new List<LaboratoryCard>();

    public virtual ICollection<OutputInvoice> OutputInvoices { get; set; } = new List<OutputInvoice>();

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual ICollection<Register> Registers { get; set; } = new List<Register>();


    public ProductTitle() { }

    public ProductTitle(string title)
    {
        Title = title;
    }
}

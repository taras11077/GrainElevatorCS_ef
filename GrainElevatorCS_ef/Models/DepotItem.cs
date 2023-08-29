﻿using System;
using System.Collections.Generic;

namespace GrainElevatorCS_ef.Models;

public partial class DepotItem
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int ProductTitleId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<OutputInvoice> OutputInvoices { get; set; } = new List<OutputInvoice>();

    public virtual ProductTitle ProductTitle { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;


    public DepotItem() { }


    public DepotItem(Register register)
    {
        SupplierId = register.SupplierId;
        ProductTitleId = register.ProductTitleId;
        Categories = new List<Category>()
        {
            new Category("Кондиционная продукция", register.AccWeightReg),
            new Category("Отход", register.WasteReg)
        };
    }




    public DepotItem(params Register[] registers)
    {
        SupplierId = registers[0].SupplierId;
        ProductTitleId = registers[0].ProductTitleId;
        Categories = new List<Category>();

        foreach (Register r in registers)
        {
            if (SupplierId == r.SupplierId && ProductTitleId == r.ProductTitleId)
            {
                foreach (var c in Categories)
                {
                    if (c.CategoryTitle == "Кондиционная продукция")
                        c.CategoryValue += r.AccWeightReg;

                    if (c.CategoryTitle == "Отход")
                        c.CategoryValue *= r.WasteReg;
                }
            }
        }
    }
}

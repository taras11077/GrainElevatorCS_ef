
using GrainElevatorCS_ef.Models;
using Microsoft.EntityFrameworkCore;

using Db db = new Db();

#region TEST: Запись в БД

// добавление Поставщика
await db.Suppliers.AddRangeAsync(
    new Supplier() { Title = "Хлібодар" },
    new Supplier() { Title = "Хортиця" });

// добавление Наименования продукции
await db.ProductTitles.AddRangeAsync(
    new ProductTitle() { Title = "пшениця" },
    new ProductTitle() { Title = "кукурудза" },
    new ProductTitle() { Title = "ячмінь" },
    new ProductTitle() { Title = "ріпак" });

await db.SaveChangesAsync();


// добавление Приходной накладной
InputInvoice inputInvoice1 = new InputInvoice() { InvNumber = "АЕ205301", ArrivalDate = DateTime.Now, VehicleNumber = "AA1111BH", SupplierId = 1, ProductTitleId = 1, PhysicalWeight = 35570 };
InputInvoice inputInvoice2 = new InputInvoice() { InvNumber = "АЕ205302", ArrivalDate = DateTime.Now, VehicleNumber = "AА2222BH", SupplierId = 1, ProductTitleId = 2, PhysicalWeight = 30750 };
InputInvoice inputInvoice3 = new InputInvoice() { InvNumber = "АЕ205303", ArrivalDate = DateTime.Now, VehicleNumber = "AA3333BH", SupplierId = 1, ProductTitleId = 3, PhysicalWeight = 27250 };
InputInvoice inputInvoice4 = new InputInvoice() { InvNumber = "АЕ205304", ArrivalDate = DateTime.Now, VehicleNumber = "АА4444ВН", SupplierId = 1, ProductTitleId = 4, PhysicalWeight = 28380 };

InputInvoice inputInvoice5 = new InputInvoice() { InvNumber = "АЕ205305", ArrivalDate = DateTime.Now, VehicleNumber = "AA5555BH", SupplierId = 2, ProductTitleId = 1, PhysicalWeight = 25350 };
InputInvoice inputInvoice6 = new InputInvoice() { InvNumber = "АЕ205306", ArrivalDate = DateTime.Now, VehicleNumber = "AА6666BH", SupplierId = 2, ProductTitleId = 2, PhysicalWeight = 33570 };
InputInvoice inputInvoice7 = new InputInvoice() { InvNumber = "АЕ205307", ArrivalDate = DateTime.Now, VehicleNumber = "AA7777BH", SupplierId = 2, ProductTitleId = 3, PhysicalWeight = 28750 };
InputInvoice inputInvoice8 = new InputInvoice() { InvNumber = "АЕ205308", ArrivalDate = DateTime.Now, VehicleNumber = "АА8888ВН", SupplierId = 2, ProductTitleId = 4, PhysicalWeight = 25250 };

await db.InputInvoices.AddRangeAsync(
    inputInvoice1, inputInvoice2, inputInvoice3, inputInvoice4,
    inputInvoice5, inputInvoice6, inputInvoice7, inputInvoice8);

await db.SaveChangesAsync();


// добавление Лабораторной карти
LaboratoryCard laboratoryCard1 = new LaboratoryCard(inputInvoice1, 101, 25.2, 15.9);
LaboratoryCard laboratoryCard2 = new LaboratoryCard(inputInvoice2, 102, 20.2, 12.9);
LaboratoryCard laboratoryCard3 = new LaboratoryCard(inputInvoice3, 103, 15.2, 10.9);
LaboratoryCard laboratoryCard4 = new LaboratoryCard(inputInvoice4, 104, 17.9, 13.1);
LaboratoryCard laboratoryCard5 = new LaboratoryCard(inputInvoice5, 105, 23.2, 13.9);
LaboratoryCard laboratoryCard6 = new LaboratoryCard(inputInvoice6, 106, 18.2, 10.9);
LaboratoryCard laboratoryCard7 = new LaboratoryCard(inputInvoice7, 107, 13.2, 8.9);
LaboratoryCard laboratoryCard8 = new LaboratoryCard(inputInvoice8, 108, 15.9, 11.1);

await db.LaboratoryCards.AddRangeAsync( laboratoryCard1, laboratoryCard2, laboratoryCard3, laboratoryCard4, 
                                        laboratoryCard5, laboratoryCard6, laboratoryCard7, laboratoryCard8);

await db.SaveChangesAsync();

// добавление Реестра
Register reg1 = new Register(1001, 1, 13, laboratoryCard1);
Register reg2 = new Register(1002, 1, 14, laboratoryCard2);
Register reg3 = new Register(1003, 1, 13, laboratoryCard3);
Register reg4 = new Register(1004, 1, 8, laboratoryCard4);
Register reg5 = new Register(1005, 1, 13, laboratoryCard5);
Register reg6 = new Register(1006, 1, 14, laboratoryCard6);
Register reg7 = new Register(1007, 1, 13, laboratoryCard7);
Register reg8 = new Register(1008, 1, 8, laboratoryCard8);


await db.Registers.AddRangeAsync(reg1, reg2, reg3, reg4, reg5, reg6, reg7, reg8);
await db.SaveChangesAsync();

// добавление Складской единици
DepotItem item1 = new DepotItem(reg1);
DepotItem item2 = new DepotItem(reg2);
DepotItem item3 = new DepotItem(reg3);
DepotItem item4 = new DepotItem(reg4);
DepotItem item5 = new DepotItem(reg5);
DepotItem item6 = new DepotItem(reg6);
DepotItem item7 = new DepotItem(reg7);
DepotItem item8 = new DepotItem(reg8);

await db.DepotItems.AddRangeAsync(item1, item2, item3, item4, item5, item6, item7, item8);
await db.SaveChangesAsync();


// добавление Прайс-листа
PriceList pl1 = new PriceList("пшениця");
pl1.AddOperation("Приемка", 130);
pl1.AddOperation("Первичная очистка", 1290);
pl1.AddOperation("Сушка в шахтной сушилке", 900);

await db.PriceLists.AddRangeAsync(pl1);
await db.SaveChangesAsync();


// добавление Акта доработки (Производство)
CompletionReport cp1 = new CompletionReport(11, DateTime.Now, reg1, reg5);

await db.CompletionReports.AddRangeAsync(cp1);
await db.SaveChangesAsync();


// добавление Акта доработки (Бухгалтерия)
cp1.CalcByPrice(pl1);
await db.SaveChangesAsync();


// добавление Расходной накладной
OutputInvoice outputInvoice1 = new OutputInvoice("PH-1001", DateTime.Now, "AA7788HE", item1, "Кондиционная продукция", 10370 );
OutputInvoice outputInvoice2 = new OutputInvoice("PH-1002", DateTime.Now, "AP1133EC", item1, "Отход", 4510);

await db.OutputInvoices.AddRangeAsync(outputInvoice1, outputInvoice2);
await db.SaveChangesAsync();

#endregion





#region TEST: Выгрузка из БД

// выгрузка Приходных накладных 
IEnumerable<InputInvoice> inputInvoices = db.InputInvoices.ToList();
foreach (InputInvoice invoice in inputInvoices)
    Console.WriteLine(invoice);



// выгрузка Реестра и ППП через ППП
IEnumerable<ProductionBatch> productionBatches = db.ProductionBatches
                                                    .Include(pb => pb.Register)
                                                    .ToList();

foreach (var pb in productionBatches)
{
    pb.Register.PrintReg();
    Console.WriteLine($"\n{pb}");
}


// выгрузка Реестра и ППП через Реестр
IEnumerable<Register> registers = db.Registers
                                  .Include(reg => reg.ProductionBatches)
                                  .ToList();

foreach (var r in registers)
{
    r.PrintReg();
    foreach (var pb in r.ProductionBatches)
        Console.WriteLine($"\n{pb}");
}


// выгрузка Расходных накладных 
IEnumerable<OutputInvoice> outputInvoices = db.OutputInvoices.ToList();
foreach (OutputInvoice invoice in outputInvoices)
    Console.WriteLine(invoice);











#endregion




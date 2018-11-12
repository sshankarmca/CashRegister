using CashRegister.Common.ModelContracts;

namespace CashRegister.DataLayer.DataModel
{
    public class SaleItem : ISaleItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int MeasurementType { get; set; }
        public int UnitOfMeasurement { get; set; }
        public decimal UnitPrice { get; set; }

    }
}

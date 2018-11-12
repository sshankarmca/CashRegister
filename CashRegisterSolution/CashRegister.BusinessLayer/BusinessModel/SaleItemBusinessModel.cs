using static CashRegister.Common.Enums;

namespace CashRegister.BusinessLayer.BusinessModel
{
    public class SaleItemBusinessModel
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

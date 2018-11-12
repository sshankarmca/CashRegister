using static CashRegister.Common.Enums;

namespace CashRegister.BusinessLayer.BusinessModel
{
    public class CouponBusinessModel
    {
        public string Code { get; set; }
        public ItemType ProductType { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal? Percentage { get; set; }
        public int? EligibleQuantity { get; set; }
        public int? DiscountQuantity { get; set; }
    }
}

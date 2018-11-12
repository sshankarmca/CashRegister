using CashRegister.Common.ModelContracts;

namespace CashRegister.DataLayer.DataModel
{
    public class Coupon : ICoupon
    {
        public string Code { get; set; }
        public int ProductType { get; set; }
        public int DiscountType { get; set; }
        public decimal? Percentage { get; set; }
        public int? EligibleQuantity { get; set; }
        public int? DiscountQuantity { get; set; }
    }
}

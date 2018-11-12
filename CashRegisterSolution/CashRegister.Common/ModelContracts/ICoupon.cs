using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Common.ModelContracts
{
    public class ICoupon
    {
        string Code { get; set; }
        int ProductType { get; set; }
        int DiscountType { get; set; }
        decimal? Percentage { get; set; }
        int? EligibleQuantity { get; set; }
        int? DiscountQuantity { get; set; }
    }
}

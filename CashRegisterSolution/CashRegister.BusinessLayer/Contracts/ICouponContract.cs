
using CashRegister.BusinessLayer.BusinessModel;

namespace CashRegister.BusinessLayer.Contracts
{
    public interface ICouponContract
    {
        CouponResult GetCoupon (string couponCode);

        CouponCollectionResult GetAllCoupons ();
    }
}

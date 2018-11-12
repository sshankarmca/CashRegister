using System.Collections.Generic;

namespace CashRegister.BusinessLayer.BusinessModel
{
    public class ResultItem
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class CouponResult : ResultItem
    {
        public CouponBusinessModel Coupon { get; set; }
    }

    public class CouponCollectionResult : ResultItem
    {
        public List<CouponBusinessModel> CouponList { get; set; }

        public CouponCollectionResult (List<CouponBusinessModel> couponList)
        {
            CouponList = couponList;
        }

        public CouponCollectionResult ():this(new List<CouponBusinessModel>())
        {
        }
    }

    public class SaleItemResult : ResultItem
    {
        public SaleItemBusinessModel SaleItem { get; set; }
    }

    public class SaleItemCollectionResult : ResultItem
    {
        public List<SaleItemBusinessModel> SaleItemList { get; set; }

        public SaleItemCollectionResult (List<SaleItemBusinessModel> saleItemList)
        {
            SaleItemList = saleItemList;
        }

        public SaleItemCollectionResult ():this(new List<SaleItemBusinessModel>())
        {
        }
    }

    public class CashRegisterSummaryResult : ResultItem
    {
        public CashRegisterSummaryItem CashRegisterSummary { get; set; }
    }



}

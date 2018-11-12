using CashRegister.BusinessLayer.Contracts;
using CashRegister.DataLayer.DataContract;
using CashRegister.DataLayer.DataModel;
using CashRegister.DataLayer.DataProviders;
using CashRegister.BusinessLayer.BusinessModel;
using System;
using static CashRegister.Common.Enums;

namespace CashRegister.BusinessLayer.Business
{
    public class CouponManager : ICouponContract
    {

        /// <summary>
        /// Constructor to support Dependency Injection (Constructor Injection)
        /// </summary>
        /// <param name="coupon"></param>
        public CouponManager (ICommonCRUDContract<Coupon> coupon)
        {
            CouponDataProvider = coupon;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CouponManager () : this(new CouponDBProvider())
        {
        }

        #region Private Properties

        private ICommonCRUDContract<Coupon> CouponDataProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// To get a specific Coupon from the data layer
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public CouponResult GetCoupon (string couponCode)
        {
            var couponResult = new CouponResult { Success = true };

            try
            {
                var coupon = CouponDataProvider.Get(couponCode);
                couponResult.Coupon = ConvertDataToBusinessModel(coupon);
            }
            catch (Exception exception)
            {
                couponResult.Success = false;
                couponResult.ErrorDescription = exception.Message;
            }

            return couponResult;
        }

        /// <summary>
        /// To get all Coupons from the Data Layer
        /// </summary>
        /// <returns></returns>
        public CouponCollectionResult GetAllCoupons ()
        {
            var couponCollectionResult = new CouponCollectionResult { Success = true };

            try
            {
                var coupons = CouponDataProvider.GetAll();

                foreach (var cpn in coupons) { couponCollectionResult.CouponList.Add(ConvertDataToBusinessModel(cpn)); }

            }
            catch (Exception exception)
            {
                couponCollectionResult.Success = false;
                couponCollectionResult.ErrorDescription = exception.Message;
            }

            return couponCollectionResult;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts Data Object into Business Object
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        private CouponBusinessModel ConvertDataToBusinessModel (Coupon coupon)
        {
            return new CouponBusinessModel
            {
                Code = coupon.Code,
                ProductType = (ItemType) coupon.ProductType,
                DiscountType = (DiscountType) coupon.DiscountType,
                Percentage = coupon.Percentage,
                EligibleQuantity = coupon.EligibleQuantity,
                DiscountQuantity = coupon.DiscountQuantity
            };
        }

        #endregion

    }
}

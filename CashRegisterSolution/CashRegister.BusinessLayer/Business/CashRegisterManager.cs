using CashRegister.BusinessLayer.BusinessModel;
using CashRegister.BusinessLayer.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using CashRegister.DataLayer.DataContract;
using CashRegister.DataLayer.DataModel;
using CashRegister.DataLayer.DataProviders;
using CashRegister.Common;

namespace CashRegister.BusinessLayer.Business
{
    public class CashRegisterManager : ICashRegisterContract
    {

        /// <summary>
        /// Constructor to support Dependency Injection (Constructor Injection)
        /// </summary>
        /// <param name="coupon"></param>
        public CashRegisterManager (ICommonCRUDContract<Coupon> coupon)
        {
            CouponDataProvider = coupon;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CashRegisterManager () : this(new CouponDBProvider())
        {
        }

        #region Private Properties

        private ICommonCRUDContract<Coupon> CouponDataProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// To get the Cash Register Summary with all Business calculations and validations
        /// </summary>
        /// <param name="transactions">Cash register transactions</param>
        /// <param name="couponCode">Coupon code to apply</param>
        /// <returns></returns>
        public CashRegisterSummaryResult GetCashRegisterSummary (List<TransactionItem> transactions, string couponCode)
        {
            var cashRegisterSummaryResult = new CashRegisterSummaryResult { Success = true };

            try
            {
                //Calculate the discount value
                var discountValue = GetDiscountValue(transactions, couponCode);

                if (transactions.Count > 0)
                {
                    cashRegisterSummaryResult.CashRegisterSummary = new CashRegisterSummaryItem
                    {
                        Transactions = transactions,
                        GrandTotal = transactions.Sum(e => e.Total),
                        DiscountTotal = discountValue ?? 0
                    };
                }
            }

            #region Custom Exception Handling
            //catch (ValidationException validationException)
            //{
            //    //Placeholder to handle custom validation exceptions
            //}
            #endregion

            catch (ApplicationException)
            {
                // Placeholder to Impement Application Exception Handling
            }

            catch (Exception exception)
            {
                cashRegisterSummaryResult.Success = false;
                cashRegisterSummaryResult.ErrorDescription = exception.Message;
                //Placeholder to Handle Unexpected error 
            }

            return cashRegisterSummaryResult;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// To calculate the discount value for various coupon types
        /// </summary>
        /// <param name="transactions">Cash register transactions</param>
        /// <param name="couponCode">Coupon code to apply</param>
        /// <returns></returns>
        internal decimal? GetDiscountValue (List<TransactionItem> transactions, string couponCode)
        {
            decimal? discountValue = 0;

            if (!String.IsNullOrEmpty(couponCode))
            {
                var coupon = CouponDataProvider.Get(couponCode);

                if (coupon != null)
                {
                    if (coupon.DiscountType == (int) Enums.DiscountType.PercentageOnTotal)
                    {
                        discountValue = GetPercentageDiscount(transactions, coupon);
                    }
                    else if (coupon.DiscountType == (int) Enums.DiscountType.FreeOnCount)
                    {
                        discountValue = GetFreeOnCountDiscount(transactions, coupon);
                    }
                }
            }

            return discountValue;
        }

        /// <summary>
        /// To calculate discount value for percentage coupons
        /// </summary>
        /// <param name="transactions">Cash register transactions</param>
        /// <param name="couponCode">Coupon code to apply</param>
        /// <returns></returns>
        internal decimal? GetPercentageDiscount (List<TransactionItem> transactions, Coupon coupon)
        {
            var gTotal = transactions.Sum(e => e.Total);

            return
                gTotal * ( coupon.Percentage / 100 );
        }

        /// <summary>
        /// To calculate discount value for free on count coupons
        /// </summary>
        /// <param name="transactions">Cash register transactions</param>
        /// <param name="couponCode">Coupon code to apply</param>
        /// <returns></returns>
        internal decimal? GetFreeOnCountDiscount (List<TransactionItem> transactions, Coupon coupon)
        {
            decimal discountValue = 0;
            /*Measurement Type Weight - Always considered as 1 Unit
                Measurement Type NumberOfUnits - Actual Units */
            //var totalCount =
            //    transactions.Where(t => t.MeasurementType == Enums.MeasurementType.Weight).Count()
            //    + transactions.Where(t => t.MeasurementType == Enums.MeasurementType.NumberOfUnits)
            //    .Sum(s => s.NumberOfUnits);

            var totalCount = transactions.Sum(e => e.NumberOfUnits);

            /*Calculation Logic for FreeOnCount coupons (For Ex. Buy 3 get 1 free)
                Here Eligible Quantity is 3 and DiscountQuantity is 1
                Divide totalCount with Eligible Quantity and get the numerator and multiply with DiscountQuantity
                So the allowed discount quantity = numerator
             */
            var allowedFreeCount = ( (int) totalCount / coupon.EligibleQuantity ) * coupon.DiscountQuantity;

            /*Get the X (allowedFreeCount) number of least valued transactions */
            var leastValuedTransactions =
                            transactions.OrderBy(e => e.UnitPrice).ToList();

            //Calculate discount amount for the allowed quantity
            int remainingDiscountCount = allowedFreeCount ?? 0;

            foreach (var tran in leastValuedTransactions)
            {
                if (tran.NumberOfUnits <= remainingDiscountCount)
                {                    
                    discountValue += ( tran.UnitPrice * (int) tran.NumberOfUnits );

                    remainingDiscountCount -= (int) tran.NumberOfUnits;
                }
                else
                {
                    discountValue += ( tran.UnitPrice * remainingDiscountCount );

                    remainingDiscountCount = 0;

                    break;
                }
            }

            return discountValue;
        }

        #endregion
        
    }


}

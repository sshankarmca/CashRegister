using CashRegister.DataLayer.DataContract;
using CashRegister.DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister.DataLayer.DataProviders
{
    /// <summary>
    /// Coupon DB Provider class implements generic Interface for CRUD operations
    /// </summary>
    public class CouponDBProvider : ICommonCRUDContract<Coupon>
    {
        public Coupon Get (string uniqueCode)
        {
            return
                SampleData.GetCoupons()
                .Where(e => e.Code.ToUpper() == uniqueCode.ToUpper())
                .FirstOrDefault();
        }

        public List<Coupon> GetAll ()
        {
            return
                SampleData.GetCoupons();
        }

        public void Add (Coupon entity)
        {
            throw new NotImplementedException();
        }

        public void Delete (string uniqueCode)
        {
            throw new NotImplementedException();
        }

        public void Update (Coupon entity)
        {
            throw new NotImplementedException();
        }
    }
}

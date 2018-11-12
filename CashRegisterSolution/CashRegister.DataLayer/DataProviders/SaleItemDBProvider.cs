using System;
using System.Collections.Generic;
using System.Linq;
using CashRegister.DataLayer.DataModel;
using CashRegister.DataLayer.DataContract;

namespace CashRegister.DataLayer
{
    /// <summary>
    /// SaleItem DB Provider class implements generic Interface for CRUD operations
    /// </summary>
    public class SaleItemDBProvider : ICommonCRUDContract<SaleItem>
    {
        public SaleItem Get (string uniqueCode)
        {
            return
                SampleData.GetItems()
                .Where(e => e.Code.ToUpper() == uniqueCode.ToUpper())
                .FirstOrDefault();
        }

        public List<SaleItem> GetAll ()
        {
            return
                SampleData.GetItems();
        }

        public void Add (SaleItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete (string uniqueCode)
        {
            throw new NotImplementedException();
        }

        public void Update (SaleItem entity)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections.Generic;

namespace CashRegister.BusinessLayer.BusinessModel
{
    /// <summary>
    /// Business Model to support display
    /// </summary>
    public class CashRegisterSummaryItem
    {
        public decimal GrandTotal { get; set; }
        public decimal DiscountTotal { get; set; }

        //ReadOnly Property - Calculated
        public decimal NetTotal
        {
            get
            {
                return GrandTotal - DiscountTotal;
            }
        }

        public ICollection<TransactionItem> Transactions { get; set; }

        public CashRegisterSummaryItem ()
        {
            Transactions = new List<TransactionItem>();
        }
    }


}

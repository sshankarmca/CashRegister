using CashRegister.BusinessLayer.BusinessModel;
using System.Collections.Generic;

namespace CashRegister.BusinessLayer.Contracts
{
    public interface ICashRegisterContract
    {
        CashRegisterSummaryResult GetCashRegisterSummary 
            (List<TransactionItem> transactions, string couponCode);
    }
}

using CashRegister.BusinessLayer.BusinessModel;

namespace CashRegister.BusinessLayer.Contracts
{
    public interface ISaleItemContract
    {
        SaleItemResult GetItem (string itemCode);

        SaleItemCollectionResult GetAllSaleItems ();
    }
}

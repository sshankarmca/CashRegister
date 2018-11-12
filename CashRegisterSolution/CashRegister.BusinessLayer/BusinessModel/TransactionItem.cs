namespace CashRegister.BusinessLayer.BusinessModel
{
    public class TransactionItem : SaleItemBusinessModel
    {
        public double NumberOfUnits { get; set; }

        //ReadOnly Property
        public decimal Total
        {
            get
            {
                return ( UnitPrice * (decimal)NumberOfUnits );
            }
        }
    }
}

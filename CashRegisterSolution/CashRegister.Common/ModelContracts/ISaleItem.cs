using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Common.ModelContracts
{
    public interface ISaleItem
    {
        string Code { get; set; }
        string Name { get; set; }
        int Type { get; set; }
        int MeasurementType { get; set; }
        int UnitOfMeasurement { get; set; }
        decimal UnitPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Common
{
    public class Enums
    {
        public enum ItemType
        {
            RiceAndFlour = 1,
            FruitsAndVegetables,
            OilAndSauses,
            DairyProducts,
            MeatAndProduce
        }

        public enum MeasurementType
        {
            NumberOfUnits = 1,
            Weight
        }

        public enum UnitOfMeasurement
        {
            Kilogram = 1,
            Pounds,
            Count
        }

        public enum DiscountType
        {
            PercentageOnTotal = 1,
            FreeOnCount
        }
    }
}

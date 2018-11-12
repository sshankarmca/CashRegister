using System.Collections.Generic;

namespace CashRegister.DataLayer.DataModel
{
    /// <summary>
    /// Using Sample data to avoid real Database interaction (Time constraint)
    /// This can be replaced with Entity Framework DBContext
    /// </summary>
    public static class SampleData
    {
        public static List<SaleItem> GetItems()
        {
            return new List<SaleItem> {
                new SaleItem
                {
                    Code = "001",
                    Name = "Whole Wheat",
                    Type = 1,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 3.49M
                },
                new SaleItem
                {
                    Code = "002",
                    Name = "Brown Rice",
                    Type = 1,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 2.94M
                },
                new SaleItem
                {
                    Code = "003",
                    Name = "Tomato Sause 20 Oz",
                    Type = 3,
                    MeasurementType = 1,
                    UnitOfMeasurement = 3,
                    UnitPrice = 1.96M,
                },
                new SaleItem
                {
                    Code = "004",
                    Name = "Extra Virgin Olive 22 Oz",
                    Type = 3,
                    MeasurementType = 1,
                    UnitOfMeasurement = 3,
                    UnitPrice = 9.97M,
                },
                new SaleItem
                {
                    Code = "005",
                    Name = "Low fat yogurt 20 Oz",
                    Type = 4,
                    MeasurementType = 1,
                    UnitOfMeasurement = 3,
                    UnitPrice = 5.97M,
                },
                new SaleItem
                {
                    Code = "006",
                    Name = "Sweet Potatoes",
                    Type = 2,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 1.60M,
                },
                new SaleItem
                {
                    Code = "007",
                    Name = "Bananas",
                    Type = 2,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 2.29M,
                },
                new SaleItem
                {
                    Code = "008",
                    Name = "Apples",
                    Type = 2,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 4.99M,
                },
                new SaleItem
                {
                    Code = "009",
                    Name = "Strawberries",
                    Type = 2,
                    MeasurementType = 2,
                    UnitOfMeasurement = 2,
                    UnitPrice = 7.99M,
                },
                new SaleItem
                {
                    Code = "010",
                    Name = "Eggs 12 Count Pack",
                    Type = 5,
                    MeasurementType = 1,
                    UnitOfMeasurement = 3,
                    UnitPrice = 4.50M,
                },
            };
        }

        public static List<Coupon> GetCoupons()
        {
            return new List<Coupon>
            {
                new Coupon
                {
                    Code = "C101",
                    ProductType = 1,
                    DiscountType = 1,
                    Percentage = 12,
                },
                new Coupon
                {
                    Code = "C102",
                    ProductType = 2,
                    DiscountType = 1,
                    Percentage = 20,
                },
                new Coupon
                {
                    Code = "C103",
                    ProductType = 3,
                    DiscountType = 2,
                    EligibleQuantity = 3,
                    DiscountQuantity = 1
                },
                new Coupon
                {
                    Code = "C104",
                    ProductType = 4,
                    DiscountType = 2,
                    EligibleQuantity = 5,
                    DiscountQuantity = 2
                },
                new Coupon
                {
                    Code = "C105",
                    ProductType = 5,
                    DiscountType = 2,
                    EligibleQuantity = 2,
                    DiscountQuantity = 1
                }
            };
        }
    }
}

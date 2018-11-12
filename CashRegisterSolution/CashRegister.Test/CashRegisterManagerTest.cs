using System.Collections.Generic;
using CashRegister.BusinessLayer.Business;
using CashRegister.BusinessLayer.BusinessModel;
using CashRegister.DataLayer.DataContract;
using CashRegister.DataLayer.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static CashRegister.Common.Enums;

namespace CashRegister.Test
{
    [TestClass]
    public class CashRegisterManagerTest
    {
        #region Mock Variables

        private static Mock<ICommonCRUDContract<Coupon>> _mockCoupon;

        #endregion

        #region Additional Test Attributes

        [TestInitialize()]
        public void MyTestInitialize ()
        {
            _mockCoupon = new Mock<ICommonCRUDContract<Coupon>>();
        }

        [TestCleanup()]
        public void MyTestCleanup () { }

        #endregion

        #region Unit Test

        [TestMethod]
        public void
            GetCashRegisterSummary_WithPercentageCoupon_WhenSuccess_WillReturnRegisterSummaryWithPercentageCalculatedSummary ()
        {

            //Arrange

            var mockCouponCode = "C001";

            var mockTransactions = new List<TransactionItem>
            {
                new TransactionItem
                {
                    ItemCode = "001",
                    ItemName = "Whole Wheat",
                    ItemType = ItemType.RiceAndFlour,
                    MeasurementType = MeasurementType.Weight,
                    UnitOfMeasurement = UnitOfMeasurement.Pounds,
                    UnitPrice = 3.49M,
                    NumberOfUnits = 2.5
                },
                new TransactionItem
                {
                    ItemCode = "002",
                    ItemName = "Brown Rice",
                    ItemType = ItemType.RiceAndFlour,
                    MeasurementType = MeasurementType.Weight,
                    UnitOfMeasurement = UnitOfMeasurement.Pounds,
                    UnitPrice = 2.94M,
                    NumberOfUnits = 3
                },
                new TransactionItem
                {
                    ItemCode = "006",
                    ItemName = "Sweet Potatoes",
                    ItemType = ItemType.FruitsAndVegetables,
                    MeasurementType = MeasurementType.Weight,
                    UnitOfMeasurement = UnitOfMeasurement.Pounds,
                    UnitPrice = 1.60M,
                    NumberOfUnits = 5
                },
            };

            var mockCoupon = new Coupon
            {
                Code = "C001",
                DiscountType =1,
                Percentage = 10
            };

            _mockCoupon.Setup(e => e.Get(mockCouponCode)).Returns(mockCoupon);

            decimal expectedGrandTotal = 0.0m;

            foreach (var item in mockTransactions)
            {
                expectedGrandTotal += ( item.UnitPrice * (decimal) item.NumberOfUnits );
            }

            decimal expectedDiscountValue =
                ( expectedGrandTotal ) * ( (decimal) mockCoupon.Percentage / 100 );

            decimal expectedNetTotal = expectedGrandTotal - expectedDiscountValue;

            //Act
            var actualResult = CashRegisterManagerBO.GetCashRegisterSummary(mockTransactions, mockCouponCode);

            //Assert
            Assert.IsTrue(actualResult.Success == true
                && actualResult.CashRegisterSummary.GrandTotal == expectedGrandTotal
                && actualResult.CashRegisterSummary.DiscountTotal == expectedDiscountValue
                && actualResult.CashRegisterSummary.NetTotal == expectedNetTotal
                );
        }

        [TestMethod]
        public void 
            GetCashRegisterSummary_WithFreeOnCountCoupon_WhenSuccess_WillReturnRegisterSummaryWithFreeOnCountCalculatedSummary ()
        {

            //Arrange

            var mockCouponCode = "C001";

            var mockTransactions = new List<TransactionItem>
            {
                new TransactionItem
                {
                    ItemCode = "001",
                    ItemName = "Whole Wheat",
                    ItemType = ItemType.RiceAndFlour,
                    MeasurementType = MeasurementType.Weight,
                    UnitOfMeasurement = UnitOfMeasurement.Pounds,
                    UnitPrice = 3.49M,
                    NumberOfUnits = 2.5
                },
                new TransactionItem
                {
                    ItemCode = "003",
                    ItemName = "Tomato Sause 20 Oz",
                    ItemType = ItemType.OilAndSauses,
                    MeasurementType = MeasurementType.NumberOfUnits,
                    UnitOfMeasurement = UnitOfMeasurement.Count,
                    UnitPrice = 1.96M,
                    NumberOfUnits = 2
                },
                new TransactionItem
                {
                    ItemCode = "004",
                    ItemName = "Extra Virgin Olive 22 Oz",
                    ItemType = ItemType.OilAndSauses,
                    MeasurementType = MeasurementType.NumberOfUnits,
                    UnitOfMeasurement = UnitOfMeasurement.Count,
                    UnitPrice = 9.97M,
                    NumberOfUnits = 4,
                },
                new TransactionItem
                {
                    ItemCode = "005",
                    ItemName = "Low fat yogurt 20 Oz",
                    ItemType = ItemType.DairyProducts,
                    MeasurementType = MeasurementType.NumberOfUnits,
                    UnitOfMeasurement = UnitOfMeasurement.Count,
                    UnitPrice = 5.97M,
                    NumberOfUnits = 2
                },
            };

            var mockCoupon = new Coupon
            {
                Code = "C103",
                DiscountType = 2,
                EligibleQuantity = 3,
                DiscountQuantity = 1
            };

            _mockCoupon.Setup(e => e.Get(mockCouponCode)).Returns(mockCoupon);

            decimal expectedGrandTotal = 0.0m;

            foreach (var item in mockTransactions)
            {
                expectedGrandTotal += ( item.UnitPrice * (decimal) item.NumberOfUnits );
            }

            decimal expectedDiscountTotal = ( 1.96m * 2 ) + ( 3.49m * 1 );

            decimal expectedNetTotal = expectedGrandTotal - expectedDiscountTotal;


            //Act
            var actualResult = CashRegisterManagerBO.GetCashRegisterSummary(mockTransactions, mockCouponCode);

            //Assert
            Assert.IsTrue(actualResult.Success == true
                && actualResult.CashRegisterSummary.GrandTotal == expectedGrandTotal
                && actualResult.CashRegisterSummary.DiscountTotal == expectedDiscountTotal
                && actualResult.CashRegisterSummary.NetTotal == expectedNetTotal
                );
        }

        #endregion

        #region Helper 

        private static CashRegisterManager CashRegisterManagerBO
        {
            get
            {
                return new CashRegisterManager(_mockCoupon.Object);
            }
        }

        private static CouponManager CouponManagerBO
        {
            get
            {
                return new CouponManager();
            }
        }

        #endregion
    }
}

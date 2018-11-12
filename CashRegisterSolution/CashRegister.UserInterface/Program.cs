using CashRegister.BusinessLayer.BusinessModel;
using System;
using System.Linq;
using System.Collections.Generic;
using CashRegister.BusinessLayer.Business;

namespace CashRegister
{
    class Program
    {
        static void Main (string[] args)
        {
            var transactions = new List<TransactionItem>();

            var couponCode = string.Empty;

            //Get All Items
            var itmList = GetAllSaleItems();

            //Get All Coupons
            var couponList = GetAllCoupons();

            if (itmList.Count > 0)
            {
                //Print Items
                PrintSaleItems(itmList);

                Console.WriteLine("Press any key to start billing...");

                Console.ReadLine();

                //Get/Validate Inputs from the User and Construct the transaction
                ConstructTransactionList(itmList, ref transactions);
            }

            if (couponList.Count > 0)
            {
                //Print Coupons
                PrintCoupons(couponList);

                //Get / Validate the Coupon Code
                couponCode = GetCouponCodeUserInput(couponList);
            }

            //Print final Cash Register Summary
            PrintCashRegisterSummary(transactions, couponCode);

            Console.ReadLine();
        }

        #region UI Supporting Methods

        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns></returns>
        private static List<SaleItemBusinessModel> GetAllSaleItems ()
        {
            var saleItemManager = new SaleItemManager();

            var saleItemResult = saleItemManager.GetAllSaleItems();

            if (saleItemResult.Success)
            {
                return saleItemResult.SaleItemList;
            }

            //Suppress the Exception (For time being)
            return new List<SaleItemBusinessModel>();

        }

        /// <summary>
        /// Get All Coupons
        /// </summary>
        /// <returns></returns>
        private static List<CouponBusinessModel> GetAllCoupons ()
        {
            var couponManager = new CouponManager();

            var couponResult = couponManager.GetAllCoupons();

            if (couponResult.Success)
            {
                return couponResult.CouponList;
            }

            //Suppress the Exception for now
            return new List<CouponBusinessModel>();
        }

        /// <summary>
        /// Print Available Items in the Console
        /// </summary>
        /// <param name="itmList"></param>
        private static void PrintSaleItems (List<SaleItemBusinessModel> itmList)
        {
            const string format = "{0,-5} {1, -25} {2, -25} {3, -10} {4, 10}";
            Console.WriteLine("Available Items in the stock...");
            Console.WriteLine(string.Format(format, "Code", "Name", "SaleBy", "UOM", "Price/Unit"));
            Console.WriteLine(Repeat('-', 85));
            foreach (var itm in itmList)
            {
                var strLine =
                    string.Format(format,
                    itm.ItemCode,
                    itm.ItemName,
                    itm.MeasurementType.ToString(),
                    itm.UnitOfMeasurement.ToString(),
                    itm.UnitPrice);

                Console.WriteLine(strLine);
            }
        }

        /// <summary>
        /// Print available coupons in the console
        /// </summary>
        /// <param name="coupons"></param>
        private static void PrintCoupons (List<CouponBusinessModel> coupons)
        {
            const string format = "{0,-5} {1, -50}";
            Console.WriteLine("Coupon List...");
            Console.WriteLine(string.Format(format, "Code", "Description"));
            Console.WriteLine(Repeat('-', 50));
            string couponDescription = string.Empty;

            foreach (var cpn in coupons)
            {
                couponDescription = ( cpn.Percentage > 0
                    ? string.Format("{0} percent discount", cpn.Percentage)
                    : string.Format("Buy {0} get {1} free", cpn.EligibleQuantity, cpn.DiscountQuantity) );

                var strLine =
                    string.Format(format,
                    cpn.Code,
                    couponDescription
                    );

                Console.WriteLine(strLine);
            }
        }

        /// <summary>
        /// Print the final Cash Register Summary in the console
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="couponCode"></param>
        private static void PrintCashRegisterSummary (List<TransactionItem> transactions, string couponCode)
        {
            var cashRegisterManager = new CashRegisterManager();

            var cashRegisterSummaryResult = cashRegisterManager.GetCashRegisterSummary(transactions, couponCode);

            if (cashRegisterSummaryResult.Success)
            {
                Console.Clear();
                Console.WriteLine("Cash Register");
                Console.WriteLine(Repeat('-', 85));
                const string format = "{0, -25} {1, -25} {2, 10} {3, 10} {4, 10}";
                Console.WriteLine(string.Format(format, "Name", "SaleBy", "Price/Unit", "NrOfQty", "Total"));
                Console.WriteLine(Repeat('-', 85));

                foreach (var itm in cashRegisterSummaryResult.CashRegisterSummary.Transactions)
                {
                    var strLine =
                        string.Format(format,
                        itm.ItemName,
                        itm.MeasurementType.ToString(),
                        itm.UnitPrice,
                        itm.NumberOfUnits,
                        itm.Total
                        );

                    Console.WriteLine(strLine);
                }

                Console.WriteLine(Repeat('-', 85));

                Console.WriteLine("{0, -20} {1, 63}", "Grand Total", Math.Round(cashRegisterSummaryResult.CashRegisterSummary.GrandTotal,2));
                Console.WriteLine("{0, -20} {1, 63}", "Discount", Math.Round(cashRegisterSummaryResult.CashRegisterSummary.DiscountTotal,2));
                Console.WriteLine("{0, -20} {1, 63}", "Nett Total", Math.Round(cashRegisterSummaryResult.CashRegisterSummary.NetTotal,2));

                Console.WriteLine(Repeat('-', 85));
            }
        }

        /// <summary>
        /// Gets the transaction inputs from the user
        /// </summary>
        /// <param name="itmList"></param>
        /// <returns></returns>
        private static TransactionItem GetSaleItemUserInput (List<SaleItemBusinessModel> itmList)
        {
            string itemCode;
            double nrOfUnits;

            Code:
            Console.WriteLine("Enter Item Code: ");
            itemCode = Console.ReadLine();

            var saleItem = itmList.Where(e => e.ItemCode.ToUpper() == itemCode.ToUpper()).FirstOrDefault();
            if (saleItem == null)
            {
                Console.WriteLine("Invalid Item Code!");
                goto Code;
            }

            Quantity:
            Console.WriteLine("Enter Quantity: ");

            var strInput = Console.ReadLine();
            if (!double.TryParse(strInput, out nrOfUnits))
            {
                Console.WriteLine("Invalid Quantity!");
                goto Quantity;
            }

            var transactionItem = new TransactionItem
            {
                ItemCode = itemCode,
                ItemName = saleItem.ItemName,
                ItemType = saleItem.ItemType,
                MeasurementType = saleItem.MeasurementType,
                UnitOfMeasurement = saleItem.UnitOfMeasurement,
                UnitPrice = saleItem.UnitPrice,
                NumberOfUnits = nrOfUnits
            };

            return transactionItem;
        }

        /// <summary>
        /// Gets the coupon code inputs from the user
        /// </summary>
        /// <param name="coupons"></param>
        /// <returns></returns>
        private static string GetCouponCodeUserInput (List<CouponBusinessModel> coupons)
        {
            string couponCode = string.Empty;

            Cpn:
            Console.WriteLine("Do you want to apply coupon (Y/N): ");
            var cpnInput = Console.ReadLine();

            if (cpnInput.ToUpper() == "Y")
            {
                Console.WriteLine("Enter coupon code: ");
                couponCode = Console.ReadLine();

                var couponItem = coupons.Where(e => e.Code.ToUpper() == couponCode.ToUpper()).FirstOrDefault();

                if (couponItem == null)
                {
                    Console.WriteLine("Invalid Coupon Code!");
                    goto Cpn;
                }
            }
            else
            {
                if (cpnInput.ToUpper() != "N") { Console.WriteLine("Invalid Key!"); goto Cpn; }
            }

            return couponCode;
        }

        /// <summary>
        /// //Gets and Validates the Inputs from the User and Construct the transaction
        /// </summary>
        /// <param name="saleItems"></param>
        /// <param name="transactions"></param>
        private static void ConstructTransactionList (List<SaleItemBusinessModel> saleItems, ref List<TransactionItem> transactions)
        {
            bool userWantsToExit = false;

            while (!userWantsToExit)
            {
                transactions.Add(GetSaleItemUserInput(saleItems));

                Cont:
                Console.WriteLine("Continue (Y/N): ");

                var input = Console.ReadLine();

                if (input.ToUpper() == "N")
                {
                    userWantsToExit = true;
                    continue;
                }                    
                if (input.ToUpper() != "Y") { Console.WriteLine("Invalid Key!"); goto Cont; }
            }
        }

        /// <summary>
        /// To repeat a particular character to print
        /// </summary>
        /// <param name="character"></param>
        /// <param name="numberOfIterations"></param>
        /// <returns></returns>
        private static string Repeat (char character, int numberOfIterations)
        {
            return "".PadLeft(numberOfIterations, character);
        }

        #endregion

    }
}

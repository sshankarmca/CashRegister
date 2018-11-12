using CashRegister.BusinessLayer.BusinessModel;
using CashRegister.BusinessLayer.Contracts;
using CashRegister.DataLayer;
using CashRegister.DataLayer.DataContract;
using CashRegister.DataLayer.DataModel;
using System;
using static CashRegister.Common.Enums;

namespace CashRegister.BusinessLayer.Business
{
    public class SaleItemManager : ISaleItemContract
    {
        /// <summary>
        /// Constructor to support Dependency Injection (Constructor Injection)
        /// </summary>
        /// <param name="saleItem"></param>
        public SaleItemManager (ICommonCRUDContract<SaleItem> saleItem)
        {
            SaleItemDataProvider = saleItem;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SaleItemManager () : this(new SaleItemDBProvider())
        {

        }

        #region Private Properties

        private ICommonCRUDContract<SaleItem> SaleItemDataProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// To get a specific Item from the data layer
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public SaleItemResult GetItem (string itemCode)
        {
            var saleItemResult = new SaleItemResult { Success = true };

            try
            {
                var saleItem = SaleItemDataProvider.Get(itemCode);

                saleItemResult.SaleItem = ConvertDataToBusinessModel(saleItem);
            }
            catch (Exception exception)
            {
                saleItemResult.Success = false;
                saleItemResult.ErrorDescription = exception.Message;
            }

            return saleItemResult;
        }

        /// <summary>
        /// To get all Items from the Data Layer
        /// </summary>
        /// <returns></returns>
        public SaleItemCollectionResult GetAllSaleItems ()
        {
            var saleItemCollectionResult = new SaleItemCollectionResult { Success = true };

            try
            {
                var saleItems = SaleItemDataProvider.GetAll();

                foreach (var itm in saleItems)
                {
                    saleItemCollectionResult.SaleItemList.Add(ConvertDataToBusinessModel(itm));
                }
            }
            catch (Exception exception)
            {
                saleItemCollectionResult.Success = false;
                saleItemCollectionResult.ErrorDescription = exception.Message;
            }

            return saleItemCollectionResult;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts Data Object into Business Object
        /// </summary>
        /// <param name="saleItem"></param>
        /// <returns></returns>
        private SaleItemBusinessModel ConvertDataToBusinessModel (SaleItem saleItem)
        {
            return new SaleItemBusinessModel
            {
                ItemCode = saleItem.Code,
                ItemName = saleItem.Name,
                ItemType = (ItemType) saleItem.Type,
                MeasurementType = (MeasurementType) saleItem.MeasurementType,
                UnitOfMeasurement = (UnitOfMeasurement) saleItem.UnitOfMeasurement,
                UnitPrice = saleItem.UnitPrice
            };
        }

        #endregion


    }
}

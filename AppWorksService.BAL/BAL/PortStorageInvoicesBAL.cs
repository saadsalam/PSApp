using System;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL;

namespace AppWorksService.BAL
{
    public class PortStorageInvoicesBAL
    {
        /// <summary>
        /// Function to get the invoice list
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public IList<PortStorageInvoicesProp> GetPortStorageInvoicesList(Nullable<DateTime> cutoffDate)
        {
            IList<PortStorageInvoicesProp> listPortStorageInvoicesProp;
            PortStorageInvoicesDAL objPortStorageInvoicesDAL = new PortStorageInvoicesDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                listPortStorageInvoicesProp = objPortStorageInvoicesDAL.GetPortStorageInvoicesList(cutoffDate); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objPortStorageInvoicesDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return listPortStorageInvoicesProp;
        }

        /// <summary>
        /// Function to get the setting details
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public string SetDefaultvalue(string valueKey)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.SetDefaultvalue(valueKey);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        #region Code To be removed after code cleanup
        //public IList<PortStoragePrintErrorProp> GetPrintErrorsForInvoiceList()
        //{
        //    IList<PortStoragePrintErrorProp> listPortStoragePrintErrorProp;
        //    PortStorageInvoicesDAL objPortStorageInvoicesDAL = new PortStorageInvoicesDAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        listPortStoragePrintErrorProp = objPortStorageInvoicesDAL.GetPrintErrorsForInvoiceList(); /// sending the new record model to the data access layer.
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        objPortStorageInvoicesDAL = null;
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //    return listPortStoragePrintErrorProp;
        //}
        #endregion



        /// <summary>
        /// Function to insert the invoice billing details
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public bool UpdateBilling(BillingProp objBillingprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.UpdateBilling(objBillingprop); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to insert the invoice billing details
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public int InsertBillingId(BillingProp objBillingprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.InsertBillingId(objBillingprop); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the port storage vehicle records for invoice generation
        /// </summary>
        /// <param name="objPortStorageVehicleProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public bool UpdatePostStorageVehicles(PortStorageVehicleProp objPortStorageVehicleProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.UpdatePostStorageVehicles(objPortStorageVehicleProp); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the setting table values
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public bool UpdateSettingsValue(string invoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.UpdateSettingsValue(invoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the setting table values for next invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Feb-12,2018</createdOn>
        public bool UpdateNextInvoiceNumberValue(string iInvoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.UpdateNextInvoiceNumberValue(iInvoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        


        /// <summary>
        /// Function to get the billing details count
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public int GetBillingCount(string invoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetBillingCount(invoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get thevehicle list for generate invoice
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <param name="customerId"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public IList<PortStorageVehicleProp> GetPortStorageVehicleList(Nullable<DateTime> cutoffDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetPortStorageVehicleList(cutoffDate, customerId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to calculate the per diem for generate invoice
        /// </summary>
        /// <param name="psVehicleId"></param>
        /// <param name="userCode"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public string CalculatePortStoragePerDiem(int psVehicleId, string userCode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.CalculatePortStoragePerDiem(psVehicleId, userCode);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public int PsRatesCount(DateTime? startDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.PsRatesCount(startDate, customerId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public PSRatesInvoiceProp PsRatesInvoice(DateTime? startDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.PsRatesInvoice(startDate, customerId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the rate details in port storage table
        /// </summary>
        /// <param name="entryRate"></param>
        /// <param name="perDiemGraceDays"></param>
        /// <param name="vehicleID"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public bool UpdateVehicleRates(decimal? entryRate, int? perDiemGraceDays, int vehicleID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.UpdateVehicleRates(entryRate, perDiemGraceDays, vehicleID);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the customer information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public CustomerSearchProp GetCustomerInfo(int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetCustomerInfo(customerId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the outside carrier leg list details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public List<LoadInfoList> GetLoadInfo(int billingId, decimal pvRatePercentage)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetLoadInfo(billingId, pvRatePercentage);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the table column names
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>List<string></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public List<string> GetTableColumnNames(string tableName)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetTableColumnNames(tableName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the vehicle leg list count details
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>List<VehicleLegCountProp></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public List<VehicleLegCountProp> GetVehicleLegsInfo(int recordId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetVehicleLegsInfo(recordId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the Invoice Credit Cost Center Number
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public int GetInvoiceCreditCostCenterNumber()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetInvoiceCreditCostCenterNumber();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to insert the invoice billing line items details
        /// </summary>
        /// <param name="BillingLineItemsProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public int InsertBillingLineItems(BillingLineItemsProp objBillingLineprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.InsertBillingLineItems(objBillingLineprop);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        /// <summary>
        ///  This method is used to Get Invoice Data For Invoice Print from database
        /// </summary>
        /// <param name="ReprintInd"></param>
        /// <param name="ReprintType"></param>
        /// <param name="DateType"></param>
        /// <param name="InvoiceDateFrom"></param>
        /// <param name="InvoiceDateTo"></param>
        /// <param name="InvoiceNumberFrom"></param>
        /// <param name="InvoiceNumberTo"></param>
        /// <returns></returns>
        public IList<PortStoragePrintInvoiceProp> GetInvoiceDataForInvoicePrint(int ReprintInd, int ReprintType, int DateType, DateTime? InvoiceDateFrom, DateTime? InvoiceDateTo, string InvoiceNumberFrom, string InvoiceNumberTo)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesDAL objPortStorageInvoicesDal = new PortStorageInvoicesDAL();
                return objPortStorageInvoicesDal.GetInvoiceDataForInvoicePrint(ReprintInd, ReprintType, DateType, InvoiceDateFrom, InvoiceDateTo, InvoiceNumberFrom, InvoiceNumberTo);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
    }
}

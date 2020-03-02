using System;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace AppWorksService.BAL
{
    public class BillingBAL
    {
        /// <summary>
        /// To Add the Record of Billing Period Admin
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-19,2016</createdOn>
        public int AddBillingPeriodAdmin(BillingPeriodprop objBillingPeriod)
        {
            int result;
            BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                result = objBillingDAL.AddBillingPeriodAdmin(objBillingPeriod); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objBillingDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }

        /// <summary>
        /// To Find Billing Period Record
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>IList<BillingPeriod></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-20,2016</createdOn>
        public IList<BillingPeriodprop> FindBillingPeriod(BillingPeriodprop objBillingPeriod)
        {
            IList<BillingPeriodprop> listBillingPeriodprop;
            BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                listBillingPeriodprop = objBillingDAL.FindBillingPeriod(objBillingPeriod); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objBillingDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return listBillingPeriodprop;
        }

           /// <summary>
        /// This Method to update Billing Period Admin Details
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns></returns>
        /// <createdOn>May-23,2016</createdOn>
        public bool UpdateBillingPeriodAdminDetails(BillingPeriodprop objBillingPeriodprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();
                return objBillingDAL.UpdateBillingPeriodAdminDetails(objBillingPeriodprop);
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
        ///  This method ised to for delete billing period admin details.
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        public bool DeleteBillingPeriodAdminDetails(int BillingPeriodID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();
                return objBillingDAL.DeleteBillingPeriodAdminDetails(BillingPeriodID);
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
        /// This method is used to check duplicate calender  and period values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool CheckDuplicateCalenderAndPeriod(int year, int period)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.CheckDuplicateCalenderAndPeriod(year, period);

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
        /// This method is used to get storage vehicle details list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-20-2016</createdon>
        public List<PortStorageVehicleProp> GetStorageVehicleDetails(PortStorageVehicleProp objPortstorageProp)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.GetStorageVehicleDetails(objPortstorageProp);
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
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public List<BillingLineItemsProp> GetLineItemsList(BillingLineItemsProp objLineItems)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.GetLineItemsList(objLineItems);
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
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public bool ResetExportedInd(int billingID)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.ResetExportedInd(billingID);
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
        /// To Add Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        public int InsertBilling(BillingListProp objBillingProp)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.InsertBilling(objBillingProp);
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
        /// To Update Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        public bool UpdateBillingTab(BillingListProp objBillingProp)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.UpdateBillingTab(objBillingProp);
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
        ///  This method is used  for deleting data from billing and related tables
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        /// // <createdOn>May-23,2016</createdOn>
        public bool DeleteBillingData(int BillingID)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.DeleteBillingData(BillingID);
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
        /// To Find Billing Record
        /// </summary>
        /// <param name="BillingProp"></param>
        /// <returns>IList<BillingProp></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-14,2016</createdOn>
        public async Task<List<BillingProp>> BillingSearch(BillingProp objBilling)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return await objBillingDAL.BillingSearch(objBilling);
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


        #region CODE TO BE REMOVED AFTER CODE CLEANUP


        ///// <summary>
        ///// This method is used to get the list of the drivers.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //public List<string> DriverList()
        //{
        //    BillingDAL objBillingDAL = new BillingDAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingDAL.DriverList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// This method is used to get the list of outside carrier.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //public List<string> OutsideCarrier()
        //{
        //    BillingDAL objBillingDAL = new BillingDAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingDAL.OutsideCarrier();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}


        ///// <summary>
        ///// This method is used to get the invoice details for a billing id.
        ///// </summary>
        ///// <param name="objCodeProp"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-20-2016</createdon>
        //public List<BillingInvoiceDetailsProp> GetInvoiceDetails(bool CreditedOutInd, bool CreditMemoInd, bool NewRunInd, int BillingID, int RunID, int CustomerID)
        //{
        //    BillingDAL objBillingDAL = new BillingDAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingDAL.GetInvoiceDetails(CreditedOutInd, CreditMemoInd, NewRunInd, BillingID, RunID, CustomerID);
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }

        //}

        #endregion



        /// <summary>
        /// To get bililng data from billing table
        /// </summary>
        /// <param name="billingID"></param>
        /// <returns></returns>
        public BillingListProp GetBillingData(int billingID)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.GetBillingData(billingID);
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
        /// This method is used to Get Billing Record Export
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="ExportDate"></param>
        /// <returns></returns>
        public List<BillingLineItemsProp> GetBillingRecordExport(int ExportType, DateTime? ExportDate)
        {

            BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objBillingDAL.GetBillingRecordExport(ExportType, ExportDate); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objBillingDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }

        /// <summary>
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetBillingExportBatchId()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
                return objBillingDAL.GetBillingExportBatchId();

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
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns>bool</returns>
        public bool SetBillingExportNextBatchId(int BatchId)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
                return objBillingDAL.SetBillingExportNextBatchId(BatchId);

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
        /// This method is used to Get billing Export File Path
        /// </summary>
        /// <returns>string</returns>
        public string GetBillingExportFilePath()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
                return objBillingDAL.GetBillingExportFilePath();

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
        /// This method is used to update Billing Line Item
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="BillingLineItemsID"></param>
        /// <param name="UserCode"></param>
        public void UpdateBillingLineItem(int BatchId, int BillingLineItemsID, string UserCode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingDAL objBillingDAL = new BillingDAL();  /// Creating The Object of BillingDAL 
                objBillingDAL.UpdateBillingLineItem(BatchId, BillingLineItemsID, UserCode);

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

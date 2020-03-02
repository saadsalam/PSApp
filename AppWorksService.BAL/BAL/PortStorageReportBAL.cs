using System;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Threading.Tasks;

namespace AppWorksService.BAL
{
    public class PortStorageReportBAL
    {
        PortStorageReportDAL objPortStorageReportDAL = new PortStorageReportDAL();
        /// <summary>
        /// This method is used to get vehicle listing repot from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="GroupByDealerInd"></param> 
        /// <returns></returns>
        public IList<VehicleListingReportProp> GetVehicleListingReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate, bool GroupByDealerInd)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.GetVehicleListingReport(ReportType, StartDate, EndDate, GroupByDealerInd);
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
        /// Get All Customer list from dataabse
        /// </summary>
        /// <returns></returns>
        public List<PortStorageRequestsReportProp> LoadCustomerList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.LoadCustomerList();
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
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageRequestsReportProp> LoadBatchList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.LoadBatchList();



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
        /// This method is used to get port storage request report from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="CustomerId"></param>
        /// <param name="VIN"></param>
        /// <param name="RequestDateFrom"></param>
        /// <param name="RequestDateTo"></param>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public IList<PortStorageRequestsReportProp> GetPortStorageRequestReport(int ReportType, int CustomerId, string VIN, Nullable<DateTime> RequestDateFrom, Nullable<DateTime> RequestDateTo, int BatchId)
        {
            
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.GetPortStorageRequestReport(ReportType, CustomerId, VIN, RequestDateFrom, RequestDateTo, BatchId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

           
        }

        /// <summary>
        /// This method is use to get port storage vehicle summery report from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<VehicleListingReportProp> GetPortStorageVehicleSummeryReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate)
        {
           
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.GetPortStorageVehicleSummeryReport(ReportType, StartDate, EndDate);
               
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

           
        }
        /// <summary>
        /// This method is used to get lane summary for report
        /// </summary>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetPortStorageLaneSummaryList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return portStorageReportDAL.GetPortStorageLaneSummaryList();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        /// <summary>
        /// This function is used to get Port Storage InBound List from database
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<PortStorageInventoryList>> GetPortStorageInBoundList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportDAL portStorageReportDAL = new PortStorageReportDAL();
                return await portStorageReportDAL.GetPortStorageInBoundList(startDate,endDate);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting Customer's AddressName
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<UserApplicationConstantsProp> GetDAIAddressName(string userCode)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.GetDAIAddressName(userCode);
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
        /// For updating compnay information
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool UpdateCompanyInfo(UserApplicationConstantsProp objCompanyinfo)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.UpdateCompanyInfo(objCompanyinfo);
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
        /// For getting Invoice list coressponding to a billingID and time period
        /// </summary>
        /// <param name="billinID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<InvoiceListProp> LoadInvoiceList(int? billinID, DateTime? startDate, DateTime? endDate)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.LoadInvoiceList(billinID, startDate, endDate);
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
        /// For getting batch list for inventory comparision 
        /// </summary>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetBatchLocationImport()
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.GetBatchLocationImport();
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
        /// For getting comparsion list coressponding to batch
        /// </summary>
        /// <param name="batchID"></param>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetInventoryComparisionList(int? batchID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.GetInventoryComparisionList(batchID);

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
        /// For updating batch id of print request vehical list
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <returns></returns>
        public bool UpdateRequestPrintIndexForVehicles(List<string> vehicleIds)
        {
            bool isRequestCompleted = false;
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportDAL.UpdateRequestPrintIndexForVehicles(vehicleIds);
            }
            catch (Exception)
            {
                isRequestCompleted = false;
                //throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return isRequestCompleted;

        }

    }
}

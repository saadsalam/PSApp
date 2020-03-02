using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using System.Threading.Tasks;
using AppWorksService.DAL.Edmx;
using AppWorks.Utilities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Entity;

namespace AppWorksService.DAL
{
    public class PortStorageReportDAL
    {
        PortStorageEntities psEntities = new PortStorageEntities();

        /// <summary>
        /// This method is used to get vehicle listing repot from database
        /// </summary>
        /// <param name="ReprintType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="GroupByDealerInd"></param>
        /// <returns></returns>

        public IList<VehicleListingReportProp> GetVehicleListingReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate, bool GroupByDealerInd)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {

                List<VehicleListingReportProp> listVehicleListingReportProp = new List<VehicleListingReportProp>();
                List<VehicleListingReportProp> lstVechiles = new List<VehicleListingReportProp>();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    //                   
                    if (StartDate != null && EndDate != null)
                    {
                        var result = psEntities.spGetVehicleListingReportDetails(ReportType, StartDate, EndDate).ToList();
                        if (result != null)
                        {
                            foreach (var val in result)
                            {
                                VehicleListingReportProp obj = new VehicleListingReportProp();
                                obj.CustomerID = (int)val.CustomerID;
                                obj.CustomerName = (GroupByDealerInd ? val.CustomerName : ""); //For grouping on Dealer
                                obj.CustomerName1 = val.CustomerName; //To display
                                obj.VIN = val.VIN;
                                obj.Make = val.Make;
                                obj.Model = val.Model;
                                obj.Color = val.Color;
                                obj.BayLocation = val.BayLocation;
                                obj.DateIn = val.DateIn;
                                obj.DateRequested = val.DateRequested;
                                obj.DateOut = val.DateOut;
                                obj.Count = result.Count;
                                listVehicleListingReportProp.Add(obj);
                                obj = null;

                            }
                        }
                    }
                    //if (GroupByDealerInd)
                    //{
                    var group = listVehicleListingReportProp.GroupBy(x => x.CustomerName1);
                    foreach (var innerGroup in group)
                    {
                        List<VehicleListingReportProp> lstVech = innerGroup.ToList();
                        var index = 1;
                        lstVech[0].TempIndex = index;
                        lstVech.Skip(1).ToList().ForEach(x => { index += 1; x.TempIndex = index; x.CustomerName1 = ""; });
                        lstVechiles.AddRange(lstVech);
                    }
                    //return lstVechiles;
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }

                //return listVehicleListingReportProp;
                return lstVechiles;
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
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    var query = (from cust in psEntities.Customers
                                 where cust.PortStorageCustomerInd == 1
                                 orderby cust.CustomerName ascending
                                 select new PortStorageRequestsReportProp
                                 {
                                     CustomerName = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     CustomerId = cust.CustomerID
                                 }
                                    );
                    return query.ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            //return lstCust;
        }

        /// <summary>
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageRequestsReportProp> LoadBatchList()
        {
            List<PortStorageRequestsReportProp> lstBatch = new List<PortStorageRequestsReportProp>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var query = (from cust in psEntities.PortStorageVehicles
                                 where cust.RequestPrintedInd == 1 && cust.RequestPrintedBatchID != null
                                 group cust by new
                                 {
                                     cust.RequestPrintedBatchID,
                                     DateRequestPrinted = DbFunctions.TruncateTime(cust.DateRequestPrinted)
                                 } into data

                                 let rawData = new
                                 {
                                     RequestPrintedBatchID = data.Key.RequestPrintedBatchID,
                                     DateRequestPrinted = data.Key.DateRequestPrinted,
                                     BatchCount = data.Count()
                                 }
                                 orderby rawData.RequestPrintedBatchID descending
                                 select new PortStorageRequestsReportProp
                                 {
                                     RequestPrintedBatchID = rawData.RequestPrintedBatchID,
                                     DateRequestPrinted = rawData.DateRequestPrinted,
                                     BatchCount = rawData.BatchCount
                                 }
                              );//.OrderByDescending(x => x.RequestPrintedBatchID);

                    return query.ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            //return lstBatch;
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
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                List<PortStorageRequestsReportProp> listPortStorageRequestsReportProp = new List<PortStorageRequestsReportProp>();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var result = psEntities.spGetPortStorageRequestReportDetails(ReportType, CustomerId, VIN, RequestDateFrom, RequestDateTo, BatchId).ToList();
                    
                    int previousBatchId = BatchId;
                    if (previousBatchId == 0 && result.Count > 0)
                    {
                        var previousPrintBatchSettings = psEntities.SettingTables.Where(x => x.ValueKey == "NextPSRequestPrintingBatchID").FirstOrDefault();
                        previousBatchId = int.Parse(previousPrintBatchSettings.ValueDescription);
                        previousPrintBatchSettings.ValueDescription = (previousBatchId + 1).ToString();
                        psEntities.SaveChanges();
                    }

                    if (result != null)
                    {
                        foreach (var val in result)
                        {
                            PortStorageRequestsReportProp obj = new PortStorageRequestsReportProp();
                            obj.PortStorageVehiclesID = val.PortStorageVehiclesID;
                            obj.VIN = val.VIN;
                            obj.ShortVIN = "*" + val.ShortVIN + "*";
                            obj.DateRequested = val.DateRequested;
                            obj.VehicleYear = val.VehicleYear;
                            obj.Make = val.Make;
                            obj.Model = val.Model;
                            obj.Color = val.Color;
                            obj.CustomerName = val.CustomerName;
                            obj.AdddressLine1 = val.AddressLine1;
                            obj.AddressLine2 = val.AddressLine2;
                            obj.City = val.City;
                            obj.State = val.State;
                            obj.Zip = val.Zip;
                            obj.BayLocation = val.BayLocation;
                            obj.DateOut = val.DateOut;
                            obj.EstimatedPickupDate = val.EstimatedPickupDate;
                            obj.DealerPrintDate = val.DealerPrintDate;
                            listPortStorageRequestsReportProp.Add(obj);
                            obj = null;

                            //update vehicle
                            var vehicle = psEntities.PortStorageVehicles.Where(x => x.PortStorageVehiclesID == val.PortStorageVehiclesID).FirstOrDefault();
                            vehicle.RequestPrintedInd = 1;
                            vehicle.RequestPrintedBatchID = (previousBatchId);
                            vehicle.DateRequestPrinted = DateTime.Now;
                            psEntities.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }

                return listPortStorageRequestsReportProp;
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

            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                List<VehicleListingReportProp> listVehicleListingReportProp = new List<VehicleListingReportProp>();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    if (StartDate != null && EndDate != null)
                    {
                        var result = psEntities.spGetPortStorageVehicleSummeryReportDetail(ReportType, StartDate, EndDate).ToList();
                        if (result != null)
                        {
                            foreach (var val in result)
                            {
                                VehicleListingReportProp obj = new VehicleListingReportProp();
                                obj.CustomerID = (int)val.CustomerID;
                                obj.CustomerName = val.CustomerName;
                                obj.Count = (int)val.Count;
                                listVehicleListingReportProp.Add(obj);
                                obj = null;

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }

                return listVehicleListingReportProp;
            }
        }

        /// <summary>
        /// For getting Lane Summary list
        /// </summary>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetPortStorageLaneSummaryList()
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                try
                {
                    var sqlQuery = psEntities.Database.SqlQuery<PortStorageInventoryList>(SqlConstants.LANE_SUMMARY_QUERY);

                    var selectedResult = sqlQuery.Select(x => new PortStorageInventoryList
                    {
                        Baylocation = x.Baylocation,
                        CustomerName = x.CustomerName,
                        RecordsCount = x.RecordsCount
                    }).ToList();

                    return selectedResult;
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

        /// <summary>
        /// For getting Inbound list coressponding to strat date and end date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<PortStorageInventoryList>> GetPortStorageInBoundList(DateTime? startDate, DateTime? endDate)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                List<PortStorageInventoryList> listPortStorageInventoryList = new List<PortStorageInventoryList>();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    if (startDate != null && endDate != null)
                    {

                        var result = await psEntities.Database.SqlQuery<PortStorageInBoundListProp>
                            //(string.Format(SqlConstants.PORT_STORAGE_INBOUND_LIST_QUERY, startDate, endDate)).ToListAsync();
                            (string.Format("Exec [spGetPortStorageInBoundList] '{0}','{1}'", startDate, endDate)).ToListAsync();

                        if (result != null)
                        {
                            foreach (var val in result)
                            {
                                PortStorageInventoryList obj = new PortStorageInventoryList();
                                obj.Location = val.Locationname;
                                obj.LoadNumber = val.LoadNumber;
                                obj.VIN = val.VIN;
                                obj.Make = val.Make;
                                obj.Model = val.Model;
                                obj.Baylocation = val.BayLocation;
                                obj.DateIn = val.DateIn;
                                obj.DeliveryBayLocation = val.DeliveryBayLocation;
                                listPortStorageInventoryList.Add(obj);
                                obj = null;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return listPortStorageInventoryList;
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
                var lstDAIAddressNameeInfo = (from appconst in psEntities.ApplicationConstants
                                              from user in psEntities.Users
                                              where user.UserCode == userCode
                                              select new UserApplicationConstantsProp
                                              {
                                                  CompanyName = appconst.CompanyName,
                                                  AddressLine1 = appconst.AddressLine1,
                                                  AddressLine2 = appconst.AddressLine2,
                                                  City = appconst.City + ", " + appconst.State + " " + appconst.Zip,
                                                  Phone = appconst.Phone.Length >= 6 && appconst.Phone != null ? "(" + appconst.Phone.Substring(0, 3) + ")" + " " + appconst.Phone.Substring(3, 3) + "-" + appconst.Phone.Substring(6, appconst.Phone.Length - 6) : appconst.Phone,
                                                  FirstName = user.FirstName + " " + user.LastName + user.PhoneExtension != string.Empty ? "Ext:" + user.PhoneExtension : " ",
                                                  State = appconst.State,
                                                  CitySep = appconst.City,
                                                  Zip = appconst.Zip,
                                                  FaxNumber = appconst.FaxNumber.Length >= 6 && appconst.FaxNumber != null ? "(" + appconst.FaxNumber.Substring(0, 3) + ")" + " " + appconst.FaxNumber.Substring(3, 3) + "-" + appconst.FaxNumber.Substring(6, appconst.FaxNumber.Length - 6) : appconst.FaxNumber,
                                                  SystemName = appconst.SystemName,
                                                  NextOrderNumber = appconst.NextOrderNumber,
                                                  CreatedDate = appconst.CreationDate,
                                                  CreatedBy = appconst.CreatedBy,
                                                  UpdatedDate = appconst.UpdatedDate,
                                                  UpdatedBy = appconst.UpdatedBy

                                              }).ToList();
                return lstDAIAddressNameeInfo;
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
            try
            {
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    bool isSuccessfull = false;
                    // ApplicationConstant customer = psEntities.ApplicationConstants.Where(x => x.usercode == objcustomer.CustomerID).FirstOrDefault();
                    ApplicationConstant companyinfo = psEntities.ApplicationConstants.FirstOrDefault();
                    if (companyinfo != null)
                    {
                        companyinfo.CompanyName = objCompanyinfo.CompanyName;
                        companyinfo.AddressLine1 = objCompanyinfo.AddressLine1;
                        companyinfo.AddressLine2 = objCompanyinfo.AddressLine2;
                        companyinfo.City = objCompanyinfo.CitySep;
                        companyinfo.Phone = objCompanyinfo.Phone;
                        companyinfo.State = objCompanyinfo.State;
                        companyinfo.Zip = objCompanyinfo.Zip;
                        companyinfo.FaxNumber = objCompanyinfo.FaxNumber;
                        companyinfo.SystemName = objCompanyinfo.SystemName;
                        companyinfo.NextOrderNumber = objCompanyinfo.NextOrderNumber;
                        companyinfo.UpdatedDate = objCompanyinfo.UpdatedDate;
                        companyinfo.UpdatedBy = objCompanyinfo.UpdatedBy;
                        psEntities.SaveChanges();
                        isSuccessfull = true;
                    }
                    return isSuccessfull;
                }
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
                var lstInvoiceList = new List<InvoiceListProp>();
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    if (endDate != null)
                        endDate = Convert.ToDateTime(endDate).AddDays(1);

                    string internalQuery = SqlConstants.VEHICLE_INVOICE_SUMMARY;
                    var parameters = new object[3];
                    var paramBillingId = new SqlParameter("BillingIDString", System.Data.SqlDbType.VarChar);
                    paramBillingId.Value = billinID.ToString();
                    var paramStartDate = new SqlParameter("startDate", System.Data.SqlDbType.DateTime);
                    paramStartDate.Value = startDate ?? (object)DBNull.Value;
                    var paramEndDate = new SqlParameter("endDate", System.Data.SqlDbType.DateTime);
                    paramEndDate.Value = endDate ?? (object)DBNull.Value;
                    var sqlQuery = objAppWorksEntities.Database.SqlQuery<InvoiceListProp>(internalQuery, new object[] { paramBillingId, paramStartDate, paramEndDate });
                    lstInvoiceList = sqlQuery.ToList<InvoiceListProp>();
                }
                return lstInvoiceList.ToList();
                //var lstInvoiceList = (from billing in psEntities.Billings
                //                      join customer in psEntities.Customers
                //                      on billing.CustomerID equals customer.CustomerID
                //                      where billing.InvoiceType == "StorageCharge" && billing.InvoiceAmount > 0
                //                      select new InvoiceListProp
                //                      {
                //                          CustomerNumber = customer.CustomerCode,
                //                          InvoiceNumber = billing.InvoiceNumber,
                //                          CustomerName = string.IsNullOrEmpty(customer.ShortName) ? customer.CustomerName : customer.ShortName,
                //                          InvoiceDate = billing.InvoiceDate,
                //                          InvoiceAmount = (double)billing.InvoiceAmount,
                //                          Units = (((from psvehicle in psEntities.PortStorageVehicles where psvehicle.BillingID == billinID select psvehicle)).Count())
                //                      }).AsQueryable();

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
                var results = (from psvi in psEntities.PortStorageVehicleLocationImports
                               group psvi by psvi.BatchID into grp

                               select new PortStorageInventoryList
                               {
                                   BatchID = grp.Key.Value,
                                   RecordsCount = grp.Count(),
                                   CreattionDate = grp.Select(x => x.CreationDate).FirstOrDefault(),
                               }).Distinct().OrderBy(x => x.BatchID);

                return results.ToList();
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
                #region OLD Code
                //var lstVIN = from psvli2 in psEntities.PortStorageVehicleLocationImports
                //             where psvli2.BatchID == batchID
                //             select psvli2.VIN;

                //var lstInventory = (from portstoragevli in psEntities.PortStorageVehicleLocationImports

                //                    join portstoragev in psEntities.PortStorageVehicles
                //                    on portstoragevli.VIN equals portstoragev.VIN into psValues
                //                    //  where psValues.Any()
                //                    from subset in psValues.Where(X => X.DateOut == null).DefaultIfEmpty()
                //                    where portstoragevli.BatchID == batchID
                //                    select new PortStorageInventoryList
                //                    {
                //                        VIN = portstoragevli.VIN,
                //                        Make = subset.Make,
                //                        Model = subset.Model,
                //                        Color = subset.Color,
                //                        DateIn = subset.DateIn,
                //                        Status = subset.VIN == null ? "No Match" : "Matched",
                //                        Location = subset.BayLocation == null ? portstoragevli.Location : subset.BayLocation
                //                    });

                //var lstNotScanned = (from portstoragev in psEntities.PortStorageVehicles
                //                     where portstoragev.DateOut == null
                //                     select new PortStorageInventoryList
                //                     {
                //                         VIN = portstoragev.VIN,
                //                         Make = portstoragev.Make,
                //                         Model = portstoragev.Model,
                //                         Color = portstoragev.Color,
                //                         DateIn = portstoragev.DateIn,
                //                         Status = "NOT SCANNED",
                //                         Location = portstoragev.BayLocation
                //                     }).Where(x => !lstVIN.Contains(x.VIN));
                #endregion
                var inventoryComparisonList = new List<PortStorageInventoryList>();
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    ((IObjectContextAdapter)objAppWorksEntities).ObjectContext.CommandTimeout = 180;
                    string internalQuery = SqlConstants.VEHICLE_COMPARISON_LIST;
                    if (batchID > 0)
                    {
                        ObjectParameter batchParam = new ObjectParameter("batchId", typeof(Int32));
                        batchParam.Value = batchID;
                        var sqlQuery = objAppWorksEntities.Database.SqlQuery<PortStorageInventoryList>(internalQuery, new SqlParameter("batchId", batchID));
                        inventoryComparisonList = sqlQuery.ToList<PortStorageInventoryList>();
                    }
                }
                return inventoryComparisonList;
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
            bool isRequsetCompleted = false;
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var previousPrintBatchSettings = psEntities.SettingTables.Where(x => x.ValueKey == "NextPSRequestPrintingBatchID").FirstOrDefault();
                int previousBatchId = int.Parse(previousPrintBatchSettings.ValueDescription);
                previousPrintBatchSettings.ValueDescription = (previousBatchId + 1).ToString();
                psEntities.SaveChanges();

                foreach (var vehicleId in vehicleIds)
                {
                    int vehicleID = int.Parse(vehicleId);
                    var vehicle = psEntities.PortStorageVehicles.Where(x => x.PortStorageVehiclesID == vehicleID).FirstOrDefault();
                    vehicle.RequestPrintedInd = 1;
                    vehicle.RequestPrintedBatchID = (previousBatchId);
                    vehicle.DateRequestPrinted = DateTime.Now;
                }
                psEntities.SaveChanges();
                isRequsetCompleted = true;
            }
            catch (Exception ex)
            {
                isRequsetCompleted = false;
                //throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return isRequsetCompleted;
        }
    }
}

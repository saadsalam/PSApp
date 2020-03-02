using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using System.Transactions;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using System.Threading.Tasks;
using AppWorksService.DAL.Edmx;
using System.Data;
using AppWorks.Utilities;
using AppWorksService.DAL.DAL;

namespace AppWorksService.DAL
{
    public class BillingDAL
    {
        /// <summary>
        /// To Add the Record of Billing Period Admin
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-19,2016</createdOn>
        public int AddBillingPeriodAdmin(BillingPeriodprop objBillingPeriod)
        {
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                int getBillingPeriodID;

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    BillingPeriod objTblBillingPeriod = new BillingPeriod();
                    objTblBillingPeriod.CalendarYear = objBillingPeriod.CalendarYear;
                    objTblBillingPeriod.CreatedBy = objBillingPeriod.CreatedBy;
                    objTblBillingPeriod.CreationDate = objBillingPeriod.CreationDate;
                    objTblBillingPeriod.PeriodClosedBy = objBillingPeriod.PeriodClosedBy;
                    objTblBillingPeriod.PeriodClosedDate = objBillingPeriod.PeriodClosedDate;
                    objTblBillingPeriod.PeriodClosedInd = objBillingPeriod.PeriodClosedInd;
                    objTblBillingPeriod.PeriodEndDate = objBillingPeriod.PeriodEndDate;
                    objTblBillingPeriod.PeriodNumber = objBillingPeriod.PeriodNumber;
                    objTblBillingPeriod.UpdatedBy = objBillingPeriod.UpdatedBy;
                    objTblBillingPeriod.UpdatedDate = objBillingPeriod.UpdatedDate;

                    psEntities.BillingPeriods.Add(objTblBillingPeriod);      /// Insert the Record in BillingPeriod Table.
                    psEntities.SaveChanges();                                /// Check the Chenges in Table After Record Insertion.
                    getBillingPeriodID = objTblBillingPeriod.BillingPeriodID;         /// Return the BillingPeriodID of Inserted Billing Period.
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return getBillingPeriodID;
            }
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
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                //GetStorageVehicleDetails(18519);
                IList<BillingPeriodprop> listBillingPeriodprop;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    if ((objBillingPeriod.CalendarYear == 0 || objBillingPeriod.CalendarYear == null) && objBillingPeriod.PeriodClosedInd == 2)
                    {
                        listBillingPeriodprop = psEntities.BillingPeriods.Select(y => new BillingPeriodprop()
                        {
                            BillingPeriodID = y.BillingPeriodID,
                            CalendarYear = y.CalendarYear,
                            PeriodNumber = y.PeriodNumber,
                            PeriodEndDate = y.PeriodEndDate,
                            PeriodClosedInd = y.PeriodClosedInd,
                            PeriodClosed = y.PeriodClosedInd == 1 ? true : false,
                            CreationDate = y.CreationDate,
                            CreatedBy = y.CreatedBy,
                            UpdatedDate = y.UpdatedDate,
                            UpdatedBy = y.UpdatedBy,
                            PeriodClosedBy = y.PeriodClosedBy,
                            PeriodClosedDate = y.PeriodClosedDate,
                        }).ToList();
                    }
                    else if (objBillingPeriod.CalendarYear != 0 && objBillingPeriod.PeriodClosedInd == 2)
                    {
                        listBillingPeriodprop = psEntities.BillingPeriods.Where(x => x.CalendarYear == objBillingPeriod.CalendarYear).Select(y => new BillingPeriodprop()
                        {
                            BillingPeriodID = y.BillingPeriodID,
                            CalendarYear = y.CalendarYear,
                            PeriodNumber = y.PeriodNumber,
                            PeriodEndDate = y.PeriodEndDate,
                            PeriodClosedInd = y.PeriodClosedInd,
                            PeriodClosed = y.PeriodClosedInd == 1 ? true : false,
                            CreationDate = y.CreationDate,
                            CreatedBy = y.CreatedBy,
                            UpdatedDate = y.UpdatedDate,
                            UpdatedBy = y.UpdatedBy,
                            PeriodClosedBy = y.PeriodClosedBy,
                            PeriodClosedDate = y.PeriodClosedDate,
                        }).ToList();
                    }
                    else if ((objBillingPeriod.CalendarYear == 0 || objBillingPeriod.CalendarYear == null) && objBillingPeriod.PeriodClosedInd != 2)
                    {
                        listBillingPeriodprop = psEntities.BillingPeriods.Where(x => x.PeriodClosedInd == objBillingPeriod.PeriodClosedInd).Select(y => new BillingPeriodprop()
                        {
                            BillingPeriodID = y.BillingPeriodID,
                            CalendarYear = y.CalendarYear,
                            PeriodNumber = y.PeriodNumber,
                            PeriodEndDate = y.PeriodEndDate,
                            PeriodClosedInd = y.PeriodClosedInd,
                            PeriodClosed = y.PeriodClosedInd == 1 ? true : false,
                            CreationDate = y.CreationDate,
                            CreatedBy = y.CreatedBy,
                            UpdatedDate = y.UpdatedDate,
                            UpdatedBy = y.UpdatedBy,
                            PeriodClosedBy = y.PeriodClosedBy,
                            PeriodClosedDate = y.PeriodClosedDate,
                        }).ToList();
                    }
                    else
                    {
                        listBillingPeriodprop = psEntities.BillingPeriods.Where(x => x.CalendarYear == objBillingPeriod.CalendarYear
                            && x.PeriodClosedInd == objBillingPeriod.PeriodClosedInd).Select(y => new BillingPeriodprop()
                            {
                                BillingPeriodID = y.BillingPeriodID,
                                CalendarYear = y.CalendarYear,
                                PeriodNumber = y.PeriodNumber,
                                PeriodEndDate = y.PeriodEndDate,
                                PeriodClosedInd = y.PeriodClosedInd,
                                PeriodClosed = y.PeriodClosedInd == 1 ? true : false,
                                CreationDate = y.CreationDate,
                                CreatedBy = y.CreatedBy,
                                UpdatedDate = y.UpdatedDate,
                                UpdatedBy = y.UpdatedBy,
                                PeriodClosedBy = y.PeriodClosedBy,
                                PeriodClosedDate = y.PeriodClosedDate,
                            }).ToList();
                    }

                    var pageCount = listBillingPeriodprop.ToList().Count;
                    if (objBillingPeriod.CurrentPageIndex == 0)
                    {
                        var queryResult = listBillingPeriodprop.ToList().Skip(objBillingPeriod.CurrentPageIndex * objBillingPeriod.PageSize).Take(objBillingPeriod.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
                    }
                    else
                    {
                        var queryResult = listBillingPeriodprop.ToList().Skip((objBillingPeriod.CurrentPageIndex * objBillingPeriod.PageSize) + (objBillingPeriod.DefaultPageSize - objBillingPeriod.PageSize)).Take(objBillingPeriod.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
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
        }

        /// <summary>
        /// This Method to update Billing Period Admin Details
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns></returns>
        /// <createdOn>May-23,2016</createdOn>
        public bool UpdateBillingPeriodAdminDetails(BillingPeriodprop objBillingPeriodprop)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    //var qry = psEntities.BillingPeriods.Select(x => x.BillingPeriodID).Contains(objBillingPeriodprop.BillingPeriodID).FirstOrDefault();// Check for VIN is exists or not in vechile table
                    //if (qry)
                    //{

                    BillingPeriod billingPeriod = psEntities.BillingPeriods.Where(B => B.BillingPeriodID == objBillingPeriodprop.BillingPeriodID).FirstOrDefault();
                    billingPeriod.PeriodClosedInd = objBillingPeriodprop.PeriodClosedInd;
                    billingPeriod.CalendarYear = objBillingPeriodprop.CalendarYear;
                    billingPeriod.PeriodEndDate = objBillingPeriodprop.PeriodEndDate;
                    billingPeriod.PeriodNumber = objBillingPeriodprop.PeriodNumber;
                    billingPeriod.PeriodClosedBy = objBillingPeriodprop.PeriodClosedBy;
                    billingPeriod.PeriodClosedDate = objBillingPeriodprop.PeriodClosedDate;
                    billingPeriod.UpdatedDate = objBillingPeriodprop.UpdatedDate;
                    billingPeriod.UpdatedBy = objBillingPeriodprop.UpdatedBy;
                    psEntities.SaveChanges();
                    result = true;
                    //}
                    return result;
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
        ///  This method ised to for delete billing period admin details.
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        /// // <createdOn>May-23,2016</createdOn>
        public bool DeleteBillingPeriodAdminDetails(int BillingPeriodID)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var BillingData = psEntities.BillingPeriods.Where(B => B.BillingPeriodID == BillingPeriodID).FirstOrDefault();
                    if (BillingData != null)
                    {
                        psEntities.BillingPeriods.Remove(BillingData);
                        psEntities.SaveChanges();
                        result = true;
                    }
                    return result;
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
        /// This method is used to check duplicate calender  and period values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool CheckDuplicateCalenderAndPeriod(int year, int period)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    var codeChk = psEntities.BillingPeriods.Where(c => c.CalendarYear == year && c.PeriodNumber == period).ToList().FirstOrDefault();
                    if (codeChk == null)
                        result = true;
                    else
                        result = false;

                    return result;
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
        /// This method is used to get storage vehicle details list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-20-2016</createdon>
        public List<PortStorageVehicleProp> GetStorageVehicleDetails(PortStorageVehicleProp objPortstorageProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {


                    var lstStorageDetails = psEntities.spGetStoragevehicleList(objPortstorageProp.BillingID).ToList().
                    Select(x => new PortStorageVehicleProp
                    {
                        PortStorageVehiclesID = x.PortStorageVehiclesID,
                        VIN = x.VIN,
                        DateIn = x.DateIn,
                        DateOut = x.DateOut,
                        Days = x.Days,
                        EntryRate = x.EntryRate,
                        PerDiemGraceDays = x.PerDiemGraceDays,
                        ChargeDays = x.ChargeDays,
                        PerDiem = x.PerDiem,
                        TotalCharge = x.TotalCharge

                    }
                                        );
                    var pageCount = lstStorageDetails.ToList().Count;
                    if (objPortstorageProp.CurrentPageIndex == 0)
                    {
                        var queryResult = lstStorageDetails.ToList().Skip(objPortstorageProp.CurrentPageIndex * objPortstorageProp.PageSize).Take(objPortstorageProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.Distinct().ToList();
                    }
                    else
                    {
                        var queryResult = lstStorageDetails.ToList().Skip((objPortstorageProp.CurrentPageIndex * objPortstorageProp.PageSize) + (objPortstorageProp.DefaultPageSize - objPortstorageProp.PageSize)).Take(objPortstorageProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.Distinct().ToList();
                    }

                    // return new List<PortStorageVehicleProp>(lstStorageDetails).ToList();
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
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public List<BillingLineItemsProp> GetLineItemsList(BillingLineItemsProp objLineItems)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {


                    var lstInvoiceLineItemsDetails = (from billing in psEntities.BillingLineItems
                                                      where billing.BillingID == objLineItems.BillingID
                                                      orderby billing.CreationDate
                                                      select new BillingLineItemsProp
                                                      {
                                                          BillingLineItemsID = billing.BillingLineItemsID,
                                                          BillingID = billing.BillingID,
                                                          CustomerNumber = billing.CustomerNumber,
                                                          InvoiceNumber = billing.InvoiceNumber,
                                                          InvoiceDate = billing.InvoiceDate,
                                                          DebitAccountNumber = billing.DebitAccountNumber,
                                                          DebitProfitCenterNumber = billing.DebitProfitCenterNumber,
                                                          DebitCostCenterNumber = billing.DebitCostCenterNumber,
                                                          CreditAccountNumber = billing.CreditAccountNumber,
                                                          CreditProfitCenterNumber = billing.CreditProfitCenterNumber,
                                                          CreditCostCenterNumber = billing.CreditCostCenterNumber,
                                                          ARTransactionAmount = billing.ARTransactionAmount,
                                                          Description = billing.Description,
                                                          ExportedInd = billing.ExportedInd,
                                                          ExportedDate = billing.ExportedDate,
                                                          CreationDate = billing.CreationDate,
                                                          CustomerID = billing.CustomerID
                                                      }
                                                   );

                    var pageCount = lstInvoiceLineItemsDetails.ToList().Count;
                    if (objLineItems.CurrentPageIndex == 0)
                    {
                        var queryResult = lstInvoiceLineItemsDetails.ToList().Skip(objLineItems.CurrentPageIndex * objLineItems.PageSize).Take(objLineItems.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.Distinct().ToList();
                    }
                    else
                    {
                        var queryResult = lstInvoiceLineItemsDetails.ToList().Skip((objLineItems.CurrentPageIndex * objLineItems.PageSize) + (objLineItems.DefaultPageSize - objLineItems.PageSize)).Take(objLineItems.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.Distinct().ToList();
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

        }

        /// <summary>
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public bool ResetExportedInd(int billingID)
        {
            try
            {
                bool issuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {


                    var lstItem = (from billing in psEntities.BillingLineItems
                                   where billing.BillingID == billingID
                                   select new BillingLineItemsProp
                                   {
                                       BillingLineItemsID = billing.BillingLineItemsID,
                                       ExportedInd = billing.ExportedInd,
                                       ExportBatchID = billing.ExportBatchID,
                                       ExportedDate = billing.ExportedDate,
                                       ExportedBy = billing.ExportedBy
                                   }).ToList();

                    foreach (var t in lstItem)
                    {
                        BillingLineItem portStorageVehicle = psEntities.BillingLineItems.Where(B => B.BillingLineItemsID == t.BillingLineItemsID).FirstOrDefault();
                        portStorageVehicle.ExportedInd = 0;
                        portStorageVehicle.ExportBatchID = null;
                        portStorageVehicle.ExportedDate = null;
                        portStorageVehicle.ExportedBy = null;
                        psEntities.SaveChanges();
                    }

                    issuccessfull = true;
                }
                return issuccessfull;
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
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                int getBillingID = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    Billing billing = new Billing();

                    billing.CustomerID = objBillingProp.CustomerID;
                    billing.OutsideCarrierInvoiceInd = objBillingProp.OutsideCarrierInvoiceInd;
                    billing.InvoiceDate = objBillingProp.InvoiceDate;                    
                    if (objBillingProp.InvoiceDate.HasValue)
                    {
                        billing.InvoiceDate = objBillingProp.InvoiceDate.Value.Date;
                    }
                    billing.InvoiceNumber = objBillingProp.InvoiceNumber;
                    billing.InvoiceType = objBillingProp.InvoiceType;
                    billing.PaymentMethod = objBillingProp.PaymentMethod;
                    billing.TransportCharges = objBillingProp.TransportCharges;
                    billing.FuelSurchargeRate = objBillingProp.FuelSurchargeRate;
                    billing.FuelSurchargeRateOverrideInd = objBillingProp.FuelSurchargeRateOverrideInd;
                    billing.FuelSurcharge = objBillingProp.FuelSurcharge;
                    billing.FuelSurchargeOverrideInd = objBillingProp.FuelSurchargeOverrideInd;
                    billing.InvoiceAmount = objBillingProp.InvoiceAmount;
                    billing.AmountPaid = objBillingProp.AmountPaid;
                    billing.CreditsIssued = objBillingProp.CreditsIssued;
                    billing.BalanceDue = objBillingProp.BalanceDue;
                    billing.DueToOutsideCarriers = objBillingProp.DueFromOutsideCarriers;
                    billing.DueFromOutsideCarriers = objBillingProp.DueFromOutsideCarriers;
                    billing.PaidInFullInd = objBillingProp.PaidInFullInd;
                    billing.PaidInFullDate = objBillingProp.PaidInFullDate;
                    billing.InvoiceStatus = "Pending";
                    billing.CreditMemoInd = objBillingProp.CreditMemoInd;
                    billing.CreditedOutInd = objBillingProp.CreditedOutInd;
                    billing.CreationDate = objBillingProp.CreationDate;
                    billing.CreatedBy = objBillingProp.CreatedBy;
                    billing.StorageCharges = objBillingProp.StorageCharges;


                    psEntities.Billings.Add(billing);      /// Insert the Record in Billing Table.
                    psEntities.SaveChanges();                                /// Check the Chenges in Table After Record Insertion.
                    getBillingID = billing.BillingID;
                    return getBillingID;/// Return the BillingID of Inserted item
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
        /// To Update Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        public bool UpdateBillingTab(BillingListProp objBillingProp)
        {
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                bool isSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var billing = (from billingtable in psEntities.Billings
                                   where billingtable.BillingID == objBillingProp.BillingID
                                   select billingtable).FirstOrDefault();
                    if (billing != null)
                    {
                        billing.OutsideCarrierInvoiceInd = objBillingProp.OutsideCarrierInvoiceInd;
                        billing.InvoiceDate = objBillingProp.InvoiceDate;
                        if (objBillingProp.InvoiceDate.HasValue)
                        {
                            billing.InvoiceDate = objBillingProp.InvoiceDate.Value.Date;
                        }
                        billing.InvoiceNumber = objBillingProp.InvoiceNumber;
                        billing.InvoiceType = objBillingProp.InvoiceType;
                        billing.PaymentMethod = objBillingProp.PaymentMethod;
                        billing.TransportCharges = objBillingProp.TransportCharges;
                        billing.FuelSurchargeRate = objBillingProp.FuelSurchargeRate;
                        billing.FuelSurchargeRateOverrideInd = objBillingProp.FuelSurchargeRateOverrideInd;
                        billing.FuelSurcharge = objBillingProp.FuelSurcharge;
                        billing.FuelSurchargeOverrideInd = objBillingProp.FuelSurchargeOverrideInd;
                        billing.InvoiceAmount = objBillingProp.InvoiceAmount;
                        billing.AmountPaid = objBillingProp.AmountPaid;
                        billing.CreditsIssued = objBillingProp.CreditsIssued;
                        billing.BalanceDue = objBillingProp.BalanceDue;
                        billing.DueToOutsideCarriers = objBillingProp.DueFromOutsideCarriers;
                        billing.DueFromOutsideCarriers = objBillingProp.DueFromOutsideCarriers;
                        billing.PaidInFullInd = objBillingProp.PaidInFullInd;
                        billing.PaidInFullDate = objBillingProp.PaidInFullDate;
                        billing.InvoiceStatus = "Pending";
                        billing.CreditMemoInd = objBillingProp.CreditMemoInd;
                        billing.CreditedOutInd = objBillingProp.CreditedOutInd;
                        billing.StorageCharges = objBillingProp.StorageCharges;
                        billing.UpdatedDate = objBillingProp.UpdatedDate;
                        billing.UpdatedBy = objBillingProp.UpdatedBy;
                        billing.Comments = objBillingProp.Comments;
                        psEntities.SaveChanges();
                        isSuccessfull = true;
                    }

                    return isSuccessfull;
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
        ///  This method is used  for deleting data from billing and related tables
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        /// // <createdOn>May-23,2016</createdOn>
        public bool DeleteBillingData(int BillingID)
        {
            using (var transaction = new TransactionScope())
            {
                bool result = false;
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    using (PortStorageEntities psEntities = new PortStorageEntities())
                    {
                        var BillingVehicleData = psEntities.Vehicles.Where(B => B.BillingID == BillingID);
                        if (BillingVehicleData != null)
                        {
                            foreach (var item in BillingVehicleData)
                            {
                                item.BilledInd = 0;
                                item.DateBilled = null;
                                item.BillingID = null;
                            }
                        }

                        var BillingLineData = psEntities.BillingLineItems.Where(B => B.BillingID == BillingID).ToList();
                        if (BillingLineData != null)
                        {
                            foreach (var item in BillingLineData)
                                psEntities.BillingLineItems.Remove(item);
                        }


                        var BillingData = psEntities.Billings.Where(B => B.BillingID == BillingID).ToList();
                        if (BillingData != null)
                        {
                            foreach (var item in BillingData)
                                psEntities.Billings.Remove(item);
                        }

                        psEntities.SaveChanges();
                        result = true;
                        transaction.Complete();
                    }
                    return result;

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
        /// To Find Billing Record
        /// </summary>
        /// <param name="BillingProp"></param>
        /// <returns>IList<BillingProp></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-14,2016</createdOn>
        public async Task<List<BillingProp>> BillingSearch(BillingProp objBilling)
        {           
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                psEntities.Database.CommandTimeout = 1200;

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var query = (from B in psEntities.Billings
                                 join C in psEntities.Customers on new { CustomerID = (int)B.CustomerID } equals new { CustomerID = C.CustomerID } into C_join
                                 from C in C_join.DefaultIfEmpty()

                                 join lo in psEntities.Locations on C.MainAddressID equals lo.LocationID into lo_join
                                 from Loc in lo_join.DefaultIfEmpty()

                                 join V in psEntities.PortStorageVehicles on new { BillingID = B.BillingID } equals new { BillingID = (int)V.BillingID } into V_join
                                 from V in V_join.DefaultIfEmpty()

                                 join L2 in psEntities.Legs on new { VehicleID = V.PortStorageVehiclesID } equals new { VehicleID = (int)L2.VehicleID } into L2_join
                                 from L2 in L2_join.DefaultIfEmpty()

                                 join L in psEntities.Loads on new { LoadID = (int)L2.LoadID } equals new { LoadID = L.LoadsID } into L_join
                                 from L in L_join.DefaultIfEmpty()

                                     //join O in psEntities.Orders on new { OrderID = (int)V.OrderID } equals new { OrderID = O.OrdersID } into O_join
                                     //from O in O_join.DefaultIfEmpty()

                                 //join OC in psEntities.OutsideCarriers on new { OutsideCarrierID = (int)L.OutsideCarrierID } equals new { OutsideCarrierID = OC.OutsideCarrierID } into OC_join
                                 //from OC in OC_join.DefaultIfEmpty()

                                 //join D in psEntities.Drivers on new { DriverID = (int)L.DriverID } equals new { DriverID = D.DriverID } into D_join
                                 //from D in D_join.DefaultIfEmpty()

                                 //join U in psEntities.Users on new { UserID = (int)D.UserID } equals new { UserID = U.UserID } into U_join
                                 //from U in U_join.DefaultIfEmpty()
                                 select new BillingProp()
                                 {
                                     CreatedBetwDtFrm = B.CreationDate,
                                     DtInvBetwDtFrm = B.InvoiceDate,
                                     DtPaidBetwDtFrm = B.PaidInFullDate,
                                     CustomerAddress = Loc.AddressLine1 + " " + (string.IsNullOrEmpty(Loc.AddressLine2) ? "" : Loc.AddressLine2 + " ") + Loc.City + ", " + Loc.State + " " + Loc.Zip,
                                     BillingID = B.BillingID,
                                     InvoiceNumber = B.InvoiceNumber,
                                     CustomerName = C.ShortName.Length > 0 ? C.ShortName : C.CustomerName,
                                     CustomerNumber = C.CustomerCode,
                                     Vin = V.VIN,
                                     CustIndent = V.CustomerIdentification,
                                     InvoiceType = B.InvoiceType,
                                     InvoiceDate = B.InvoiceDate,
                                     //LoadNumber = B.RunID > 0 ? L.LoadNumber : "",
                                     //CarrierName =B.RunID > 0 &&L.DriverID > 0 ? (U.LastName + ", " + U.FirstName) :B.RunID > 0 && L.OutsideCarrierID > 0 ? OC.CarrierName : "",
                                     //Driver = L.DriverID > 0 ? U.FirstName : string.Empty,
                                     //OutsideCarrier = B.OutsideCarrierInvoiceInd == 1 ? OC.CarrierName : (SqlFunctions.DataLength(C.ShortName) > 0 ? C.ShortName : C.CustomerName),
                                     InvoiceStatus = B.InvoiceStatus
                                 }).Distinct().OrderByDescending(x => x.InvoiceDate).AsQueryable();

                    if ((objBilling.CustomerName != null) && (objBilling.CustomerName.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerName.Contains(objBilling.CustomerName));
                    }
                    if ((objBilling.InvoiceStatus != null) && (objBilling.InvoiceStatus.Length > 0) && (objBilling.InvoiceStatus != "All"))
                    {
                        query = query.Where(cnd => cnd.InvoiceStatus == objBilling.InvoiceStatus.Replace(" ", string.Empty));
                    }
                    //if ((objBilling.PONumber != null) && (objBilling.PONumber.Length > 0))
                    //{
                    //    query = query.Where(cnd => cnd.PONumber.Contains(objBilling.PONumber));
                    //}
                    if ((objBilling.InvoiceType != null) && (objBilling.InvoiceType.Length > 0) && (objBilling.InvoiceType != "All"))
                    {
                        query = query.Where(cnd => cnd.InvoiceType == objBilling.InvoiceType.Replace(" ", string.Empty));
                    }
                    if ((objBilling.InvoiceNumber != null) && (objBilling.InvoiceNumber.Length > 0))
                    {
                        query = query.Where(cnd => cnd.InvoiceNumber.Contains(objBilling.InvoiceNumber));
                    }

                    //if ((objBilling.OrderNumber != null) && (objBilling.OrderNumber.Length > 0))
                    //{
                    //    query = query.Where(cnd => cnd.OrderNumber.Contains(objBilling.OrderNumber));
                    //}

                    if ((objBilling.Vin != null) && (objBilling.Vin.Length > 0))
                    {
                        query = query.Where(cnd => cnd.Vin.Contains(objBilling.Vin));
                    }

                    if ((objBilling.CustomerNumber != null) && (objBilling.CustomerNumber.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerNumber.Contains(objBilling.CustomerNumber));
                    }
                    //if ((objBilling.LoadNumber != null) && (objBilling.LoadNumber.Length > 0))
                    //{
                    //    query = query.Where(cnd => cnd.LoadNumber.Contains(objBilling.LoadNumber));
                    //}
                    if ((objBilling.CustIndent != null) && (objBilling.CustIndent.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustIndent.Contains(objBilling.CustIndent));
                    }
                    if ((objBilling.CreatedBetwDtFrm != null) && (objBilling.CreatedBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => cnd.CreatedBetwDtFrm >= objBilling.CreatedBetwDtFrm && cnd.CreatedBetwDtFrm <= objBilling.CreatedBetwDtTo);
                    }
                    else
                    {
                        if ((objBilling.CreatedBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => cnd.CreatedBetwDtFrm >= objBilling.CreatedBetwDtFrm);
                        }
                        if ((objBilling.CreatedBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => cnd.CreatedBetwDtFrm <= objBilling.CreatedBetwDtTo);
                        }
                    }
                    //if ((objBilling.Driver != null) && (objBilling.Driver.Length > 0) && (objBilling.Driver != "All"))
                    //{
                    //    query = query.Where(cnd => cnd.Driver == objBilling.Driver);
                    //}
                    if ((objBilling.DtInvBetwDtFrm != null) && (objBilling.DtInvBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => cnd.DtInvBetwDtFrm >= objBilling.DtInvBetwDtFrm && cnd.DtInvBetwDtFrm <= objBilling.DtInvBetwDtTo);
                    }

                    else
                    {
                        if ((objBilling.DtInvBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => cnd.DtInvBetwDtFrm >= objBilling.DtInvBetwDtFrm);
                        }
                        if ((objBilling.DtInvBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => cnd.DtInvBetwDtFrm <= objBilling.DtInvBetwDtTo);
                        }
                    }
                    //if ((objBilling.OutsideCarrier != null) && (objBilling.OutsideCarrier.Length > 0) && (objBilling.OutsideCarrier != "All"))
                    //{
                    //    query = query.Where(cnd => cnd.OutsideCarrier == objBilling.OutsideCarrier);
                    //}
                    if ((objBilling.DtPaidBetwDtFrm != null) && (objBilling.DtPaidBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => cnd.DtPaidBetwDtFrm >= objBilling.DtPaidBetwDtFrm && cnd.DtPaidBetwDtFrm <= objBilling.DtPaidBetwDtTo);
                    }
                    else
                    {
                        if ((objBilling.DtPaidBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => cnd.DtPaidBetwDtFrm >= objBilling.DtPaidBetwDtFrm);
                        }
                        if ((objBilling.DtPaidBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => cnd.DtPaidBetwDtFrm <= objBilling.DtPaidBetwDtTo);
                        }
                    }

                    query = query.DistinctByInvoice(x => x.InvoiceNumber).OrderByDescending(x => x.InvoiceDate).AsQueryable();

                    //listBillingPropDetails = (from DistVal in query
                    //                          select new BillingProp
                    //                          {
                    //                              CreatedBetwDtFrm = DistVal.CreationDate,
                    //                              DtInvBetwDtFrm = DistVal.InvoiceDate,
                    //                              DtPaidBetwDtFrm = DistVal.PaidInFullDate,
                    //                              CustomerAddress = DistVal.CustomerAddress,
                    //                              BillingID = DistVal.BillingID,
                    //                              InvoiceNumber = DistVal.InvoiceNumber,
                    //                              CustomerName = DistVal.CustomerName,
                    //                              CustomerNumber = DistVal.CustomerNumber,
                    //                              Vin = DistVal.Vin,
                    //                              CustIndent = DistVal.CustIndent,
                    //                              Driver = DistVal.Driver,
                    //                              OutsideCarrier = DistVal.OutsideCarrier,
                    //                              InvoiceType = DistVal.InvoiceType,
                    //                              InvoiceDate = DistVal.InvoiceDate,
                    //                              LoadNumber = DistVal.LoadNumber,
                    //                              CarrierName = DistVal.CarrierName,
                    //                              InvoiceStatus = DistVal.InvoiceStatus
                    //                          }).DistinctByInvoice(x => x.InvoiceNumber).OrderByDescending(x => x.InvoiceDate);

                    var pageCount = query.Count();

                    if (objBilling.PageSize > 0)
                    {
                        if (objBilling.CurrentPageIndex == 0)
                        {
                            var queryResult = (query.Skip(objBilling.CurrentPageIndex * objBilling.PageSize).Take(objBilling.PageSize)).ToList();
                            if (queryResult.Any())
                            {
                                queryResult.FirstOrDefault().TotalPageCount = pageCount;
                            }
                            return queryResult.OrderBy(x => x.InvoiceDate).Distinct().ToList();
                        }
                        else
                        {
                            var queryResult = (query.Skip((objBilling.CurrentPageIndex * objBilling.PageSize) + (objBilling.DefaultPageSize - objBilling.PageSize)).Take(objBilling.PageSize)).ToList();

                            if (queryResult.Any())
                            {
                                queryResult.FirstOrDefault().TotalPageCount = pageCount;
                            }
                            return queryResult.OrderBy(x => x.InvoiceDate).Distinct().ToList();
                        }
                    }
                    else
                        return await query.OrderBy(x => x.InvoiceDate).ToListAsync();
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
        }



        #region CODE TO BE REMOVED AFTER CODE CLEANUP

        /////bind outside carrier


        ///// <summary>
        ///// This method is used to get the list of the drivers.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //public List<string> DriverList()
        //{
        //    List<string> result = null;
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        using (PortStorageEntities psEntities = new PortStorageEntities())
        //        {
        //            //Sta: SELECT U.LastName+', '+U.FirstName,D.DriverID
        //            //Sta: FROM Users U, Driver D
        //            //Sta: WHERE U.UserID = D. UserID
        //            //;  Sta: AND U.RecordStatus = 'Active' AND D.RecordStatus = 'Active'
        //            //Sta: ORDER BY U.LastName, U.FirstName
        //            var codeChk = (from qry in psEntities.Users join objdriv in psEntities.Drivers on qry.UserID equals objdriv.UserID where qry.RecordStatus == "active" && objdriv.RecordStatus == "active" select qry).ToList().OrderBy(x => x.FirstName).OrderBy(y => y.LastName);
        //            if (codeChk != null)
        //            {
        //                result = codeChk.Select(x => x.LastName + " " + x.FirstName).ToList();
        //            }
        //            return result;
        //        }
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
        //    List<string> result = null;
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        using (PortStorageEntities psEntities = new PortStorageEntities())
        //        {
        //            //Begin statement
        //            // Sta: SELECT OC.CarrierName,OC.OutsideCarrierID
        //            // Sta: FROM Outsidecarrier OC
        //            // ;  Sta: WHERE OC.RecordStatus = 'Active'
        //            // Sta: ORDER BY OC.CarrierName
        //            var codeChk = (from qry in psEntities.OutsideCarriers where qry.RecordStatus == "active" select qry).ToList().OrderBy(x => x.CarrierName);
        //            if (codeChk != null)
        //            {
        //                result = codeChk.Select(x => x.CarrierName).ToList();
        //            }
        //            return result;
        //        }
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
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        using (PortStorageEntities psEntities = new PortStorageEntities())
        //        {

        //            var lstInvoiceDetails = psEntities.spGetBillingInvoiceDetails(CreditedOutInd, CreditMemoInd, NewRunInd, BillingID, RunID, CustomerID).ToList().
        //                                    Select(x => new BillingInvoiceDetailsProp
        //                                    {
        //                                        VIN = x.VIN,
        //                                        LoadNumber = x.LoadNumber,
        //                                        PickUpLocationID = x.PickupLocationID,
        //                                        PickUpLocation = x.PickupLocation,
        //                                        DropOffLocationID = x.DropoffLocationID,
        //                                        DropOffLocation = x.Destination,
        //                                        OrderID = x.OrdersID,
        //                                        OrderNumber = x.OrderNumber,
        //                                        PONumber = x.PONumber,
        //                                        ChargeRate = x.ChargeRate
        //                                    }
        //                                    );
        //            return new List<BillingInvoiceDetailsProp>(lstInvoiceDetails).ToList();
        //        }

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
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var lstBillingData = (from billing in psEntities.Billings
                                          join line in psEntities.BillingLineItems
                                          on billing.InvoiceNumber equals line.InvoiceNumber into line_join
                                          from line in line_join.DefaultIfEmpty()

                                          join customer in psEntities.Customers
                                          on billing.CustomerID equals customer.CustomerID into cust_join
                                          from customer in cust_join.DefaultIfEmpty()

                                          where billing.BillingID == billingID

                                          select new BillingListProp
                                          {
                                              BillingID = billing.BillingID,
                                              DATBillingID = billing.DATBillingID,
                                              CustomerID = billing.CustomerID,
                                              InvoiceStatus = billing.InvoiceStatus,
                                              InvoiceType = billing.InvoiceType,
                                              StorageCharges = billing.StorageCharges,
                                              InvoiceDate = billing.InvoiceDate,
                                              PaymentMethod = billing.PaymentMethod,
                                              FuelSurchargeRate = billing.FuelSurchargeRate,
                                              FuelSurchargeRateOverrideInd = billing.FuelSurchargeRateOverrideInd,
                                              FuelSurcharge = billing.FuelSurcharge,
                                              FuelSurchargeOverrideInd = billing.FuelSurchargeOverrideInd,
                                              InvoiceAmount = billing.InvoiceAmount,
                                              AmountPaid = billing.AmountPaid,
                                              DueFromOutsideCarriers = billing.DueFromOutsideCarriers,
                                              DueToOutsideCarriers = billing.DueToOutsideCarriers,
                                              TransportCharges = billing.TransportCharges,
                                              CreditsIssued = billing.CreditsIssued,
                                              BalanceDue = billing.BalanceDue,
                                              CreditedOutInd = billing.CreditedOutInd,
                                              CreditMemoInd = billing.CreditMemoInd,
                                              RunID = billing.RunID,
                                              Comments = billing.Comments,
                                              CreationDate = billing.CreationDate,
                                              CreatedBy = billing.CreatedBy,
                                              UpdatedDate = billing.UpdatedDate,
                                              UpdatedBy = billing.UpdatedBy,
                                              CustomerNumber = line.CustomerNumber,
                                              InvoiceNumber = billing.InvoiceNumber,
                                              CustomerName = customer.ShortName.Length > 0 ? customer.ShortName : customer.CustomerName,
                                              TotalCharge = psEntities.PortStorageVehicles.Where(a => a.BillingID == billingID).Sum(x => x.TotalCharge)
                                          }).FirstOrDefault();

                    var transportCharge = psEntities.Vehicles.Where(x => x.BillingID == billingID).Sum(c => c.ChargeRate);
                    var storageCharge = psEntities.PortStorageVehicles.Where(x => x.BillingID == billingID).Sum(c => c.TotalCharge);
                    if (lstBillingData.InvoiceType == "TransportCharge" || lstBillingData.InvoiceType == "Credit")
                    {
                        lstBillingData.StorageCharges = 0;
                        lstBillingData.TransportCharges = transportCharge.HasValue ? transportCharge : lstBillingData.TransportCharges;
                        lstBillingData.InvoiceAmount = lstBillingData.TransportCharges + lstBillingData.FuelSurcharge;
                    }
                    else if (lstBillingData.InvoiceType == "StorageCharge" || lstBillingData.InvoiceType == "ExportCharge")
                    {
                        lstBillingData.TransportCharges = 0;
                        lstBillingData.StorageCharges = storageCharge.HasValue ? storageCharge : lstBillingData.StorageCharges;
                        lstBillingData.InvoiceAmount = lstBillingData.StorageCharges;
                    }
                    //ToDo: Calculate Invoice amount based on Invoice detail list
                    else
                    {
                        lstBillingData.TransportCharges = 0;
                        lstBillingData.StorageCharges = 0;
                        //lstBillingData.InvoiceAmount                                                 
                    }

                    //check if there is a DATBillingId
                    //to see if there is a cross reference invoice number
                    if (lstBillingData.DATBillingID.HasValue && lstBillingData.DATBillingID > 0)
                    {
                        var data = psEntities.Billings.Where(x => x.BillingID == lstBillingData.DATBillingID).FirstOrDefault();
                        if (data != null)
                        {
                            lstBillingData.CrossRefNumber = data.InvoiceNumber;
                        }
                    }
                    else
                    {
                        lstBillingData.CrossRefNumber = (from b1 in psEntities.Billings
                                                         join b2 in psEntities.Billings
                                                         on b1.BillingID equals b2.DATBillingID into billing_join
                                                         from bJoin in billing_join.DefaultIfEmpty()
                                                         where b1.BillingID == lstBillingData.BillingID
                                                         select bJoin.InvoiceNumber
                                              ).FirstOrDefault();
                    }

                    return lstBillingData;
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
        /// This method is used to Get Billing Record Export
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="ExportDate"></param>
        /// <returns></returns>
        public List<BillingLineItemsProp> GetBillingRecordExport(int ExportType, DateTime? ExportDate)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    var query = (from qry in psEntities.BillingLineItems
                                 join bli in psEntities.Billings on qry.BillingID equals bli.BillingID
                                 join cust in psEntities.Customers on bli.CustomerID equals cust.CustomerID

                                 select new BillingLineItemsProp
                                 {
                                     BillingLineItemsID = qry.BillingLineItemsID,
                                     CustomerID = qry.CustomerID,
                                     BillingID = qry.BillingID,
                                     CustomerNumber = qry.CustomerNumber,
                                     InvoiceNumber = qry.InvoiceNumber,
                                     InvoiceDate = qry.InvoiceDate,
                                     DebitAccountNumber = qry.DebitAccountNumber,
                                     DebitProfitCenterNumber = qry.DebitProfitCenterNumber,
                                     DebitCostCenterNumber = qry.DebitCostCenterNumber,
                                     CreditAccountNumber = qry.CreditAccountNumber,
                                     CreditProfitCenterNumber = qry.CreditProfitCenterNumber,
                                     CreditCostCenterNumber = qry.CreditCostCenterNumber,
                                     ARTransactionAmount = qry.ARTransactionAmount,
                                     CreditMemoInd = qry.CreditMemoInd,
                                     Description = qry.Description,
                                     ExportedInd = qry.ExportedInd,
                                     ExportBatchID = qry.ExportBatchID,
                                     ExportedDate = qry.ExportedDate,
                                     ExportedBy = qry.ExportedBy,
                                     CreationDate = qry.CreationDate,
                                     CreatedBy = qry.CreatedBy,
                                     UpdatedDate = qry.UpdatedDate,
                                     UpdatedBy = qry.UpdatedBy,
                                     CustomerOf = cust.CustomerOf,
                                     PrintedInd = bli.PrintedInd,
                                     TheYear = qry.InvoiceDate.Value.Year,
                                     TheMonth = qry.InvoiceDate.Value.Month

                                 }
                                     ).AsQueryable();

                    if (ExportType == 0)
                    {
                        query = query.Where((x => x.ExportedInd == null || x.ExportedInd == 0));
                        query = query.Where(x => x.PrintedInd == 1);
                    }
                    else
                    {
                        DateTime ExportDateAdd = Convert.ToDateTime(ExportDate).AddDays(1);
                        query = query.Where(x => x.ExportedInd == 1);
                        query = query.Where(x => x.ExportedDate >= ExportDate && x.ExportedDate < ExportDateAdd);

                    }
                    // Sta: ORDER BY C.CustomerOf, TheYear, TheMonth, BLI.InvoiceDate,BLI.InvoiceNumber, BLI.BillingLineItemsID
                    //query = query.

                    return query.OrderBy(x => x.CustomerOf).ThenBy(x => x.InvoiceDate).ThenBy(x => x.BillingLineItemsID).ToList();
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
        /// This method is used to get batch id for BillingExport
        /// </summary>
        /// <returns></returns>
        public int GetBillingExportBatchId()
        {
            int BatchId = 0;
            try
            {

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var query = (from qry in psEntities.SettingTables
                                 where qry.ValueKey.Equals("NextBillingExportBatchID")
                                 select new { BatchId = qry.ValueDescription }).FirstOrDefault();
                    BatchId = Convert.ToInt32(query.BatchId);


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
            return BatchId;
        }


        /// <summary>
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public bool SetBillingExportNextBatchId(int BatchId)
        {
            bool Result = false;
            try
            {

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    SettingTable settingTable = psEntities.SettingTables.Where(v => v.ValueKey.Equals("NextBillingExportBatchID")).FirstOrDefault();
                    settingTable.ValueDescription = Convert.ToString(BatchId + 1);
                    psEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
                    Result = true;


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
            return Result;
        }


        /// <summary>
        /// This method is used to Get billing Export File Path
        /// </summary>
        /// <returns>string</returns>
        public string GetBillingExportFilePath()
        {
            string FilePath = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var query = (from qry in psEntities.SettingTables
                                 where qry.ValueKey.Equals("BillingLineItemsExportDirectory")
                                 select new { FilePath = qry.ValueDescription }).FirstOrDefault();
                    FilePath = query.FilePath;


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
            return FilePath;
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
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    BillingLineItem billingLineItem = psEntities.BillingLineItems.Where(b => b.BillingLineItemsID == BillingLineItemsID).FirstOrDefault();
                    billingLineItem.ExportedInd = 1;
                    billingLineItem.ExportedDate = DateTime.Now;
                    billingLineItem.ExportedBy = UserCode;
                    billingLineItem.ExportBatchID = BatchId;
                    billingLineItem.UpdatedDate = DateTime.Now;
                    billingLineItem.UpdatedBy = UserCode;
                    psEntities.SaveChanges();

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

    }

    public static class getDistinctInvoice
    {

        public static IEnumerable<TSource> DistinctByInvoice<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}

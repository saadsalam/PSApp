using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using AppWorks.Utilities;

namespace AppWorksService.DAL
{
    public class PortStorageInvoicesDAL
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
            List<PortStorageInvoicesProp> listPortStorageInvoicesProp = new List<PortStorageInvoicesProp>();
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    var result = objAppWorksEntities.spGetPortStorageInvoicesList(cutoffDate.HasValue ? cutoffDate.Value.Date : cutoffDate).ToList();
                    if (result != null)
                    {
                        foreach (var val in result)
                        {
                            PortStorageInvoicesProp obj = new PortStorageInvoicesProp();
                            obj.BillingAddressID = (int)val.CustomerID;
                            obj.CustName = val.CustName;
                            obj.CustomerCode = val.CustomerCode;
                            obj.CustomerId = (int)val.CustomerID;
                            obj.Status = val.Status;
                            obj.Unit = (int)val.Unit;
                            listPortStorageInvoicesProp.Add(obj);
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
            return listPortStorageInvoicesProp;
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
            int getBillingID;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    Billing objBilling = new Billing();
                    objBilling.AmountPaid = objBillingprop.AmountPaid;
                    objBilling.BalanceDue = objBillingprop.BalanceDue;
                    objBilling.Comments = objBillingprop.Comments;
                    objBilling.CreatedBy = objBillingprop.CreatedBy;
                    objBilling.CreationDate = objBillingprop.CreationDate;
                    objBilling.CreditedOutBy = objBillingprop.CreditedOutBy;
                    objBilling.CreditedOutInd = objBillingprop.CreditedOutInd;
                    objBilling.CreditMemoID = objBillingprop.CreditMemoID;
                    objBilling.CreditMemoInd = objBillingprop.CreditMemoInd;
                    objBilling.CreditsIssued = objBillingprop.CreditsIssued;
                    objBilling.CustomerID = objBillingprop.CustomerID;
                    objBilling.DATBillingID = objBillingprop.DATBillingID;
                    objBilling.DATBillingPercentage = objBillingprop.DATBillingPercentage;
                    objBilling.DateExported = objBillingprop.DateExported;
                    objBilling.DatePrinted = objBillingprop.DatePrinted;
                    objBilling.DueFromOutsideCarriers = objBillingprop.DueFromOutsideCarriers;
                    objBilling.DueToOutsideCarriers = objBillingprop.DueToOutsideCarriers;
                    objBilling.ExportedInd = objBillingprop.ExportedInd;
                    objBilling.FuelSurcharge = objBillingprop.FuelSurcharge;
                    objBilling.FuelSurchargeOverrideInd = objBillingprop.FuelSurchargeOverrideInd;
                    objBilling.FuelSurchargeRate = objBillingprop.FuelSurchargeRate;
                    objBilling.FuelSurchargeRateOverrideInd = objBillingprop.FuelSurchargeRateOverrideInd;
                    objBilling.InvoiceAmount = objBillingprop.InvoiceAmount;
                    objBilling.InvoiceNumber = objBillingprop.InvoiceNumber;
                    objBilling.InvoiceStatus = objBillingprop.InvoiceStatus;
                    objBilling.InvoiceType = objBillingprop.InvoiceType;
                    objBilling.OtherCharge1 = objBillingprop.OtherCharge1;
                    objBilling.OtherCharge1Description = objBillingprop.OtherCharge1Description;
                    objBilling.OtherCharge2 = objBillingprop.OtherCharge2;
                    objBilling.OtherCharge2Description = objBillingprop.OtherCharge2Description;
                    objBilling.OtherCharge3 = objBillingprop.OtherCharge3;
                    objBilling.OtherCharge3Description = objBillingprop.OtherCharge3Description;
                    objBilling.OtherCharge4 = objBillingprop.OtherCharge4;
                    objBilling.OtherCharge4Description = objBillingprop.OtherCharge4Description;
                    objBilling.OutsideCarrierID = objBillingprop.OutsideCarrierID;
                    objBilling.OutsideCarrierInvoiceInd = objBillingprop.OutsideCarrierInvoiceInd;
                    objBilling.PaidInFullDate = objBillingprop.PaidInFullDate;
                    objBilling.PaidInFullInd = objBillingprop.PaidInFullInd;
                    objBilling.PaymentMethod = objBillingprop.PaymentMethod;
                    objBilling.InvoiceDate = objBillingprop.InvoiceDate;
                    if (objBillingprop.InvoiceDate.HasValue)
                    {
                        objBilling.InvoiceDate = objBillingprop.InvoiceDate.Value.Date;//remove time component
                    }
                    objBilling.PrintedInd = objBillingprop.PrintedInd;                    
                    if (objBillingprop.PrintedInd == null)
                    {
                        objBilling.PrintedInd = 0;//default to 0
                    }
                    objBilling.RunID = objBillingprop.RunID;
                    objBilling.StorageCharges = objBillingprop.StorageCharges;
                    objBilling.TransportCharges = objBillingprop.TransportCharges;
                    objBilling.UpdatedBy = objBillingprop.UpdatedBy;
                    objBilling.UpdatedDate = objBillingprop.UpdatedDate;
                    objAppWorksEntities.Billings.Add(objBilling);      /// Insert the Record in BillingPeriod Table.
                    objAppWorksEntities.SaveChanges();                                /// Check the Chenges in Table After Record Insertion.
                    getBillingID = objBilling.BillingID;              /// Return the BillingPeriodID of Inserted Billing Period.
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
            return getBillingID;
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
            bool result;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    PortStorageVehicle objPortStorageVehicle = new PortStorageVehicle();
                    objPortStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(x => x.PortStorageVehiclesID == objPortStorageVehicleProp.PortStorageVehiclesID).FirstOrDefault();
                    objPortStorageVehicle.TotalCharge = objPortStorageVehicleProp.TotalCharge;
                    objPortStorageVehicle.EntryRate = objPortStorageVehicleProp.EntryRate;
                    objPortStorageVehicle.PerDiemGraceDays = objPortStorageVehicleProp.PerDiemGraceDays;
                    objPortStorageVehicle.BilledInd = 1;
                    objPortStorageVehicle.BillingID = objPortStorageVehicleProp.BillingID;
                    objPortStorageVehicle.DateBilled = DateTime.Now;
                    objPortStorageVehicle.VehicleStatus = "OutGated";
                    objAppWorksEntities.SaveChanges();
                    result = true;
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
            return result;
        }

        /// <summary>
        /// Function to insert the invoice billing details
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public bool UpdateBilling(BillingProp objBillingprop)
        {
            bool updated = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    Billing objBilling = new Billing();
                    objBilling = objAppWorksEntities.Billings.Where(x => x.BillingID == objBillingprop.BillingID).FirstOrDefault();
                    if (!objBilling.Equals(null))
                    {
                        objBilling.AmountPaid = objBillingprop.AmountPaid;
                        objBilling.BalanceDue = objBillingprop.BalanceDue;
                        objBilling.Comments = objBillingprop.Comments;
                        objBilling.CreatedBy = objBillingprop.CreatedBy;
                        objBilling.CreationDate = objBillingprop.CreationDate;
                        objBilling.CreditedOutBy = objBillingprop.CreditedOutBy;
                        objBilling.CreditedOutInd = objBillingprop.CreditedOutInd;
                        objBilling.CreditMemoID = objBillingprop.CreditMemoID;
                        objBilling.CreditMemoInd = objBillingprop.CreditMemoInd;
                        objBilling.CreditsIssued = objBillingprop.CreditsIssued;
                        objBilling.CustomerID = objBillingprop.CustomerID;
                        objBilling.DATBillingID = objBillingprop.DATBillingID;
                        objBilling.DATBillingPercentage = objBillingprop.DATBillingPercentage;
                        objBilling.DateExported = objBillingprop.DateExported;
                        objBilling.DatePrinted = objBillingprop.DatePrinted;
                        objBilling.DueFromOutsideCarriers = objBillingprop.DueFromOutsideCarriers;
                        objBilling.DueToOutsideCarriers = objBillingprop.DueToOutsideCarriers;
                        objBilling.ExportedInd = objBillingprop.ExportedInd;
                        objBilling.FuelSurcharge = objBillingprop.FuelSurcharge;
                        objBilling.FuelSurchargeOverrideInd = objBillingprop.FuelSurchargeOverrideInd;
                        objBilling.FuelSurchargeRate = objBillingprop.FuelSurchargeRate;
                        objBilling.FuelSurchargeRateOverrideInd = objBillingprop.FuelSurchargeRateOverrideInd;
                        objBilling.InvoiceAmount = objBillingprop.InvoiceAmount;
                        objBilling.InvoiceDate = objBillingprop.InvoiceDate;
                        if (objBillingprop.InvoiceDate.HasValue)
                        {
                            objBilling.InvoiceDate = objBillingprop.InvoiceDate.Value.Date;
                        }
                        objBilling.InvoiceNumber = objBillingprop.InvoiceNumber;
                        objBilling.InvoiceStatus = objBillingprop.InvoiceStatus;
                        objBilling.InvoiceType = objBillingprop.InvoiceType;
                        objBilling.OtherCharge1 = objBillingprop.OtherCharge1;
                        objBilling.OtherCharge1Description = objBillingprop.OtherCharge1Description;
                        objBilling.OtherCharge2 = objBillingprop.OtherCharge2;
                        objBilling.OtherCharge2Description = objBillingprop.OtherCharge2Description;
                        objBilling.OtherCharge3 = objBillingprop.OtherCharge3;
                        objBilling.OtherCharge3Description = objBillingprop.OtherCharge3Description;
                        objBilling.OtherCharge4 = objBillingprop.OtherCharge4;
                        objBilling.OtherCharge4Description = objBillingprop.OtherCharge4Description;
                        objBilling.OutsideCarrierID = objBillingprop.OutsideCarrierID;
                        objBilling.OutsideCarrierInvoiceInd = objBillingprop.OutsideCarrierInvoiceInd;
                        objBilling.PaidInFullDate = objBillingprop.PaidInFullDate;
                        objBilling.PaidInFullInd = objBillingprop.PaidInFullInd;
                        objBilling.PaymentMethod = objBillingprop.PaymentMethod;
                        objBilling.PrintedInd = objBillingprop.PrintedInd;
                        if (objBillingprop.PrintedInd == null)
                        { objBilling.PrintedInd = 0; }
                        objBilling.RunID = objBillingprop.RunID;
                        objBilling.StorageCharges = objBillingprop.StorageCharges;
                        objBilling.TransportCharges = objBillingprop.TransportCharges;
                        objBilling.UpdatedBy = objBillingprop.UpdatedBy;
                        objBilling.UpdatedDate = objBillingprop.UpdatedDate;
                        objAppWorksEntities.SaveChanges();
                        updated = true;
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
            return updated;
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
            string result = string.Empty;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    string getDescription = (from qry in objAppWorksEntities.SettingTables where qry.ValueKey == valueKey select qry.ValueDescription).FirstOrDefault();
                    if (getDescription != null)
                    {
                        result = getDescription;
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


        #region Code To be removed after code cleanup


        //public IList<PortStoragePrintErrorProp> GetPrintErrorsForInvoiceList()
        //{
        //    // creating the object of PortStorageEntities Database
        //    using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
        //    {
        //        List<PortStoragePrintErrorProp> listPortStoragePrintErrorProp = new List<PortStoragePrintErrorProp>();

        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        try
        //        {
        //            var result = objAppWorksEntities.spGetPrintErrorsForInvoiceList().ToList();
        //            if (result != null)
        //            {
        //                foreach (var val in result)
        //                {
        //                    PortStoragePrintErrorProp obj = new PortStoragePrintErrorProp();
        //                    obj.RunID = val.RunID;
        //                    obj.CustomerName = val.CustomerName;
        //                    obj.OrderNumber = val.OrderNumber;
        //                    obj.VIN = val.VIN;
        //                    listPortStoragePrintErrorProp.Add(obj);
        //                    obj = null;
        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {

        //            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        }

        //        return listPortStoragePrintErrorProp;
        //    }
        //}

        #endregion


        /// <summary>
        /// Function to update the setting table values
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public bool UpdateSettingsValue(string invoiceNumber)
        {
            bool updated;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    SettingTable objSettings = new SettingTable();
                    objSettings = objAppWorksEntities.SettingTables.Where(x => x.ValueKey == "NextPortStorageInvoiceNumber").FirstOrDefault();
                    if (objSettings != null)
                    {
                        objSettings.ValueDescription = invoiceNumber;
                        objAppWorksEntities.SaveChanges();
                    }
                    updated = true;
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
            return updated;
        }

        /// <summary>
        /// /// Function to update the setting table values for next invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public bool UpdateNextInvoiceNumberValue(string iInvoiceNumber)
        {
            bool updated;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    SettingTable objSettings = new SettingTable();
                    objSettings = objAppWorksEntities.SettingTables.Where(x => x.ValueKey == "NextInvoiceNumber").FirstOrDefault();
                    if (objSettings != null)
                    {
                        objSettings.ValueDescription = iInvoiceNumber;
                        objAppWorksEntities.SaveChanges();
                    }
                    updated = true;
                }
            }
            catch (Exception)
            {
                //throw;
                updated = false;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return updated;
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
            int result = 0;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var getDescription = (from qry in objAppWorksEntities.Billings where qry.InvoiceNumber == invoiceNumber select qry).ToList();
                    if (getDescription != null)
                    {
                        result = getDescription.Count;
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
        /// Function to get thevehicle list for generate invoice
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <param name="customerId"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public IList<PortStorageVehicleProp> GetPortStorageVehicleList(DateTime? cutoffDate, int customerId)
        {
            List<PortStorageVehicleProp> listPortStorageProp = new List<PortStorageVehicleProp>();

            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    //listPortStorageProp = objAppWorksEntities.Database.SqlQuery<PortStorageVehicleProp>
                    //("SELECT PSV.PortStorageVehiclesID,PSV.CustomerID, PSV.EntryRate, PSV.EntryRateOverrideInd, 0,"
                    //+ " CASE WHEN PSV.PerDiemGraceDays > DATEDIFF(day,PSV.DateIn, PSV.DateOut)+1 THEN DATEDIFF(day,PSV.DateIn, PSV.DateOut)+1 ELSE PSV.PerDiemGraceDays END PerDiemGraceDays,"
                    //+ " PSV.PerDiemGraceDaysOverrideInd, PSV.DateIn, PSV.DateOut, DATEDIFF(day,PSV.DateIn, PSV.DateOut)+1,-1,PSV.VIN,PSV.Make"       //     ;; defaulting total charge to negative one so we will know when there is data missing
                    //+ " FROM PortStorageVehicles PSV"
                    //+ " WHERE PSV.CustomerID = " + customerId + ""
                    //+ " AND ISNULL(PSV.BilledInd,0) = 0"
                    //+ " AND PSV.DateIn IS NOT NULL"
                    //+ " AND PSV.DateOut IS NOT NULL").ToList();

                    var result = objAppWorksEntities.spGetPortStorageVehicleList(customerId).ToList();

                    if (result != null)
                    {
                        foreach (var val in result)
                        {
                            PortStorageVehicleProp obj = new PortStorageVehicleProp();
                            obj.PortStorageVehiclesID = val.PortStorageVehiclesID;
                            obj.CustomerID = val.CustomerID;
                            obj.EntryRate = val.EntryRate;                            
                            obj.EntryRateOverrideInd = val.EntryRateOverrideInd;
                            obj.PerDiemGraceDays = val.PerDiemGraceDays;
                            obj.PerDiemGraceDaysOverrideInd = val.PerDiemGraceDaysOverrideInd;
                            obj.DateIn = val.DateIn;
                            obj.DateOut = val.DateOut;
                            obj.VIN = val.VIN;
                            obj.Make = val.Make;
                            listPortStorageProp.Add(obj);
                            obj = null;
                        }
                    }

                    // AND PSV.DateOut < DATEADD(day,1,@[iCutoffDate])
                    if (cutoffDate != null)
                    {
                        listPortStorageProp = listPortStorageProp.Where(x => x.DateOut.Value.Date <= cutoffDate.Value.Date).ToList();
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

            return listPortStorageProp;
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
            string status = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    var result = objAppWorksEntities.spCalculatePortStoragePerDiem(psVehicleId, userCode);
                    if (result != null)
                    {
                        foreach (var val in result)
                        {
                            status = val.ReturnCode.ToString() + "," + val.ReturnMessage + "," + val.TotalPerDiem.ToString();
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
            return status;
        }


        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public int PsRatesCount(DateTime? dateIn, int customerId)
        {
            int result = 0;

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var getDescription = (from qry in objAppWorksEntities.PortStorageRates
                                          where dateIn >= qry.StartDate
                                          && dateIn < SqlFunctions.DateAdd("day", 1, qry.EndDate ?? (DbFunctions.CreateDateTime(2099, 12, 31, 0, 0, 0)))
                                          && qry.CustomerID == customerId
                                          select qry).ToList();

                    if (getDescription != null)
                    {
                        result = getDescription.Count;
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
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public PSRatesInvoiceProp PsRatesInvoice(DateTime? dateIn, int customerId)
        {
            PSRatesInvoiceProp rateResult = null;

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var result = (from qry in objAppWorksEntities.PortStorageRates
                                  where dateIn >= qry.StartDate
                                  && dateIn < SqlFunctions.DateAdd("day", 1, qry.EndDate ?? (DbFunctions.CreateDateTime(2099, 12, 31, 0, 0, 0)))
                                  && qry.CustomerID == customerId
                                  select qry).FirstOrDefault();

                    if (result != null)
                    {
                        rateResult = new PSRatesInvoiceProp();
                        rateResult.EntryFee = result.EntryFee;
                        rateResult.PerDiemGraceDays = result.PerDiemGraceDays;
                    }

                    return rateResult;
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
            bool updated = false;

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var portStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(x => x.PortStorageVehiclesID == vehicleID).FirstOrDefault();

                    if (portStorageVehicle != null)
                    {
                        portStorageVehicle.EntryRate = entryRate;
                        portStorageVehicle.PerDiemGraceDays = perDiemGraceDays;
                        int result = objAppWorksEntities.SaveChanges();
                        if (result == (int)Enums.SuccessCode.SavedSuccessFully)
                        {
                            updated = true;
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

            return updated;
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
            CustomerSearchProp getCustInfo = new CustomerSearchProp();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    getCustInfo = (from qry in objAppWorksEntities.Customers
                                   where qry.CustomerID == customerId
                                   select new CustomerSearchProp
                                   {
                                       BulkBillingInd = qry.BulkBillingInd,
                                       CustomerCode = qry.CustomerCode,
                                       CustomerOf = qry.CustomerOf
                                   }).FirstOrDefault();
                    return getCustInfo;
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
        /// Function to get the outside carrier leg list details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public List<LoadInfoList> GetLoadInfo(int billingId, decimal pvRatePercentage)
        {
            List<LoadInfoList> listPortStorageProp = new List<LoadInfoList>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    double pvRatePercentageNew = Convert.ToDouble(pvRatePercentage);
                    var result = objAppWorksEntities.spGetLoadInfoData(billingId, pvRatePercentageNew).ToList();
                    if (result != null)
                    {

                        foreach (var val in result)
                        {
                            LoadInfoList obj = new LoadInfoList();

                            obj.VehicleID = (int)val.VehicleID;
                            obj.LegsID = (int)val.LegsID;
                            obj.LoadID = val.LoadsID;
                            obj.OutsideCarrierBasePay = val.OutsideCarrierPay;
                            obj.OutsideCarrierFuelSCPay = val.OutsideCarrierFuelSCPay;
                            obj.OutsideCarrierTotalPay = val.OutsideCarrierTotalPay;
                            obj.LoadNumber = Convert.ToInt32(val.LoadNumber);
                            obj.ExportedInd = val.ExportedInd;
                            obj.ElectronicBillOfLadingInd = val.ElectronicBillOfLadingInd;
                            listPortStorageProp.Add(obj);
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
            return listPortStorageProp;
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
            List<string> listColumnNames = new List<string>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                switch (tableName)
                {
                    case "Billing":
                        listColumnNames = typeof(Billing).GetProperties().Select(property => property.Name).ToList();
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
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
            return listColumnNames;
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
            List<VehicleLegCountProp> listPortStorageProp = new List<VehicleLegCountProp>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    //listPortStorageProp = objAppWorksEntities.Database.SqlQuery<VehicleLegCountProp>("SELECT COUNT(L1.LegsID),COUNT(L2.LegsID) "
                    // + "FROM Vehicle V"
                    // + " LEFT JOIN Legs L1 ON V.VehicleID = L1.VehicleID AND L1.OutsideCarrierLegInd = 0"
                    // + " LEFT JOIN Legs L2 ON V.VehicleID = L2.VehicleID AND L2.OutsideCarrierLegInd = 1"
                    // + " WHERE V.BillingID = " + recordId + ""
                    // + " GROUP BY V.VehicleID").ToList();
                    var result = objAppWorksEntities.spGetVehicleLegsInfo(recordId).ToList();
                    if (result != null)
                    {
                        foreach (var val in result)
                        {
                            VehicleLegCountProp obj = new VehicleLegCountProp();
                            if (val.Column1 != null)
                            { obj.LegsCount1 = (int)val.Column1; }
                            if (val.Column2 != null)
                            { obj.LegsCount2 = (int)val.Column2; }
                            listPortStorageProp.Add(obj);
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
            return listPortStorageProp;
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
            int getCount;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));                
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    getCount = Convert.ToInt32(objAppWorksEntities.spGetInvoiceCreditCostCenterNumber().ToString());
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
            return getCount;
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
            int getBillingLineID;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    BillingLineItem objBillingLine = new BillingLineItem();
                    objBillingLine.CustomerID = objBillingLineprop.CustomerID;
                    objBillingLine.BillingID = objBillingLineprop.BillingID;
                    objBillingLine.CustomerNumber = objBillingLineprop.CustomerNumber;
                    objBillingLine.InvoiceNumber = objBillingLineprop.InvoiceNumber;
                    objBillingLine.InvoiceDate = objBillingLineprop.InvoiceDate;
                    if (objBillingLineprop.InvoiceDate.HasValue)
                    {
                        objBillingLine.InvoiceDate = objBillingLineprop.InvoiceDate.Value.Date;//remove time component
                    }
                    objBillingLine.DebitAccountNumber = objBillingLineprop.DebitAccountNumber;
                    objBillingLine.DebitProfitCenterNumber = objBillingLineprop.DebitProfitCenterNumber;
                    objBillingLine.DebitCostCenterNumber = objBillingLineprop.DebitCostCenterNumber;
                    objBillingLine.CreditAccountNumber = objBillingLineprop.CreditAccountNumber;
                    objBillingLine.CreditProfitCenterNumber = objBillingLineprop.CreditProfitCenterNumber;
                    objBillingLine.CreditCostCenterNumber = objBillingLineprop.CreditCostCenterNumber;
                    objBillingLine.ARTransactionAmount = objBillingLineprop.ARTransactionAmount;
                    objBillingLine.CreditMemoInd = objBillingLineprop.CreditMemoInd;
                    objBillingLine.Description = objBillingLineprop.Description;
                    objBillingLine.ExportedInd = objBillingLineprop.ExportedInd;
                    objBillingLine.ExportBatchID = objBillingLineprop.ExportBatchID;
                    objBillingLine.ExportedDate = objBillingLineprop.ExportedDate;
                    objBillingLine.ExportedBy = objBillingLineprop.ExportedBy;
                    objBillingLine.CreationDate = objBillingLineprop.CreationDate;
                    objBillingLine.CreatedBy = objBillingLineprop.CreatedBy;
                    objBillingLine.UpdatedDate = objBillingLineprop.UpdatedDate;
                    objBillingLine.UpdatedBy = objBillingLineprop.UpdatedBy;
                    objAppWorksEntities.BillingLineItems.Add(objBillingLine);      /// Insert the Record in BillingPeriod Table.
                    objAppWorksEntities.SaveChanges();                                /// Check the Chenges in Table After Record Insertion.
                    getBillingLineID = objBillingLine.BillingLineItemsID;              /// Return the BillingPeriodID of Inserted Billing Period.
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
            return getBillingLineID;
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
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                //IList<PortStoragePrintInvoiceProp> listPortStoragePrintInvoiceProp;
                List<PortStoragePrintInvoiceProp> listPortStoragePrintInvoiceProp = new List<PortStoragePrintInvoiceProp>();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var result = objAppWorksEntities.spGetInvoiceDataForInvPrint(ReprintInd, ReprintType, DateType, InvoiceDateFrom, InvoiceDateTo, InvoiceNumberFrom, InvoiceNumberTo).ToList();
                    if (result != null)
                    {

                        foreach (var val in result)
                        {
                            PortStoragePrintInvoiceProp obj = new PortStoragePrintInvoiceProp();

                            obj.BillingId = val.BillingID;
                            obj.VIN = val.VIN;
                            obj.CustomerName = val.CustomerName;
                            obj.CustomerCode = val.CustomerCode;
                            obj.AddressLine1 = val.AddressLine1;
                            obj.AddressLine2 = val.AddressLine2;
                            obj.City = val.City;
                            obj.State = val.State;
                            obj.Zip = val.Zip;
                            obj.InvoiceNumber = val.InvoiceNumber;
                            if (val.InvoiceDate != null)
                            { obj.InvoiceDate = (DateTime)val.InvoiceDate; }
                            obj.Model = val.Model;
                            obj.DateIn = val.DateIn;
                            obj.DateOut = val.DateOut;
                            obj.EntryRate = val.EntryRate;
                            obj.DailyStorage = val.DailyStorage;
                            obj.StorageDays = val.StorageDays;
                            obj.BilledDays = val.BilledDays;
                            obj.TotalCharge = val.TotalCharge;
                            obj.InvoiceAmount = val.InvoiceAmount;
                            obj.OtherCharge1 = val.OtherCharge1;
                            obj.OtherCharge2 = val.OtherCharge2;
                            obj.OtherCharge3 = val.OtherCharge3;
                            obj.OtherCharge4 = val.OtherCharge4;
                            obj.Header = string.Empty;
                            listPortStoragePrintInvoiceProp.Add(obj);
                            obj = null;
                        }
                    }

                    if (ReprintInd == 0)
                    {
                        foreach (var item in listPortStoragePrintInvoiceProp)
                        {
                            Billing objBilling = new Billing();
                            objBilling = objAppWorksEntities.Billings.Where(x => x.BillingID == item.BillingId).FirstOrDefault();
                            if (objBilling != null)
                            {
                                objBilling.InvoiceStatus = "Printed";
                                objBilling.PrintedInd = 1;
                                objBilling.DatePrinted = DateTime.Now;
                                objAppWorksEntities.SaveChanges();
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

                return listPortStoragePrintInvoiceProp;
            }
        }
    }
}

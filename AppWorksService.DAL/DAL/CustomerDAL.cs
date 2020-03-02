using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;
using AppWorks.Utilities;

namespace AppWorksService.DAL
{
    /// <summary>
    /// class for common method for application.
    /// </summary>
    public class CustomerDAL
    {
        /// <summary>
        /// To find the customer search details
        /// </summary>
        /// <param name="objCuatomerProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-4,2016</createdOn>
        public List<CustomerSearchProp> GetCustomerSearchDetails(CustomerSearchProp objCuatomerProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    var query = (from cust in objAppWorksEntities.Customers
                                 join loc in objAppWorksEntities.Locations
                                 on cust.MainAddressID equals loc.LocationID into tblValues
                                 //where tblValues.Any()
                                 from subset in tblValues.DefaultIfEmpty()

                                 join rates in objAppWorksEntities.PortStorageRates.Where(x => x.StartDate != null && x.EndDate == null)
                                 on cust.CustomerID equals rates.CustomerID into rateValues
                                 //where tblValues.Any()
                                 from subsetrates in rateValues.DefaultIfEmpty()

                                 select new CustomerSearchProp
                                 {

                                     CustomerCode = cust.CustomerCode,
                                     CustomerID = cust.CustomerID,
                                     CustomerType = cust.CustomerType,
                                     //CustomerName = string.IsNullOrEmpty(cust.ShortName) ? cust.CustomerName : cust.ShortName,
                                     CustomerName = cust.CustomerName,
                                     CustomerActualName = cust.CustomerName,
                                     AddressLine1 = subset.AddressLine1,
                                     City = subset.City,
                                     State = subset.State,
                                     Zip = subset.Zip,
                                     DBAName = cust.DBAName,
                                     ShortName = cust.ShortName,
                                     CustomerOf = cust.CustomerOf,
                                     BillingAddressID = cust.BillingAddressID,
                                     MainAddressID = cust.MainAddressID,
                                     //OtherInformations
                                     VendorNumber = cust.VendorNumber,
                                     DefaultBillingMethod = cust.DefaultBillingMethod,
                                     RecordStatus = cust.RecordStatus,
                                     SortOrder = cust.SortOrder,
                                     EligibleForAutoLoad = cust.EligibleForAutoLoadConfigInd,
                                     ALwaysVVIPInd = cust.AlwaysShowInWIPInd,
                                     CollectionIssueInd = cust.CollectionsIssueInd,
                                     AssignedToExternalCollectionsInd = cust.AssignedToExternalCollectionsInd,
                                     BulkBillingInd = cust.BulkBillingInd,
                                     DoNotPrintInvoiceInd = cust.DoNotPrintInvoiceInd,
                                     DoNotExportInvoiceInd = cust.DoNotExportInvoiceInfoInd,
                                     HideNewFreightFromBrokerInd = cust.HideNewFreightFromBrokersInd,
                                     PortStorageCustomerInd = cust.PortStorageCustomerInd,
                                     AutoPortExportCustomerInd = cust.AutoportExportCustomerInd,
                                     RequiresPONumberInd = cust.RequiresPONumberInd,
                                     SendEmailConfirmationsInd = cust.SendEmailConfirmationsInd,
                                     STIFollowupRequiredInd = cust.STIFollowupRequiredInd,
                                     DamagePhotoRequiredInd = cust.DamagePhotoRequiredInd,
                                     ApplyFuelSurchargeInd = cust.ApplyFuelSurchargeInd,
                                     FuelSurchargePercent = cust.FuelSurchargePercent != null ? (float)cust.FuelSurchargePercent : (float)0.00,
                                     LoadNumberPrefix = cust.LoadNumberPrefix,
                                     LoadNumberLength = cust.LoadNumberLength,
                                     NextLoadNUmber = cust.NextLoadNumber,
                                     BookingNumberPrefix = cust.BookingNumberPrefix,
                                     HandHeldScannerCustomerCode = cust.HandheldScannerCustomerCode,
                                     DriverComment = cust.DriverComment,
                                     InternalComment = cust.InternalComment,
                                     CreatedDate = cust.CreationDate,
                                     CreatedBy = cust.CreatedBy,
                                     UpdatedDate = cust.UpdatedDate,
                                     UpdatedBy = cust.UpdatedBy,
                                     EntryRate = subsetrates.EntryFee,
                                     PerDiemGraceDays = subsetrates.PerDiemGraceDays


                                 }).AsQueryable().Distinct();
                    if ((objCuatomerProp.PortStorageCustomerInd != null) && (objCuatomerProp.PortStorageCustomerInd > 0))
                    {
                        query = query.Where(cnd => cnd.PortStorageCustomerInd == objCuatomerProp.PortStorageCustomerInd);
                    }
                    if ((objCuatomerProp.CustomerName != null) && (objCuatomerProp.CustomerName.Length > 0))
                    {
                        query = query.Where(cnd => cnd.ShortName.Contains(objCuatomerProp.CustomerName) || cnd.CustomerActualName.Contains(objCuatomerProp.CustomerName));
                    }
                    if ((objCuatomerProp.CustomerType != null && objCuatomerProp.CustomerType != "All") && (objCuatomerProp.CustomerType.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerType.Contains(objCuatomerProp.CustomerType));
                    }
                    if ((objCuatomerProp.CustomerCode != null) && (objCuatomerProp.CustomerCode.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerCode.Contains(objCuatomerProp.CustomerCode));
                    }
                    if ((objCuatomerProp.City != null) && (objCuatomerProp.City.Length > 0))
                    {
                        query = query.Where(cnd => cnd.City.Contains(objCuatomerProp.City));
                    }
                    if ((objCuatomerProp.State != null) && (objCuatomerProp.State.Length > 0))
                    {
                        query = query.Where(cnd => cnd.State.Contains(objCuatomerProp.State));
                    }
                    if ((objCuatomerProp.Zip != null) && (objCuatomerProp.Zip.Length > 0))
                    {
                        query = query.Where(cnd => cnd.Zip.Contains(objCuatomerProp.Zip));
                    }
                    if ((objCuatomerProp.CustomerID > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerID == objCuatomerProp.CustomerID);
                    }

                    List<CustomerSearchProp> objCustomerDetails = new List<CustomerSearchProp>();
                    objCustomerDetails = (from DistVal in query.AsEnumerable()
                                          select new CustomerSearchProp
                                          {
                                              CustomerCode = DistVal.CustomerCode,
                                              CustomerID = DistVal.CustomerID,
                                              CustomerType = DistVal.CustomerType,
                                              //CustomerName = string.IsNullOrEmpty(cust.ShortName) ? cust.CustomerName : cust.ShortName,
                                              CustomerName = DistVal.CustomerName,
                                              CustomerActualName = DistVal.CustomerName,
                                              AddressLine1 = DistVal.AddressLine1,
                                              City = DistVal.City,
                                              State = DistVal.State,
                                              Zip = DistVal.Zip,
                                              DBAName = DistVal.DBAName,
                                              ShortName = DistVal.ShortName,
                                              CustomerOf = DistVal.CustomerOf,
                                              BillingAddressID = DistVal.BillingAddressID,
                                              MainAddressID = DistVal.MainAddressID,
                                              //OtherInformations
                                              VendorNumber = DistVal.VendorNumber,
                                              DefaultBillingMethod = DistVal.DefaultBillingMethod,
                                              RecordStatus = DistVal.RecordStatus,
                                              SortOrder = DistVal.SortOrder,
                                              EligibleForAutoLoad = DistVal.EligibleForAutoLoad,
                                              ALwaysVVIPInd = DistVal.ALwaysVVIPInd,
                                              CollectionIssueInd = DistVal.CollectionIssueInd,
                                              AssignedToExternalCollectionsInd = DistVal.AssignedToExternalCollectionsInd,
                                              BulkBillingInd = DistVal.BulkBillingInd,
                                              DoNotPrintInvoiceInd = DistVal.DoNotPrintInvoiceInd,
                                              DoNotExportInvoiceInd = DistVal.DoNotExportInvoiceInd,
                                              HideNewFreightFromBrokerInd = DistVal.HideNewFreightFromBrokerInd,
                                              PortStorageCustomerInd = DistVal.PortStorageCustomerInd,
                                              AutoPortExportCustomerInd = DistVal.AutoPortExportCustomerInd,
                                              RequiresPONumberInd = DistVal.RequiresPONumberInd,
                                              SendEmailConfirmationsInd = DistVal.SendEmailConfirmationsInd,
                                              STIFollowupRequiredInd = DistVal.STIFollowupRequiredInd,
                                              DamagePhotoRequiredInd = DistVal.DamagePhotoRequiredInd,
                                              ApplyFuelSurchargeInd = DistVal.ApplyFuelSurchargeInd,
                                              FuelSurchargePercent = DistVal.FuelSurchargePercent != null ? (float)DistVal.FuelSurchargePercent : (float)0.00,
                                              LoadNumberPrefix = DistVal.LoadNumberPrefix,
                                              LoadNumberLength = DistVal.LoadNumberLength,
                                              NextLoadNUmber = DistVal.NextLoadNUmber,
                                              BookingNumberPrefix = DistVal.BookingNumberPrefix,
                                              HandHeldScannerCustomerCode = DistVal.HandHeldScannerCustomerCode,
                                              DriverComment = DistVal.DriverComment,
                                              InternalComment = DistVal.InternalComment,
                                              CreatedDate = DistVal.CreatedDate,
                                              CreatedBy = DistVal.CreatedBy,
                                              UpdatedDate = DistVal.UpdatedDate,
                                              UpdatedBy = DistVal.UpdatedBy,
                                              EntryRate = DistVal.EntryRate,
                                              PerDiemGraceDays = DistVal.PerDiemGraceDays

                                          }).DistinctByID(x => x.CustomerID).ToList();


                    var pageCount = query.ToList().Count;
                    if (objCuatomerProp.CurrentPageIndex == 0)
                    {
                        var queryResult = objCustomerDetails.ToList().Skip(objCuatomerProp.CurrentPageIndex * objCuatomerProp.PageSize).Take(objCuatomerProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
                    }
                    else
                    {
                        var queryResult = objCustomerDetails.ToList().Skip((objCuatomerProp.CurrentPageIndex * objCuatomerProp.PageSize) + (objCuatomerProp.DefaultPageSize - objCuatomerProp.PageSize)).Take(objCuatomerProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
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
        /// To Load The Customer Type 
        /// </summary>
        /// <param name="objCuatomerProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<string> CustomerType()
        {
            try
            {
                List<string> lstCustomerType = new List<string>();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var list = (from tblCustomer in objAppWorksEntities.Customers select tblCustomer.CustomerType).Distinct().ToList();
                    if (list.Count > 0)
                        lstCustomerType = list.ToList();
                }
                return lstCustomerType;
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
        /// Get the code list coressponding to the given code type
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public List<CodeList> LoadCodeList(string codeType)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    var lstCodeList = (from tblCodes in objAppWorksEntities.Codes
                                       where tblCodes.RecordStatus == "Active" && tblCodes.CodeType == codeType
                                       select new CodeList
                                       {
                                           Description = tblCodes.CodeDescription,
                                           Code1 = tblCodes.Code1,
                                           CodeType = tblCodes.CodeType
                                       }).Distinct().OrderBy(x => x.CodeType).ToList();
                    return lstCodeList;
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
        /// Get Location List for a perticular location type or all location type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="locationType"></param>
        /// <returns></returns>
        public List<LocationList> GetLocationList(LocationList objLocationProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstLocationList = (from tblLocation in objAppWorksEntities.Locations
                                           where ((tblLocation.ParentRecordID == objLocationProp.CustomerId && tblLocation.ParentRecordTable == "Customer") || tblLocation.ParentRecordTable == "Common")
                                           select new LocationList
                                           {
                                               LocationID = tblLocation.LocationID,
                                               ParentRecordTable = tblLocation.ParentRecordTable == "Common" ? "Yes" : "No",
                                               LocationName = tblLocation.LocationName,
                                               LocationShortName = tblLocation.LocationShortName,
                                               AddressLine1 = tblLocation.AddressLine1,
                                               AddressLine2 = tblLocation.AddressLine2,
                                               City = tblLocation.City,
                                               State = tblLocation.State,
                                               Zip = tblLocation.Zip,
                                               Country = tblLocation.Country,
                                               CustomerLocationCode = tblLocation.CustomerLocationCode,
                                               MainPhone = tblLocation.MainPhone,
                                               FaxNumber = tblLocation.FaxNumber,
                                               PrimaryContactFirstName = tblLocation.PrimaryContactFirstName + " " + tblLocation.PrimaryContactLastName,
                                               PrimaryContactPhone = tblLocation.PrimaryContactPhone,
                                               PrimaryContactExtension = tblLocation.PrimaryContactExtension,
                                               DeliveryTimes = tblLocation.DeliveryTimes,
                                               LocationType = tblLocation.LocationType,
                                               LocationSubType = tblLocation.LocationSubType,
                                               PrimaryContactEmail = tblLocation.PrimaryContactEmail,

                                               AlternateContactFirstName = tblLocation.AlternateContactFirstName,
                                               AlternateContactLastName = tblLocation.AlternateContactLastName,
                                               AlternateContactPhone = tblLocation.AlternateContactPhone,
                                               AlternateContactExtension = tblLocation.AlternateContactExtension,
                                               AlternateContactCellPhone = tblLocation.AlternateContactCellPhone,
                                               AlternateContactEmail = tblLocation.AlternateContactEmail,
                                               RecordStatus = tblLocation.RecordStatus,
                                               CreatedBy = tblLocation.CreatedBy,
                                               CreationDate = tblLocation.CreationDate,
                                               UpdatedBy = tblLocation.UpdatedBy,
                                               UpdatedDate = tblLocation.UpdatedDate,
                                               OtherPhone1Description = tblLocation.OtherPhone1Description,
                                               OtherPhone1 = tblLocation.OtherPhone1,
                                               OtherPhone2Description = tblLocation.OtherPhone2Description,
                                               OtherPhone2 = tblLocation.OtherPhone2,
                                           }
                                          ).AsQueryable();
                    if ((objLocationProp.LocationType != null) && (!objLocationProp.LocationType.ToUpper().Equals("ALL")))
                    {
                        string LocType = objLocationProp.LocationType.Replace(" ", "");
                        lstLocationList = lstLocationList.Where(x => x.LocationType.Equals(LocType)).Distinct().OrderBy(x => x.LocationName);
                    }
                    ////return query.ToList();
                    ////var pageCount = lstLocationList.ToList().Count;
                    //var queryResult = lstLocationList.ToList().Skip(objLocationProp.CurrentPageIndex * objLocationProp.PageSize).Take(objLocationProp.PageSize).ToList();
                    //if (queryResult != null && queryResult.Count > 0)
                    //{
                    //    queryResult.FirstOrDefault().TotalPageCount = pageCount;
                    //}
                    //return queryResult.ToList();

                    var getQueryVal = lstLocationList.ToList();
                    var pageCount = getQueryVal.ToList().Count;

                    if (objLocationProp.CurrentPageIndex == 0 && objLocationProp.PageSize > 0)
                    {
                        var queryResult = getQueryVal.ToList().Skip(objLocationProp.CurrentPageIndex * objLocationProp.PageSize).Take(objLocationProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.OrderBy(x => x.LocationName).ToList();
                    }
                    else if (objLocationProp.PageSize > 0)
                    {
                        var queryResult = getQueryVal.ToList().Skip((objLocationProp.CurrentPageIndex * objLocationProp.PageSize) + (objLocationProp.DefaultPageSize - objLocationProp.PageSize)).Take(objLocationProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.OrderBy(x => x.LocationName).ToList();
                    }
                    else
                    {
                        return lstLocationList.OrderBy(x => x.LocationName).ToList();
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
        /// To get the data for billing address for a perticular customer.
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public List<LocationList> GetBillingStreetAddress(int addressID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstBillingStreetAddress = (from tblLocation in objAppWorksEntities.Locations
                                                   where tblLocation.LocationID == addressID // can be Billingaddress or MainAddress
                                                   select new LocationList
                                                   {
                                                       LocationID = tblLocation.LocationID,
                                                       AddressLine1 = tblLocation.AddressLine1,
                                                       AddressLine2 = tblLocation.AddressLine2,
                                                       City = tblLocation.City,
                                                       State = tblLocation.State,
                                                       Zip = tblLocation.Zip,
                                                       Country = tblLocation.Country,
                                                       MainPhone = tblLocation.MainPhone,
                                                       FaxNumber = tblLocation.FaxNumber,
                                                       PrimaryContactFirstName = tblLocation.PrimaryContactFirstName + " " + tblLocation.PrimaryContactLastName,
                                                       PrimaryContactPhone = tblLocation.PrimaryContactPhone,
                                                       PrimaryContactEmail = tblLocation.PrimaryContactEmail,
                                                       OtherPhone1Description = tblLocation.OtherPhone1Description,
                                                       OtherPhone1 = tblLocation.OtherPhone1,
                                                       OtherPhone2Description = tblLocation.OtherPhone2Description,
                                                       OtherPhone2 = tblLocation.OtherPhone2,
                                                   }).ToList();
                    return lstBillingStreetAddress;
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

        #region Code to be removed after cleanup

        ///// <summary>
        ///// To get the for from location for a customer..
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> FromLocationList(int customerID)
        //{
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //        using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
        //        {
        //            var lstLocationList = (from tblLocation in objAppWorksEntities.Locations
        //                                   join tblChargerRate in objAppWorksEntities.ChargeRates
        //                                   on tblLocation.LocationID equals tblChargerRate.StartLocationID
        //                                   where tblChargerRate.CustomerID == customerID
        //                                   select new LocationList
        //                                   {
        //                                       LocationID = tblLocation.LocationID,
        //                                       LocationName = tblLocation.LocationName
        //                                   }).OrderBy(x => x.LocationName).ToList();

        //            return lstLocationList;
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
        ///// To get the data for to location for a customer..
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> ToLocationList(int customerID)
        //{
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //        using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
        //        {
        //            var lstLocationList = (from tblLocation in objAppWorksEntities.Locations
        //                                   join tblChargerRate in objAppWorksEntities.ChargeRates
        //                                   on tblLocation.LocationID equals tblChargerRate.EndLocationID
        //                                   where tblChargerRate.CustomerID == customerID
        //                                   select new LocationList
        //                                   {
        //                                       LocationID = tblLocation.LocationID,
        //                                       LocationName = tblLocation.LocationName
        //                                   }).OrderBy(x => x.LocationName).ToList();

        //            return lstLocationList;
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



        #region Code to be removed after cleanup
        ///// <summary>
        ///// To get the order history for a perticular customer.
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <param name="orderStatus"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public List<OrderHistoryList> GetOrderHistory(int customerID, string orderStatus, DateTime? startDate, DateTime? endDate)
        //{
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //        using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
        //        {

        //            var lstOrderHistory = (from tblOrder in objAppWorksEntities.Orders
        //                                   join tblpickuplocation in objAppWorksEntities.Locations
        //                                    on tblOrder.PickupLocation equals tblpickuplocation.LocationID into temppickuplocation
        //                                   where temppickuplocation.Any()
        //                                   from pickuplocation in temppickuplocation.DefaultIfEmpty()

        //                                   join tbldropofflocation in objAppWorksEntities.Locations
        //                                    on tblOrder.DropoffLocation equals tbldropofflocation.LocationID into tempdropofflocation
        //                                   where tempdropofflocation.Any()
        //                                   from dropofflocation in tempdropofflocation.DefaultIfEmpty()
        //                                   where tblOrder.CustomerID == customerID
        //                                   select new OrderHistoryList
        //                                   {
        //                                       OrderID = tblOrder.OrdersID,
        //                                       OrderNumber = tblOrder.OrderNumber,
        //                                       PickUpLocationID = tblOrder.DropoffLocation,
        //                                       PickUpLocationName = pickuplocation.LocationName,
        //                                       DropOffLocationID = tblOrder.DropoffLocation,
        //                                       DropOffLocationName = dropofflocation.LocationName,
        //                                       Units = tblOrder.Units,
        //                                       OrderStatus = tblOrder.OrderStatus,
        //                                       CreationDate = tblOrder.CreationDate,
        //                                       UpdatedDate = tblOrder.UpdatedDate
        //                                   }).AsQueryable();

        //            if (!orderStatus.ToUpper().Equals("ALL"))
        //                lstOrderHistory = lstOrderHistory.Where(x => x.OrderStatus == orderStatus).OrderBy(x => x.CreationDate);
        //            if (!startDate.Equals(null))
        //                lstOrderHistory = lstOrderHistory.Where(x => x.CreationDate >= startDate).OrderBy(x => x.CreationDate);
        //            if (!endDate.Equals(null))
        //                lstOrderHistory = lstOrderHistory.Where(x => x.CreationDate < Convert.ToDateTime(endDate).AddDays(1)).OrderBy(x => x.CreationDate);

        //            return lstOrderHistory.ToList();
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
        /// To get the notes list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<CustomerNoteList> GetNotesList(int customerID, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstNotesList = (from tblCustomerNotes in objAppWorksEntities.CustomerNotes
                                        where tblCustomerNotes.CustomerID == customerID
                                        select new CustomerNoteList
                                        {
                                            CustomerNotesID = tblCustomerNotes.CustomerNotesID,
                                            Note = tblCustomerNotes.Note,
                                            CreationDate = tblCustomerNotes.CreationDate,
                                            CreatedBy = tblCustomerNotes.CreatedBy
                                        }).AsQueryable();

                    if (!startDate.Equals(null))
                        lstNotesList = lstNotesList.Where(x => x.CreationDate >= startDate).OrderByDescending(x => x.CreationDate);
                    if (!endDate.Equals(null))
                        lstNotesList = lstNotesList.Where(x => x.CreationDate < Convert.ToDateTime(endDate).AddDays(1)).OrderByDescending(x => x.CreationDate);

                    return lstNotesList.ToList();
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
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PortStorageRateList> GetPortStorageRateList(PortStorageRateList objPortStorageRateProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstPortStorageList = (from tblPortStorageRate in objAppWorksEntities.PortStorageRates
                                              where tblPortStorageRate.CustomerID == objPortStorageRateProp.CustomerID
                                              select new PortStorageRateList
                                              {
                                                  CustomerID = tblPortStorageRate.CustomerID,
                                                  PortStorageRatesID = tblPortStorageRate.PortStorageRatesID,
                                                  EntryFee = tblPortStorageRate.EntryFee,
                                                  PerDiem = tblPortStorageRate.PerDiem,
                                                  PerDiemGraceDays = tblPortStorageRate.PerDiemGraceDays,
                                                  StartDate = tblPortStorageRate.StartDate,
                                                  EndDate = tblPortStorageRate.EndDate,
                                                  CreatedBy = tblPortStorageRate.CreatedBy,
                                                  CreationDate=tblPortStorageRate.CreationDate,
                                                  UpdatedBy=tblPortStorageRate.UpdatedBy,
                                                  UpdatedDate=tblPortStorageRate.UpdatedDate
                                              }).AsQueryable();
                    var endDate = Convert.ToDateTime(objPortStorageRateProp.EndDate).AddDays(1);
                    if (!objPortStorageRateProp.StartDate.Equals(null))
                        lstPortStorageList = lstPortStorageList.Where(x => x.StartDate >= objPortStorageRateProp.StartDate).OrderBy(x => x.StartDate);
                    if (!objPortStorageRateProp.EndDate.Equals(null))
                        lstPortStorageList = lstPortStorageList.Where(x => x.StartDate < endDate).OrderBy(x => x.StartDate);

                    if ((!objPortStorageRateProp.PortStorageRatesID.Equals(null)) && (objPortStorageRateProp.PortStorageRatesID>0))
                        lstPortStorageList = lstPortStorageList.Where(x => x.PortStorageRatesID == objPortStorageRateProp.PortStorageRatesID).OrderBy(x => x.StartDate);
                    //var pageCount = lstPortStorageList.OrderBy(x => x.StartDate).ToList().Count;

                    if (objPortStorageRateProp.CurrentPageIndex == 0 && objPortStorageRateProp.PageSize > 0)
                    {
                        var queryResult = lstPortStorageList.ToList().OrderBy(x => x.CustomerID).Skip(objPortStorageRateProp.CurrentPageIndex * objPortStorageRateProp.PageSize).Take(objPortStorageRateProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count() > 0)
                        {
                            //queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.ToList();
                    }
                    else if (objPortStorageRateProp.PageSize > 0)
                    {
                        var queryResult = lstPortStorageList.ToList().OrderBy(x => x.CustomerID).Skip((objPortStorageRateProp.CurrentPageIndex * objPortStorageRateProp.PageSize) + (objPortStorageRateProp.DefaultPageSize - objPortStorageRateProp.PageSize)).Take(objPortStorageRateProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count() > 0)
                        {
                            //queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.ToList();
                    }
                    //else
                    //{
                    //    return lstPortStorageList.OrderBy(x => x.CustomerID).ToList();
                    //}
                    return lstPortStorageList.ToList();
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
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// 
        /// <returns></returns>
        public bool AddPortStorageRate(PortStorageRateList objPortStorageRateProp)
        {
            var isSuccessfull = false;
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                PortStorageRate objPortStorageRate = new PortStorageRate();
                objPortStorageRate.CreatedBy = objPortStorageRateProp.CreatedBy;
                objPortStorageRate.CreationDate = objPortStorageRateProp.CreationDate;
                objPortStorageRate.UpdatedBy = objPortStorageRateProp.UpdatedBy;
                objPortStorageRate.UpdatedDate = objPortStorageRateProp.UpdatedDate;
                objPortStorageRate.CustomerID = objPortStorageRateProp.CustomerID;
                objPortStorageRate.EntryFee = objPortStorageRateProp.EntryFee;
                objPortStorageRate.PerDiem = objPortStorageRateProp.PerDiem;
                objPortStorageRate.PerDiemGraceDays = objPortStorageRateProp.PerDiemGraceDays;
                objPortStorageRate.StartDate = objPortStorageRateProp.StartDate;
                objPortStorageRate.EndDate = objPortStorageRateProp.EndDate;
                objAppWorksEntities.PortStorageRates.Add(objPortStorageRate);  /// Insert the Record in Respected Table.
                objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                isSuccessfull = true;
            }
            
            return isSuccessfull;

        }

        /// <summary>
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// 
        /// <returns></returns>
        public bool UpdatePortStorageRate(PortStorageRateList objPortStorageRateProp)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    PortStorageRate objPortStorageRate = objAppWorksEntities.PortStorageRates.Where(x => x.PortStorageRatesID == objPortStorageRateProp.PortStorageRatesID).FirstOrDefault();

                    if (!objPortStorageRate.Equals(null))
                    {
                        objPortStorageRate.CreatedBy = objPortStorageRateProp.CreatedBy;
                        objPortStorageRate.CreationDate = objPortStorageRateProp.CreationDate;
                        objPortStorageRate.UpdatedBy = objPortStorageRateProp.UpdatedBy;
                        objPortStorageRate.UpdatedDate = objPortStorageRateProp.UpdatedDate;
                        objPortStorageRate.CustomerID = objPortStorageRateProp.CustomerID;
                        objPortStorageRate.EntryFee = objPortStorageRateProp.EntryFee;
                        objPortStorageRate.PerDiem = objPortStorageRateProp.PerDiem;
                        objPortStorageRate.PerDiemGraceDays = objPortStorageRateProp.PerDiemGraceDays;
                        objPortStorageRate.StartDate = objPortStorageRateProp.StartDate;
                        objPortStorageRate.EndDate = objPortStorageRateProp.EndDate;
                        objAppWorksEntities.SaveChanges();          /// Update  Chenges in Table.
                        IsSuccessfull = true;
                    }
                }
                return IsSuccessfull;

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
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// 
        /// <returns></returns>
        public bool DeletePortStorageRate(int portStorageRateID)
        {
            try
            {
                bool result = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var portStorageRateData = objAppWorksEntities.PortStorageRates.Where(v => v.PortStorageRatesID == portStorageRateID).FirstOrDefault();
                    if (portStorageRateData != null)
                    {
                        objAppWorksEntities.PortStorageRates.Remove(portStorageRateData);
                        objAppWorksEntities.SaveChanges();
                        result = true;
                    }
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

        /// <summary>
        /// Get the origin location list for Location Performance Standard
        /// </summary>
        /// <returns></returns>
        public List<LocationList> LoadPerformanceStndrdOriginLocationList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstOrigin = (from tblOriginLocation in objAppWorksEntities.Locations
                                     where (tblOriginLocation.LocationSubType.Contains("Port") || tblOriginLocation.LocationSubType.Contains("Railyard"))
                                     && tblOriginLocation.ParentRecordTable == "Common"
                                     select new LocationList
                                     {
                                         LocationName = tblOriginLocation.LocationShortName.Length > 0 ? tblOriginLocation.LocationShortName : tblOriginLocation.LocationName,
                                         LocationID = tblOriginLocation.LocationID
                                     }).OrderBy(x => x.LocationName).ToList();

                    return lstOrigin;
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
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {

            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    //LocationEmailContact locationmEailContact = new LocationEmailContact();
                    //locationmEailContact.LocationID = objLocationEmailContact.LocationID;
                    //locationmEailContact.GreetingName = objLocationEmailContact.GreetingName;
                    //locationmEailContact.EmailAddress = objLocationEmailContact.EmailAddress;
                    //locationmEailContact.HTMLEmailSupportedInd = objLocationEmailContact.HTMLEmailSupportedInd;
                    //locationmEailContact.PickupNoticeInd = objLocationEmailContact.PickupNoticeInd;
                    //locationmEailContact.STIDeliveryNoticeInd = objLocationEmailContact.STIDeliveryNoticeInd;
                    //locationmEailContact.BookingRecordInd = objLocationEmailContact.BookingRecordInd;
                    //locationmEailContact.BillOfLadingInd = objLocationEmailContact.BillOfLadingInd;
                    //locationmEailContact.CreationDate = objLocationEmailContact.CreationDate;
                    //locationmEailContact.CreatedBy = objLocationEmailContact.CreatedBy;
                    //locationmEailContact.UpdatedDate = objLocationEmailContact.UpdatedDate;
                    //locationmEailContact.UpdatedBy = objLocationEmailContact.UpdatedBy;

                    //objAppWorksEntities.LocationEmailContacts.Add(locationmEailContact);  /// Insert the Record in Respected Table.
                    //objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        /// For inserting LocationPerformancestandard in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {

            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    //LocationPerformanceStandard locationPerformanceStandard = new LocationPerformanceStandard();
                    //locationPerformanceStandard.CustomerID = objLocationPerformanceStandard.CustomerID;
                    //locationPerformanceStandard.OriginID = objLocationPerformanceStandard.OriginID;
                    //locationPerformanceStandard.DestinationID = objLocationPerformanceStandard.DestinationID;
                    //locationPerformanceStandard.StandardDays = objLocationPerformanceStandard.StandardDays;
                    //locationPerformanceStandard.CreationDate = objLocationPerformanceStandard.CreationDate;
                    //locationPerformanceStandard.CreatedBy = objLocationPerformanceStandard.CreatedBy;
                    //locationPerformanceStandard.UpdatedDate = objLocationPerformanceStandard.UpdatedDate;
                    //locationPerformanceStandard.UpdatedBy = objLocationPerformanceStandard.UpdatedBy;
                    //locationPerformanceStandard.StandardBasis = objLocationPerformanceStandard.StandardBasis;

                    //objAppWorksEntities.LocationPerformanceStandards.Add(locationPerformanceStandard);  /// Insert the Record in Respected Table.
                    //objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        /// For updating locationemailcontact information in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {

            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    //LocationEmailContact locationEmailContacts = objAppWorksEntities.LocationEmailContacts.Where(x => x.LocationEmailContactsID == objLocationEmailContact.LocationEmailContactsID).FirstOrDefault();

                    //if (!locationEmailContacts.Equals(null))
                    //{
                    //    locationEmailContacts.GreetingName = objLocationEmailContact.GreetingName;
                    //    locationEmailContacts.EmailAddress = objLocationEmailContact.EmailAddress;
                    //    locationEmailContacts.HTMLEmailSupportedInd = objLocationEmailContact.HTMLEmailSupportedInd;
                    //    locationEmailContacts.PickupNoticeInd = objLocationEmailContact.PickupNoticeInd;
                    //    locationEmailContacts.BillOfLadingInd = objLocationEmailContact.BillOfLadingInd;
                    //    locationEmailContacts.STIDeliveryNoticeInd = objLocationEmailContact.STIDeliveryNoticeInd;
                    //    locationEmailContacts.BookingRecordInd = objLocationEmailContact.BookingRecordInd;

                    //    locationEmailContacts.UpdatedDate = objLocationEmailContact.UpdatedDate;
                    //    locationEmailContacts.UpdatedBy = objLocationEmailContact.UpdatedBy;

                    //    objAppWorksEntities.SaveChanges();          /// Update  Chenges in Table.
                    //    IsSuccessfull = true;
                    //}
                }
                return IsSuccessfull;

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
        /// For updating LocationPerformancestandard informance in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    //LocationPerformanceStandard locationPerformanceStandard = objAppWorksEntities.LocationPerformanceStandards.Where(x => x.LocationPerformanceStandardsID == objLocationPerformanceStandard.LocationPerformanceStandardsID).FirstOrDefault();

                    //locationPerformanceStandard.OriginID = objLocationPerformanceStandard.OriginID;
                    //locationPerformanceStandard.StandardDays = objLocationPerformanceStandard.StandardDays;
                    //locationPerformanceStandard.StandardBasis = objLocationPerformanceStandard.StandardBasis;
                    //locationPerformanceStandard.UpdatedDate = objLocationPerformanceStandard.UpdatedDate;
                    //locationPerformanceStandard.UpdatedBy = objLocationPerformanceStandard.UpdatedBy;

                    //objAppWorksEntities.SaveChanges();          /// update  Chenges in Table.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public int AddLocation(LocationList objLocation)
        {
            try
            {
                int locationID = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    Location location = new Location();
                    location.ParentRecordID = objLocation.ParentRecordID;
                    location.ParentRecordTable = objLocation.ParentRecordTable;
                    location.LocationType = objLocation.LocationType;
                    location.LocationSubType = objLocation.LocationSubType;
                    location.LocationName = objLocation.LocationName;
                    location.LocationShortName = objLocation.LocationShortName;
                    location.CustomerRegionCode = objLocation.CustomerRegionCode;
                    location.CustomerLocationCode = objLocation.CustomerLocationCode;
                    location.AddressLine1 = objLocation.AddressLine1;
                    location.AddressLine2 = objLocation.AddressLine2;
                    location.State = objLocation.State;
                    location.City = objLocation.City;
                    location.Zip = objLocation.Zip;
                    location.Country = objLocation.Country;
                    location.MainPhone = objLocation.MainPhone;
                    location.FaxNumber = objLocation.FaxNumber;
                    location.PrimaryContactFirstName = objLocation.PrimaryContactFirstName;
                    location.PrimaryContactLastName = objLocation.PrimaryContactLastName;
                    location.PrimaryContactCellPhone = objLocation.PrimaryContactCellPhone;
                    location.PrimaryContactEmail = objLocation.PrimaryContactEmail;
                    location.PrimaryContactExtension = objLocation.PrimaryContactExtension;
                    location.PrimaryContactPhone = objLocation.PrimaryContactPhone;
                    location.AlternateContactFirstName = objLocation.AlternateContactFirstName;
                    location.AlternateContactLastName = objLocation.AlternateContactLastName;
                    location.AlternateContactCellPhone = objLocation.AlternateContactCellPhone;
                    location.AlternateContactEmail = objLocation.AlternateContactEmail;
                    location.AlternateContactExtension = objLocation.AlternateContactExtension;
                    location.AlternateContactPhone = objLocation.AlternateContactPhone;
                    location.OtherPhone1 = objLocation.OtherPhone1;
                    location.OtherPhone1Description = objLocation.OtherPhone1Description;
                    location.OtherPhone2 = objLocation.OtherPhone2;
                    location.OtherPhone2Description = objLocation.OtherPhone2Description;
                    location.AuctionPayOverrideInd = objLocation.AuctionPayOverrideInd;
                    location.AuctionPayRate = objLocation.AuctionPayRate;
                    location.FlatDeliveryPayInd = objLocation.FlatDeliveryPayInd;
                    location.FlatDeliveryPayRate = objLocation.FlatDeliveryPayRate;
                    location.MileagePayBoostOverrideInd = objLocation.MileagePayBoostOverrideInd;
                    location.MileagePayBoost = objLocation.MileagePayBoost;
                    location.DeliveryTimes = objLocation.DeliveryTimes;
                    location.LocationNotes = objLocation.LocationNotes;
                    location.DriverDirections = objLocation.DriverDirections;
                    location.SortOrder = objLocation.SortOrder;
                    location.AlwaysShowInWIPInd = objLocation.AlwaysShowInWIPInd;
                    location.RecordStatus = objLocation.RecordStatus;
                    location.SPLCCode = objLocation.SPLCCode;
                    location.DeliveryHoldInd = objLocation.DeliveryHoldInd;
                    location.DeliveryHoldDate = objLocation.DeliveryHoldDate;
                    location.DeliveryHoldBy = objLocation.DeliveryHoldBy;
                    location.DeliveryHoldReason = objLocation.DeliveryHoldReason;
                    location.NightDropAllowedInd = objLocation.NightDropAllowedInd;
                    location.STIAllowedInd = objLocation.STIAllowedInd;
                    location.AssignedDealerInd = objLocation.AssignedDealerInd;
                    location.ShagPayAllowedInd = objLocation.ShagPayAllowedInd;
                    location.ShortHaulPaySchedule = objLocation.ShortHaulPaySchedule;
                    location.NYBridgeAdditiveEligibleInd = objLocation.NYBridgeAdditiveEligibleInd;
                    location.ShowInDriverQueueInd = objLocation.ShowInDriverQueueInd;
                    location.LocationMessage = objLocation.LocationMessage;
                    location.HotDealerInd = objLocation.HotDealerInd;
                    location.DisableLoadBuildingInd = objLocation.DisableLoadBuildingInd;
                    location.LocationHasInspectorsInd = objLocation.LocationHasInspectorsInd;
                    location.SendDriversEmailInd = objLocation.SendDriversEmailInd;
                    location.NotesInd = objLocation.NotesInd;
                    location.DirectionsInd = objLocation.DirectionsInd;
                    location.HoursInd = objLocation.HoursInd;
                    location.AddressInd = objLocation.AddressInd;
                    location.DeliveryHoldEndDate = objLocation.DeliveryHoldEndDate;
                    location.EnforceLoadBoardRulesInd = objLocation.EnforceLoadBoardRulesInd;
                    location.CreationDate = objLocation.CreationDate;
                    location.CreatedBy = objLocation.CreatedBy;
                    location.UpdatedDate = objLocation.UpdatedDate;
                    location.UpdatedBy = objLocation.UpdatedBy;

                    objAppWorksEntities.Locations.Add(location);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    locationID = location.LocationID;
                }
                return locationID;
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
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    CustomerNote customerNotes = new CustomerNote();
                    customerNotes.Note = objCustomerNotes.Note;
                    customerNotes.CreatedBy = objCustomerNotes.CreatedBy;
                    customerNotes.CreationDate = objCustomerNotes.CreationDate;

                    objAppWorksEntities.CustomerNotes.Add(customerNotes);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;

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
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    CustomerNote customerNotes = new CustomerNote();
                    customerNotes.Note = objCustomerNotes.Note;
                    customerNotes.CreatedBy = objCustomerNotes.CreatedBy;
                    customerNotes.CreationDate = objCustomerNotes.CreationDate;

                    objAppWorksEntities.SaveChanges();          /// Update records in DB..
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        /// For updating other information of customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateCustomerSearchDetails(CustomerSearchProp objcustomer)
        {

            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    Customer customer = objAppWorksEntities.Customers.Where(x => x.CustomerID == objcustomer.CustomerID).FirstOrDefault();

                    if (customer != null)
                    {
                        customer.ShortName = objcustomer.ShortName;
                        customer.CustomerName = objcustomer.CustomerName;
                        customer.DBAName = objcustomer.DBAName;
                        customer.CustomerType = objcustomer.CustomerType;
                        //OtherInformations
                        customer.VendorNumber = objcustomer.VendorNumber;
                        customer.DefaultBillingMethod = objcustomer.DefaultBillingMethod;
                        customer.RecordStatus = objcustomer.RecordStatus;
                        customer.SortOrder = objcustomer.SortOrder;
                        customer.EligibleForAutoLoadConfigInd = objcustomer.EligibleForAutoLoad;
                        customer.AlwaysShowInWIPInd = objcustomer.ALwaysVVIPInd;
                        customer.CollectionsIssueInd = objcustomer.CollectionIssueInd;
                        customer.AssignedToExternalCollectionsInd = objcustomer.AssignedToExternalCollectionsInd;
                        customer.BulkBillingInd = objcustomer.BulkBillingInd;
                        customer.DoNotPrintInvoiceInd = objcustomer.DoNotPrintInvoiceInd;
                        customer.DoNotExportInvoiceInfoInd = objcustomer.DoNotExportInvoiceInd;
                        customer.HideNewFreightFromBrokersInd = objcustomer.HideNewFreightFromBrokerInd;
                        customer.PortStorageCustomerInd = objcustomer.PortStorageCustomerInd;
                        customer.AutoportExportCustomerInd = objcustomer.AutoPortExportCustomerInd;
                        customer.RequiresPONumberInd = objcustomer.RequiresPONumberInd;
                        customer.SendEmailConfirmationsInd = objcustomer.SendEmailConfirmationsInd;
                        customer.STIFollowupRequiredInd = objcustomer.STIFollowupRequiredInd;
                        customer.DamagePhotoRequiredInd = objcustomer.DamagePhotoRequiredInd;
                        customer.ApplyFuelSurchargeInd = objcustomer.ApplyFuelSurchargeInd;
                        customer.FuelSurchargePercent = Convert.ToDecimal(objcustomer.FuelSurchargePercent);
                        customer.LoadNumberPrefix = objcustomer.LoadNumberPrefix;
                        customer.LoadNumberLength = objcustomer.LoadNumberLength;
                        customer.NextLoadNumber = objcustomer.NextLoadNUmber;
                        customer.BookingNumberPrefix = objcustomer.BookingNumberPrefix;
                        customer.HandheldScannerCustomerCode = objcustomer.HandHeldScannerCustomerCode;
                        customer.DriverComment = objcustomer.DriverComment;
                        customer.InternalComment = objcustomer.InternalComment;
                        customer.UpdatedDate = objcustomer.UpdatedDate;
                        customer.UpdatedBy = objcustomer.UpdatedBy;

                        objAppWorksEntities.SaveChanges();          /// Update  Chenges in Table.
                        IsSuccessfull = true;
                    }
                }
                return IsSuccessfull;
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
        /// For Updating location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateLocation(LocationList objLocation)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    Location location = (from qry in objAppWorksEntities.Locations where qry.LocationID == objLocation.LocationID select qry).FirstOrDefault();// Check for VIN is exists or not in vechile table
                    if (location != null)
                    {
                        location.LocationType = objLocation.LocationType;
                        location.LocationSubType = objLocation.LocationSubType;
                        location.LocationName = objLocation.LocationName;
                        location.LocationShortName = objLocation.LocationShortName;
                        location.CustomerRegionCode = objLocation.CustomerRegionCode;
                        location.CustomerLocationCode = objLocation.CustomerLocationCode;
                        location.AddressLine1 = objLocation.AddressLine1;
                        location.AddressLine2 = objLocation.AddressLine2;
                        location.State = objLocation.State;
                        location.City = objLocation.City;
                        location.Zip = objLocation.Zip;
                        location.Country = objLocation.Country;
                        location.MainPhone = objLocation.MainPhone;
                        location.FaxNumber = objLocation.FaxNumber;
                        location.PrimaryContactFirstName = objLocation.PrimaryContactFirstName;
                        location.PrimaryContactLastName = objLocation.PrimaryContactLastName;
                        location.PrimaryContactCellPhone = objLocation.PrimaryContactCellPhone;
                        location.PrimaryContactEmail = objLocation.PrimaryContactEmail;
                        location.PrimaryContactExtension = objLocation.PrimaryContactExtension;
                        location.PrimaryContactPhone = objLocation.PrimaryContactPhone;
                        location.AlternateContactFirstName = objLocation.AlternateContactFirstName;
                        location.AlternateContactLastName = objLocation.AlternateContactLastName;
                        location.AlternateContactCellPhone = objLocation.AlternateContactCellPhone;
                        location.AlternateContactEmail = objLocation.AlternateContactEmail;
                        location.AlternateContactExtension = objLocation.AlternateContactExtension;
                        location.AlternateContactPhone = objLocation.AlternateContactPhone;
                        location.OtherPhone1 = objLocation.OtherPhone1;
                        location.OtherPhone1Description = objLocation.OtherPhone1Description;
                        location.OtherPhone2 = objLocation.OtherPhone2;
                        location.OtherPhone2Description = objLocation.OtherPhone2Description;
                        location.AuctionPayOverrideInd = objLocation.AuctionPayOverrideInd;
                        location.AuctionPayRate = objLocation.AuctionPayRate;
                        location.FlatDeliveryPayInd = objLocation.FlatDeliveryPayInd;
                        location.FlatDeliveryPayRate = objLocation.FlatDeliveryPayRate;
                        location.MileagePayBoostOverrideInd = objLocation.MileagePayBoostOverrideInd;
                        location.MileagePayBoost = objLocation.MileagePayBoost;
                        location.DeliveryTimes = objLocation.DeliveryTimes;
                        location.LocationNotes = objLocation.LocationNotes;
                        location.DriverDirections = objLocation.DriverDirections;
                        location.SortOrder = objLocation.SortOrder;
                        location.AlwaysShowInWIPInd = objLocation.AlwaysShowInWIPInd;
                        location.RecordStatus = objLocation.RecordStatus;
                        location.SPLCCode = objLocation.SPLCCode;
                        location.DeliveryHoldInd = objLocation.DeliveryHoldInd;
                        location.DeliveryHoldDate = objLocation.DeliveryHoldDate;
                        location.DeliveryHoldBy = objLocation.DeliveryHoldBy;
                        location.DeliveryHoldReason = objLocation.DeliveryHoldReason;
                        location.NightDropAllowedInd = objLocation.NightDropAllowedInd;
                        location.STIAllowedInd = objLocation.STIAllowedInd;
                        location.AssignedDealerInd = objLocation.AssignedDealerInd;
                        location.ShagPayAllowedInd = objLocation.ShagPayAllowedInd;
                        location.ShortHaulPaySchedule = objLocation.ShortHaulPaySchedule;
                        location.NYBridgeAdditiveEligibleInd = objLocation.NYBridgeAdditiveEligibleInd;
                        location.ShowInDriverQueueInd = objLocation.ShowInDriverQueueInd;
                        location.LocationMessage = objLocation.LocationMessage;
                        location.HotDealerInd = objLocation.HotDealerInd;
                        location.DisableLoadBuildingInd = objLocation.DisableLoadBuildingInd;
                        location.LocationHasInspectorsInd = objLocation.LocationHasInspectorsInd;
                        location.SendDriversEmailInd = objLocation.SendDriversEmailInd;
                        location.NotesInd = objLocation.NotesInd;
                        location.DirectionsInd = objLocation.DirectionsInd;
                        location.HoursInd = objLocation.HoursInd;
                        location.AddressInd = objLocation.AddressInd;
                        location.DeliveryHoldEndDate = objLocation.DeliveryHoldEndDate;
                        location.EnforceLoadBoardRulesInd = objLocation.EnforceLoadBoardRulesInd;
                        //location.CreationDate = objLocation.CreationDate;
                        //location.CreatedBy = objLocation.CreatedBy;
                        location.UpdatedDate = objLocation.UpdatedDate;
                        location.UpdatedBy = objLocation.UpdatedBy;

                        objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                        IsSuccessfull = true;
                    }
                }
                return IsSuccessfull;
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
        /// For inserting Customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public int InsertCustomer(CustomerSearchProp objCustomer)
        {

            try
            {
                //bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                int customerID = 0;
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    Customer customer = new Customer();

                    customer.CustomerCode = objCustomer.CustomerCode;
                    customer.CustomerName = objCustomer.CustomerName;
                    customer.ShortName = objCustomer.ShortName;
                    customer.CustomerType = objCustomer.CustomerType;
                    customer.DBAName = objCustomer.DBAName;
                    customer.CreationDate = objCustomer.CreatedDate;
                    customer.CreatedBy = objCustomer.CreatedBy;

                    objAppWorksEntities.Customers.Add(customer);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    customerID = customer.CustomerID;
                }
                return customerID;
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
        ///  This Method to deete the location details
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteLocation(int locationID)
        {

            try
            {
                bool result = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var locationData = objAppWorksEntities.Locations.Where(v => v.LocationID == locationID).FirstOrDefault();
                    if (locationData != null)
                    {
                        objAppWorksEntities.Locations.Remove(locationData);
                        objAppWorksEntities.SaveChanges();
                        result = true;
                    }
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

        /// <summary>
        /// For Updating customer address in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateCustomerAddredss(string addressType, int customerID, int locationID)
        {
            try
            {
                bool IsSuccessfull = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    Customer customer = objAppWorksEntities.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();
                    if (customer != null)
                    {
                        if (addressType.ToUpper() == "BILLINGADDRESS")
                            customer.BillingAddressID = locationID;
                        else if (addressType.ToUpper() == "MAINADDRESS")
                            customer.MainAddressID = locationID;
                        //else if (addressType.ToUpper() == "ALL")
                        //{
                        //    customer.BillingAddressID = locationID;
                        //    customer.MainAddressID = locationID;
                        //}
                        objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                        IsSuccessfull = true;
                    }

                }
                return IsSuccessfull;
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
        /// For getting address type of customer from the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public string GetCustomerAddredssType(int locationID, int customerID)
        {

            try
            {
                string addressType = string.Empty;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    Customer customer = objAppWorksEntities.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();
                    if (customer != null)
                    {
                        if (customer.BillingAddressID == locationID && customer.MainAddressID == locationID)
                            addressType = "ALL";
                        else if (customer.BillingAddressID == locationID)
                            addressType = "BillingAddress";
                        else if (customer.MainAddressID == locationID)
                            addressType = "MainAddress";
                    }
                    return addressType;
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

    public static class getDistinctID
    {

        public static IEnumerable<TSource> DistinctByID<TSource, TKey>
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

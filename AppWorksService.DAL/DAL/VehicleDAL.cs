using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Transactions;
using System.Data.Entity;
using AppWorksService.DAL.Edmx;
using AppWorks.Utilities;
using System.Threading.Tasks;

namespace AppWorksService.DAL
{
    public class VehicleDAL
    {
        /// <summary>
        /// To find the vehicle search details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-2,2016</createdOn>
        public async Task<List<VehicleProp>> GetVehicleSearchDetails(VehicleProp objVehicleProp)
        {

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    objAppWorksEntities.Database.CommandTimeout = 300;
                    // Then split the lines at "\r\n".   
                    string[] stringSeparators = new string[] { "\r\n" };

                    string[] arrVIN = objVehicleProp.Vin.ToString().Split(stringSeparators, StringSplitOptions.None);

                    var query = (from qry in objAppWorksEntities.PortStorageVehicles
                                 join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                     into joincust
                                 from cust in joincust.DefaultIfEmpty()
                                 join bill in objAppWorksEntities.Billings on qry.BillingID equals bill.BillingID
                                 into joinbill
                                 from bill in joinbill.DefaultIfEmpty()

                                 join defaultrates in objAppWorksEntities.PortStorageRates.Where(x => x.StartDate != null && x.EndDate == null) on qry.CustomerID equals defaultrates.CustomerID
                                 into joinrates
                                 from rates in joinrates.DefaultIfEmpty()

                                 select new VehicleProp
                                 {
                                     CustomerID = (int)qry.CustomerID,
                                     VehiclesID = qry.PortStorageVehiclesID,
                                     Vin = qry.VIN,
                                     Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     MakeModel = qry.Make + " " + qry.Model,
                                     Make = qry.Make,
                                     Model = qry.Model,
                                     BayLocation = qry.BayLocation,
                                     DateIn = qry.DateIn,
                                     DateRequested = qry.DateRequested,
                                     DateOut = qry.DateOut,
                                     VehicleStatus = qry.VehicleStatus,
                                     LastPhysicalDate = qry.LastPhysicalDate,
                                     CreationDate = qry.CreationDate,
                                     UpdatedDate = qry.UpdatedDate,
                                     EstimatedPickupDate = qry.EstimatedPickupDate,
                                     CreatedBy = qry.CreatedBy,
                                     UpdatedBy = qry.UpdatedBy,
                                     Year = qry.VehicleYear,
                                     InBetwDtFrm = bill.InvoiceDate,
                                     CustomerNumber = !String.IsNullOrEmpty(cust.CustomerCode) ? cust.CustomerCode : String.Empty,
                                     CustomerName = cust.CustomerName,
                                     DtRequestBetwDtFrm = qry.DateRequested,
                                     InvoiceNumber = bill.InvoiceNumber,
                                     CustomerIdentification = qry.CustomerIdentification,
                                     EntryRate = qry.EntryRate,
                                     EntryRateOverrideInd = (int)qry.EntryRateOverrideInd,
                                     PerDiemGraceDaysOverrideInd = (int)qry.PerDiemGraceDaysOverrideInd,
                                     PerDiemGraceDays = qry.PerDiemGraceDays,
                                     TotalCharge = qry.TotalCharge,
                                     DefaultEntryRate = rates.EntryFee,
                                     DefaultPerDiem = rates.PerDiem,
                                     DefaultPerDiemGraceDays = rates.PerDiemGraceDays
                                 }).Where(x => (objVehicleProp.VehicleStatus == null || x.VehicleStatus == objVehicleProp.VehicleStatus)).AsQueryable();

                    if ((objVehicleProp.Vin != null) && (objVehicleProp.Vin.Length > 0))
                    {
                        query = query.Where(cnd => arrVIN.Any(t => cnd.Vin.Contains(t)));
                    }
                    if ((objVehicleProp.Year != null) && (objVehicleProp.Year.Length > 0))
                    {
                        query = query.Where(cnd => cnd.Year.Equals(objVehicleProp.Year));
                    }
                    if ((objVehicleProp.MakeModel != null) && (objVehicleProp.MakeModel.Length > 0))//need to check
                    {
                        query = query.Where(cnd => cnd.MakeModel.Contains(objVehicleProp.MakeModel));
                    }
                    if ((!String.IsNullOrWhiteSpace(objVehicleProp.CustomerNumber)) && (objVehicleProp.CustomerNumber.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerNumber.Contains(objVehicleProp.CustomerNumber.Trim()));
                    }
                    if ((objVehicleProp.BayLocation != null) && (objVehicleProp.BayLocation.Length > 0))
                    {
                        query = query.Where(cnd => cnd.BayLocation.Contains(objVehicleProp.BayLocation));
                    }
                    if ((objVehicleProp.CustomerName != null) && (objVehicleProp.CustomerName.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerName.Contains(objVehicleProp.CustomerName));
                    }
                    if ((objVehicleProp.InBetwDtFrm != null) && (objVehicleProp.InBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => cnd.InBetwDtFrm >= objVehicleProp.InBetwDtFrm && cnd.InBetwDtFrm <= objVehicleProp.InBetwDtTo);
                    }
                    else
                    {
                        if ((objVehicleProp.InBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => cnd.InBetwDtFrm >= objVehicleProp.InBetwDtFrm);
                        }
                        if ((objVehicleProp.InBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => cnd.InBetwDtFrm <= objVehicleProp.InBetwDtTo);
                        }
                    }

                    if ((objVehicleProp.DtInBetwDtFrm != null) && (objVehicleProp.DtInBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => DbFunctions.TruncateTime(cnd.DateIn) >= objVehicleProp.DtInBetwDtFrm && DbFunctions.TruncateTime(cnd.DateIn) <= objVehicleProp.DtInBetwDtTo);
                    }
                    else
                    {
                        if ((objVehicleProp.DtInBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => DbFunctions.TruncateTime(cnd.DateIn) >= objVehicleProp.DtInBetwDtFrm);
                        }
                        if ((objVehicleProp.DtInBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => DbFunctions.TruncateTime(cnd.DateIn) <= objVehicleProp.DtInBetwDtTo);
                        }
                    }
                    if ((objVehicleProp.InvoiceNumber != null) && (objVehicleProp.InvoiceNumber.Length > 0))
                    {
                        query = query.Where(cnd => cnd.InvoiceNumber.Contains(objVehicleProp.InvoiceNumber));
                    }
                    if ((objVehicleProp.DtRequestBetwDtFrm != null) && (objVehicleProp.DtRequestBetwDtTo != null))// done
                    {
                        query = query.Where(cnd => cnd.DtRequestBetwDtFrm >= objVehicleProp.DtRequestBetwDtFrm && cnd.DtRequestBetwDtFrm <= objVehicleProp.DtRequestBetwDtTo);
                    }
                    else
                    {
                        if ((objVehicleProp.DtRequestBetwDtFrm != null))// done
                        {
                            query = query.Where(cnd => cnd.DtRequestBetwDtFrm >= objVehicleProp.DtRequestBetwDtFrm);
                        }
                        if ((objVehicleProp.DtRequestBetwDtTo != null))// done
                        {
                            query = query.Where(cnd => cnd.DtRequestBetwDtFrm <= objVehicleProp.DtRequestBetwDtTo);
                        }
                    }
                    if ((objVehicleProp.CustomerIdentification != null) && (objVehicleProp.CustomerIdentification.Length > 0))
                    {
                        query = query.Where(cnd => cnd.CustomerIdentification.Contains(objVehicleProp.CustomerIdentification));
                    }
                    if ((objVehicleProp.DtOutBetwDtFrm != null) && (objVehicleProp.DtOutBetwDtTo != null))//done
                    {
                        query = query.Where(cnd => cnd.DateOut >= objVehicleProp.DtOutBetwDtFrm && cnd.DateOut <= objVehicleProp.DtOutBetwDtTo);
                    }
                    else
                    {
                        if ((objVehicleProp.DtOutBetwDtFrm != null))//done
                        {
                            query = query.Where(cnd => cnd.DateOut >= objVehicleProp.DtOutBetwDtFrm);
                        }
                        if ((objVehicleProp.DtOutBetwDtTo != null))//done
                        {
                            query = query.Where(cnd => cnd.DateOut <= objVehicleProp.DtOutBetwDtTo);
                        }
                    }
                    if ((objVehicleProp.Make != null) && (objVehicleProp.Make.Length > 0))//done
                    {
                        query = query.Where(cnd => cnd.Make.Contains(objVehicleProp.Make));
                    }
                    if ((objVehicleProp.Model != null) && (objVehicleProp.Model.Length > 0))//done
                    {
                        query = query.Where(cnd => cnd.Model.Contains(objVehicleProp.Model));
                    }


                    switch (objVehicleProp.SortColumn)
                    {
                        case VehicleLocatorGridColumns.MakeModel:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.MakeModel) : query.OrderByDescending(x => x.MakeModel);
                            break;
                        case VehicleLocatorGridColumns.Location:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.BayLocation) : query.OrderByDescending(x => x.BayLocation);
                            break;
                        case VehicleLocatorGridColumns.CustomerName:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.CustomerName) : query.OrderByDescending(x => x.CustomerName);
                            break;
                        case VehicleLocatorGridColumns.DateIn:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.DateIn) : query.OrderByDescending(x => x.DateIn);
                            break;
                        case VehicleLocatorGridColumns.DateOut:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.DateOut) : query.OrderByDescending(x => x.DateOut);
                            break;
                        case VehicleLocatorGridColumns.Requested:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.DateRequested) : query.OrderByDescending(x => x.DateRequested);
                            break;
                        case VehicleLocatorGridColumns.VIN:
                            query = objVehicleProp.SortOrder.Equals(SortOrder.ASC) ? query.OrderBy(x => x.Vin) : query.OrderByDescending(x => x.Vin);
                            break;
                        default:
                            query = query.OrderBy(x => x.Vin);
                            break;
                    }

                    var pageCount = query.Count();
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "getting Count {2} function ::{0} {1}.", DateTime.Now, DateTime.Now, MethodBase.GetCurrentMethod().Name));

                    if (objVehicleProp.CurrentPageIndex == 0 && objVehicleProp.PageSize > 0)
                    {
                        var queryResult = await Task.Run(() => query.Skip(objVehicleProp.CurrentPageIndex * objVehicleProp.PageSize).Take(objVehicleProp.PageSize).ToList());
                        if (queryResult != null && queryResult.Count() > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Return 1 {2} function ::{0} {1}.", DateTime.Now, DateTime.Now, MethodBase.GetCurrentMethod().Name));
                        return queryResult;// .ToList();
                    }
                    else if (objVehicleProp.PageSize > 0)
                    {
                        var queryResult = query.Skip((objVehicleProp.CurrentPageIndex * objVehicleProp.PageSize) + (objVehicleProp.DefaultPageSize - objVehicleProp.PageSize)).Take(objVehicleProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count() > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Return 2 {2} function ::{0} {1}.", DateTime.Now, DateTime.Now, MethodBase.GetCurrentMethod().Name));
                        return await Task.Run(() => queryResult);//.ToList();
                    }
                    else
                    {
                        return await Task.Run(() => query.ToList());
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
        /// To insert the new vehicle details
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-04,2016</createdOn>
        public int InsertVehicleDetails(VehicleProp objVehicleProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                int getVehicleId;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    PortStorageVehicle portStorageVehicle = new PortStorageVehicle();

                    //objTblVehicle.CustomerID = (int)objVehicleProp.CustomerID;
                    //objTblVehicle.VehicleYear = objVehicleProp.Year;
                    //objTblVehicle.Make = objVehicleProp.Make;
                    //objTblVehicle.Model = objVehicleProp.Model;
                    //objTblVehicle.Bodystyle = objVehicleProp.Bodystyle;
                    //objTblVehicle.VIN = objVehicleProp.Vin;
                    //objTblVehicle.Color = objVehicleProp.Color;
                    //objTblVehicle.VehicleLength = objVehicleProp.VehicleLength;
                    //objTblVehicle.VehicleWidth = objVehicleProp.VehicleWidth;
                    //objTblVehicle.VehicleHeight = objVehicleProp.VehicleHeight;
                    //objTblVehicle.VehicleStatus = objVehicleProp.VehicleStatus;
                    //objTblVehicle.CustomerIdentification = objVehicleProp.CustomerIdentification;
                    //objTblVehicle.SizeClass = objVehicleProp.SizeClass;
                    //objTblVehicle.BayLocation = objVehicleProp.BayLocation;
                    //objTblVehicle.EntryRate = (decimal)objVehicleProp.EntryRate;
                    //objTblVehicle.EntryRateOverrideInd = (int)objVehicleProp.EntryRateOverrideInd;
                    //objTblVehicle.PerDiemGraceDays = (int)objVehicleProp.PerDiemGraceDays;
                    //objTblVehicle.PerDiemGraceDaysOverrideInd = (int)objVehicleProp.PerDiemGraceDaysOverrideInd;
                    //objTblVehicle.TotalCharge = (int)objVehicleProp.TotalCharge;
                    //objTblVehicle.DateIn = objVehicleProp.DateIn;
                    //objTblVehicle.DateRequested = objVehicleProp.DateRequested;
                    //objTblVehicle.DateOut = objVehicleProp.DateOut;
                    //objTblVehicle.BilledInd = (int)objVehicleProp.BilledInd;
                    //objTblVehicle.BillingID = (int)objVehicleProp.BillingID;
                    //objTblVehicle.DateBilled = (DateTime)objVehicleProp.DateBilled;
                    //objTblVehicle.VINDecodedInd = (int)objVehicleProp.VinDecodedInd;
                    //objTblVehicle.Note = objVehicleProp.Note;
                    //objTblVehicle.RecordStatus = objVehicleProp.RecordStatus;
                    //objTblVehicle.CreationDate = (DateTime)objVehicleProp.CreationDate;
                    //objTblVehicle.CreatedBy = objVehicleProp.CreatedBy;
                    //objTblVehicle.UpdatedDate = (DateTime)objVehicleProp.UpdatedDate;
                    //objTblVehicle.UpdatedBy = objVehicleProp.UpdatedBy;
                    //objTblVehicle.CreditHoldInd = (int)objVehicleProp.CreditHoldInd;
                    //objTblVehicle.CreditHoldBy = objVehicleProp.CreditHoldBy;
                    //objTblVehicle.RequestPrintedInd = (int)objVehicleProp.RequestPrintedInd;
                    //objTblVehicle.EstimatedPickupDate = (DateTime)objVehicleProp.EstimatedPickupDate;
                    //objTblVehicle.DealerPrintDate = (DateTime)objVehicleProp.DealerPrintDate;
                    //objTblVehicle.DealerPrintBy = objVehicleProp.DealerPrintBy;
                    //objTblVehicle.RequestedBy = objVehicleProp.RequestedBy;
                    //objTblVehicle.RequestPrintedBatchID = (int)objVehicleProp.RequestPrintedBatchID;
                    //objTblVehicle.DateRequestPrinted = (DateTime)objVehicleProp.DateRequestPrinted;
                    //objTblVehicle.LastPhysicalDate = (DateTime)objVehicleProp.LastPhysicalDate;

                    //objAppWorksEntities.PortStorageVehicles.Add(objTblVehicle);  /// Insert the Record in Respected Table.
                    //objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    //getVehicleId = objTblVehicle.PortStorageVehiclesID;             /// Return the UserId of Inserted User.
                    portStorageVehicle.VIN = objVehicleProp.Vin;
                    portStorageVehicle.Note = objVehicleProp.Note;
                    portStorageVehicle.CustomerID = objVehicleProp.CustomerID;
                    portStorageVehicle.VehicleYear = objVehicleProp.Year;
                    portStorageVehicle.Make = objVehicleProp.Make;
                    portStorageVehicle.Model = objVehicleProp.Model;
                    portStorageVehicle.Bodystyle = objVehicleProp.Bodystyle;
                    portStorageVehicle.VehicleLength = objVehicleProp.VehicleLength;
                    portStorageVehicle.Color = objVehicleProp.Color;
                    portStorageVehicle.VehicleWidth = objVehicleProp.VehicleWidth;
                    portStorageVehicle.VehicleHeight = objVehicleProp.VehicleHeight;
                    portStorageVehicle.VehicleStatus = objVehicleProp.VehicleStatus;
                    portStorageVehicle.CustomerIdentification = objVehicleProp.CustomerIdentification;
                    portStorageVehicle.SizeClass = objVehicleProp.SizeClass;
                    portStorageVehicle.BayLocation = objVehicleProp.BayLocation;
                    portStorageVehicle.EntryRate = objVehicleProp.EntryRate;
                    portStorageVehicle.EntryRateOverrideInd = objVehicleProp.EntryRateOverrideInd;
                    portStorageVehicle.PerDiemGraceDays = objVehicleProp.PerDiemGraceDays;
                    portStorageVehicle.PerDiemGraceDaysOverrideInd = objVehicleProp.PerDiemGraceDaysOverrideInd;
                    portStorageVehicle.TotalCharge = objVehicleProp.TotalCharge;
                    portStorageVehicle.DateIn = objVehicleProp.DateIn; ;
                    portStorageVehicle.DateRequested = objVehicleProp.DateRequested;
                    portStorageVehicle.DateOut = objVehicleProp.DateOut;
                    portStorageVehicle.BilledInd = objVehicleProp.BilledInd;
                    portStorageVehicle.BillingID = objVehicleProp.BillingID;
                    portStorageVehicle.VINDecodedInd = objVehicleProp.VinDecodedInd;
                    portStorageVehicle.RecordStatus = objVehicleProp.RecordStatus;
                    portStorageVehicle.CreationDate = objVehicleProp.CreationDate;
                    portStorageVehicle.CreatedBy = objVehicleProp.CreatedBy;
                    portStorageVehicle.CreditHoldInd = objVehicleProp.CreditHoldInd;
                    portStorageVehicle.CreditHoldBy = objVehicleProp.CreditHoldBy;
                    portStorageVehicle.RequestPrintedInd = objVehicleProp.RequestPrintedInd;
                    portStorageVehicle.EstimatedPickupDate = objVehicleProp.EstimatedPickupDate;
                    portStorageVehicle.DealerPrintDate = objVehicleProp.DealerPrintDate;
                    portStorageVehicle.DealerPrintBy = objVehicleProp.DealerPrintBy;
                    portStorageVehicle.RequestedBy = objVehicleProp.RequestedBy;
                    portStorageVehicle.RequestPrintedBatchID = objVehicleProp.RequestPrintedBatchID;
                    portStorageVehicle.DateRequestPrinted = objVehicleProp.DateRequestPrinted;
                    portStorageVehicle.LastPhysicalDate = objVehicleProp.LastPhysicalDate;
                    objAppWorksEntities.PortStorageVehicles.Add(portStorageVehicle);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    getVehicleId = portStorageVehicle.PortStorageVehiclesID;             /// Return the UserId of Inserted User.
                    if (getVehicleId > 0)
                    {
                        PortStorageVehiclesLocationHistory lochis = new PortStorageVehiclesLocationHistory();
                        lochis.PortStorageVehiclesID = getVehicleId;
                        lochis.BayLocation = objVehicleProp.BayLocation;
                        lochis.LocationDate = DateTime.Now;
                        lochis.CreationDate = DateTime.Now;
                        lochis.CreatedBy = objVehicleProp.CreatedBy;
                        lochis.UpdatedDate = DateTime.Now;
                        lochis.UpdatedBy = objVehicleProp.UpdatedBy;
                        objAppWorksEntities.PortStorageVehiclesLocationHistories.Add(lochis);
                        objAppWorksEntities.SaveChanges();
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
                return getVehicleId;
            }
        }


        /// <summary>
        /// This Method is used to Get Port Storage Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public List<VehicleProp> GetPortStorageProcessignDetails(string vehicleId)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    int portStorageVehicleId = int.Parse(vehicleId);

                    var query = (from qry in objAppWorksEntities.PortStorageVehicles
                                 join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                 into JoinedCust
                                 from cust in JoinedCust.DefaultIfEmpty()
                                 join loc in objAppWorksEntities.Locations on cust.BillingAddressID equals loc.LocationID
                                 into Joinedloc
                                 from loc in Joinedloc.DefaultIfEmpty()
                                 // where qry.VIN.Equals(VIN)
                                 where qry.PortStorageVehiclesID == portStorageVehicleId
                                 select new VehicleProp
                                 {
                                     VehiclesID = qry.PortStorageVehiclesID,
                                     Vin = qry.VIN,
                                     Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     DateIn = qry.DateIn,
                                     DateRequested = qry.DateRequested,
                                     Year = qry.VehicleYear,
                                     Make = qry.Make,
                                     MakeModel = qry.Model,
                                     BayLocation = qry.BayLocation,
                                     VehicleStatus = RequestedVehicleStatus.UpdatePending,//"Update Pending";
                                     Color = qry.Color,
                                     EstimatedPickupDate = qry.EstimatedPickupDate,
                                     DealerPrintDate = qry.DealerPrintDate,
                                     RequestedBy = qry.RequestedBy,
                                     AdddressLine1 = loc.AddressLine1,
                                     City = loc.City,
                                     State = loc.State,
                                     Zip = loc.Zip 
                                     

                                 }
                                     ).AsQueryable();


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
        }


        /// <summary>
        /// This method is used to update Port Storage Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public int UpdatePortStorageProcessignDetails(VehicleProp objVehicleProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                int getVehicleId = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var result = (from qry in objAppWorksEntities.PortStorageVehicles where qry.PortStorageVehiclesID == objVehicleProp.VehiclesID select qry.VIN).ToList();// Check for VIN is exists or not in vechile table
                    if (result.Count == 1)
                    {
                        //var tempVIN = (from qry in objAppWorksEntities.PortStorageVehicles where qry.VIN.EndsWith(objVehicleProp.Vin) select qry).FirstOrDefault();
                        PortStorageVehicle portStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(v => v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();
                        portStorageVehicle.Note = objVehicleProp.Note;
                        portStorageVehicle.UpdatedBy = objVehicleProp.UpdatedBy;
                        portStorageVehicle.UpdatedDate = objVehicleProp.UpdatedDate;
                        portStorageVehicle.EstimatedPickupDate = objVehicleProp.EstimatedPickupDate;
                        portStorageVehicle.DealerPrintDate = objVehicleProp.DealerPrintDate;
                        portStorageVehicle.RequestedBy = objVehicleProp.RequestedBy;
                        portStorageVehicle.PortStorageVehiclesID = objVehicleProp.VehiclesID;
                        objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
                        getVehicleId = portStorageVehicle.PortStorageVehiclesID;             /// Return the PortStorageVehiclesID of update PortStorageVehicle.
                        return getVehicleId;
                    }
                    else
                    {
                        return getVehicleId;
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
        /// This Method is used to Get Port Storage for Date Out Processign Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<VehicleProp> GetPortStorageDateOutProcessignDetails(string VIN)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.PortStorageVehicles
                                 join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                 into JoinedCust
                                 from cust in JoinedCust.DefaultIfEmpty()
                                 where qry.VIN.Contains(VIN)
                                 select new VehicleProp
                                 {
                                     Vin = qry.VIN,
                                     Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     DateIn = qry.DateIn,
                                     DateOut = qry.DateOut,
                                     MakeModel = qry.Model,
                                     VehiclesID = qry.PortStorageVehiclesID,
                                     BayLocation = qry.BayLocation,
                                     VehicleStatus = "Update Pending",
                                 }
                                     ).AsQueryable();
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
        }


        /// <summary>
        /// This method is used to Update Port Storage for date out Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int UpdatePortStorageDateOutProcessignDetails(VehicleProp objVehicleProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                int getVehicleId = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    var result = (from qry in objAppWorksEntities.PortStorageVehicles where qry.PortStorageVehiclesID == objVehicleProp.VehiclesID select qry.VIN).ToList();// Check for VIN is exists or not in vechile table
                    if (result.Count == 1)
                    {
                        //var tempVIN = (from qry in objAppWorksEntities.PortStorageVehicles where qry.PortStorageVehiclesID == objVehicleProp.VehiclesID select qry).FirstOrDefault();
                        PortStorageVehicle portStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(v => v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();
                        portStorageVehicle.Note = objVehicleProp.Note;
                        objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
                        getVehicleId = portStorageVehicle.PortStorageVehiclesID;             /// Return the PortStorageVehiclesID of update PortStorageVehicle.
                        return getVehicleId;
                    }
                    else
                    {
                        return getVehicleId;
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
        /// This method is used to update the Port Storage request Processing date
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>String</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public string RequestBatchProcess(VehicleProp objVehicleProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    string status = string.Empty;

                    var qry = objAppWorksEntities.PortStorageVehicles.Where(x => x.VIN == objVehicleProp.Vin && x.PortStorageVehiclesID == objVehicleProp.VehiclesID && x.DateRequested != null).FirstOrDefault();

                    if (qry == null)
                    {
                        var result = objAppWorksEntities.Database.SqlQuery<spUpdatePortStorageRequestDate_Result>("EXEC spUpdatePortStorageRequestDate {0},{1},{2},{3},{4}",
                        objVehicleProp.Vin,
                        objVehicleProp.DateRequested.HasValue ? objVehicleProp.DateRequested.Value : (DateTime?)null,
                        objVehicleProp.Note,
                        objVehicleProp.UpdatedBy,
                        objVehicleProp.ReturnToInventory
                        ).ToList<spUpdatePortStorageRequestDate_Result>();

                        PortStorageVehicle portStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(v => v.VIN == objVehicleProp.Vin && v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();

                        if (portStorageVehicle != null)
                        {
                            portStorageVehicle.DateRequested = objVehicleProp.DateRequested;
                            portStorageVehicle.RequestedBy = objVehicleProp.UpdatedBy;
                            objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
                        }

                        if (result != null)
                        {
                            foreach (var val in result)
                            {
                                if (val.RC != (int)Enums.ErrorCodes.VinNotFound)
                                { status = val.RM; }
                            }
                        }
                    }
                    else
                    {
                        if (objVehicleProp.DateRequested != null)
                        {
                            status = string.Format("Request Date Is Already {0}", qry.DateRequested.Value.Date.ToString("MM/dd/yyyy"));
                        }
                        else
                        {
                            using (PortStorageEntities objAppWorksEntities2 = new PortStorageEntities())
                            {
                                PortStorageVehicle portStorageVehicle = objAppWorksEntities2.PortStorageVehicles.Where(v => v.VIN == objVehicleProp.Vin && v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();
                                portStorageVehicle.DateRequested = objVehicleProp.DateRequested;
                                portStorageVehicle.VehicleStatus = "InInventory";
                                portStorageVehicle.UpdatedBy = objVehicleProp.UpdatedBy;
                                portStorageVehicle.UpdatedDate = DateTime.Now;
                                objAppWorksEntities2.SaveChanges();          /// Check the Chenges in Table After Record update.
                                status = RequestedVehicleStatus.Updated; //"Updated Successfully";
                            }
                        }
                    }
                    return status;
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
        /// This method is used to update the Port Storage Processign date out
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public string DateOutBatchProcess(VehicleProp objVehicleProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                //int result = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    string status = string.Empty;
                    /// Calling The Stored Procedure
                    var result = objAppWorksEntities.spUpdatePortStorageDateOut(objVehicleProp.Vin, objVehicleProp.DateOut, objVehicleProp.Note, objVehicleProp.UpdatedBy).FirstOrDefault();
                    // creating the object of PortStorageEntities Database
                    using (PortStorageEntities objAppWorksEntities1 = new PortStorageEntities())
                    {
                        PortStorageVehicle portStorageVehicle = objAppWorksEntities1.PortStorageVehicles.Where(v => v.VIN == objVehicleProp.Vin && v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();
                        if (result != null)
                        {
                            if (result.RC != (int)Enums.ErrorCodes.VinNotFound)
                                status = result.RM;

                            if (result.RC == 0)
                            {
                                portStorageVehicle.DateOut = objVehicleProp.DateOut;
                                objAppWorksEntities1.SaveChanges();          /// Check the Chenges in Table After Record update.
                            }
                        }
                        return status;
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
        /// This method is used to Decode The VIN.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int DecodeVIN(string Vin)
        {
            int value = 0;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    if (Vin.Length == 17)
                    {
                        var record = (from TblPortStorageVehical in objAppWorksEntities.PortStorageVehicles
                                      where SqlFunctions.PatIndex(Vin.ToString().Substring(1, 8) + "_" + Vin.ToString().Substring(10, 2), TblPortStorageVehical.VIN) > 0 && TblPortStorageVehical.VINDecodedInd == 1 && TblPortStorageVehical.VIN != Vin
                                      select new { TblPortStorageVehical.VehicleYear, TblPortStorageVehical.Make, TblPortStorageVehical.Model, TblPortStorageVehical.Bodystyle, TblPortStorageVehical.VehicleLength, TblPortStorageVehical.VehicleWidth, TblPortStorageVehical.VehicleHeight, TblPortStorageVehical.SizeClass, TblPortStorageVehical }).FirstOrDefault();

                        if (Convert.ToInt32(record) > 0)
                        {

                        }
                        else
                        {

                        }
                    }
                    return value;
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
        ///  This method is used to get vehicle detil by VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public VehicleProp GetVecheleDetailByVIN(string VIN,int? vehicleId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.PortStorageVehicles
                                 join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                 into JoinedCust
                                 from cust in JoinedCust.DefaultIfEmpty()
                                 where qry.VIN.ToUpper().Contains(VIN) && qry.DateOut != null 
                                 && (vehicleId==null || qry.PortStorageVehiclesID == vehicleId)
                                 select new VehicleProp
                                 {
                                     Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     DateIn = qry.DateIn,
                                     DateOut = qry.DateOut,
                                     DateRequested = qry.DateRequested,
                                     VehiclesID = qry.PortStorageVehiclesID

                                 }
                                 ).FirstOrDefault();

                    return query;

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
        ///  This method is used to get vehicle detil by VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<VehicleProp> CheckMultipleVecheleDetailByVIN(string VIN)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.PortStorageVehicles
                                 join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                 into JoinedCust
                                 from cust in JoinedCust.DefaultIfEmpty()
                                 where qry.VIN.ToUpper().Contains(VIN)
                                 //&& qry.DateOut != null
                                 select new VehicleProp
                                 {
                                     Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                     DateIn = qry.DateIn,
                                     DateOut = qry.DateOut,
                                     VehiclesID = qry.PortStorageVehiclesID
                                 }
                                 ).AsQueryable();

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

        }

        /// <summary>
        ///  This Method to use the Fill Vehical Status
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<string> VehicalStatusList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var list = (from Tbl in objAppWorksEntities.PortStorageVehicles orderby Tbl.VehicleStatus select Tbl.VehicleStatus).Distinct().ToList();
                    return list.ToList();
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
        ///  This Method to Call The Vehical Information For Fill the Vehical Details Form
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<VehicleProp> CallVehialDetailsbyVIN(VehicleProp objVehicleProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var list = (from qry in objAppWorksEntities.PortStorageVehicles
                                join cust in objAppWorksEntities.Customers on qry.CustomerID equals cust.CustomerID
                                     into joincust
                                from cust in joincust.DefaultIfEmpty()
                                join bill in objAppWorksEntities.Billings on qry.BillingID equals bill.BillingID
                                     into joinbill
                                from bill in joinbill.DefaultIfEmpty()

                                join rates in objAppWorksEntities.PortStorageRates.Where(x => x.StartDate != null && x.EndDate == null) on qry.CustomerID equals rates.CustomerID
                                     into joinrates
                                from rates in joinrates.DefaultIfEmpty()

                                join loc in objAppWorksEntities.Locations on cust.BillingAddressID equals loc.LocationID
                                into Joinedloc
                                from loc in Joinedloc.DefaultIfEmpty()

                                where qry.PortStorageVehiclesID == objVehicleProp.VehiclesID
                                select new VehicleProp
                                {
                                    CustomerID = (int)qry.CustomerID,
                                    VehiclesID = qry.PortStorageVehiclesID,
                                    Vin = qry.VIN,
                                    Name = !string.IsNullOrEmpty(cust.ShortName) ? cust.ShortName : cust.CustomerName,
                                    CustomerName = cust.CustomerName,
                                    CustomerNumber = cust.CustomerCode,
                                    Make = qry.Make,
                                    Model = qry.Model,
                                    BilledInd = qry.BilledInd == null ? 0 : (int)qry.BilledInd,
                                    BayLocation = qry.BayLocation,
                                    DateIn = qry.DateIn,
                                    DateRequested = qry.DateRequested,
                                    DateOut = qry.DateOut,
                                    VehicleStatus = qry.VehicleStatus,
                                    RecordStatus = qry.RecordStatus,
                                    Year = qry.VehicleYear,
                                    LastPhysicalDate = qry.LastPhysicalDate,
                                    CreationDate = qry.CreationDate,
                                    UpdatedDate = qry.UpdatedDate,
                                    EstimatedPickupDate = qry.EstimatedPickupDate,
                                    Color = qry.Color,
                                    Bodystyle = qry.Bodystyle,
                                    CustomerIdentification = qry.CustomerIdentification,
                                    RequestedBy = qry.RequestedBy,
                                    DealerPrintDate = qry.DealerPrintDate,
                                    Note = qry.Note,
                                    EntryRate = qry.EntryRate,
                                    EntryRateOverrideInd = (int)qry.EntryRateOverrideInd,
                                    PerDiemGraceDays = qry.PerDiemGraceDays,
                                    PerDiemGraceDaysOverrideInd = (int)qry.PerDiemGraceDaysOverrideInd,
                                    TotalCharge = qry.TotalCharge,
                                    DefaultEntryRate = rates.EntryFee,
                                    DefaultPerDiemGraceDays = rates.PerDiemGraceDays,
                                    DefaultPerDiem = rates.PerDiem,
                                    CreatedBy = qry.CreatedBy,
                                    UpdatedBy = qry.UpdatedBy,
                                    VinDecodedInd = qry.VINDecodedInd.HasValue ? qry.VINDecodedInd.Value : 0,
                                    InvoiceNumber = bill.InvoiceNumber,
                                    InvoiceDate = bill.InvoiceDate,
                                    AdddressLine1 = loc.AddressLine1,
                                    City = loc.City,
                                    State = loc.State,
                                    Zip = loc.Zip,
                                    RequestPrintedInd = (int)qry.RequestPrintedInd   // return existing RequestPrintedInd status 
                                }).AsEnumerable().ToList();
                    return list.ToList();
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
        ///  This Method to Update The Vehical Search Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool UpdateVehicalSearchDetails(VehicleProp objVehicleProp)
        {
            bool result = false;
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    //var qry = objAppWorksEntities.PortStorageVehicles.Select(x => x.VIN).Contains(objVehicleProp.Vin);// Check for VIN is exists or not in vechile table
                    //if (qry)
                    //{
                    if (objVehicleProp.VehiclesID > 0)
                    {
                        PortStorageVehicle portStorageVehicle = objAppWorksEntities.PortStorageVehicles.Where(v => v.PortStorageVehiclesID == objVehicleProp.VehiclesID).FirstOrDefault();
                        string prevBayLocation = portStorageVehicle.BayLocation;
                        portStorageVehicle.Note = objVehicleProp.Note;
                        portStorageVehicle.VIN = objVehicleProp.Vin;
                        portStorageVehicle.CustomerID = objVehicleProp.CustomerID;
                        portStorageVehicle.VehicleYear = objVehicleProp.Year;
                        portStorageVehicle.Make = objVehicleProp.Make;
                        portStorageVehicle.Model = objVehicleProp.Model;
                        portStorageVehicle.Bodystyle = objVehicleProp.Bodystyle;
                        portStorageVehicle.VehicleLength = objVehicleProp.VehicleLength;
                        portStorageVehicle.Color = objVehicleProp.Color;
                        portStorageVehicle.VehicleWidth = objVehicleProp.VehicleWidth;
                        portStorageVehicle.VehicleHeight = objVehicleProp.VehicleHeight;
                        portStorageVehicle.VehicleStatus = objVehicleProp.VehicleStatus;
                        portStorageVehicle.CustomerIdentification = objVehicleProp.CustomerIdentification;
                        portStorageVehicle.SizeClass = objVehicleProp.SizeClass;
                        portStorageVehicle.BayLocation = objVehicleProp.BayLocation;
                        portStorageVehicle.EntryRate = objVehicleProp.EntryRate;
                        portStorageVehicle.EntryRateOverrideInd = objVehicleProp.EntryRateOverrideInd;
                        portStorageVehicle.PerDiemGraceDays = objVehicleProp.PerDiemGraceDays;
                        portStorageVehicle.PerDiemGraceDaysOverrideInd = objVehicleProp.PerDiemGraceDaysOverrideInd;
                        portStorageVehicle.TotalCharge = objVehicleProp.TotalCharge;
                        portStorageVehicle.DateIn = objVehicleProp.DateIn; ;
                        portStorageVehicle.DateRequested = objVehicleProp.DateRequested;
                        portStorageVehicle.DateOut = objVehicleProp.DateOut;
                        portStorageVehicle.BilledInd = objVehicleProp.BilledInd;
                        portStorageVehicle.VINDecodedInd = objVehicleProp.VinDecodedInd;
                        portStorageVehicle.RecordStatus = objVehicleProp.RecordStatus;
                        portStorageVehicle.UpdatedDate = objVehicleProp.UpdatedDate;
                        portStorageVehicle.UpdatedBy = objVehicleProp.UpdatedBy;
                        portStorageVehicle.CreditHoldInd = objVehicleProp.CreditHoldInd;
                        portStorageVehicle.CreditHoldBy = objVehicleProp.CreditHoldBy;
                        portStorageVehicle.RequestPrintedInd = objVehicleProp.RequestPrintedInd;
                        portStorageVehicle.EstimatedPickupDate = objVehicleProp.EstimatedPickupDate;
                        portStorageVehicle.DealerPrintDate = objVehicleProp.DealerPrintDate;
                        portStorageVehicle.DealerPrintBy = objVehicleProp.DealerPrintBy;
                        portStorageVehicle.RequestedBy = objVehicleProp.RequestedBy;
                        portStorageVehicle.RequestPrintedBatchID = objVehicleProp.RequestPrintedBatchID;
                        portStorageVehicle.DateRequestPrinted = objVehicleProp.DateRequestPrinted;
                        if (prevBayLocation.ToUpper() != objVehicleProp.BayLocation.ToUpper())
                            portStorageVehicle.LastPhysicalDate = DateTime.Now;
                        objAppWorksEntities.SaveChanges();
                        result = true;
                        if (result)
                        {
                            var query = (from lochis in objAppWorksEntities.PortStorageVehiclesLocationHistories
                                         where lochis.PortStorageVehiclesID == objVehicleProp.VehiclesID
                                         select lochis).ToList().OrderByDescending(x => x.PortStorageVehiclesLocationHistoryID).FirstOrDefault();
                            if (query != null)
                            {
                                if ((query.BayLocation.ToString().Trim().ToLower()) != (objVehicleProp.BayLocation.ToString().Trim().ToLower()))
                                {
                                    PortStorageVehiclesLocationHistory lochis = new PortStorageVehiclesLocationHistory();
                                    lochis.PortStorageVehiclesID = objVehicleProp.VehiclesID;
                                    lochis.BayLocation = objVehicleProp.BayLocation;
                                    lochis.LocationDate = DateTime.Now;
                                    lochis.CreationDate = DateTime.Now;
                                    lochis.CreatedBy = objVehicleProp.CreatedBy;
                                    lochis.UpdatedDate = DateTime.Now;
                                    lochis.UpdatedBy = objVehicleProp.UpdatedBy;

                                    objAppWorksEntities.PortStorageVehiclesLocationHistories.Add(lochis);
                                    objAppWorksEntities.SaveChanges();
                                }
                            }
                            else
                            {
                                PortStorageVehiclesLocationHistory lochis = new PortStorageVehiclesLocationHistory();
                                lochis.PortStorageVehiclesID = objVehicleProp.VehiclesID;
                                lochis.BayLocation = objVehicleProp.BayLocation;
                                lochis.LocationDate = DateTime.Now;
                                lochis.CreationDate = DateTime.Now;
                                lochis.CreatedBy = objVehicleProp.CreatedBy;
                                lochis.UpdatedDate = DateTime.Now;
                                lochis.UpdatedBy = objVehicleProp.UpdatedBy;

                                objAppWorksEntities.PortStorageVehiclesLocationHistories.Add(lochis);
                                objAppWorksEntities.SaveChanges();
                            }
                        }
                    }
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
        ///  This Method to Update The Vehical Search Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool DeleteVehicalSearchDetails(int VehiclesID)
        {
            bool result = false;
            using (var transaction = new TransactionScope())
            {
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    // creating the object of PortStorageEntities Database
                    using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                    {
                        var VehicleData = objAppWorksEntities.PortStorageVehicles.Where(v => v.PortStorageVehiclesID == VehiclesID).FirstOrDefault();
                        if (VehicleData != null)
                        {
                            objAppWorksEntities.PortStorageVehicles.Remove(VehicleData);
                            objAppWorksEntities.SaveChanges();
                        }

                        var HistoryData = objAppWorksEntities.PortStorageVehiclesLocationHistories.Where(x => x.PortStorageVehiclesID == VehiclesID);
                        if (HistoryData != null)
                        {
                            foreach (PortStorageVehiclesLocationHistory psvls in HistoryData)
                            {
                                objAppWorksEntities.PortStorageVehiclesLocationHistories.Remove(psvls);
                            }
                            objAppWorksEntities.SaveChanges();
                        }

                        var DamageDetailData = objAppWorksEntities.PSVehicleDamageDetails.Where(x => x.PortStorageVehiclesID == VehiclesID);
                        if (DamageDetailData != null)
                        {
                            foreach (PSVehicleDamageDetail psvd in DamageDetailData)
                            {
                                objAppWorksEntities.PSVehicleDamageDetails.Remove(psvd);
                            }
                            objAppWorksEntities.SaveChanges();
                        }

                        var InspectionData = objAppWorksEntities.PSVehicleInspections.Where(x => x.PortStorageVehiclesID == VehiclesID).FirstOrDefault();
                        if (InspectionData != null)
                        {
                            objAppWorksEntities.PSVehicleInspections.Remove(InspectionData);
                            objAppWorksEntities.SaveChanges();
                        }

                        transaction.Complete();
                        result = true;
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



        }


        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool CheckMultipleVIN(string vIN)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var VehicleData = objAppWorksEntities.PortStorageVehicles.Where(v => v.VIN == vIN && (v.DateOut == null || (v.DateRequested != null && v.DateOut == null))).FirstOrDefault();
                    if (VehicleData != null)
                    {
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
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public PortStorageVehicleProp GetDecodedPortStorageVIN(string vin)
        {
            try
            {
                string decodedVIN = vin.Substring(0, 8) + "_" + vin.Substring(9, 2) + "%";
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var portstorage = (from portstoragevehicle in objAppWorksEntities.PortStorageVehicles
                                       where portstoragevehicle.VINDecodedInd == 1 &&
                                       portstoragevehicle.VIN != vin &&
                                       SqlFunctions.PatIndex(decodedVIN, portstoragevehicle.VIN) > 0
                                       //orderby portstoragevehicle.PortStorageVehiclesID
                                       select new PortStorageVehicleProp
                                       {
                                           VehicleYear = portstoragevehicle.VehicleYear,
                                           Make = portstoragevehicle.Make,
                                           Model = portstoragevehicle.Model,
                                           Bodystyle = portstoragevehicle.Bodystyle,
                                           VehicleLength = portstoragevehicle.VehicleLength,
                                           VehicleWidth = portstoragevehicle.VehicleWidth,
                                           VehicleHeight = portstoragevehicle.VehicleHeight,
                                           SizeClass = portstoragevehicle.SizeClass
                                       }).FirstOrDefault();
                    return portstorage;
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
        ///  This Method is for getting the settings for decode vin 
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<string> GetDecodeSettings()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstSettings = (from settings1 in objAppWorksEntities.SettingTables
                                       from setings2 in objAppWorksEntities.SettingTables
                                       from setings3 in objAppWorksEntities.SettingTables
                                       where settings1.ValueKey == "DataOneServerURL" && setings2.ValueKey == "DataOneClientName" && setings3.ValueKey == "DataOneAccessCode"
                                       select new List<string> { settings1.ValueDescription, setings2.ValueDescription, setings3.ValueDescription }
                                              ).FirstOrDefault();
                    return lstSettings;
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
        ///  This Method is for inserting information in  the decodevin  table
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool InsertDecodeVIN(VINDecodeList objVINDecode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    VINDecode vindecode = new VINDecode();
                    vindecode.VINSquish = objVINDecode.VINSquish;
                    vindecode.VehicleYear = objVINDecode.VehicleYear;
                    vindecode.Make = objVINDecode.Make;
                    vindecode.Model = objVINDecode.Model;                                        
                    vindecode.Bodystyle = objVINDecode.Bodystyle;
                    vindecode.VehicleLength = objVINDecode.VehicleLength;
                    vindecode.VehicleHeight = objVINDecode.VehicleHeight;
                    vindecode.VehicleWeight = objVINDecode.VehicleWeight;
                    vindecode.CreationDate = objVINDecode.CreationDate;
                    vindecode.CreatedBy = objVINDecode.CreatedBy;

                    objAppWorksEntities.VINDecodes.Add(vindecode);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public VINDecodeList GetDecodedVINForVINDecode(string vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var vinDecode = (from decodevinvehicle in objAppWorksEntities.VINDecodes
                                     where decodevinvehicle.VINSquish == vin.Substring(0, 8) + vin.Substring(9, 2)
                                     select new VINDecodeList
                                     {
                                         VINSquish = decodevinvehicle.VINSquish,
                                         VehicleYear = decodevinvehicle.VehicleYear,
                                         Make = decodevinvehicle.Make,
                                         Model = decodevinvehicle.Model,
                                         Bodystyle = decodevinvehicle.Bodystyle,
                                         VehicleLength = decodevinvehicle.VehicleLength,
                                         VehicleHeight = decodevinvehicle.VehicleHeight,
                                         VehicleWidth = decodevinvehicle.VehicleWidth,
                                         VehicleWeight = decodevinvehicle.VehicleWeight
                                     }).FirstOrDefault();

                    return vinDecode;
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
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public VINDecodeList GetDecodeDataFromWeb(string vin, string bodyStylep)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                List<string> lstDecodeSettings = GetDecodeSettings();
                string dataOneServerURL = string.Empty;
                string dataOneClientName = string.Empty;
                string dataOneAccessCode = string.Empty;
                if (lstDecodeSettings.Count > 0)
                {
                    dataOneServerURL = lstDecodeSettings[0] + "?";
                    dataOneClientName = lstDecodeSettings[1];
                    dataOneAccessCode = lstDecodeSettings[2];
                }

                //;  HTTPPage (con(cDataOneServerURL,'?clientName=',cDataOneClientName,'&vinNumbers=',cVIN,'&bodyStyles=',cBodyStyle,'&accessCode=',cDataOneAccessCode)) Returns cPage     ;; ;  HTTPPage ('http://xmlvindecoder.com/harnessXML/harnessRest.php?clientName=BlueEdgeData&vinNumbers=4S3BL616157214693&bodyStyles=&accessCode=xdmhfdcbabmbvgbakccaimnipebabqxyuigzdabebefncznptg') Returns cPage
                //create the constructor with post type and few data
                string url = dataOneServerURL + "clientName=" + dataOneClientName + "&vinNumbers=" + vin + "&bodyStyles=" + bodyStylep + "&accessCode=" + dataOneAccessCode;

                MyWebRequest myRequest = new MyWebRequest(url);

                //MyWebRequest myRequest = new MyWebRequest("http://xmlvindecoder.com/harnessXML/harnessRest.php?clientName=BlueEdgeData&vinNumbers=4S3BL616157214693&bodyStyles=&accessCode=xdmhfdcbabmbvgbakccaimnipebabqxyuigzdabebefncznptg");
                string decodeDataWeb = myRequest.GetResponse();
                string vehicleYear = string.Empty;
                string make = string.Empty;
                string model = string.Empty;
                string bodyStyle = string.Empty;
                string vehicleLength = string.Empty;
                string vehiclewidth = string.Empty;
                string vehicleHeight = string.Empty;
                string vehicleWeight = string.Empty;
                string decodeError = string.Empty;
                VINDecodeList vindecode = new VINDecodeList();
                XmlDocument xmlVINDecode = new XmlDocument();
                xmlVINDecode.LoadXml(decodeDataWeb);//Set search as calculation {cHtmlTagList.#S1='<common_year>'}
                if (xmlVINDecode != null)
                {
                    XmlNodeList xCommonYear = xmlVINDecode.GetElementsByTagName("common_year");
                    if (xCommonYear.Count > 0)
                        vehicleYear = xCommonYear[0].InnerXml;
                    //XmlNodeList xMake = xmlVINDecode.GetElementsByTagName("make");
                    //if (xMake.Count > 0)
                    //     make = xMake[0].InnerXml;
                    //XmlNodeList xModel = xmlVINDecode.GetElementsByTagName("model");
                    //if (xModel.Count > 0)
                    //     model = xModel[0].InnerXml;
                    //XmlNodeList xTrim = xmlVINDecode.GetElementsByTagName("trim");
                    //if (xTrim.Count > 0)
                    //    bodyStyle = xTrim[0].InnerXml;
                    //XmlNodeList xAvailable_styleName = xmlVINDecode.GetElementsByTagName("available_style name");
                    //if (xAvailable_styleName.Count > 0)
                    //{
                    //    if (xAvailable_styleName.Count > 0)
                    //        bodyStyle = bodyStyle + xAvailable_styleName[0].InnerXml;
                    //}
                    //XmlNodeList xLength = xmlVINDecode.GetElementsByTagName("length");
                    //if (xLength.Count > 0)
                    //    vehicleLength = xLength[0].InnerXml;
                    //XmlNodeList xWidth = xmlVINDecode.GetElementsByTagName("width");
                    //if (xWidth.Count > 0)
                    //    vehiclewidth = xWidth[0].InnerXml;
                    //XmlNodeList xHeight = xmlVINDecode.GetElementsByTagName("height");
                    //if (xHeight.Count > 0)
                    //    vehicleHeight = xHeight[0].InnerXml;
                    //XmlNodeList XCurb_weight = xmlVINDecode.GetElementsByTagName("curb_weight");
                    //if (XCurb_weight.Count > 0)
                    //    vehicleWeight = xWidth[0].InnerXml;
                    //XmlNodeList XDecoded_weight = xmlVINDecode.GetElementsByTagName("decoded_weight");
                    //if (XDecoded_weight.Count > 0)
                    //{
                    //    if (XCurb_weight.Count < 1)
                    //        vehiclewidth = XDecoded_weight[0].InnerXml;

                    //}
                    XmlNodeList xDecoded_error = xmlVINDecode.GetElementsByTagName("decoder_error");
                    if (xDecoded_error.Count > 0)
                    {
                        decodeError = string.Empty;
                        if (xDecoded_error.Count > 0)
                            decodeError = xDecoded_error[0].InnerXml;
                    }
                    XmlNodeList xVIN_error = xmlVINDecode.GetElementsByTagName("error_message");
                    if (xVIN_error.Count > 0)
                    {
                        decodeError = string.Empty;
                        if (xVIN_error.Count > 0)
                            decodeError = xVIN_error[0].InnerXml;
                    }
                    if (vin.Length > 0 && vin != null)
                    {
                        vindecode.VINSquish = vin.Substring(0, 8) + vin.Substring(9, 2);
                        vindecode.CreationDate = DateTime.Now;
                        vindecode.CreatedBy = "DataOneDecode";
                    }
                    else
                    {
                        vindecode.VINSquish = string.Empty;
                        vindecode.CreationDate = null;
                        vindecode.CreatedBy = string.Empty;
                    }
                    make = (xmlVINDecode.GetElementsByTagName("make").Count > 0 ? xmlVINDecode.GetElementsByTagName("make")[0].InnerXml :
                        (xmlVINDecode.GetElementsByTagName("common_make").Count > 0 ? xmlVINDecode.GetElementsByTagName("common_make")[0].InnerXml : string.Empty));

                    model = (xmlVINDecode.GetElementsByTagName("model").Count > 0 ? xmlVINDecode.GetElementsByTagName("model")[0].InnerXml
                        : (xmlVINDecode.GetElementsByTagName("common_model").Count > 0 ? xmlVINDecode.GetElementsByTagName("common_model")[0].InnerXml : string.Empty));

                    bodyStyle = (xmlVINDecode.GetElementsByTagName("trim").Count > 0 ? xmlVINDecode.GetElementsByTagName("trim")[0].InnerXml
                        : (xmlVINDecode.GetElementsByTagName("common_trim").Count > 0 ? xmlVINDecode.GetElementsByTagName("common_trim")[0].InnerXml : string.Empty));

                    //bodyStyle += xmlVINDecode.GetElementsByTagName("available_style").Count > 0 ? xmlVINDecode.GetElementsByTagName("available_style")[0].InnerXml : string.Empty;
                    bodyStyle = xmlVINDecode.GetElementsByTagName("available_style").Count > 0 ? xmlVINDecode.GetElementsByTagName("available_style")[0].Attributes["name"].Value : string.Empty;

                    if (!string.IsNullOrEmpty(bodyStyle))
                    {
                        var bracketPosition = bodyStyle.IndexOf('(', 0);

                        if (bracketPosition > -1)
                        {
                            bodyStyle = bodyStyle.Substring(0, bracketPosition).Trim();
                        }
                        bodyStyle = bodyStyle.Length >= 50 ? bodyStyle.Substring(0, 50) : bodyStyle.Substring(0, bodyStyle.Length);
                    }

                    vindecode.VehicleYear = vehicleYear;
                    vindecode.Make = make;
                    vindecode.Model = model;
                    vindecode.Bodystyle = bodyStyle;
                    vindecode.VehicleLength = vehicleLength;
                    vindecode.VehicleWidth = vehiclewidth;
                    vindecode.VehicleHeight = vehicleHeight;
                    vindecode.VehicleWeight = vehicleWeight;

                    vindecode.DecodeError = decodeError;
                }

                if (vindecode.DecodeError != string.Empty)
                {
                    return vindecode;
                }
                else
                {
                    bool isSuccessfull = false;
                    isSuccessfull = InsertDecodeVIN(vindecode);
                    if (isSuccessfull)
                    {
                        return vindecode;
                    }
                    else
                    {
                        VINDecodeList viner = new VINDecodeList();
                        viner.DecodeError = "Unable to insert data in VinDecode!";
                        return viner;
                    }
                }
                //Sta: VALUES( SUBSTRING(@[pvVIN],1,8)+SUBSTRING(@[pvVIN],10,2),@[cVehicleYear],@[cMake],@[cModel],@[cBodyStyle],
                //Sta: @[cLength],@[cWidth],@[cHeight],@[cCurbWeight],CURRENT_TIMESTAMP,'DataOneDecode')
                //show the response string on the console screen.
                //Console.WriteLine(myRequest.GetResponse());
                //lstDecodeSettings.va
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
        /// This method is used to Storage Vehicle Outgate for security.
        /// </summary>
        /// <param name="objStorageVehicleOutgateProp"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>July 13, 2016</createdOn>
        public List<StorageVehicleOutgateProp> UpdateStorageVehicleOutgate(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                List<StorageVehicleOutgateProp> lstStorageVehicleOutgateProp = new List<StorageVehicleOutgateProp>();
                //int result = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    string status = string.Empty;
                    /// Calling The Stored Procedure

                    var result = objAppWorksEntities.spUpdatePortStorageDateOut(objStorageVehicleOutgateProp.Vin, objStorageVehicleOutgateProp.DateOut, "", objStorageVehicleOutgateProp.User).FirstOrDefault();

                    if (result != null)
                    {
                        StorageVehicleOutgateProp objData = new StorageVehicleOutgateProp
                        {
                            ReturnCode = result.RC,
                            ReturnMessage = result.RM
                        };
                        lstStorageVehicleOutgateProp.Add(objData);

                    }
                    using (PortStorageEntities objAppWorksEntities1 = new PortStorageEntities())
                    {
                        var query = from qry in objAppWorksEntities1.PortStorageVehicles
                                    where qry.VIN == objStorageVehicleOutgateProp.Vin
                                    orderby qry.PortStorageVehiclesID descending
                                    select new { qry.VIN };
                        if (query != null)
                        {
                            foreach (var value in query)
                            {
                                StorageVehicleOutgateProp objData = new StorageVehicleOutgateProp
                                {

                                    Vin = value.VIN

                                };
                                lstStorageVehicleOutgateProp.Add(objData);
                            }
                        }
                    }
                    return lstStorageVehicleOutgateProp;
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
        /// This method is used to Storage Vehicle Outgate for security.
        /// </summary>
        /// <param name="objStorageVehicleOutgateProp"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>July 13, 2016</createdOn>
        public StorageVehicleOutgateProp UpdateStorageVehicleOutgateData(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                StorageVehicleOutgateProp lstStorageVehicleOutgateProp = new StorageVehicleOutgateProp();
                //int result = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    string status = string.Empty;
                    /// Calling The Stored Procedure
                    var dat = (from qry in objAppWorksEntities.PortStorageVehicles where qry.VIN.Contains(objStorageVehicleOutgateProp.Vin) && qry.DateOut == null select qry).FirstOrDefault();
                    if (dat != null)
                    {
                        var result =objAppWorksEntities.spUpdatePortStorageDateOut(objStorageVehicleOutgateProp.Vin, objStorageVehicleOutgateProp.DateOut, "", objStorageVehicleOutgateProp.User).FirstOrDefault();
                        if (result != null)
                        {
                            lstStorageVehicleOutgateProp.ReturnCode = result.RC;
                            lstStorageVehicleOutgateProp.ReturnMessage = result.RM;
                        }
                        else
                        {
                            lstStorageVehicleOutgateProp.ReturnCode = 1002;
                            lstStorageVehicleOutgateProp.ReturnMessage = "VIN not found in Port Storage Vehicles table";
                        }
                    }
                    else
                    {
                        lstStorageVehicleOutgateProp.ReturnCode = 1002;
                        lstStorageVehicleOutgateProp.ReturnMessage = "VIN not found in Port Storage Vehicles table";
                    }
                    using (PortStorageEntities objAppWorksEntities1 = new PortStorageEntities())
                    {
                        var query = (from qry in objAppWorksEntities1.PortStorageVehicles
                                     where qry.VIN == objStorageVehicleOutgateProp.Vin
                                     orderby qry.PortStorageVehiclesID descending
                                     select new { qry.VIN }).FirstOrDefault();
                        if (query != null)
                        {

                            lstStorageVehicleOutgateProp.Vin = query.VIN;

                        }
                    }

                    return lstStorageVehicleOutgateProp;
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

    public class MyWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public MyWebRequest(string url)
        {
            // Create a request using a URL that can receive a post.

            request = WebRequest.Create(url);
        }

        public MyWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public MyWebRequest(string url, string method, string data)
            : this(url, method)
        {

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

    }
}

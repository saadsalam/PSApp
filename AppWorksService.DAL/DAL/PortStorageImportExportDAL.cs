using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;
using System.Globalization;
using System.Reflection;
using System.Data.Entity.SqlServer;
using AppWorksService.DAL.Edmx;
using AppWorks.Utilities;
using System.Data.Entity.Infrastructure;

namespace AppWorksService.DAL
{
    public class PortStorageImportExportDAL
    {
        /// <summary>
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> LoadVehiclesBatchList(string Vin)
        {

            List<PortStorageVehicleImportProp> lstBatch = new List<PortStorageVehicleImportProp>();
            try
            {   
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    ((IObjectContextAdapter)objAppWorksEntities).ObjectContext.CommandTimeout = 180;
                    string internalQuery = SqlConstants.VEHICLE_BATCH_LIST_ALL;
                    if (!string.IsNullOrEmpty(Vin))
                    {
                        internalQuery = string.Format(SqlConstants.VEHICLE_BATCH_LIST_SEARCH, Vin);
                    }
                    var sqlQuery = objAppWorksEntities.Database.SqlQuery<PortStorageVehicleImportProp>(internalQuery);
                    var batchRecords = sqlQuery.GroupBy(grp => grp.BatchId).Select(x => new PortStorageVehicleImportProp
                    {
                        BatchId = x.Key,
                        Vin = x.FirstOrDefault().Vin,
                        BatchCount = x.ToList().Count,
                        CreationDate = x.FirstOrDefault().CreationDate
                    }).OrderByDescending(x=>x.BatchId);
                    return batchRecords.ToList();
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
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageVehiclesBatchId()
        {
            int BatchId = 0;
            try
            {

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("NextPortStorageVehicleImportBatchID")
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
        public bool SetPortStorageVehiclesNextBatchId(int BatchId)
        {
            bool Result = false;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    SettingTable settingTable = objAppWorksEntities.SettingTables.Where(v => v.ValueKey.Equals("NextPortStorageVehicleImportBatchID")).FirstOrDefault();
                    settingTable.ValueDescription = Convert.ToString(BatchId + 1);
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
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
        /// This method is used to get message from storeprocedure.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageVehicles(int BatchId, string User)
        {
            PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();

            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                //int result = 0;
                ((IObjectContextAdapter)objAppWorksEntities).ObjectContext.CommandTimeout = 1800;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    string status = string.Empty;
                    /// Calling The Stored Procedure
                    var result = objAppWorksEntities.spImportPortStorageVehiclesData(BatchId, User).FirstOrDefault();
                    if (result != null)
                    {
                        portStorageVehicleImportProp.ReturnCode = result.ReturnCode;
                        portStorageVehicleImportProp.ReturnMessage = result.ReturnMessage;
                    }

                    return portStorageVehicleImportProp;


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
        /// This method is used to Get Port Storage Vehicle List
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> GetPortStorageVehicleImportList(int BatchId)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    var query = (from qry in objAppWorksEntities.PortStorageVehiclesImports
                                 where qry.BatchID == BatchId
                                 orderby qry.PortStorageVehiclesImportID
                                 select new PortStorageVehicleImportProp
                                 {
                                     PortStorageVehiclesImportID = qry.PortStorageVehiclesImportID,
                                     BatchId = qry.BatchID,
                                     Vin = qry.VIN,
                                     DateIn = qry.DateIn,
                                     DealerCode = qry.DealerCode,
                                     ModelYear = qry.ModelYear,
                                     ModelName = qry.ModelName,
                                     Color = qry.Color,
                                     Location = qry.Location,
                                     RecordStatus = qry.RecordStatus,
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
        /// This method is used to Get Port Storage Import File Directory
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileDirectory()
        {
            string ImportFileDirectory = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("PortStorageReadDirectory")
                                 select new { ImportFileDirectory = qry.ValueDescription }).FirstOrDefault();
                    ImportFileDirectory = query.ImportFileDirectory;


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
            return ImportFileDirectory;
        }

        /// <summary>
        /// This method is used to get Import File Name
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileName()
        {
            string ImportFileName = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("PortStorageVehicleImportFileName")
                                 select new { ImportFileName = qry.ValueDescription }).FirstOrDefault();
                    ImportFileName = query.ImportFileName;


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
            return ImportFileName;
        }

        /// <summary>
        /// This method is used to get Import Archive Directory
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileArchiveDirectory()
        {
            string ImportFileArchiveDirectory = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("PortStorageWriteDirectory")
                                 select new { ImportFileArchiveDirectory = qry.ValueDescription }).FirstOrDefault();
                    ImportFileArchiveDirectory = query.ImportFileArchiveDirectory;


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
            return ImportFileArchiveDirectory;
        }

        /// <summary>
        /// This method is used to Load Batch list for Location from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> LoadLocationBatchList(string Vin)
        {
            List<PortStorageVehicleImportProp> lstBatch = new List<PortStorageVehicleImportProp>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from psvi in objAppWorksEntities.PortStorageVehicleLocationImports
                                 group psvi by psvi.BatchID into grp

                                 select new PortStorageVehicleImportProp
                                 {

                                     BatchId = grp.Key.Value,
                                     Vin = grp.Select(x => x.VIN).FirstOrDefault(),
                                     BatchCount = grp.Count(),
                                     CreationDate = grp.Select(x => x.CreationDate).FirstOrDefault(),

                                 });//.Distinct().OrderByDescending(x => x.BatchId).AsQueryable();
                    if (Vin != null)
                    {
                        query = query.Where(cnd => cnd.Vin.Contains(Vin));//.AsQueryable(); 
                    }
                    query = query.Distinct().OrderByDescending(x => x.BatchId).AsQueryable();

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
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageLocationBatchId()
        {
            int BatchId = 0;
            try
            {

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("NextPortStorageLocationImportBatchID")
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
        public bool SetPortStorageLocationNextBatchId(int BatchId)
        {
            bool Result = false;
            try
            {

                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    SettingTable settingTable = objAppWorksEntities.SettingTables.Where(v => v.ValueKey.Equals("NextPortStorageLocationImportBatchID")).FirstOrDefault();
                    settingTable.ValueDescription = Convert.ToString(BatchId + 1);
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record update.
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
        /// This method is used to get message from storeprocedure.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageLocation(int BatchId, string User)
        {
            PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();

            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                //int result = 0;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    ((IObjectContextAdapter)objAppWorksEntities).ObjectContext.CommandTimeout = 1800;
                    /// Calling The Stored Procedure
                    var result = objAppWorksEntities.spImportPortStorageLocations(BatchId, User).FirstOrDefault();
                    if (result != null)
                    {
                        portStorageVehicleImportProp.ReturnCode = result.ReturnCode;
                        portStorageVehicleImportProp.ReturnMessage = result.ReturnMessage;
                    }
                    return portStorageVehicleImportProp;


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
        /// This method is used to Get Port Storage Vehicle List
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> GetPortStorageLocationImportList(int BatchId)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    var query = (from qry in objAppWorksEntities.PortStorageVehicleLocationImports
                                 where qry.BatchID == BatchId
                                 orderby qry.PortStorageVehicleLocationImportID
                                 select new PortStorageVehicleImportProp
                                 {
                                     PortStorageVehiclesLocationImportID = qry.PortStorageVehicleLocationImportID,
                                     BatchId = qry.BatchID,
                                     Vin = qry.VIN,
                                     Location = qry.Location,
                                     RecordStatus = qry.RecordStatus,
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
        /// This method is used to get Import File Name
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageLocationImportFileName()
        {
            string ImportFileName = string.Empty;
            try
            {
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var query = (from qry in objAppWorksEntities.SettingTables
                                 where qry.ValueKey.Equals("PortStorageLocationImportFileName")
                                 select new { ImportFileName = qry.ValueDescription }).FirstOrDefault();
                    ImportFileName = query.ImportFileName;


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
            return ImportFileName;
        }
        /// <summary>
        /// This method is used to get data and insert data in database
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool LocationImportTransactionProcess(int BatchId, string User)
        {
            bool Result = true;
            try
            {

                List<VehicleLocationImportTransactionProp> vehicleLocationImportTransactionProp = new List<VehicleLocationImportTransactionProp>();
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {

                    string CurruntDatabaseName = objAppWorksEntities.Database.Connection.Database; // Get currunt database name 

                    var result = objAppWorksEntities.spGetDataStoragePhyDataImportLocation(CurruntDatabaseName).ToList();
                    if (result != null && result.Count > 0)
                    {
                        PortStorageVehicleLocationImport portStorageVehicleLocationImport = new PortStorageVehicleLocationImport();
                        foreach (var item in result)
                        {
                            VehicleLocationImportTransactionProp obj = new VehicleLocationImportTransactionProp();
                            obj.VinNumberAndVinKey = item.VINNumberAndVINKey;
                            obj.Row = item.Row;
                            obj.Bay = item.Bay;
                            obj.ByRowFlag = item.ByRowFlg;
                            obj.HandheldActionDate = item.HandheldActionDate;
                            obj.UserCode = item.UserCode;
                            vehicleLocationImportTransactionProp.Add(obj);
                            obj = null;
                        }
                        if (vehicleLocationImportTransactionProp.Count > 0)
                        {
                            foreach (var val in vehicleLocationImportTransactionProp)
                            {

                                //                            If len(iSqlRow.VIN)>17
                                //If (len(iSqlRow.VIN)=18)&(left(iSqlRow.VIN,1)='I')
                                //Calculate iSqlRow.VIN as right(iSqlRow.VIN,17)
                                //Else
                                //Calculate iSqlRow.VIN as left(iSqlRow.VIN,17)
                                if (val.VinNumberAndVinKey.Length > 17)
                                {
                                    if ((val.VinNumberAndVinKey.Length == 18) && (val.VinNumberAndVinKey.Substring(0, 1).Equals("I")))
                                    {
                                        val.VinNumberAndVinKey = val.VinNumberAndVinKey.Remove(0, 1);
                                    }
                                    else
                                    {
                                        val.VinNumberAndVinKey = val.VinNumberAndVinKey.Substring(0, 17);
                                    }

                                }

                                portStorageVehicleLocationImport.BatchID = BatchId;
                                portStorageVehicleLocationImport.VIN = val.VinNumberAndVinKey;
                                portStorageVehicleLocationImport.Location = (val.Row + " " + val.Bay).Trim();
                                portStorageVehicleLocationImport.ImportedInd = 0;
                                portStorageVehicleLocationImport.CreationDate = DateTime.Now;
                                portStorageVehicleLocationImport.CreatedBy = User;
                                portStorageVehicleLocationImport.RecordStatus = "Import Pending";
                                objAppWorksEntities.PortStorageVehicleLocationImports.Add(portStorageVehicleLocationImport);  /// Insert the Record in Respected Table.
                                objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                                Result = false;
                            }
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
            return Result;

        }

        /// <summary>
        /// This method is used to get data and insert data in database for vehicle
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool VehicleImportTransactionProcess(int batchId, string user)
        {
            bool isSucceeded = false;

            var query = string.Empty;
            try
            {
                List<VehicleLocationImportTransactionProp> vehicleLocationImportTransactionProp = new List<VehicleLocationImportTransactionProp>();

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    VINDecodeList lstVINDecodeList = new VINDecodeList();

                    //If jst(lSessionHost,'U')='DAIDB'
                    // Sta: exec DataHub.dbo.DAIYMSP_GetStorageReceiptData
                    // Else If jst(lSessionHost,'U')='DAIDBVPC'
                    // Sta: exec DataHUBVPC.dbo.DAIYMSP_GetStorageReceiptData
                    // Else
                    // Sta: exec DataHubDev.dbo.DAIYMSP_GetStorageReceiptData
                    // End If

                    // Get current database name
                    string currentDatabaseName = objAppWorksEntities.Database.Connection.Database;

                    //if (currentDatabaseName.Equals("psdb", StringComparison.OrdinalIgnoreCase))
                    //{
                        query = SqlConstants.VEHICLE_LOCATION_IMPORT_TRANSACTION;
                    //}

                    var result = objAppWorksEntities.Database.SqlQuery<VehicleLocationImportTransactionProp>(query).ToList();



                    if (result != null && result.Count > 0)
                    {
                        PortStorageVehiclesImport portStorageVehiclesImport = new PortStorageVehiclesImport();

                        foreach (var item in result)
                        {
                            VehicleLocationImportTransactionProp obj = new VehicleLocationImportTransactionProp();
                            obj.VinNumberAndVinKey = item.VinNumberAndVinKey;
                            obj.Row = item.Row;
                            obj.Bay = item.Bay;
                            obj.DealerCode = item.DealerCode;
                            obj.ColorCode = item.ColorCode;
                            obj.DamageCode = item.DamageCode;
                            obj.HandheldActionDate = item.HandheldActionDate;
                            obj.UserCode = item.UserCode;
                            vehicleLocationImportTransactionProp.Add(obj);
                            obj = null;
                        }

                        if (vehicleLocationImportTransactionProp.Count > 0)
                        {
                            foreach (var val in vehicleLocationImportTransactionProp)
                            {
                                if (val.VinNumberAndVinKey.Length > 17)// check validation
                                {
                                    if ((val.VinNumberAndVinKey.Length == 18) && (val.VinNumberAndVinKey.Substring(0, 1).Equals("I")))
                                    {
                                        val.VinNumberAndVinKey = val.VinNumberAndVinKey.Remove(0, 1);
                                    }
                                    else
                                    {
                                        val.VinNumberAndVinKey = val.VinNumberAndVinKey.Substring(0, 17);
                                    }

                                }

                                lstVINDecodeList = GetDataByDecodeVIN(val.VinNumberAndVinKey);

                                if (lstVINDecodeList != null)
                                {
                                    portStorageVehiclesImport.VehicleYear = lstVINDecodeList.VehicleYear;
                                    portStorageVehiclesImport.Make = lstVINDecodeList.Make;
                                    portStorageVehiclesImport.Model = lstVINDecodeList.Model;
                                    portStorageVehiclesImport.Bodystyle = lstVINDecodeList.Bodystyle;
                                    portStorageVehiclesImport.VehicleLength = lstVINDecodeList.VehicleLength;
                                    portStorageVehiclesImport.VehicleWidth = lstVINDecodeList.VehicleWidth;
                                    portStorageVehiclesImport.VehicleHeight = lstVINDecodeList.VehicleHeight;
                                }
                                else
                                {
                                    portStorageVehiclesImport.VehicleYear = string.Empty;
                                    portStorageVehiclesImport.Make = string.Empty;
                                    portStorageVehiclesImport.Model = string.Empty;
                                    portStorageVehiclesImport.Bodystyle = string.Empty;
                                    portStorageVehiclesImport.VehicleLength = string.Empty;
                                    portStorageVehiclesImport.VehicleWidth = string.Empty;
                                    portStorageVehiclesImport.VehicleHeight = string.Empty;
                                }

                                portStorageVehiclesImport.VINDecodedInd = 0;
                                portStorageVehiclesImport.BatchID = batchId;
                                portStorageVehiclesImport.DateIn = val.HandheldActionDate.Value.ToString("MM/dd/yyyy");

                                portStorageVehiclesImport.DealerCode = val.DealerCode;
                                portStorageVehiclesImport.VIN = val.VinNumberAndVinKey;

                                portStorageVehiclesImport.Color = val.ColorCode;
                                //portStorageVehiclesImport.DamageCodeList = val.DealerCode;
                                portStorageVehiclesImport.DamageCodeList = val.DamageCode;
                                portStorageVehiclesImport.Location = (val.Row + " " + val.Bay).Trim();
                                portStorageVehiclesImport.ImportedInd = 0;
                                portStorageVehiclesImport.CreationDate = DateTime.Now;
                                portStorageVehiclesImport.CreatedBy = user;

                                portStorageVehiclesImport.RecordStatus = "Import Pending";
                                objAppWorksEntities.PortStorageVehiclesImports.Add(portStorageVehiclesImport);  /// Insert the Record in Respected Table.
                                objAppWorksEntities.SaveChanges();
                                isSucceeded = true;
                            }
                        }
                    }
                    else
                    {
                        isSucceeded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return isSucceeded;
        }

        /// <summary>
        /// This method is used to get data using Decode Vin functionality
        /// </summary>
        /// <param name="Vin"></param>
        /// <returns></returns>
        private VINDecodeList GetDataByDecodeVIN(string Vin)
        {
            VINDecodeList lstVINDecodeList = new VINDecodeList();
            VehicleDAL vehicleDAL = new VehicleDAL();
            try
            {
                var lstPsVehicle = vehicleDAL.GetDecodedPortStorageVIN(Vin);
                if (lstPsVehicle != null)
                {
                    lstVINDecodeList.VehicleYear = lstPsVehicle.VehicleYear;
                    lstVINDecodeList.Make = lstPsVehicle.Make;
                    lstVINDecodeList.Model = lstPsVehicle.Model;
                    lstVINDecodeList.Bodystyle = lstPsVehicle.Bodystyle;
                    lstVINDecodeList.VehicleLength = lstPsVehicle.VehicleLength;
                    lstVINDecodeList.VehicleWidth = lstPsVehicle.VehicleWidth;
                    lstVINDecodeList.VehicleHeight = lstPsVehicle.VehicleHeight;
                    //lstVINDecodeList.VINDecodeID = (int)lstPsVehicle.VINDecodedInd;
                }
                else
                {
                    var lstDecodeVin = vehicleDAL.GetDecodedVINForVINDecode(Vin);
                    if (lstDecodeVin != null)
                    {
                        lstVINDecodeList.VehicleYear = lstDecodeVin.VehicleYear;
                        lstVINDecodeList.Make = lstDecodeVin.Make;
                        lstVINDecodeList.Model = lstDecodeVin.Model;
                        lstVINDecodeList.Bodystyle = lstDecodeVin.Bodystyle;
                        lstVINDecodeList.VehicleLength = lstDecodeVin.VehicleLength;
                        lstVINDecodeList.VehicleWidth = lstDecodeVin.VehicleWidth;
                        lstVINDecodeList.VehicleHeight = lstDecodeVin.VehicleHeight;
                        //lstVINDecodeList.VINDecodeID = (int)lstDecodeVin.VINDecodeID;
                    }
                    else
                    {
                        string Bodystyle = null;
                        var lstVINDataWeb = vehicleDAL.GetDecodeDataFromWeb(Vin, Bodystyle);
                        if (lstVINDataWeb != null)
                        {
                            if (lstVINDataWeb.DecodeError.Length == 0)
                            {
                                lstVINDecodeList.VehicleYear = lstVINDataWeb.VehicleYear;
                                lstVINDecodeList.Make = lstVINDataWeb.Make;
                                lstVINDecodeList.Model = lstVINDataWeb.Model;
                                lstVINDecodeList.Bodystyle = lstVINDataWeb.Bodystyle;
                                lstVINDecodeList.VehicleLength = lstVINDataWeb.VehicleLength;
                                lstVINDecodeList.VehicleWidth = lstVINDataWeb.VehicleWidth;
                                lstVINDecodeList.VehicleHeight = lstVINDataWeb.VehicleHeight;
                                //lstVINDecodeList.VINDecodeID = (int)lstVINDataWeb.VINDecodeID;
                            }
                            else
                            {
                                lstVINDecodeList = null;
                            }
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
            return lstVINDecodeList;


        }


    }
}

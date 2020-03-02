using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Globalization;
using System.Reflection;

namespace AppWorksService.BAL
{
   public class PortStorageImportExportBAL
    {
       /// <summary>
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
       public List<PortStorageVehicleImportProp> LoadVehiclesBatchList(string Vin)
       {
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.LoadVehiclesBatchList(Vin);
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
       /// This method is used to get batch id 
       /// </summary>
       /// <returns></returns>
       public int GetPortStorageVehiclesBatchId()
       {
           try
           {
              
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageVehiclesBatchId();
              
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
       public bool SetPortStorageVehiclesNextBatchId(int BatchId)
       {
           try
           {
             
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.SetPortStorageVehiclesNextBatchId(BatchId);

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
       /// This method is used to get message from storeprocedure.
       /// </summary>
       /// <param name="BatchId"></param>
       /// <param name="User"></param>
       /// <returns></returns>
       public PortStorageVehicleImportProp ImportPortStorageVehicles(int BatchId, string User)
       {
           try
            {
             
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.ImportPortStorageVehicles(BatchId,User);

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
       public List<PortStorageVehicleImportProp> GetPortStorageVehicleImportList(int BatchId)
       {
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageVehicleImportList(BatchId);
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
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageVehiclesImportFileDirectory();

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
       public string GetPortStorageVehiclesImportFileName()
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageVehiclesImportFileName();

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
       /// This method is used to get Import Archive Directory
       /// </summary>
       /// <returns>string</returns>
       public string GetPortStorageVehiclesImportFileArchiveDirectory()
       {
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageVehiclesImportFileArchiveDirectory();

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
       public List<PortStorageVehicleImportProp> LoadLocationBatchList(string Vin)
       {
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.LoadLocationBatchList(Vin);
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
       /// This method is used to get batch id 
       /// </summary>
       /// <returns></returns>
       public int GetPortStorageLocationBatchId()
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageLocationBatchId();

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
       public bool SetPortStorageLocationNextBatchId(int BatchId)
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.SetPortStorageLocationNextBatchId(BatchId);

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
       /// This method is used to get message from storeprocedure.
       /// </summary>
       /// <param name="BatchId"></param>
       /// <param name="User"></param>
       /// <returns></returns>
       public PortStorageVehicleImportProp ImportPortStorageLocation(int BatchId, string User)
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.ImportPortStorageLocation(BatchId, User);

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
       public List<PortStorageVehicleImportProp> GetPortStorageLocationImportList(int BatchId)
       {
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageLocationImportList(BatchId);
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
           try
           {
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.GetPortStorageLocationImportFileName();

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
       /// This method is used to get data and insert data in database
       /// </summary>
       /// <param name="BatchId"></param>
       /// <param name="User"></param>
       /// <returns></returns>
       public bool LocationImportTransactionProcess(int BatchId, string User)
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.LocationImportTransactionProcess(BatchId, User);

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
       /// This method is used to get data and insert data in database
       /// </summary>
       /// <param name="BatchId"></param>
       /// <param name="User"></param>
       /// <returns></returns>
       public bool VehicleImportTransactionProcess(int BatchId, string User)
       {
           try
           {

               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               PortStorageImportExportDAL portStorageImportExportDAL = new PortStorageImportExportDAL();
               return portStorageImportExportDAL.VehicleImportTransactionProcess(BatchId, User);

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

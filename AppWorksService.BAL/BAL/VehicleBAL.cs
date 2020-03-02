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
    public class VehicleBAL
    {

        /// <summary>
        /// Function To find the vehicle search details.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-2,2016</createdOn>
        public async Task<List<VehicleProp>> GetVehicleSearchDetails(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                return await objVehicleDAL.GetVehicleSearchDetails(objVehicleProp);
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
        /// Function To find the vehicle search details.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-4,2016</createdOn>
        public int InsertVehicleDetails(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                return objVehicleDAL.InsertVehicleDetails(objVehicleProp);
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
        /// This Method is used to Get Port Storage Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public List<VehicleProp> GetPortStorageProcessignDetails(string VIN)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                return objVehicleDAL.GetPortStorageProcessignDetails(VIN);
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
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public int UpdatePortStorageProcessignDetails(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Insert PortStorage Processign Details Method.
                return objVehicleDAL.UpdatePortStorageProcessignDetails(objVehicleProp);
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
        /// This Method is used to Get Port Storage for Date Out Processign Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<VehicleProp> GetPortStorageDateOutProcessignDetails(string VIN)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                return objVehicleDAL.GetPortStorageDateOutProcessignDetails(VIN);
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
        /// This method is used to update Port Storage for Date Out Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int UpdatePortStorageDateOutProcessignDetails(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling update PortStorage Processing for date out Details Method.
                return objVehicleDAL.UpdatePortStorageDateOutProcessignDetails(objVehicleProp);
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
        /// This method is used to update Port Storage request processing date
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public string RequestBatchProcess(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Insert PortStorage Processign Details Method.
                return objVehicleDAL.RequestBatchProcess(objVehicleProp);
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
        /// This method is used to update Port Storage for processing date out
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public string DateOutBatchProcess(VehicleProp objVehicleProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Insert PortStorage Processign Details Method.
                return objVehicleDAL.DateOutBatchProcess(objVehicleProp);
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

        /// </summary>
        /// <param></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int DecodeVIN(string Vin)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Decode VIN.
                return objVehicleDAL.DecodeVIN(Vin);
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

            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Decode VIN.
                return objVehicleDAL.GetVecheleDetailByVIN(VIN,vehicleId);
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

            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Decode VIN.
                return objVehicleDAL.CheckMultipleVecheleDetailByVIN(VIN);
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
                VehicleDAL objVehicleDAL = new VehicleDAL();
                return objVehicleDAL.VehicalStatusList();
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
        ///  This Method to Call The Vehical Information By VIN
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<VehicleProp> CallVehialDetailsbyVIN(VehicleProp objVehicleProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleDAL objVehicleDAL = new VehicleDAL();
                return objVehicleDAL.CallVehialDetailsbyVIN(objVehicleProp);
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
        ///  This Method to Update Vehical Details Information.
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool UpdateVehicalSearchDetails(VehicleProp objVehicleProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleDAL objVehicleDAL = new VehicleDAL();
                return objVehicleDAL.UpdateVehicalSearchDetails(objVehicleProp);
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
        /// This method is used to delete vehicle details
        /// </summary>
        /// <param name="VehiclesID"></param>
        /// <returns></returns>
        public bool DeleteVehicalSearchDetails(int VehiclesID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleDAL objVehicleDAL = new VehicleDAL();
                return objVehicleDAL.DeleteVehicalSearchDetails(VehiclesID);
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
        public bool CheckMultipleVIN(string vIN)
        {
            try
            {
                VehicleDAL objVehicleDAL = new VehicleDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleDAL.CheckMultipleVIN(vIN);

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
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleDAL objVehicleDAL = new VehicleDAL();

                return objVehicleDAL.GetDecodedPortStorageVIN(vin);
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

                VehicleDAL objVehicleDAL = new VehicleDAL();
                return objVehicleDAL.InsertDecodeVIN(objVINDecode);
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
        public VINDecodeList GetDecodedVINForVINDecode(string vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleDAL objVehicleDAL = new VehicleDAL();

                return objVehicleDAL.GetDecodedVINForVINDecode(vin);

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
                VehicleDAL objVehicleDAL = new VehicleDAL();

                return objVehicleDAL.GetDecodeDataFromWeb(vin,bodyStylep);

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
        /// <createdOn>July 7, 2016</createdOn>
        public List<StorageVehicleOutgateProp> UpdateStorageVehicleOutgate(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Insert PortStorage Vehicle Outgate Details Method.
                return objVehicleDAL.UpdateStorageVehicleOutgate(objStorageVehicleOutgateProp);
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
        /// <createdOn>July 7, 2016</createdOn>
        public StorageVehicleOutgateProp UpdateStorageVehicleOutgateData(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            VehicleDAL objVehicleDAL = new VehicleDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling Insert PortStorage Vehicle Outgate Details Method.
                return objVehicleDAL.UpdateStorageVehicleOutgateData(objStorageVehicleOutgateProp);
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

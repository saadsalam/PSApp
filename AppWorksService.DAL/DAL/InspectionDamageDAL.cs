using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    public class InspectionDamageDAL
    {
        /// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public List<PerDiemProp> GetInspectionDamageDetails(string VIN)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            List<PerDiemProp> objclass  = new List<PerDiemProp>();
            try
            {
               // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    // query to get details from database
                    //var list = (from tblPSVehicalInspection in objAppWorksEntities.PSVehicleInspections
                    //            join tblPSVehicalDamageDetails in objAppWorksEntities.PSVehicleDamageDetails
                    //            on tblPSVehicalInspection.PSVehicleInspectionID equals tblPSVehicalDamageDetails.PSVehicleInspectionID
                    //            join tblPortStorageVehicals in objAppWorksEntities.PortStorageVehicles on tblPSVehicalInspection.PortStorageVehiclesID equals tblPortStorageVehicals.PortStorageVehiclesID
                    //            join tblCode1 in objAppWorksEntities.Codes on tblPSVehicalDamageDetails.DamageCode.Substring(0, 2) equals tblCode1.Code1 && tblCode1.CodeType == "DamageAreaCode"
                    //            join tblCode2 in objAppWorksEntities.Codes on tblPSVehicalDamageDetails.DamageCode.Substring(3, 2) equals tblCode2.Code1 && tblCode2.CodeType.Contains("DamageTypeCode")
                    //            join tblCode3 in objAppWorksEntities.Codes on tblPSVehicalDamageDetails.DamageCode.Substring(5, 1) equals tblCode3.Code1 && tblCode3.CodeType.Contains("DamageSeverityCode")
                    //            where tblPortStorageVehicals.VIN == VIN
                    //            orderby tblPSVehicalInspection.InspectionType, tblPSVehicalInspection.InspectionDate, tblPSVehicalDamageDetails.DamageCode
                    //            select new PerDiemProp
                    //            {


                    //            }).ToList();

                    return objclass;
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
        /// function to Load the Location History
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public List<PortStorageLocationHistoryProp> GetLocationHistory(int portStorageVehicalID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var list = (from tblPortStorageLocationHistory in objAppWorksEntities.PortStorageVehiclesLocationHistories
                                where tblPortStorageLocationHistory.PortStorageVehiclesID == portStorageVehicalID
                                select new PortStorageLocationHistoryProp
                                {
                                    PortStorageVehiclesLocationHistoryID=tblPortStorageLocationHistory.PortStorageVehiclesLocationHistoryID,
                                    BayLocation = tblPortStorageLocationHistory.BayLocation,
                                    LocationDate = tblPortStorageLocationHistory.LocationDate,
                                    CreationDate = tblPortStorageLocationHistory.CreationDate,
                                    Createdby = tblPortStorageLocationHistory.CreatedBy,
                                    UpdatedDate = tblPortStorageLocationHistory.UpdatedDate,
                                    Updatedby = tblPortStorageLocationHistory.CreatedBy    // Set UpdatedbyInfo
                                }).ToList().OrderByDescending(x => x.PortStorageVehiclesLocationHistoryID);
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
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using System.Linq;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;
using AppWorks.Utilities;

namespace AppWorksService.DAL
{
    public class PortStorageDamageDetailsDAL
    {
        /// <summary>
        /// function to get edit the Damage Code
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>Int<returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-6,2016</createdOn>
        public int EditDamageCode(PortStorageDamageDetailsProp objPortStorageDamageDetailsProp)
        {
            int value = 0;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var tblPSVehicalDamageDetails = (from qry in objAppWorksEntities.PSVehicleDamageDetails where qry.PSVehicleDamageDetailID == objPortStorageDamageDetailsProp.PSVehicleDamageDetailID select qry).FirstOrDefault();
                    //Sta: UPDATE PSVehicleDamageDetail
                    //Sta: SET DamageCode = @[lNewDamageCode]
                    //Sta: WHERE PSVehicleDamageDetailID = @[iInspectionsAndDamageList.iVehicleDamageDetailID]
                    if (tblPSVehicalDamageDetails != null)
                    {
                        tblPSVehicalDamageDetails.DamageCode = objPortStorageDamageDetailsProp.DamageCode;
                        value = objAppWorksEntities.SaveChanges();
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
            return value;
        }

        /// <summary>
        /// function to Add the Damage Code
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>Int<returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int AddDamageCode(DamageCodeProp objDamageCodeProp)
        {
            int value = 0;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    //Sta: SELECT TOP 1 PSVehicleInspectionID
                    //Sta: FROM PSVehicleInspection
                    //Sta: WHERE PortStorageVehiclesID = @[iVehicleID]
                    //Sta: AND InspectionType = @[iInspectionType]
                    //Sta: ORDER BY PSVehicleInspectionID DESC
                    var getInspectioDetails = (from qry in objAppWorksEntities.PSVehicleInspections where qry.PortStorageVehiclesID == objDamageCodeProp.PortStorageVehiclesID && qry.InspectionType == objDamageCodeProp.PSVehicleInspectionID orderby qry.PSVehicleInspectionID descending select qry).FirstOrDefault();
                    if (getInspectioDetails == null)
                    {
                        System.Data.Entity.Core.Objects.ObjectParameter objReturnId = new System.Data.Entity.Core.Objects.ObjectParameter("rRecordID", typeof(int));
                        System.Data.Entity.Core.Objects.ObjectParameter objReturnCode = new System.Data.Entity.Core.Objects.ObjectParameter("rReturnCode", 0);
                        var getResult = objAppWorksEntities.spCreatePortStorageVehicleInspectionRecord(objDamageCodeProp.PortStorageVehiclesID, objDamageCodeProp.PSVehicleInspectionID, objDamageCodeProp.InspectionDate, objDamageCodeProp.InspectedBy, 0, 0, 0, "", objReturnId, objReturnCode);

                    }

                    if (getInspectioDetails == null)
                    {
                        getInspectioDetails = (from qry in objAppWorksEntities.PSVehicleInspections where qry.PortStorageVehiclesID == objDamageCodeProp.PortStorageVehiclesID && qry.InspectionType == objDamageCodeProp.PSVehicleInspectionID orderby qry.PSVehicleInspectionID descending select qry).FirstOrDefault();
                    }
                    // add damagecode
                    System.Data.Entity.Core.Objects.ObjectParameter rReturnCode = new System.Data.Entity.Core.Objects.ObjectParameter("rReturnCode", typeof(int));
                    var result = objAppWorksEntities.spProcessPortStorageDamageCode(objDamageCodeProp.PortStorageVehiclesID, getInspectioDetails.PSVehicleInspectionID, objDamageCodeProp.DamageCode, objDamageCodeProp.DamageDescription, objDamageCodeProp.InspectionDate, objDamageCodeProp.InspectedBy, rReturnCode);

                    //objAppWorksEntities.Database.ExecuteSqlCommand("spProcessPortStorageDamageCode", objDamageCodeProp.PortStorageVehiclesID, getInspectioDetails.PSVehicleInspectionID, objDamageCodeProp.DamageCode, objDamageCodeProp.DamageDescription, objDamageCodeProp.InspectionDate, objDamageCodeProp.InspectedBy, rReturnCode);

                    if (rReturnCode != null && rReturnCode.Value != null)
                    {
                        value = Convert.ToInt32(rReturnCode);
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return value;
        }


        /// <summary>
        /// function to Add the Inspection Type Only
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List<returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public List<string> InspectionTypeOnly()
        {
            List<string> lstInspectionType = new List<string>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    //var list = (from qrycode in objAppWorksEntities.Codes select qrycode.CodeDescription).ToList();
                    var list = (from qry in objAppWorksEntities.Codes
                                where qry.RecordStatus == "Active" && qry.CodeType == "PSInspectionType"
                                orderby qry.CodeType, qry.SortOrder, qry.Code1
                                select qry.CodeDescription).ToList();
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
            return lstInspectionType;
        }

        /// <summary>
        ///  This Method to Call The Vehical Information For damage details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<PortStorageDamageDetailsProp> BindVehicleDamageDetail(int vehicleId)
        {
            try
            {
                List<PortStorageDamageDetailsProp> data = new List<PortStorageDamageDetailsProp>();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var vehicleDetail = objAppWorksEntities.PortStorageVehicles.FirstOrDefault(x => x.PortStorageVehiclesID == vehicleId);

                    if (vehicleDetail != null)
                    {
                        var vehicleVin = vehicleDetail.VIN;

                        var result = objAppWorksEntities.Database.SqlQuery<PortStorageDamageDetailsStoredProcedureResult>(string.Format(SqlConstants.INSPECTION_DAMAGE_DETAILS_QUERY, vehicleVin)).ToList();

                        data = result.Select(x => new PortStorageDamageDetailsProp
                        {
                            InsPDate = x.InspectionDate,
                            InsPType = x.InspectionType,
                            InsPBy = x.InspectedBy,
                            STI = x.SubjectToInspection,
                            DamageCode = x.DamageCode,
                            Attened = x.AttendedInd,
                            CreatedBy = x.CreatedBy,
                            UpdatedBy = x.UpdatedBy,
                            DamageDetailId = x.PSVehicleDamageDetailID.GetValueOrDefault()
                        }).ToList();
                    }
                }

                return data;
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

using System;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;


namespace AppWorksService.BAL
{

    public class InspectionDamageBAL
    {
        /// <summary>
        /// Function To Load the Location History
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public List<PortStorageLocationHistoryProp> GetLocationHistory(int portStorageVehicalID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                InspectionDamageDAL objInspectionDamageDAL = new InspectionDamageDAL();
                return objInspectionDamageDAL.GetLocationHistory(portStorageVehicalID);
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

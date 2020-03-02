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
    public class PerDiemBAL
    {

        /// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public List<PerDiemProp> GetPerDiemVehicalDetails(int portStorageVehiclesId)
        {
            PerDiemDAL objPerDiemDAL = new PerDiemDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //get the details of Per Diem details.
                return objPerDiemDAL.GetPerDiemVehicalDetails(portStorageVehiclesId);
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

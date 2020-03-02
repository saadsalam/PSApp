using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.DAL;
using AppWorksService.Properties;

namespace AppWorksService.BAL
{
    public class PortStorageDamageDetailBAL
    {
        /// <summary>
        /// function to get edit the Damage Code
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-6,2016</createdOn>
        public int EditDamageCode(PortStorageDamageDetailsProp objPortStorageDamageDetailsProp)
        {
            int value = 0;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageDamageDetailsDAL objPortStorageDamageDetailsDAL = new PortStorageDamageDetailsDAL();
                value = objPortStorageDamageDetailsDAL.EditDamageCode(objPortStorageDamageDetailsProp);
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
        /// function To Get The Inspection Type.
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-12,2016</createdOn>
        public List<string> InspectionTypeOnly()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageDamageDetailsDAL objPortStorageDamageDetailsDAL = new PortStorageDamageDetailsDAL();
                return objPortStorageDamageDetailsDAL.InspectionTypeOnly();
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
        /// function to get the vehicle damge details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public List<PortStorageDamageDetailsProp> BindVehicleDamageDetail(int portStorageVehiclesId)
        {
            PortStorageDamageDetailsDAL objPortStorageDamageDetailsDAL = new PortStorageDamageDetailsDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //get the details of Per Diem details.
                return objPortStorageDamageDetailsDAL.BindVehicleDamageDetail(portStorageVehiclesId);
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
        /// function to Add the Damage Code
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>Int<returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int AddDamageCode(DamageCodeProp objDamageCodeProp)
        {
            PortStorageDamageDetailsDAL objPortStorageDamageDetailsDAL = new PortStorageDamageDetailsDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //add the details of damage code details.
                return objPortStorageDamageDetailsDAL.AddDamageCode(objDamageCodeProp);
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

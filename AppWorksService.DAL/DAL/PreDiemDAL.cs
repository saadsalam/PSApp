using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using System.Data.Entity.SqlServer;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    public class PerDiemDAL
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
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
              // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    // query to get details from database
                    var query = (from qry in objAppWorksEntities.PortStoragePerDiems
                                 where qry.PortStorageVehiclesID == portStorageVehiclesId
                                 select new PerDiemProp
                                 {
                                     PortStoragePerDiemID = SqlFunctions.StringConvert((double)qry.PortStoragePerDiemID),
                                     PerDiemDate = qry.PerDiemDate,
                                     PerDiemVal = qry.PerDiem,
                                     PortStorageVehiclesID = qry.PortStorageVehiclesID,
                                 }
                              ).ToList();
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
    }
}

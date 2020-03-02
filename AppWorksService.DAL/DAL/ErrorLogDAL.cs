using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using AppWorksService.DAL.Edmx;
using System.Reflection;
using System.Globalization;
using AppWorksService.Properties;

namespace AppWorksService.DAL.DAL
{
    public class ErrorLogDAL 
    {
        public void LogErrorToDb(ErrorLogProp errorProp)
        {
            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    objAppWorksEntities.ErrorLogs.Add(new ErrorLog { SystemMac = errorProp.SystemMac, DateTime = DateTime.Now, Message = errorProp.Message, Level = "ERROR", Logger = errorProp.Logger, Thread = errorProp.Thread,BuildVersion=errorProp.BuildVersion });
                    objAppWorksEntities.SaveChanges();
                }
            }
            catch (Exception e)
            {

                CommonDAL.logger.LogError(typeof(string), e);
            }
            finally
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
           
        }
    }
}
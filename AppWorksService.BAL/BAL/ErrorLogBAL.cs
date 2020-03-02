using AppWorksService.DAL;
using AppWorksService.DAL.DAL;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.BAL.BAL
{
    public class ErrorLogBAL
    {
        public void LogErrorToDb(ErrorLogProp errorProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                ErrorLogDAL objErrorLogDAL = new ErrorLogDAL();
                objErrorLogDAL.LogErrorToDb(errorProp);
            }
            catch (Exception e)
            {
                CommonDAL.logger.LogError(typeof(string), e);
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }
    }
}

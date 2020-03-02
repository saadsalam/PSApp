using System;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.DAL;
using AppWorksService.Properties;

namespace AppWorksService.BAL
{
    /// <summary>
    /// BusinessAccessLayer To Find User Module
    /// </summary>
    public class FindUserBAL
    {
        /// <summary>
        /// Function To Get The All Users Role.
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<string> RoleList()
        {
            List<string> objRoleList = new List<string>();
            try
            {
                FindUserDAL objFindUserDAL = new FindUserDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objRoleList=objFindUserDAL.RoleList();

            }catch(Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End4 {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objRoleList;
        }

        /// <summary>
        /// Function To Get The All Record Status.
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<string> RecordStatus()
        {
            List<string> objRecordStatusList = new List<string>();
            try
            {
                FindUserDAL objFindUserDAL = new FindUserDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objRecordStatusList = objFindUserDAL.RecordStatus();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End4 {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objRecordStatusList;
        }

        /// <summary>
        /// Function To Get The Users Lists.
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<FindUserDetails> GetUserRecord(FindUserProp objFindUserProp)
        {
            List<FindUserDetails> objFindUserDetails = new List<FindUserDetails>();
            try
            {
               FindUserDAL objFindUserDAL = new FindUserDAL();
               CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
               objFindUserDetails= objFindUserDAL.GetUserRecord(objFindUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End4 {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objFindUserDetails;
        }
        /// <summary>
        /// Function To Remove the User Details
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>
        /// Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-29,2016</createdOn>
        public int RemoveUserDetails(FindUserProp objFindUserProp)
        {
            int value = 0;
            try
            {
                FindUserDAL objFindUserDAL = new FindUserDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                value = objFindUserDAL.RemoveUserDetails(objFindUserProp);
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
    }
}

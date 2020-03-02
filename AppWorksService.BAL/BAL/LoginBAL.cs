using System;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;

namespace AppWorksService.BAL
{
    /// <summary>
    /// BusinessAccessLayer for Login functionality
    /// </summary>

    public class LoginBAL
    {
        /// <summary>
        /// Function to Get the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>Bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public LoginResult ValidateLogin(LoginProperties objLoginProp)
        {
            LoginResult result = new LoginResult();
            LoginDAL objLoginDAL = new LoginDAL(); /// Creating Object for LoginDAL Class 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                /// Calling Validate Login Mentod from LoginDAL
                result = objLoginDAL.ValidateLogin(objLoginProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw ex;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }

        /// <summary>
        /// Get all the user details about once login is verified
        /// </summary>
        /// <param name="loginProps"></param>
        /// <returns></returns>
        public LoginProperties GetLoggedInUserDetails(LoginProperties loginProps)
        {
            LoginDAL loginDAL = new LoginDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {                
                loginProps = loginDAL.GetLoggedInUserDetails(loginProps);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return loginProps;
        }

        /// <summary>
        /// Function to Get the RoleName of Logged User.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-19,2016</createdOn>
        public List<string> GetUserRole(LoginProperties objLoginProp)
        {
            LoginDAL objLoginDAL = new LoginDAL();  /// Creating Object for LoginDAL. 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            List<string> objUserRoleList = new List<string>();
            try
            {
                /// Calling GetUserRole Method from LoginDal and Get Logged User User Role.
                objUserRoleList = objLoginDAL.GetUserRole(objLoginProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objUserRoleList;
        }

        /// <summary>
        /// Function to get the information of Logged User.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-20,2016</createdOn>
        public string GetUserName(LoginProperties objLoginProp)
        {
            LoginDAL objLoginDAL = new LoginDAL();  /// Creating Object for LoginDAL. 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            string userName = string.Empty;
            try
            {
                /// Calling UserName Method for User's Name.
                userName = objLoginDAL.LoggedUser(objLoginProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return userName;
        }
    }
}

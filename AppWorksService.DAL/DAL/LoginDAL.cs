using System;
using System.Net;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    /// <summary>
    /// DataAccessLayer for Login functionality
    /// </summary>

    public class LoginDAL
    {
        /// <summary>
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>Bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public LoginResult ValidateLogin(LoginProperties objLoginProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {              
                LoginResult result = new LoginResult();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    SqlParameter objSqlParam = new SqlParameter();
                    var procResult = new SqlParameter
                    {
                        ParameterName = "@procResult",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Output,
                        Size = 50
                    };
                    var paramUserCode = new SqlParameter
                    {
                        ParameterName = "@UserCode",
                        Value = objLoginProp.Username,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    var paramPassword = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = objLoginProp.Password,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    var paramHostName = new SqlParameter
                    {
                        ParameterName = "@HostName",
                        Value = Dns.GetHostName(),
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    var paramBuildDate = new SqlParameter
                    {
                        ParameterName = "@BuildDate",
                        Value = DateTime.Now,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input
                    };
                    /// Calling The Stored Procedure
                    objAppWorksEntities.Database.ExecuteSqlCommand("exec @procResult = spDATSLogin @UserCode, @Password,@HostName,@BuildDate", paramUserCode, paramPassword, paramHostName, paramBuildDate, procResult);
                    var resultValue = procResult.Value;
                    if (resultValue != null)
                    {
                        if (Convert.ToInt32(resultValue) == 0)
                        {
                            result.IsLoginSuccessful = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonDAL.logger.LogError(this.GetType(), ex);
                    throw;
                }
                finally
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }

                return result;
            }
        }

        /// <summary>
        /// Function to check the User Role of  Logged User.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-19,2016</createdOn>
        public List<string> GetUserRole(LoginProperties objLoginProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                List<string> userRolelist = new List<string>();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {

                    int userId = (from T in objAppWorksEntities.Users
                                  where T.UserCode == objLoginProp.Username
                                  && (T.Password == objLoginProp.Password || T.PIN == objLoginProp.Password)
                                  select T.UserID).FirstOrDefault();

                    if (userId != 0)
                    {
                        userRolelist = (from userRole in objAppWorksEntities.UserRoles
                                        where userRole.UserID == userId
                                        select userRole.RoleName).ToList();
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

                return userRolelist;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        public LoginProperties GetLoggedInUserDetails(LoginProperties loginProps)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                var userDetails = (from user in objAppWorksEntities.Users
                                   where user.UserCode == loginProps.Username
                                   && (user.Password == loginProps.Password || user.PIN == loginProps.Password)
                                   select user).FirstOrDefault();

                if (userDetails != null)
                {
                    //if FirstName OR LastName is empty then show same Username on Main window
                    if (string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName))
                    {
                        loginProps.FullUserName = loginProps.Username;
                    }
                    else
                    {
                        loginProps.FullUserName = string.Format("{0} {1}", userDetails.FirstName, userDetails.LastName);
                    }

                    loginProps.UserRoles = (from userRole in objAppWorksEntities.UserRoles
                                            where userRole.UserID == userDetails.UserID
                                            select userRole.RoleName).ToList();
                }
            }

            return loginProps;
        }

        /// <summary>
        /// Function to Get Logged User Information.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-20,2016</createdOn>
        public string LoggedUser(LoginProperties objLoginProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                string fullName = string.Empty;

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {

                    var userDetails = (from user in objAppWorksEntities.Users
                                       where user.UserCode == objLoginProp.Username
                                       && (user.Password == objLoginProp.Password || user.PIN == objLoginProp.Password)
                                       select user).FirstOrDefault();

                    if (userDetails != null)
                    {
                        //if FirstName OR LastName is empty then show same Username on Main window
                        if (string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName))
                        {
                            fullName = objLoginProp.Username;
                        }
                        else
                        {
                            fullName = string.Format("{0} {1}", userDetails.FirstName, userDetails.LastName);
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

                return fullName;
            }
        }
    }
}

using System;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;

namespace AppWorksService.BAL
{
    /// <summary>
    /// BusinessAccessLayer for UserAdmin functionality
    /// </summary>
    public class AdminUserBAL
    {
        /// <summary>
        /// Function to check the Existance of Current User.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>Bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-19,2016</createdOn>

        public bool IsUserExists(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            bool result;
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();  /// Creating The Object of AdminUserDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                result = objAdminUserDAL.IsUserExists(objAdminUserProp, lstRoles); /// Getting The Result of Calling Method IsUserExists.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }

        /// <summary>
        /// Function foe get the role of current user.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-22,2016</createdOn>
        public List<string> ExistsUserRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstExistsRole = new List<string>();
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();  /// Creating The Object of AdminUserDAL
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                objlstExistsRole = objAdminUserDAL.EixstingRoleList(objAdminUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objlstExistsRole;
        }

        /// <summary>
        /// Function To Chanege the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> AllRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstNewRole = new List<string>();
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();  /// Creating The Object of AdminUserDAL
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                objlstNewRole = objAdminUserDAL.AllRoleList(objAdminUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objlstNewRole;
        }

        /// <summary>
        /// Function To Chanege the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> ModifiedUserList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstNewRole = new List<string>();
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();  /// Creating The Object of AdminUserDAL
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                objlstNewRole = objAdminUserDAL.NewRoleList(objAdminUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objlstNewRole;
        }

        /// <summary>
        /// Function To Remove the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> RemovedUserList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstNewRole = new List<string>();
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();  /// Creating The Object of AdminUserDAL
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling and binding the existing user Role.
                objlstNewRole = objAdminUserDAL.RemoveRoleList(objAdminUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objlstNewRole;
        }

        /// <summary>
        /// Function To Modify the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public AdminUserDeatils GetModificationRecord(AdminUserProp objAdminUserProp)
        {
            AdminUserDeatils objAdminUserDeatils = new AdminUserDeatils();
            AdminUserDAL objAdminUserDAL = new AdminUserDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objAdminUserDeatils = objAdminUserDAL.GetModificationRecord(objAdminUserProp);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objAdminUserDeatils;
        }

        /// <summary>
        /// Function To Update the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public void UpdateUserDetails(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            try
            {
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objAdminUserDAL.UpdateUserDetails(objAdminUserProp, lstRoles);
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
        /// Function To Update the user role  .
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public void DeleteUserRecord(AdminUserProp objAdminUserProp)
        {
            try
            {
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objAdminUserDAL.RemoveUserDetails(objAdminUserProp);
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
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleList> GetRolesSelection(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();

                return objAdminUserDAL.GetRolesSelection(userID);
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
        /// To get list of system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<AdminUserProp> GetSystemSettings()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                return objAdminUserDAL.GetSystemSettings();
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
        /// To update system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdateSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                return objAdminUserDAL.UpdateSystemSettings(setting);
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
        /// find system settings
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public List<AdminUserProp> FindSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                return objAdminUserDAL.FindSystemSettings(setting);

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
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool DeleteSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                return objAdminUserDAL.DeleteSystemSettings(setting);
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
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool InsertSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserDAL objAdminUserDAL = new AdminUserDAL();
                return objAdminUserDAL.InsertSystemSettings(setting);
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

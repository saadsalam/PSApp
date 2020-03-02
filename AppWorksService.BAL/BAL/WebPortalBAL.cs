using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.DAL;
using AppWorksService.Properties;
using System.Globalization;
using System.Reflection;

namespace AppWorksService.BAL
{
    public  class WebPortalBAL
    {
        WebPortalDAL objWebPortalDAL = new WebPortalDAL();
        
        /// <summary>
        /// To get the user list for customer web portal.
        /// </summary>
        /// <returns></returns>
        public List<WebPortalUserList> GetUsers(WebPortalUserList objUserList)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objWebPortalDAL.GetUsers(objUserList);
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
        /// To get list of all modules with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ModuleList> GetModules(int userID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objWebPortalDAL.GetModules(userID);
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
        /// To get list of all customer with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserCustomerList> GetCustomers(int userID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objWebPortalDAL.GetCustomers(userID);
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
        /// For inserting New User in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUser(WebPortalUserList objUser)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalDAL.AddUser(objUser);
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
        /// For inserting Customers of a user in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserCustomer(UserCustomerList objUserCustomer)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalDAL.AddUserCustomer(objUserCustomer);
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
        /// For inserting user modules in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserModule(ModuleList objModuleUser)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalDAL.AddUserModule(objModuleUser);
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
        ///  This Method to deete a particular user
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteUser(int userID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalDAL.DeleteUser(userID);
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
        public List<RoleList> GetRoles(int userID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objWebPortalDAL.GetRoles(userID);
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
        /// To get list of all groups with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GroupList> GetGroups(int userID)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objWebPortalDAL.GetGroups(userID);
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
        /// Method for inserting a new user with customers,modules,roles and groups
        /// </summary>
        /// <param name="objUser"></param>
        /// <param name="lstUserCustomer"></param>
        /// <param name="lstUserModules"></param>
        /// <param name="lstUserRoles"></param>
        /// <param name="lstUserGroups"></param>
        /// <returns></returns>
        public bool InsertUpdateUser(WebPortalUserList objUser, List<UserCustomerList> lstUserCustomer, List<ModuleList> lstUserModules, List<RoleList> lstUserRoles, List<GroupList> lstUserGroups)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                return objWebPortalDAL.InsertUpdateUser(objUser, lstUserCustomer, lstUserModules, lstUserRoles, lstUserGroups);
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
        /// method for checking duplicate emailid
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicateEmail(string emailID)
        {
            try
            {
                return objWebPortalDAL.CheckDuplicateEmail(emailID);
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
        /// method for checking duplicate username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicatePortalUserName(string userName)
        {
            try
            {
                return objWebPortalDAL.CheckDuplicatePortalUserName(userName);
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
        /// To get the vehicle inventory details coressponding to vin number and customer id.
        /// </summary>
        /// <param name="vinNUmber"></param>
        /// <param name="customerID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public List<PortStorageVehicleProp> GetPortStorageInventoryDetails(PortStorageVehicleProp objPortStorageProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                return objWebPortalDAL.GetPortStorageInventoryDetails(objPortStorageProp);
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
        ///  This Method is to update the complete list of  request checked vehicles
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool UpdateRequestCheckedVehicles(List<PortStorageVehicleProp> lstRequestedVechicles, string user)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalDAL.UpdateRequestCheckedVehicles(lstRequestedVechicles, user);
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

using System;
using AppWorksService.BAL;
using AppWorksService.Properties;
using System.Collections.Generic;
using AppWorksService.DAL;
using System.Globalization;
using System.Reflection;
using Props = AppWorks.Utilities;
using System.Threading.Tasks;
using System.ServiceModel;
using AppWorks.WCFAuthentication.Lib.Behaviours;
using AppWorksService.BAL.BAL;

namespace AppWorksService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    //Implementing service as PerSession instance
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Single)]
    [CustomInspectorBehavior]
    public class AppWorksService : IAppWorksServices
    {
        /// <summary>
        /// Method to validate the Login. 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>13-Apr-2016</createddate>
        public LoginResult ValidateLogin(LoginProperties objLoginProp)
        {
            LoginResult result = new LoginResult();

            LoginBAL objLoginBAL = new LoginBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = objLoginBAL.ValidateLogin(objLoginProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            if (result.IsLoginSuccessful)
            {
                result.Token = OperationContext.Current.SessionId;
            }
            else
            {
                result.Token = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        public LoginProperties GetLoggedInUserDetails(LoginProperties loginProps)
        {
            LoginBAL objLoginBAL = new LoginBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                loginProps = objLoginBAL.GetLoggedInUserDetails(loginProps);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return loginProps;
        }

        /// <summary>
        /// Method to Check the User Role of the Logged User. 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>19-Apr-2016</createddate>
        public List<string> GetUserRole(LoginProperties objLoginProp)
        {
            LoginBAL objLoginBAL = new LoginBAL(); /// Creating Object for LoginBAL.
            List<string> userRoleList = new List<string>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                /// Calling the Role of Logged User in Role Name Variable.
                userRoleList = objLoginBAL.GetUserRole(objLoginProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return userRoleList;
        }

        /// <summary>
        /// Method to get Logged User's Name. 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>19-Apr-2016</createddate>
        public string UserName(LoginProperties objLoginProp)
        {
            LoginBAL objLoginBAL = new LoginBAL(); /// Creating Object for LoginBAL.
            string username = string.Empty;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                /// Calling the Role of Logged User in Role Name Variable.
                username = objLoginBAL.GetUserName(objLoginProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return username;
        }

        /// <summary>
        /// Method to Check the Existance of the Logged User. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>19-Apr-2016</createddate>
        public bool IsUserExists(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            bool result;
            AdminUserBAL objAdminUserBAL = new AdminUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                /// Method To Check The Existing User Status.
                result = objAdminUserBAL.IsUserExists(objAdminUserProp, lstRoles);
                if (result)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                result = false;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }

        /// <summary>
        /// Method to get the current role for logged user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>22-Apr-2016</createddate>
        public List<string> ExistingUserRole(AdminUserProp objAdminUserProp)
        {
            List<string> objExistsUserRole = new List<string>();
            AdminUserBAL objAdminUserBAL = new AdminUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objExistsUserRole = objAdminUserBAL.ExistsUserRoleList(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objExistsUserRole;
        }

        /// <summary>
        /// Method to get the All Role for user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>23-Apr-2016</createddate>
        public List<string> AllRoles(AdminUserProp objAdminUserProp)
        {
            List<string> objNewUserRole = new List<string>();
            AdminUserBAL objAdminUserBAL = new AdminUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objNewUserRole = objAdminUserBAL.AllRoleList(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objNewUserRole;
        }

        /// <summary>
        /// Method to get the Modified or Assigned role for user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>23-Apr-2016</createddate>
        public List<string> ModifiedUserRole(AdminUserProp objAdminUserProp)
        {
            List<string> objNewUserRole = new List<string>();
            AdminUserBAL objAdminUserBAL = new AdminUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objNewUserRole = objAdminUserBAL.ModifiedUserList(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objNewUserRole;
        }

        /// <summary>
        /// Method to get the Modified or Removed role for user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>23-Apr-2016</createddate>
        public List<string> RemoveUserRole(AdminUserProp objAdminUserProp)
        {
            List<string> objNewUserRole = new List<string>();
            AdminUserBAL objAdminUserBAL = new AdminUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objNewUserRole = objAdminUserBAL.RemovedUserList(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objNewUserRole;
        }

        /// <summary>
        /// Method to get the Modified or Removed role for user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>26-Apr-2016</createddate>
        public List<string> RecordList()
        {
            List<string> objRecordList = new List<string>();
            FindUserBAL objFindUserBAL = new FindUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objRecordList = objFindUserBAL.RecordStatus();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objRecordList;
        }

        /// <summary>
        /// Method to get the Role List for Find User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>26-Apr-2016</createddate>
        public List<string> RoleList()
        {
            List<string> objRoleList = new List<string>();
            FindUserBAL objFindUserBAL = new FindUserBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objRoleList = objFindUserBAL.RoleList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objRoleList;
        }

        /// <summary>
        /// Method to get the List of User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>26-Apr-2016</createddate>
        public List<FindUserDetails> GetUserRecord(FindUserProp objFindUserProp)
        {
            List<FindUserDetails> objFindUserDetails = new List<FindUserDetails>();
            try
            {
                FindUserBAL objFindUserBAL = new FindUserBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objFindUserDetails = objFindUserBAL.GetUserRecord(objFindUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objFindUserDetails;
        }

        /// <summary>
        /// Method to get the Modification Record
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>28-Apr-2016</createddate>
        public AdminUserDeatils GetModificationRecord(AdminUserProp objAdminUserProp)
        {
            AdminUserDeatils objAdminUserDetails = new AdminUserDeatils();
            try
            {
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objAdminUserDetails = objAdminUserBAL.GetModificationRecord(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return objAdminUserDetails;
        }

        /// <summary>
        /// Method to Save The Updated Record
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>28-Apr-2016</createddate>
        public void UpdateUserDetails(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            try
            {
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objAdminUserBAL.UpdateUserDetails(objAdminUserProp, lstRoles);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Method to Remove The Record
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>28-Apr-2016</createddate>
        public void DeleteUserDetails(AdminUserProp objAdminUserProp)
        {
            try
            {
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objAdminUserBAL.DeleteUserRecord(objAdminUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Method to Remove The Record
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>29-Apr-2016</createddate>
        public int RemoveUserDetails(FindUserProp objFindUserProp)
        {
            int Value = 0;
            try
            {
                FindUserBAL objFindUserBAL = new FindUserBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                Value = objFindUserBAL.RemoveUserDetails(objFindUserProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return Value;
        }

        /// <summary>
        /// Method to get the Modified or Removed role for user. 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>23-Apr-2016</createddate>
        public async Task<List<VehicleProp>> GetVehicleSearchDetails(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for AdminUserBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return await objVehicleBAL.GetVehicleSearchDetails(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        #region "Customer Related Functions"
        /// <summary>
        /// Method to get the details of customer searched. 
        /// </summary>
        /// <param name="objCustomerProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>4-May-2016</createddate>
        public List<CustomerSearchProp> GetVehicleSearchDetails(CustomerSearchProp objCustomerProp)
        {
            CustomerBAL objVehicleBAL = new CustomerBAL(); /// Creating the Object for CustomerBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.GetCustomerSearchDetails(objCustomerProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Method to insert the vehicle details of customer. 
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>4-May-2016</createddate>
        public int InsertVehicleDetails(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for CustomerBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.InsertVehicleDetails(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>// Mark
        public List<PerDiemProp> GetPerDiemVehicalDetails(int portStorageVehiclesId)
        {
            PerDiemBAL objPerDiemBAL = new PerDiemBAL(); /// Creating the Object for CustomerBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objPerDiemBAL.GetPerDiemVehicalDetails(portStorageVehiclesId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Funtion To Edit The Damage Code.
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public int EditDamageCode(PortStorageDamageDetailsProp objPortStorageDamageDetailsProp)
        {
            int result = 0;
            PortStorageDamageDetailBAL objPortStorageDamageDetailBAL = new PortStorageDamageDetailBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = objPortStorageDamageDetailBAL.EditDamageCode(objPortStorageDamageDetailsProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }

        /// <summary>
        /// Funtion To Edit The Damage Code.
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public List<CustomerSearchProp> GetCustomerSearchDetails(CustomerSearchProp objCustomerProp)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objCustomerBAL.GetCustomerSearchDetails(objCustomerProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        public List<PerDiemProp> GetVehiclePerDiemSearchDetails(int portStorageVehiclesId)
        {
            PerDiemBAL objPerDiemBAL = new PerDiemBAL(); /// Creating the Object for CustomerBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objPerDiemBAL.GetPerDiemVehicalDetails(portStorageVehiclesId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        #endregion

        /// <summary>
        /// This method is used to update Port Storage Processign Details
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public int UpdatePortStorageProcessignDetails(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.UpdatePortStorageProcessignDetails(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This Method is used to Get Port Storage Processign Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public List<VehicleProp> GetPortStorageProcessignDetails(string VIN)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.GetPortStorageProcessignDetails(VIN);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to update Port Storage Processign for date out Details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int UpdatePortStorageDateOutProcessignDetails(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.UpdatePortStorageDateOutProcessignDetails(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This Method is used to Get Port Storage Processign Details for date out
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<VehicleProp> GetPortStorageDateOutProcessignDetails(string VIN)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.GetPortStorageDateOutProcessignDetails(VIN);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This Method is used to Get List of Customer Type
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<string> CustomerType()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CustomerBAL objCustomerBAL = new CustomerBAL();
                return objCustomerBAL.CutomerType();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This Method is used to Decode VIN
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public int DecodeVIN(string Vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.DecodeVIN(Vin);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        /// <summary>
        /// This Method is used to Call All Vehicals Locations
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<PortStorageLocationHistoryProp> GetLocationHistory(int portStorageVehicalID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                InspectionDamageBAL objInspectionDamageBAL = new InspectionDamageBAL();
                return objInspectionDamageBAL.GetLocationHistory(portStorageVehicalID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        public VehicleProp GetVecheleDetailByVIN(string VIN, int? vehicleId)
        {
            try
            {
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.GetVecheleDetailByVIN(VIN, vehicleId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to update the  Port Storage request date 
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-12,2016</createdOn>
        public string RequestBatchProcess(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.RequestBatchProcess(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to update the  Port Storage date out
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-12,2016</createdOn>
        public string DateOutBatchProcess(VehicleProp objVehicleProp)
        {
            VehicleBAL objVehicleBAL = new VehicleBAL(); /// Creating the Object for VehicleBAL
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.DateOutBatchProcess(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        public List<VehicleProp> CheckMultipleVecheleDetailByVIN(string VIN)
        {
            try
            {
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.CheckMultipleVecheleDetailByVIN(VIN);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// This Method is used for Inspection Type.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-12,2016</createdOn>
        public List<string> InspectionTypeOnly()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageDamageDetailBAL objPortStorageDamageDetailBAL = new PortStorageDamageDetailBAL();
                return objPortStorageDamageDetailBAL.InspectionTypeOnly();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to use the Fill Vehical Status
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<string> VehicalStatusList()
        {
            try
            {
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.VehicalStatusList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to use the Fill Vehical Status
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public List<VehicleProp> CallVehialDetailsbyVIN(VehicleProp objVehicleProp)
        {
            try
            {
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.CallVehialDetailsbyVIN(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method Used To Update The Vehical Details Record.
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool UpdateVehicalSearchDetails(VehicleProp objVehicleProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.UpdateVehicalSearchDetails(objVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        public List<PortStorageDamageDetailsProp> BindVehicleDamageDetail(int portStorageVehicalID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageDamageDetailBAL objPortStorageDamageDetailBAL = new PortStorageDamageDetailBAL();
                return objPortStorageDamageDetailBAL.BindVehicleDamageDetail(portStorageVehicalID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        public int AddDamageCode(DamageCodeProp objDamageCodeProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageDamageDetailBAL objPortStorageDamageDetailBAL = new PortStorageDamageDetailBAL();
                return objPortStorageDamageDetailBAL.AddDamageCode(objDamageCodeProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This function is used to delete vehicle details.
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        public bool DeleteVehicalSearchDetails(int VehicleId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.DeleteVehicalSearchDetails(VehicleId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This function is use to insert billing period detials.
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns></returns>
        public int AddBillingPeriod(BillingPeriodprop objBillingPeriodprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();
                return objBillingBAL.AddBillingPeriodAdmin(objBillingPeriodprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is use to get billing period detailds for bind grid
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns></returns>
        public IList<BillingPeriodprop> FindBillingPeriod(BillingPeriodprop objBillingPeriodprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();
                return objBillingBAL.FindBillingPeriod(objBillingPeriodprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This function is used to insert code admin details.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        public int AddCodeAdmin(CodeProp objCodeProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeBAL objCodeBAL = new CodeBAL();
                return objCodeBAL.AddCodeAdmin(objCodeProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to use the Fill Code type
        /// </summary>
        /// <returns>List</returns>
        public List<string> CodeTypeList()
        {
            try
            {
                CodeBAL objCodeBAL = new CodeBAL();
                return objCodeBAL.CodeTypeList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This function Delete Billing Period Admin Details
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        public bool DeleteBillingPeriodAdminDetails(int BillingPeriodID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();
                return objBillingBAL.DeleteBillingPeriodAdminDetails(BillingPeriodID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This function is used update Billing Period Admin Details
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns></returns>
        public bool UpdateBillingPeriodAdminDetails(BillingPeriodprop objBillingPeriodprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();
                return objBillingBAL.UpdateBillingPeriodAdminDetails(objBillingPeriodprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to use the Find Code
        /// </summary>
        /// <returns>List</returns>
        public IList<CodeProp> FindCode(CodeProp objCodeProp)
        {
            try
            {
                CodeBAL objCodeBAL = new CodeBAL();
                return objCodeBAL.FindCode(objCodeProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This function is used to update record of code admin details
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        public bool ModifyCodeAdminRecord(CodeProp objCodeProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeBAL objCodeBAL = new CodeBAL();
                return objCodeBAL.UpdateCodeAdminDetails(objCodeProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This function is used to delete code admin detils.
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public bool DeleteCodeAdminDetails(int CodeID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeBAL objCodeBAL = new CodeBAL();
                return objCodeBAL.DeleteCodeAdminDetails(CodeID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// this is used to Get Port Storage Invoices List  
        /// </summary>
        /// <returns></returns>
        public IList<PortStorageInvoicesProp> GetPortStorageInvoicesList(Nullable<DateTime> cutoffDate)
        {

            try
            {
                PortStorageInvoicesBAL objPortStorageInvoicesBAL = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBAL.GetPortStorageInvoicesList(cutoffDate);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Get the code list coressponding to the given code type
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public List<CodeList> LoadCodeList(string codeType)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CustomerBAL objCustomerBAL = new CustomerBAL();
                return objCustomerBAL.LoadCodeList(codeType);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Get Location List for a perticular location type or all location type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="locationType"></param>
        /// <returns></returns>
        public List<LocationList> GetLocationList(LocationList objLocationProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CustomerBAL objCustomerBAL = new CustomerBAL();
                return objCustomerBAL.GetLocationList(objLocationProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the data for billing address for a perticular customer.
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public List<LocationList> GetBillingStreetAddress(int addressID)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.GetBillingStreetAddress(addressID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        #region Code should be removed after cleanup
        ///// <summary>
        ///// To get the for from location .
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> FromLocationList(int customerID)
        //{
        //    CustomerBAL objCustomerBAL = new CustomerBAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerBAL.FromLocationList(customerID);
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDAL.logger.LogError(this.GetType(), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// To get the data for to location .
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> ToLocationList(int customerID)
        //{
        //    CustomerBAL objCustomerBAL = new CustomerBAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerBAL.ToLocationList(customerID);
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDAL.logger.LogError(this.GetType(), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}





        ///// <summary>
        ///// To get the order history for a perticular customer.
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <param name="orderStatus"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public List<OrderHistoryList> GetOrderHistory(int customerID, string orderStatus, DateTime? startDate, DateTime? endDate)
        //{
        //    CustomerBAL objCustomerBAL = new CustomerBAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerBAL.GetOrderHistory(customerID, orderStatus, startDate, endDate);
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDAL.logger.LogError(this.GetType(), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// this is used to Get Port Storage print error Invoices List  
        ///// </summary>
        ///// <returns></returns>
        //public IList<PortStoragePrintErrorProp> GetPrintErrorsForInvoiceList()
        //{
        //    try
        //    {
        //        PortStorageInvoicesBAL objPortStorageInvoicesBAL = new PortStorageInvoicesBAL();
        //        return objPortStorageInvoicesBAL.GetPrintErrorsForInvoiceList();
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDAL.logger.LogError(this.GetType(), ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        #endregion


        /// <summary>
        /// To get the notes list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<CustomerNoteList> NotesList(int customerID, DateTime? startDate, DateTime? endDate)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.GetNotesList(customerID, startDate, endDate);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PortStorageRateList> GetPortStorageRateList(PortStorageRateList objPortStorageRateProp)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.GetPortStorageRateList(objPortStorageRateProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Get the origin location list for Location Performance Standard
        /// </summary>
        /// <returns></returns>
        public bool AddPortStorageRate(PortStorageRateList objPortStorageRateProp)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.AddPortStorageRate(objPortStorageRateProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        /// <summary>
        /// For updating locationemailcontact information in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdatePortStorageRate(PortStorageRateList objPortStorageRateProp)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.UpdatePortStorageRate(objPortStorageRateProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        /// <summary>
        ///  This Method to deete the location details
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeletePortStorageRate(int portStorageRateID)
        {
            CustomerBAL objCustomerBal = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBal.DeletePortStorageRate(portStorageRateID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Get the origin location list for Location Performance Standard
        /// </summary>
        /// <returns></returns>
        public List<LocationList> LoadPerformanceStndrdOriginLocationList()
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.LoadPerformanceStndrdOriginLocationList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.AddLocationEmailContact(objLocationEmailContact);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting LocationPerformancestandard in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.AddLocationPerformanceStandard(objLocationPerformanceStandard);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For updating locationemailcontact information in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.UpdateLocationEmailContact(objLocationEmailContact);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For updating LocationPerformancestandard informance in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.UpdateLocationPerformanceStandard(objLocationPerformanceStandard);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public int AddLocation(LocationList objLocation)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.AddLocation(objLocation);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.AddCustomerNotes(objCustomerNotes);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.UpdateCustomerNotes(objCustomerNotes);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For updating other information of customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateCustomerSearchDetails(CustomerSearchProp objcustomer)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.UpdateCustomerSearchDetails(objcustomer);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting Customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public int InsertCustomer(CustomerSearchProp objCustomer)
        {
            CustomerBAL objCustomerBAL = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBAL.InsertCustomer(objCustomer);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to deete the location details
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteLocation(int locationID)
        {
            CustomerBAL objCustomerBal = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBal.DeleteLocation(locationID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// function UpdateBilling to update the billing details.
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns></returns>
        public bool UpdateBilling(BillingProp objBillingprop)
        {
            try
            {
                PortStorageInvoicesBAL objPsInvoicesBal = new PortStorageInvoicesBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objPsInvoicesBal.UpdateBilling(objBillingprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to insert the invoice billing details
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public int InsertBillingId(BillingProp objBillingprop)
        {
            try
            {
                PortStorageInvoicesBAL objPsInvoicesBal = new PortStorageInvoicesBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objPsInvoicesBal.InsertBillingId(objBillingprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the port storage vehicle records for invoice generation
        /// </summary>
        /// <param name="objPortStorageVehicleProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        public bool UpdatePostStorageVehicles(PortStorageVehicleProp objPortStorageVehicleProp)
        {
            try
            {
                PortStorageInvoicesBAL objPsInvoicesBal = new PortStorageInvoicesBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objPsInvoicesBal.UpdatePostStorageVehicles(objPortStorageVehicleProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the setting details
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public string SetDefaultvalue(string valueKey)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.SetDefaultvalue(valueKey);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the Next invoice number value
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public bool UpdateSettingsValue(string invoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.UpdateSettingsValue(invoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }



        /// <summary>
        /// Function to get the billing details count
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public int GetBillingCount(string invoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesDal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesDal.GetBillingCount(invoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get thevehicle list for generate invoice
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <param name="customerId"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public IList<PortStorageVehicleProp> GetPortStorageVehicleList(Nullable<DateTime> cutoffDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetPortStorageVehicleList(cutoffDate, customerId); /// sending the new record model to the data access layer.
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateLocation(LocationList objLocation)
        {
            CustomerBAL objCustomerBal = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBal.UpdateLocation(objLocation);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to calculate the per diem for generate invoice
        /// </summary>
        /// <param name="psVehicleId"></param>
        /// <param name="userCode"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        public string CalculatePortStoragePerDiem(int psVehicleId, string userCode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.CalculatePortStoragePerDiem(psVehicleId, userCode);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public int PsRatesCount(DateTime? startDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.PsRatesCount(startDate, customerId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public PSRatesInvoiceProp PsRatesInvoice(DateTime? startDate, int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.PsRatesInvoice(startDate, customerId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to update the rate details in port storage table
        /// </summary>
        /// <param name="entryRate"></param>
        /// <param name="perDiemGraceDays"></param>
        /// <param name="vehicleID"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        public bool UpdateVehicleRates(decimal? entryRate, int? perDiemGraceDays, int vehicleID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.UpdateVehicleRates(entryRate, perDiemGraceDays, vehicleID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to check duplicate code and codetype values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool CheckDuplicateCodeAndType(string code, string codeType)
        {
            CodeBAL objCodeBAL = new CodeBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objCodeBAL.CheckDuplicateCodeAndType(code, codeType);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the customer information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public CustomerSearchProp GetCustomerInfo(int customerId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetCustomerInfo(customerId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the outside carrier leg list details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public List<LoadInfoList> GetLoadInfo(int billingId, decimal pvRatePercentage)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetLoadInfo(billingId, pvRatePercentage);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the table column names
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>List<string></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        public List<string> GetTableColumnNames(string tableName)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetTableColumnNames(tableName);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to check duplicate calender  and period values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool CheckDuplicateCalenderAndPeriod(int year, int period)
        {
            BillingBAL objBillingBAL = new BillingBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.CheckDuplicateCalenderAndPeriod(year, period);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the vehicle leg list count details
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>List<VehicleLegCountProp></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public List<VehicleLegCountProp> GetVehicleLegsInfo(int recordId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetVehicleLegsInfo(recordId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get the Invoice Credit Cost Center Number
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public int GetInvoiceCreditCostCenterNumber()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.GetInvoiceCreditCostCenterNumber();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to insert the invoice billing line items details
        /// </summary>
        /// <param name="BillingLineItemsProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        public int InsertBillingLineItems(BillingLineItemsProp objBillingLineprop)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.InsertBillingLineItems(objBillingLineprop);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For Updating customer address in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateCustomerAddredss(string addressType, int customerID, int locationID)
        {
            CustomerBAL objCustomerBal = new CustomerBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBal.UpdateCustomerAddredss(addressType, customerID, locationID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting address type of customer from the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public string GetCustomerAddredssType(int locationID, int customerID)
        {
            CustomerBAL objCustomerBal = new CustomerBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerBal.GetCustomerAddredssType(locationID, customerID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to get the code list.
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="codeDesc"></param>
        /// <returns></returns>
        /// <createdOn>Jun-06-2016</createdon>
        public IList<CodeProp> GetCodeDetailsForInvoice(string codeType, string codeDesc)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeBAL objCodeBal = new CodeBAL();
                return objCodeBal.GetCodeDetailsForInvoice(codeType, codeDesc);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }



        /// <summary>
        /// this is used to Get Port Storage print Invoices data  
        /// </summary>
        /// <param name="ReprintInd"></param>
        /// <param name="ReprintType"></param>
        /// <param name="DateType"></param>
        /// <param name="InvoiceDateFrom"></param>
        /// <param name="InvoiceDateTo"></param>
        /// <param name="InvoiceNumberFrom"></param>
        /// <param name="InvoiceNumberTo"></param>
        /// <returns></returns>
        public IList<PortStoragePrintInvoiceProp> GetInvoiceDataForInvoicePrint(int ReprintInd, int ReprintType, int DateType, Nullable<DateTime> InvoiceDateFrom, Nullable<DateTime> InvoiceDateTo, string InvoiceNumberFrom, string InvoiceNumberTo)
        {
            try
            {
                PortStorageInvoicesBAL objPortStorageInvoicesBAL = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBAL.GetInvoiceDataForInvoicePrint(ReprintInd, ReprintType, DateType, InvoiceDateFrom, InvoiceDateTo, InvoiceNumberFrom, InvoiceNumberTo);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to get vehicle listing repot from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="GroupByDealerInd"></param>
        /// <returns></returns>
        public IList<VehicleListingReportProp> GetVehicleListingReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate, bool GroupByDealerInd)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.GetVehicleListingReport(ReportType, StartDate, EndDate, GroupByDealerInd);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Get All Customer list from dataabse
        /// </summary>
        /// <returns></returns>
        public List<PortStorageRequestsReportProp> LoadCustomerList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.LoadCustomerList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageRequestsReportProp> LoadBatchList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.LoadBatchList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to get port storage request report from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="CustomerId"></param>
        /// <param name="VIN"></param>
        /// <param name="RequestDateFrom"></param>
        /// <param name="RequestDateTo"></param>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        public IList<PortStorageRequestsReportProp> GetPortStorageRequestReport(int ReportType, int CustomerId, string VIN, Nullable<DateTime> RequestDateFrom, Nullable<DateTime> RequestDateTo, int BatchId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.GetPortStorageRequestReport(ReportType, CustomerId, VIN, RequestDateFrom, RequestDateTo, BatchId);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is use to get port storage vehicle summery report from database
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<VehicleListingReportProp> GetPortStorageVehicleSummeryReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.GetPortStorageVehicleSummeryReport(ReportType, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to Get Port Storage Lane Summary List
        /// </summary>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetPortStorageLaneSummaryList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return portStorageReportBAL.GetPortStorageLaneSummaryList();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This function is used to get Port Storage InBound List from database
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<List<PortStorageInventoryList>> GetPortStorageInBoundList(Nullable<DateTime> StartDate, Nullable<DateTime> EndDate)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageReportBAL portStorageReportBAL = new PortStorageReportBAL();
                return await portStorageReportBAL.GetPortStorageInBoundList(StartDate, EndDate);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting Customer's AddressName
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<UserApplicationConstantsProp> GetDAIAddressName(string userCode)
        {
            PortStorageReportBAL objPortStorageReportBAL = new PortStorageReportBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportBAL.GetDAIAddressName(userCode);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting Invoice list coressponding to a billingID and time period
        /// </summary>
        /// <param name="billinID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<InvoiceListProp> LoadInvoiceList(int? billinID, DateTime? startDate, DateTime? endDate)
        {
            PortStorageReportBAL objPortStorageReportBAL = new PortStorageReportBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportBAL.LoadInvoiceList(billinID, startDate, endDate);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting batch list for inventory comparision 
        /// </summary>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetBatchLocationImport()
        {
            PortStorageReportBAL objPortStorageReportBAL = new PortStorageReportBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportBAL.GetBatchLocationImport();
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For getting comparsion list coressponding to batch
        /// </summary>
        /// <param name="batchID"></param>
        /// <returns></returns>
        public List<PortStorageInventoryList> GetInventoryComparisionList(int? batchID)
        {
            PortStorageReportBAL objPortStorageReportBAL = new PortStorageReportBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objPortStorageReportBAL.GetInventoryComparisionList(batchID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the user list for customer web portal.
        /// </summary>
        /// <returns></returns>
        public List<WebPortalUserList> GetUsers(WebPortalUserList objUserList)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                return objWebPortalBAL.GetUsers(objUserList);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get list of all modules with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ModuleList> GetModules(int userID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                return objWebPortalBAL.GetModules(userID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }

        /// <summary>
        /// To get list of all customer with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserCustomerList> GetCustomers(int userID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                return objWebPortalBAL.GetCustomers(userID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting New User in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUser(WebPortalUserList objUser)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.AddUser(objUser);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting Customers of a user in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserCustomer(UserCustomerList objUserCustomer)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.AddUserCustomer(objUserCustomer);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For inserting user modules in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserModule(ModuleList objModuleUser)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.AddUserModule(objModuleUser);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method to deete a particular user
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteUser(int userID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.DeleteUser(userID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleList> GetRoles(int userID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.GetRoles(userID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get list of all groups with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GroupList> GetGroups(int userID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objWebPortalBAL.GetGroups(userID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalBAL.InsertUpdateUser(objUser, lstUserCustomer, lstUserModules, lstUserRoles, lstUserGroups);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// method for checking duplicate emailid
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicateEmail(string emailID)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalBAL.CheckDuplicateEmail(emailID);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// method for checking duplicate username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicatePortalUserName(string userName)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalBAL.CheckDuplicatePortalUserName(userName);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalBAL.GetPortStorageInventoryDetails(objPortStorageProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is to update the complete list of  request checked vehicles
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool UpdateRequestCheckedVehicles(List<PortStorageVehicleProp> lstRequestedVechicles, string user)
        {
            WebPortalBAL objWebPortalBAL = new WebPortalBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objWebPortalBAL.UpdateRequestCheckedVehicles(lstRequestedVechicles, user);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool CheckMultipleVIN(string vIN)
        {
            try
            {
                VehicleBAL objVehicleBAL = new VehicleBAL();
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objVehicleBAL.CheckMultipleVIN(vIN);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public PortStorageVehicleProp GetDecodedPortStorageVIN(string vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.GetDecodedPortStorageVIN(vin);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is for inserting information in  the decodevin  table
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public bool InsertDecodeVIN(VINDecodeList objVINDecode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.InsertDecodeVIN(objVINDecode);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public VINDecodeList GetDecodedVINForVINDecode(string vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.GetDecodedVINForVINDecode(vin);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        public VINDecodeList GetDecodeDataFromWeb(string vin, string bodyStylep)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.GetDecodeDataFromWeb(vin, bodyStylep);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();

                return objAdminUserBAL.GetRolesSelection(userID);
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

        #region :: Sprint 5 ::
        /// <summary>
        /// This method is used to Storage Vehicle Outgate for security.
        /// </summary>
        /// <param name="objStorageVehicleOutgateProp"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>July 7, 2016</createdOn>
        public List<StorageVehicleOutgateProp> UpdateStorageVehicleOutgate(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.UpdateStorageVehicleOutgate(objStorageVehicleOutgateProp);
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
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageVehiclesBatchId()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageVehiclesBatchId();

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
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns>bool</returns>
        public bool SetPortStorageVehiclesNextBatchId(int BatchId)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.SetPortStorageVehiclesNextBatchId(BatchId);

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
        /// This method is used to get message from storeprocedure.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageVehicles(int BatchId, string User)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.ImportPortStorageVehicles(BatchId, User);

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
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> GetPortStorageVehicleImportList(int BatchId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.GetPortStorageVehicleImportList(BatchId);
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
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> LoadVehiclesBatchList(string Vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.LoadVehiclesBatchList(Vin);
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
        /// This method is used to Get Port Storage Import File Directory
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileDirectory()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageVehiclesImportFileDirectory();

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
        /// This method is used to get Import File Name
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileName()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageVehiclesImportFileName();

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
        /// This method is used to get Import Archive Directory
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageVehiclesImportFileArchiveDirectory()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageVehiclesImportFileArchiveDirectory();

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
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageLocationBatchId()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageVehiclesBatchId();

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
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns>bool</returns>
        public bool SetPortStorageLocationNextBatchId(int BatchId)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.SetPortStorageLocationNextBatchId(BatchId);

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
        /// This method is used to get message from storeprocedure.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageLocation(int BatchId, string User)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.ImportPortStorageLocation(BatchId, User);

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
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> GetPortStorageLocationImportList(int BatchId)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.GetPortStorageLocationImportList(BatchId);
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
        /// This method is used to Load Batch list from database/
        /// </summary>
        /// <returns></returns>
        public List<PortStorageVehicleImportProp> LoadLocationBatchList(string Vin)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();

                return portStorageImportExportBAL.LoadLocationBatchList(Vin);
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
        /// This method is used to get Import File Name
        /// </summary>
        /// <returns>string</returns>
        public string GetPortStorageLocationImportFileName()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.GetPortStorageLocationImportFileName();

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
        /// This method is used to get data and insert data in database for Location
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool LocationImportTransactionProcess(int BatchId, string User)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.LocationImportTransactionProcess(BatchId, User);

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
        /// This method is used to get data and insert data in database for vehicle
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool VehicleImportTransactionProcess(int BatchId, string User)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageImportExportBAL portStorageImportExportBAL = new PortStorageImportExportBAL();
                return portStorageImportExportBAL.VehicleImportTransactionProcess(BatchId, User);

            }
            catch (Exception ex)
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

                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                return objAdminUserBAL.GetSystemSettings();
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
        /// For updating compnay information
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool UpdateCompanyInfo(UserApplicationConstantsProp objCompanyinfo)
        {
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                PortStorageReportBAL objReportBAL = new PortStorageReportBAL();
                return objReportBAL.UpdateCompanyInfo(objCompanyinfo);
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
        /// This method is used to get storage vehicle details list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-20-2016</createdon>
        public List<PortStorageVehicleProp> GetStorageVehicleDetails(PortStorageVehicleProp objPortstorageProp)
        {
            BillingBAL objBillingBAL = new BillingBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.GetStorageVehicleDetails(objPortstorageProp);
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
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public List<BillingLineItemsProp> GetLineItemsList(BillingLineItemsProp objLineItems)
        {
            BillingBAL objBillingBAL = new BillingBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.GetLineItemsList(objLineItems);
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
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        public bool ResetExportedInd(int billingID)
        {
            BillingBAL objBillingBAL = new BillingBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.ResetExportedInd(billingID);
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
        /// To Add Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        public int InsertBilling(BillingListProp objBillingProp)
        {
            BillingBAL objBillingBAL = new BillingBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.InsertBilling(objBillingProp);
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
        /// To Update Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        public bool UpdateBillingTab(BillingListProp objBillingProp)
        {
            BillingBAL objBillingBAL = new BillingBAL();

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.UpdateBillingTab(objBillingProp);
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
        ///  This method is used  for deleting data from billing and related tables
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        /// // <createdOn>May-23,2016</createdOn>
        public bool DeleteBillingData(int BillingID)
        {
            BillingBAL objBillingBAL = new BillingBAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingBAL.DeleteBillingData(BillingID);
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

        #region CODE TO BE REMOVED AFTER CODE CLEANUP

        ///// <summary>
        ///// This method is used to get the invoice details for a billing id.
        ///// </summary>
        ///// <param name="objCodeProp"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-20-2016</createdon>
        //public List<BillingInvoiceDetailsProp> GetInvoiceDetails(bool CreditedOutInd, bool CreditMemoInd, bool NewRunInd, int BillingID, int RunID, int CustomerID)
        //{
        //    BillingBAL objBillingBAL = new BillingBAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingBAL.GetInvoiceDetails(CreditedOutInd, CreditMemoInd, NewRunInd, BillingID, RunID, CustomerID);
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }

        //}

        ///// <summary>
        ///// This method is used to get the list of the drivers.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //public List<string> DriverList()
        //{
        //    BillingBAL objBillingBAL = new BillingBAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingBAL.DriverList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// This method is used to get the list of outside carrier.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //public List<string> OutsideCarrier()
        //{
        //    BillingBAL objBillingBAL = new BillingBAL();
        //    try
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        return objBillingBAL.OutsideCarrier();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        #endregion

        /// <summary>
        /// To get the billing search details coressponding to search criteria.
        /// </summary>
        /// <param name="BillingProp"></param>
        /// <returns>List<BillingProp></returns>
        public async Task<List<BillingProp>> BillingSearch(BillingProp objPortStorageProp)
        {
            List<BillingProp> objBilling = new List<BillingProp>();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBal = new BillingBAL();
                return await objBillingBal.BillingSearch(objPortStorageProp);//objWebPortalBAL.GetPortStorageInventoryDetails(objPortStorageProp);
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get bililng data from billing table
        /// </summary>
        /// <param name="billingID"></param>
        /// <returns></returns>
        public BillingListProp GetBillingData(int billingID)
        {
            BillingDAL objBillingDAL = new BillingDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objBillingDAL.GetBillingData(billingID);
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
        /// This method is used to Get Billing Record Export
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="ExportDate"></param>
        /// <returns></returns>
        public List<BillingLineItemsProp> GetBillingRecordExport(int ExportType, DateTime? ExportDate)
        {

            BillingBAL objBillingBAL = new BillingBAL();  /// Creating The Object of BillingDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objBillingBAL.GetBillingRecordExport(ExportType, ExportDate); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objBillingBAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }

        /// <summary>
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        public int GetBillingExportBatchId()
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();  /// Creating The Object of BillingDAL 
                return objBillingBAL.GetBillingExportBatchId();

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
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns>bool</returns>
        public bool SetBillingExportNextBatchId(int BatchId)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();  /// Creating The Object of BillingDAL 
                return objBillingBAL.SetBillingExportNextBatchId(BatchId);

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
        /// This method is used to Get billing Export File Path
        /// </summary>
        /// <returns>string</returns>
        public string GetBillingExportFilePath()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();  /// Creating The Object of BillingDAL 
                return objBillingBAL.GetBillingExportFilePath();

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
        /// This method is used to update Billing Line Item
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="BillingLineItemsID"></param>
        /// <param name="UserCode"></param>
        public void UpdateBillingLineItem(int BatchId, int BillingLineItemsID, string UserCode)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BillingBAL objBillingBAL = new BillingBAL();  /// Creating The Object of BillingDAL 
                objBillingBAL.UpdateBillingLineItem(BatchId, BillingLineItemsID, UserCode);

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
        /// This method is used to Storage Vehicle Outgate for security.
        /// </summary>
        /// <param name="objStorageVehicleOutgateProp"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>July 7, 2016</createdOn>
        public StorageVehicleOutgateProp UpdateStorageVehicleOutgateData(StorageVehicleOutgateProp objStorageVehicleOutgateProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                VehicleBAL objVehicleBAL = new VehicleBAL();
                return objVehicleBAL.UpdateStorageVehicleOutgateData(objStorageVehicleOutgateProp);
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
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                return objAdminUserBAL.UpdateSystemSettings(setting);
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
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                return objAdminUserBAL.InsertSystemSettings(setting);
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
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                return objAdminUserBAL.DeleteSystemSettings(setting);
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
        public List<AdminUserProp> FindSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserBAL objAdminUserBAL = new AdminUserBAL();
                return objAdminUserBAL.FindSystemSettings(setting);
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

        #endregion

        /// <summary>
        /// For updating batch id of print request vehical list
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <returns></returns>
        public bool UpdateRequestPrintIndexForVehicles(List<string> vehicleIds)
        {
            bool isRequestCOmpleted = false;
            PortStorageReportBAL objPortStorageReportBAL = new PortStorageReportBAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                isRequestCOmpleted = objPortStorageReportBAL.UpdateRequestPrintIndexForVehicles(vehicleIds);
            }
            catch (Exception ex)
            {
                isRequestCOmpleted = false;
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return isRequestCOmpleted;
        }

        /// <summary>
        /// Function to update the setting table values for next invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Feb-12,2018</createdOn>
        public bool UpdateNextInvoiceNumberValue(string iInvoiceNumber)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageInvoicesBAL objPortStorageInvoicesBal = new PortStorageInvoicesBAL();
                return objPortStorageInvoicesBal.UpdateNextInvoiceNumberValue(iInvoiceNumber); /// sending the new record model to the data access layer.
            }
            catch (Exception ex)
            {
                CommonDAL.logger.LogError(this.GetType(), ex);
                throw;
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Log Error To database using Entity
        /// </summary>
        /// <param name="message"></param>
        /// <param name="macAddress"></param>
        /// <param name="ex"></param>
        public void LogErrorToDb(ErrorLogProp errorProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                ErrorLogBAL objErrorLogBAL = new ErrorLogBAL();
                objErrorLogBAL.LogErrorToDb(errorProp);
            }
            catch (Exception e)
            {
                CommonDAL.logger.LogError(this.GetType(), e);
            }
            finally
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.UtilResources.logEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }
    }
}

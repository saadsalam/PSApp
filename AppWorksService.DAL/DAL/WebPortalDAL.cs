using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using System.Transactions;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    public class WebPortalDAL
    {
        /// <summary>
        /// To get the user list for customer web portal.
        /// </summary>
        /// <returns></returns>
        public List<WebPortalUserList> GetUsers(WebPortalUserList objUserList)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {


                    var lstUsers = (from users in portalEntities.UserAccounts
                                    orderby users.username
                                    select new WebPortalUserList
                                    {
                                        userID = users.userID,
                                        username = users.username,
                                        firstname = users.firstname,
                                        lastname = users.lastname,
                                        password = users.password,
                                        email = users.email,
                                        phone = users.Phone,
                                        isActive = users.isActive,
                                        lastLogin = users.lastLogin
                                    }
                                   );

                    var pageCount = lstUsers.ToList().Count;
                    if (objUserList.CurrentPageIndex == 0)
                    {
                        var queryResult = lstUsers.ToList().Skip(objUserList.CurrentPageIndex * objUserList.PageSize).Take(objUserList.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
                    }
                    else
                    {
                        var queryResult = lstUsers.ToList().Skip((objUserList.CurrentPageIndex * objUserList.PageSize) + (objUserList.DefaultPageSize - objUserList.PageSize)).Take(objUserList.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
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
        }

        /// <summary>
        /// To get list of all modules with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ModuleList> GetModules(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {
                    var lstSelectedModules = (from usermodule in portalEntities.ModuleUsers
                                              join module in portalEntities.Modules
                                              on usermodule.moduleId equals module.moduleId
                                              where usermodule.userId == userID
                                              select new ModuleList
                                              {
                                                  ModuleID = module.moduleId,
                                                  ModuleCode = module.code,
                                                  ModuleName = module.name,
                                                  IsSelected = true
                                              }
                                             ).Distinct();
                    var lstModules = (from modules in portalEntities.Modules
                                      select new ModuleList
                                      {
                                          ModuleID = modules.moduleId,
                                          ModuleCode = modules.code,
                                          ModuleName = modules.name,
                                          IsSelected = true,
                                      }
                                     ).Distinct();

                    var lstexclude = lstModules.Select(x => new ModuleList { ModuleID = x.ModuleID, ModuleCode = x.ModuleCode, ModuleName = x.ModuleName, IsSelected = false })
                                    .Except(lstSelectedModules.Select(x => new ModuleList { ModuleID = x.ModuleID, ModuleCode = x.ModuleCode, ModuleName = x.ModuleName, IsSelected = false }));

                    var lstMerge = lstexclude.Union(lstSelectedModules);

                    return lstMerge.OrderBy(x => x.ModuleCode).ToList();
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

        /// <summary>
        /// To get list of all customer with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserCustomerList> GetCustomers(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities objAppWorksEntitiesPortal = new PortalDbEntities())
                {
                    using (PortStorageEntities portalEntities = new PortStorageEntities())
                    {

                        var LstCUstomers = objAppWorksEntitiesPortal.spGetSelectedUserCustomerList(userID).Select(x => new UserCustomerList { CustomerID = x.CustomerID, CustomerName = x.CustomerName, IsSelected = x.IsSelected == 0 ? false : true });
                        List<UserCustomerList> lstUserCustomers = new List<UserCustomerList>();
                        lstUserCustomers = LstCUstomers.ToList();
                        return lstUserCustomers;
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

        }

        /// <summary>
        /// For inserting New User in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUser(WebPortalUserList objUser)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {

                    UserAccount user = new UserAccount();
                    user.username = objUser.username;
                    user.password = objUser.password;
                    user.email = objUser.email;
                    user.Phone = objUser.phone;
                    user.firstname = objUser.firstname;
                    user.lastname = objUser.lastname;
                    user.lastLogin = objUser.lastLogin;
                    user.isActive = objUser.isActive;

                    portalEntities.UserAccounts.Add(user);  /// Insert the Record in Respected Table.
                    portalEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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

            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;
                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {

                    UserCustomerRole usercustomerrole = new UserCustomerRole();
                    usercustomerrole.customerId = objUserCustomer.CustomerID;
                    usercustomerrole.userId = objUserCustomer.UserID;

                    portalEntities.UserCustomerRoles.Add(usercustomerrole);  /// Insert the Record in Respected Table.
                    portalEntities.SaveChanges();/// Check the Chenges in Table After Record Insertion.

                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {

                    ModuleUser usermodule = new ModuleUser();
                    usermodule.userId = objModuleUser.UserID;
                    usermodule.moduleId = objModuleUser.ModuleID;

                    portalEntities.ModuleUsers.Add(usermodule);  /// Insert the Record in Respected Table.
                    portalEntities.SaveChanges(); /// Check the Chenges in Table After Record Insertion.

                    IsSuccessfull = true;
                }
                return IsSuccessfull;

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
        ///  This Method is to delete a particular user.
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteUser(int userID)
        {

            using (var transaction = new TransactionScope())
            {
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    bool result = false;

                    using (PortalDbEntities portalEntities = new PortalDbEntities())
                    {
                        CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                        var userCustomerData = portalEntities.UserCustomerRoles.Where(x => x.userId == userID).ToList();
                        if (userCustomerData != null)
                        {
                            foreach (var customer in userCustomerData)
                            {
                                portalEntities.UserCustomerRoles.Remove(customer);
                                portalEntities.SaveChanges();
                            }
                        }

                        var userModuleData = portalEntities.ModuleUsers.Where(x => x.userId == userID).ToList();
                        if (userModuleData != null)
                        {
                            foreach (var module in userModuleData)
                            {
                                portalEntities.ModuleUsers.Remove(module);
                                portalEntities.SaveChanges();
                            }
                        }

                        var userRoleData = portalEntities.RoleMembers.Where(x => x.userId == userID).ToList();
                        if (userRoleData != null)
                        {
                            foreach (var role in userRoleData)
                            {
                                portalEntities.RoleMembers.Remove(role);
                                portalEntities.SaveChanges();
                            }
                        }

                        var userGroupData = portalEntities.GroupMembers.Where(x => x.userID == userID).ToList();
                        if (userGroupData != null)
                        {
                            foreach (var group in userGroupData)
                            {
                                portalEntities.GroupMembers.Remove(group);
                                portalEntities.SaveChanges();
                            }
                        }

                        var userInfoData = portalEntities.UserAccounts.Where(x => x.userID == userID).FirstOrDefault();
                        if (userInfoData != null)
                        {
                            userInfoData.isActive = 0;
                            portalEntities.SaveChanges();
                        }
                        result = true;
                        transaction.Complete();
                    }
                    return result;
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

        /// <summary>
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleList> GetRoles(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {
                    var lstSelectedRoles = (from rolemember in portalEntities.RoleMembers
                                            join role in portalEntities.Roles
                                            on rolemember.roleId equals role.id
                                            where rolemember.userId == userID
                                            select new RoleList
                                            {
                                                RoleID = role.id,
                                                RoleName = role.roleName,
                                                Description = role.description,
                                                IsSelected = true
                                            }
                                             ).Distinct();
                    var lstRoles = (from role in portalEntities.Roles
                                    select new RoleList
                                    {
                                        RoleID = role.id,
                                        RoleName = role.roleName,
                                        Description = role.description,
                                        IsSelected = true,
                                    }
                                     ).Distinct();

                    var lstexclude = lstRoles.Select(x => new RoleList { RoleID = x.RoleID, RoleName = x.RoleName, Description = x.Description, IsSelected = false })
                                    .Except(lstSelectedRoles.Select(x => new RoleList { RoleID = x.RoleID, RoleName = x.RoleName, Description = x.Description, IsSelected = false }));

                    var lstMerge = lstexclude.Union(lstSelectedRoles);

                    return lstMerge.OrderBy(x => x.RoleName).ToList();
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

        /// <summary>
        /// To get list of all groups with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<GroupList> GetGroups(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {

                    var lstSelectedGroups = (from groupmember in portalEntities.GroupMembers
                                             join usergroup in portalEntities.UserGroups
                                             on groupmember.groupID equals usergroup.groupID
                                             where groupmember.userID == userID
                                             select new GroupList
                                             {
                                                 GroupID = usergroup.groupID,
                                                 GroupName = usergroup.groupname,
                                                 Description = usergroup.description,
                                                 IsSelected = true
                                             }
                                             ).Distinct();
                    var lstGroups = (from usergroup in portalEntities.UserGroups
                                     select new GroupList
                                     {
                                         GroupID = usergroup.groupID,
                                         GroupName = usergroup.groupname,
                                         Description = usergroup.description,
                                         IsSelected = true
                                     }
                                     ).Distinct();

                    var lstexclude = lstGroups.Select(x => new GroupList { GroupID = x.GroupID, GroupName = x.GroupName, Description = x.Description, IsSelected = false })
                                    .Except(lstSelectedGroups.Select(x => new GroupList { GroupID = x.GroupID, GroupName = x.GroupName, Description = x.Description, IsSelected = false }));

                    var lstMerge = lstexclude.Union(lstSelectedGroups);

                    return lstMerge.OrderBy(x => x.GroupName).ToList();
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

        /// <summary>
        /// method for checking duplicate username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicatePortalUserName(string userName)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {
                    bool result = false;
                    var userData = portalEntities.UserAccounts.Where(x => x.username.ToLower() == userName.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        result = true;

                    }

                    return result;
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

        /// <summary>
        /// method for checking duplicate emailid
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckDuplicateEmail(string emailID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {
                    bool result = false;
                    var userData = portalEntities.UserAccounts.Where(x => x.email.ToLower() == emailID.ToLower()).FirstOrDefault();
                    if (userData != null)
                    {
                        result = true;

                    }

                    return result;
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

        /// <summary>
        /// For inserting user roles in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserRole(RoleList objUserRole)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {

                    RoleMember userrole = new RoleMember();
                    userrole.userId = objUserRole.UserID;
                    userrole.roleId = objUserRole.RoleID;
                    userrole.dateCreated = objUserRole.DateCreated;
                    userrole.whoUpdated = objUserRole.WhoCreated;

                    portalEntities.RoleMembers.Add(userrole);  /// Insert the Record in Respected Table.
                    portalEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
        /// For inserting user Groups in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddUserGroup(GroupList objUsergroup)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool IsSuccessfull = false;

                using (PortalDbEntities portalEntities = new PortalDbEntities())
                {
                    GroupMember usergroup = new GroupMember();
                    usergroup.userID = objUsergroup.UserID;
                    usergroup.groupID = objUsergroup.GroupID; ;
                    usergroup.dateCreated = objUsergroup.DateCreated;
                    usergroup.whoUpdated = objUsergroup.WhoCreated;

                    portalEntities.GroupMembers.Add(usergroup);  /// Insert the Record in Respected Table.
                    portalEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    IsSuccessfull = true;
                }
                return IsSuccessfull;
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
            using (var transaction = new TransactionScope())
            {
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    using (PortalDbEntities portalEntities = new PortalDbEntities())
                    {

                        bool result = false;

                        int userID = 0;
                        var userrData = portalEntities.UserAccounts.Where(x => x.userID == objUser.userID).FirstOrDefault();

                        if (userrData == null) // Insert
                        {
                            UserAccount userlist = new UserAccount();
                            userlist.username = objUser.username;
                            userlist.password = objUser.password;
                            userlist.firstname = objUser.firstname;
                            userlist.lastname = objUser.lastname;
                            userlist.password = objUser.password;
                            userlist.email = objUser.email;
                            userlist.Phone = objUser.phone;
                            userlist.isActive = objUser.isActive;
                            userlist.isSuperUser = objUser.isSuperUser;
                            //userlist.lastLogin = objUser.lastLogin;
                            userlist.dateCreated = objUser.dateCreated;
                            userlist.whoCreated = objUser.whoCreated;
                            userlist.dateModified = objUser.dateModified;
                            userlist.whoModified = objUser.whoModified;
                            portalEntities.UserAccounts.Add(userlist);
                            portalEntities.SaveChanges();
                            userID = userlist.userID;
                        }
                        else// Update 
                        {
                            userrData.username = objUser.username;
                            userrData.password = objUser.password;
                            userrData.firstname = objUser.firstname;
                            userrData.lastname = objUser.lastname;
                            userrData.password = objUser.password;
                            userrData.email = objUser.email;
                            userrData.Phone = objUser.phone;
                            userrData.isActive = objUser.isActive;
                            userrData.isSuperUser = objUser.isSuperUser;
                            // userrData.lastLogin = objUser.lastLogin;
                            userrData.dateCreated = objUser.dateCreated;
                            userrData.whoCreated = objUser.whoCreated;
                            userrData.dateModified = objUser.dateModified;
                            userrData.whoModified = objUser.whoModified;
                            portalEntities.SaveChanges();
                            userID = objUser.userID;
                        }


                        if (userID > 0)
                        {

                            var userCustomerData = portalEntities.UserCustomerRoles.Where(x => x.userId == userID).ToList();
                            if (userCustomerData != null)
                            {
                                foreach (var customer in userCustomerData)
                                {
                                    portalEntities.UserCustomerRoles.Remove(customer);
                                    portalEntities.SaveChanges();
                                }
                            }
                            foreach (var item in lstUserCustomer)
                            {
                                item.UserID = userID;
                                if (item.IsSelected == true)
                                    AddUserCustomer(item);
                            }


                            var userModuleData = portalEntities.ModuleUsers.Where(x => x.userId == userID).ToList();
                            if (userModuleData != null)
                            {
                                foreach (var module in userModuleData)
                                {
                                    portalEntities.ModuleUsers.Remove(module);
                                    portalEntities.SaveChanges();
                                }
                            }
                            foreach (var item in lstUserModules)
                            {
                                item.UserID = userID;
                                if (item.IsSelected == true)
                                    AddUserModule(item);
                            }

                            var userRoleData = portalEntities.RoleMembers.Where(x => x.userId == userID).ToList();
                            if (userRoleData != null)
                            {
                                foreach (var role in userRoleData)
                                {
                                    portalEntities.RoleMembers.Remove(role);
                                    portalEntities.SaveChanges();
                                }
                            }
                            foreach (var item in lstUserRoles)
                            {
                                item.UserID = userID;
                                if (item.IsSelected == true)
                                    AddUserRole(item);
                            }

                            var userGroupData = portalEntities.GroupMembers.Where(x => x.userID == userID).ToList();
                            if (userGroupData != null)
                            {
                                foreach (var group in userGroupData)
                                {
                                    portalEntities.GroupMembers.Remove(group);
                                    portalEntities.SaveChanges();
                                }
                            }
                            foreach (var item in lstUserGroups)
                            {
                                item.UserID = userID;
                                if (item.IsSelected == true)
                                    AddUserGroup(item);
                            }
                            //portalEntities.SaveChanges();
                            result = true;
                        }

                        if (result)
                            transaction.Complete();

                        return result;
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

                using (PortStorageEntities portalEntities = new PortStorageEntities())
                {

                    var lstInventory = (from portstoragevehicles in portalEntities.PortStorageVehicles
                                        where portstoragevehicles.CustomerID == objPortStorageProp.CustomerID
                                        orderby portstoragevehicles.VIN
                                        select new PortStorageVehicleProp
                                        {
                                            PortStorageVehiclesID = portstoragevehicles.PortStorageVehiclesID,
                                            CustomerID = portstoragevehicles.CustomerID,
                                            VIN = portstoragevehicles.VIN,
                                            VehicleYear = portstoragevehicles.VehicleYear,
                                            Make = portstoragevehicles.Make,
                                            Model = portstoragevehicles.Model,
                                            Color = portstoragevehicles.Color,
                                            BayLocation = portstoragevehicles.BayLocation,
                                            DateIn = portstoragevehicles.DateIn,
                                            DateRequested = portstoragevehicles.DateRequested,
                                            VehicleStatus = portstoragevehicles.VehicleStatus,
                                            RequestedBy = portstoragevehicles.RequestedBy,
                                            DateOut = portstoragevehicles.DateOut,
                                            IsSelected = false,
                                            Days = 0

                                        }).AsQueryable();

                    if (!string.IsNullOrEmpty(objPortStorageProp.VIN))
                        lstInventory = lstInventory.Where(x => x.VIN.Contains(objPortStorageProp.VIN));

                    var pageCount = lstInventory.ToList().Count;
                    if (objPortStorageProp.CurrentPageIndex == 0 && objPortStorageProp.PageSize > 0)
                    {
                        var queryResult = lstInventory.ToList().Skip(objPortStorageProp.CurrentPageIndex * objPortStorageProp.PageSize).Take(objPortStorageProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
                    }
                    else if (objPortStorageProp.PageSize > 0)
                    {
                        var queryResult = lstInventory.ToList().Skip((objPortStorageProp.CurrentPageIndex * objPortStorageProp.PageSize) + (objPortStorageProp.DefaultPageSize - objPortStorageProp.PageSize)).Take(objPortStorageProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult;
                    }
                    else
                        return lstInventory.ToList();
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

        /// <summary>
        ///  This Method is to update request checked vehicles
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool UpdateVehiclesStatus(PortStorageVehicleProp portStorageVehicle, string userName)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    bool result = false;

                    using (PortStorageEntities portalEntities = new PortStorageEntities())
                    {

                        var portStorageVehicleData = portalEntities.PortStorageVehicles.Where(x => x.PortStorageVehiclesID == portStorageVehicle.PortStorageVehiclesID).FirstOrDefault();
                        if (portStorageVehicleData != null)
                        {
                            portStorageVehicleData.VehicleStatus = "Requested";
                            portStorageVehicleData.DateRequested = DateTime.Now;
                            portStorageVehicleData.RequestedBy = userName;
                            portalEntities.SaveChanges();
                        }
                        result = true;
                        transaction.Complete();
                        return result;
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

        /// <summary>
        ///  This Method is to update the complete list of  request checked vehicles
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool UpdateRequestCheckedVehicles(List<PortStorageVehicleProp> lstRequestedVechicles, string user)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    bool result = false;

                    using (PortStorageEntities portalEntities = new PortStorageEntities())
                    {
                        foreach (var vehicle in lstRequestedVechicles)
                        {
                            if (vehicle.IsSelected == true)
                                UpdateVehiclesStatus(vehicle, user);
                        }

                        result = true;
                        transaction.Complete();
                        return result;
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
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    /// <summary>
    /// DataAccessLayer for UserAdmin functionality
    /// </summary>
    public class AdminUserDAL
    {
        /// <summary>
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-18,2016</createdOn>
        public bool IsUserExists(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                bool result = false;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    // Count Existance of User
                    var count = (from list in objAppWorksEntities.Users
                                 where list.UserCode == objAdminUserProp.UserCode
                                 select list).Count();
                    if (Convert.ToInt32(count) == 0)
                    {
                        result = true;
                        AddAdminUser(objAdminUserProp, lstRoles);
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
                return result;
            }
        }

        /// <summary>
        /// To Add the Record of existing User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-19,2016</createdOn>
        public int AddAdminUser(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                int getUserId;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    User objTblUsers = new User();
                    objTblUsers.UserCode = objAdminUserProp.UserCode;
                    objTblUsers.FirstName = objAdminUserProp.FirstName;
                    objTblUsers.LastName = objAdminUserProp.LastName;
                    objTblUsers.Password = objAdminUserProp.Password;
                    objTblUsers.PIN = objAdminUserProp.PIN;
                    objTblUsers.Phone = objAdminUserProp.Phone;
                    objTblUsers.PhoneExtension = objAdminUserProp.PhoneExtension;
                    objTblUsers.CellPhone = objAdminUserProp.CellPhone;
                    objTblUsers.EmailAddress = objAdminUserProp.EmailAddress;
                    objTblUsers.FaxNumber = objAdminUserProp.FaxNumber;
                    objTblUsers.LabelXOffset = Convert.ToDecimal(objAdminUserProp.LabelXOffset);
                    objTblUsers.LabelYOffset = Convert.ToDecimal(objAdminUserProp.LabelYOffset);
                    objTblUsers.IMEI = objAdminUserProp.IMEI;
                    objTblUsers.LastConnection = objAdminUserProp.LastConnection;
                    if (!string.IsNullOrEmpty(objAdminUserProp.datsVersion))
                    {
                        objTblUsers.datsVersion = Convert.ToInt16(objAdminUserProp.datsVersion);
                    }
                    objTblUsers.RecordStatus = objAdminUserProp.RecordStatus;
                    objTblUsers.CreationDate = objAdminUserProp.CreationDate;
                    objTblUsers.CreatedBy = objAdminUserProp.CreatedBy;
                    objTblUsers.UpdatedDate = objAdminUserProp.UpdatedDate;
                    objTblUsers.UpdatedBy = objAdminUserProp.UpdatedBy;
                    objTblUsers.EmployeeNumber = objAdminUserProp.EmployeeNumber;
                    objTblUsers.PortPassIDNumber = objAdminUserProp.PortPassIDNumber;
                    objTblUsers.Department = objAdminUserProp.Department;
                    if (!string.IsNullOrEmpty(objAdminUserProp.StraightTimeRate))
                    {
                        objTblUsers.StraightTimeRate = Convert.ToDecimal(objAdminUserProp.StraightTimeRate);
                    }
                    if (!string.IsNullOrEmpty(objAdminUserProp.PieceRateRate))
                    {
                        objTblUsers.PieceRateRate = Convert.ToDecimal(objAdminUserProp.PieceRateRate);

                    }
                    if (!string.IsNullOrEmpty(objAdminUserProp.PDIRate))
                    {
                        objTblUsers.PDIRate = Convert.ToDecimal(objAdminUserProp.PDIRate);

                    }
                    if (!string.IsNullOrEmpty(objAdminUserProp.FlatBenefitPayRate))
                    {
                        objTblUsers.FlatBenefitPayRate = Convert.ToDecimal(objAdminUserProp.FlatBenefitPayRate);

                    }
                    objTblUsers.AlternateEmailAddress = objAdminUserProp.AlternateEmailAddress;
                    objAppWorksEntities.Users.Add(objTblUsers);  /// Insert the Record in Respected Table.
                    objAppWorksEntities.SaveChanges();          /// Check the Chenges in Table After Record Insertion.
                    getUserId = objTblUsers.UserID;             /// Return the UserId of Inserted User.
                    /// Insert the Default Role in UserRole Table
                    //UserRole objTblUserRole = new UserRole();
                    //objTblUserRole.RoleName = "Default Role";
                    //objTblUserRole.UserID = getUserId;
                    //objTblUserRole.CreationDate = objAdminUserProp.CreationDate;
                    //objTblUserRole.CreatedBy = objAdminUserProp.CreatedBy;
                    //objTblUserRole.UpdatedDate = objAdminUserProp.UpdatedDate;
                    //objTblUserRole.UpdatedBy = objAdminUserProp.UpdatedBy;
                    //objAppWorksEntities.UserRoles.Add(objTblUserRole);
                    //objAppWorksEntities.SaveChanges();

                    if (lstRoles.Count > 0 && getUserId > 0)
                    {
                        foreach (RoleList role in lstRoles)
                        {
                            if ((bool)role.IsSelected)
                            {
                                UserRole userrole = new UserRole();
                                userrole.UserID = getUserId;
                                userrole.RoleName = role.RoleName;
                                userrole.CreationDate = DateTime.Now;
                                userrole.CreatedBy = objTblUsers.CreatedBy;
                                objAppWorksEntities.UserRoles.Add(userrole);
                                objAppWorksEntities.SaveChanges();
                            }
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
                return getUserId;
            }
        }

        /// <summary>
        /// To Show the Existing Role of User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-22,2016</createdOn>
        public List<string> EixstingRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstExistsRole = new List<string>();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    int userId = (from T in objAppWorksEntities.Users where T.UserCode == objAdminUserProp.UserCode && (T.Password == objAdminUserProp.Password || T.PIN == objAdminUserProp.Password) select T.UserID).FirstOrDefault();
                    if (userId != 0)
                    {
                        objlstExistsRole = (from tblList in objAppWorksEntities.UserRoles where tblList.UserID == userId select tblList.RoleName).ToList();

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
            return objlstExistsRole;
        }

        /// <summary>
        /// To Add the All Roles Existing in Application
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> AllRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstAllRole = new List<string>();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    objlstAllRole = (from tblList in objAppWorksEntities.UserRoles orderby tblList.RoleName select tblList.RoleName).Distinct().ToList();
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
            return objlstAllRole;
        }

        /// <summary>
        /// To Add the New Role of User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> NewRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstNewRole = new List<string>();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    int userId = (from T in objAppWorksEntities.Users where T.UserCode == objAdminUserProp.UserCode && (T.Password == objAdminUserProp.Password || T.PIN == objAdminUserProp.Password) select T.UserID).FirstOrDefault();
                    if (userId != 0)
                    {
                        /// Insert the Default Role in UserRole Table
                        UserRole objTblUserRole = new UserRole();
                        objTblUserRole.RoleName = objAdminUserProp.newRole;
                        objTblUserRole.UserID = userId;
                        objTblUserRole.CreationDate = objAdminUserProp.CreationDate;
                        objTblUserRole.CreatedBy = objAdminUserProp.CreatedBy;
                        objTblUserRole.UpdatedDate = objAdminUserProp.UpdatedDate;
                        objTblUserRole.UpdatedBy = objAdminUserProp.UpdatedBy;
                        objAppWorksEntities.UserRoles.Add(objTblUserRole);
                        objAppWorksEntities.SaveChanges();

                        objlstNewRole = (from tblList in objAppWorksEntities.UserRoles where tblList.UserID == userId select tblList.RoleName).ToList();
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
            return objlstNewRole;
        }

        /// <summary>
        /// To Remove the Existing Role of User
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public List<string> RemoveRoleList(AdminUserProp objAdminUserProp)
        {
            List<string> objlstNewRole = new List<string>();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    int userId = (from T in objAppWorksEntities.Users where T.UserCode == objAdminUserProp.UserCode && (T.Password == objAdminUserProp.Password || T.PIN == objAdminUserProp.Password) select T.UserID).FirstOrDefault();
                    if (userId != 0)
                    {
                        var x = (from tblUser in objAppWorksEntities.UserRoles where tblUser.UserID == userId && tblUser.RoleName == objAdminUserProp.newRole select tblUser).FirstOrDefault();
                        objAppWorksEntities.UserRoles.Remove(x);
                        objAppWorksEntities.SaveChanges();
                        objlstNewRole = (from tblList in objAppWorksEntities.UserRoles where tblList.UserID == userId select tblList.RoleName).ToList();
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
            return objlstNewRole;
        }

        /// <summary>
        /// To Modify the Record of existing User
        /// </summary>
        /// <param name="AdminUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public AdminUserDeatils GetModificationRecord(AdminUserProp objAdminUserProp)
        {
            AdminUserDeatils objAdminUserDeatils = new AdminUserDeatils();
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    objAdminUserDeatils = (from tblUsers in objAppWorksEntities.Users
                                           where tblUsers.UserCode == objAdminUserProp.UserCode
                                           select new AdminUserDeatils
                                           {
                                               UserIDInfo = tblUsers.UserID,
                                               UserCodeInfo = tblUsers.UserCode,
                                               FirstNameInfo = tblUsers.FirstName,
                                               LastNameInfo = tblUsers.LastName,
                                               PhoneInfo = tblUsers.Phone,
                                               ExtentionInfo = tblUsers.PhoneExtension,
                                               CellPhoneInfo = tblUsers.CellPhone,
                                               FaxNumberInfo = tblUsers.FaxNumber,
                                               EmailInfo = tblUsers.EmailAddress,
                                               PasswordInfo = tblUsers.Password,
                                               PortPassIdInfo = tblUsers.PortPassIDNumber,
                                               PinInfo = tblUsers.PIN,
                                               LblXOffsetInfo = (decimal)tblUsers.LabelXOffset,
                                               LblYOffsetInfo = (decimal)tblUsers.LabelYOffset,
                                               EmployeeInfo = tblUsers.EmployeeNumber,
                                               RecordStatusInfo = tblUsers.RecordStatus,
                                               CreationDateInfo = tblUsers.CreationDate,
                                               CreatedByInfo = tblUsers.CreatedBy
                                           }).FirstOrDefault();

                    //= list.ToList();
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
        }

        /// <summary>
        /// To Update the Modified Record of existing User
        /// </summary>
        /// <param name="AdminUserProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public void UpdateUserDetails(AdminUserProp objAdminUserProp, List<RoleList> lstRoles)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var objTblUsers = (from qry in objAppWorksEntities.Users where qry.UserCode == objAdminUserProp.UserCode select qry).FirstOrDefault();//objAppWorksEntities.Users.Find(objAdminUserProp.UserCode);

                    if (objTblUsers != null)
                    {
                        objTblUsers.UserCode = objAdminUserProp.UserCode;
                        objTblUsers.FirstName = objAdminUserProp.FirstName;
                        objTblUsers.LastName = objAdminUserProp.LastName;
                        objTblUsers.Password = objAdminUserProp.Password;
                        objTblUsers.PIN = objAdminUserProp.PIN;
                        objTblUsers.Phone = objAdminUserProp.Phone;
                        objTblUsers.PhoneExtension = objAdminUserProp.PhoneExtension;
                        objTblUsers.CellPhone = objAdminUserProp.CellPhone;
                        objTblUsers.EmailAddress = objAdminUserProp.EmailAddress;
                        objTblUsers.FaxNumber = objAdminUserProp.FaxNumber;
                        objTblUsers.LabelXOffset = Convert.ToDecimal(objAdminUserProp.LabelXOffset);
                        objTblUsers.LabelYOffset = Convert.ToDecimal(objAdminUserProp.LabelYOffset);
                        objTblUsers.IMEI = objAdminUserProp.IMEI;
                        objTblUsers.LastConnection = objAdminUserProp.LastConnection;
                        if (objAdminUserProp.datsVersion != "")
                        {
                            objTblUsers.datsVersion = Convert.ToInt16(objAdminUserProp.datsVersion);
                        }
                        objTblUsers.RecordStatus = objAdminUserProp.RecordStatus;
                        objTblUsers.CreationDate = objAdminUserProp.CreationDate;
                        objTblUsers.CreatedBy = objAdminUserProp.CreatedBy;
                        objTblUsers.UpdatedDate = objAdminUserProp.UpdatedDate;
                        objTblUsers.UpdatedBy = objAdminUserProp.UpdatedBy;
                        objTblUsers.EmployeeNumber = objAdminUserProp.EmployeeNumber;
                        objTblUsers.PortPassIDNumber = objAdminUserProp.PortPassIDNumber;
                        objTblUsers.Department = objAdminUserProp.Department;
                        if (objAdminUserProp.StraightTimeRate != "")
                        {
                            objTblUsers.StraightTimeRate = Convert.ToDecimal(objAdminUserProp.StraightTimeRate);

                        }
                        if (objAdminUserProp.PieceRateRate != "")
                        {
                            objTblUsers.PieceRateRate = Convert.ToDecimal(objAdminUserProp.PieceRateRate);

                        }
                        if (objAdminUserProp.PDIRate != "")
                        {
                            objTblUsers.PDIRate = Convert.ToDecimal(objAdminUserProp.PDIRate);

                        }
                        if (objAdminUserProp.FlatBenefitPayRate != "")
                        {
                            objTblUsers.FlatBenefitPayRate = Convert.ToDecimal(objAdminUserProp.FlatBenefitPayRate);

                        }
                        objTblUsers.AlternateEmailAddress = objAdminUserProp.AlternateEmailAddress;
                        objAppWorksEntities.SaveChanges();

                        if (lstRoles.Count > 0)
                        {
                            var lstUserRoles = (from roles in objAppWorksEntities.UserRoles
                                                where roles.UserID == objAdminUserProp.UserID
                                                select roles);
                            if (lstUserRoles.Count() > 0)
                            {
                                foreach (UserRole urole in lstUserRoles.ToList())
                                {
                                    objAppWorksEntities.UserRoles.Remove(urole);
                                    objAppWorksEntities.SaveChanges();
                                }
                            }

                            foreach (RoleList role in lstRoles)
                            {
                                if ((bool)role.IsSelected)
                                {
                                    UserRole userrole = new UserRole();
                                    userrole.UserID = objAdminUserProp.UserID;
                                    userrole.RoleName = role.RoleName;
                                    userrole.CreationDate = DateTime.Now;
                                    userrole.CreatedBy = objTblUsers.CreatedBy;
                                    objAppWorksEntities.UserRoles.Add(userrole);
                                    objAppWorksEntities.SaveChanges();
                                }
                            }
                        }
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
        /// To Remove the Record of existing User
        /// </summary>
        /// <param name="AdminUserProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public void RemoveUserDetails(AdminUserProp objAdminUserProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    int userId = (from T in objAppWorksEntities.Users where T.UserCode == objAdminUserProp.UserCode && (T.Password == objAdminUserProp.Password || T.PIN == objAdminUserProp.Password) select T.UserID).FirstOrDefault();
                    if (userId != 0)
                    {
                        var x = (from tblUser in objAppWorksEntities.Users where tblUser.UserID == userId && tblUser.Password == objAdminUserProp.Password select tblUser).FirstOrDefault();
                        objAppWorksEntities.Users.Remove(x);
                        objAppWorksEntities.SaveChanges();
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
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleList> GetRolesSelection(int userID)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstSelectedRoles = (from userroles in objAppWorksEntities.Codes
                                            join role in objAppWorksEntities.UserRoles
                                            on userroles.Code1 equals role.RoleName
                                            where role.UserID == userID
                                            select new RoleList
                                            {
                                                RoleName = role.RoleName,
                                                IsSelected = true
                                            }
                                             ).Distinct();
                    var lstRoles = (from userroles in objAppWorksEntities.Codes
                                    where userroles.CodeType == "UserRole"
                                    select new RoleList
                                    {
                                        RoleName = userroles.Code1,
                                        IsSelected = true
                                    }
                                     ).Distinct();

                    var lstexclude = lstRoles.Select(x => new RoleList { RoleName = x.RoleName, IsSelected = false })
                                    .Except(lstSelectedRoles.Select(x => new RoleList { RoleName = x.RoleName, IsSelected = false }));

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
        /// To get list of system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<AdminUserProp> GetSystemSettings()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var lstSystemSettings = (from settings in objAppWorksEntities.SettingTables
                                             orderby settings.ValueKey
                                             select new AdminUserProp
                                             {
                                                 ValueKey = settings.ValueKey,
                                                 ValueDescription = settings.ValueDescription
                                             }
                                             ).Distinct().ToList();

                    return lstSystemSettings;
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
        /// To update system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdateSystemSettings(AdminUserProp setting)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                bool result = false;

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var settingData = objAppWorksEntities.SettingTables.Where(x => x.ValueKey == setting.ValueKey).FirstOrDefault();
                    settingData.ValueDescription = setting.ValueDescription;
                    objAppWorksEntities.SaveChanges();
                    result = true;
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

        /// <summary>
        /// Find Systems Settings
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public List<AdminUserProp> FindSystemSettings(AdminUserProp setting)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                List<AdminUserProp> listSettings = new List<AdminUserProp>();

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var settingData = objAppWorksEntities.SettingTables.AsQueryable();

                    if (!string.IsNullOrEmpty(setting.ValueKey))
                    {
                        settingData = settingData.AsQueryable().Where(x => x.ValueKey.Contains(setting.ValueKey));
                    }
                    if (!string.IsNullOrEmpty(setting.ValueDescription))
                    {
                        settingData = settingData.Where(x => x.ValueKey.Contains(setting.ValueDescription));
                    }
                    listSettings = settingData.Select(x => new AdminUserProp() { ValueKey = x.ValueKey, ValueDescription = x.ValueDescription }).ToList();
                }

                return listSettings;
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
        /// add system settings
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool InsertSystemSettings(AdminUserProp setting)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                bool result = false;

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    SettingTable newSetting = new SettingTable();
                    newSetting.ValueKey = setting.ValueKey;
                    newSetting.ValueDescription = setting.ValueDescription;
                    objAppWorksEntities.SettingTables.Add(newSetting);
                    objAppWorksEntities.SaveChanges();
                    result = true;
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

        /// <summary>
        /// Delete system settings
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool DeleteSystemSettings(AdminUserProp setting)
        {
            try
            {

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                bool result = false;

                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    var settingData = objAppWorksEntities.SettingTables.Where(x => x.ValueKey == setting.ValueKey).FirstOrDefault();
                    objAppWorksEntities.SettingTables.Remove(settingData);
                    objAppWorksEntities.SaveChanges();
                    result = true;
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
}

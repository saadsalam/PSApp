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
    /// DataAccessLayer To Find User Module
    /// </summary>
    public class FindUserDAL
    {
        /// <summary>
        /// Function To Get The Users Infoemation.
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-25,2016</createdOn>
        public void UsersList(FindUserProp objFindUserProp)
        {
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {


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
        /// Function To Get The All Roles.
        /// </summary>
        /// <param name="objFindUserProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<string> RoleList()
        {
            List<string> lstRoles = new List<string>();
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    lstRoles = (from tblUserRoles in objAppWorksEntities.UserRoles select tblUserRoles.RoleName).Distinct().ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return lstRoles;
            }
        }

        /// <summary>
        /// Function To Get The Record Status.
        /// </summary>
        /// <param></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<string> RecordStatus()
        {
            List<string> lstRecordStatus = new List<string>();
            // creating the object of PortStorageEntities Database
            using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    //lstRecordStatus.Add("All");
                    lstRecordStatus.Add("Active");
                    lstRecordStatus.Add("Inactive");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return lstRecordStatus;
            }
        }
        /// <summary>
        /// Function To Get The Record of Users.
        /// </summary>
        /// <param></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-26,2016</createdOn>
        public List<FindUserDetails> GetUserRecord(FindUserProp objFindUserProp)
        {
            List<FindUserDetails> objFindUserDetails = new List<FindUserDetails>();
            try
            {
                PortStorageEntities objAppWorksEntities = new PortStorageEntities();

                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var query = (from tblUsers in objAppWorksEntities.Users
                             join tblUserRole in objAppWorksEntities.UserRoles on tblUsers.UserID equals tblUserRole.UserID
                             orderby tblUsers.UserID
                             select new FindUserDetails
                             {
                                 UserCode = tblUsers.UserCode,
                                 FirstName = tblUsers.FirstName,
                                 LastName = tblUsers.LastName,
                                 UserID = tblUsers.UserID,
                                 UserRole = tblUserRole.RoleName,
                                 UserStatus = tblUsers.RecordStatus,

                             }).AsQueryable().Distinct();
                //objFindUserDetails = list.ToList();
                if ((objFindUserProp.UserCode != null) && (objFindUserProp.UserCode.Length > 0))
                {
                    query = query.Where(cnd => cnd.UserCode.Contains(objFindUserProp.UserCode) || cnd.FirstName.Contains(objFindUserProp.UserCode) || cnd.LastName.Contains(objFindUserProp.UserCode));
                }
                if ((objFindUserProp.FirstName != null) && (objFindUserProp.FirstName.Length > 0))
                {
                    query = query.Where(cnd => cnd.FirstName.Contains(objFindUserProp.FirstName));
                }
                if ((objFindUserProp.LastName != null) && (objFindUserProp.LastName.Length > 0))
                {
                    query = query.Where(cnd => cnd.LastName.Contains(objFindUserProp.LastName));
                }
                if ((objFindUserProp.SelectedRole != null) && (objFindUserProp.SelectedRole.Length > 0) && (objFindUserProp.SelectedRole != "All"))
                {
                    query = query.Where(cnd => cnd.UserRole == objFindUserProp.SelectedRole);
                }
                if ((objFindUserProp.selectedStatusRole != null) && (objFindUserProp.selectedStatusRole.Length > 0) && (objFindUserProp.selectedStatusRole != "All"))
                {
                    query = query.Where(cnd => cnd.UserStatus == objFindUserProp.selectedStatusRole);
                }

                //objFindUserDetails = query.ToList();
                objFindUserDetails = (from DistVal in query.AsEnumerable()
                                      select new FindUserDetails
                                      {
                                          UserCode = DistVal.UserCode,
                                          FirstName = DistVal.FirstName,
                                          LastName = DistVal.LastName,
                                          UserID = DistVal.UserID,
                                          UserRole = DistVal.UserRole,
                                          UserStatus = DistVal.UserStatus,

                                      }).DistinctBy(x => x.UserID).ToList();

                var pageCount = objFindUserDetails.ToList().Count;
                if (objFindUserProp.CurrentPageIndex == 0)
                {
                    var queryResult = objFindUserDetails.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                    //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                    if (queryResult != null && queryResult.Count > 0)
                    {
                        queryResult.FirstOrDefault().TotalPageCount = pageCount;
                    }
                    return queryResult.Distinct().ToList();
                }
                else
                {
                    var queryResult = objFindUserDetails.ToList().Skip((objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize) + (objFindUserProp.DefaultPageSize - objFindUserProp.PageSize)).Take(objFindUserProp.PageSize).ToList();
                    //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                    if (queryResult != null && queryResult.Count > 0)
                    {
                        queryResult.FirstOrDefault().TotalPageCount = pageCount;
                    }
                    return queryResult.Distinct().ToList();
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
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-29,2016</createdOn>
        public int RemoveUserDetails(FindUserProp objFindUserProp)
        {
            int value = 0;
            try
            {
                int userID = Convert.ToInt32(objFindUserProp.UserID);
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // creating the object of PortStorageEntities Database
                using (PortStorageEntities objAppWorksEntities = new PortStorageEntities())
                {
                    string userName = (from tblUser in objAppWorksEntities.Users where tblUser.UserID == userID select tblUser.UserCode).FirstOrDefault();
                    string password = (from tblUser in objAppWorksEntities.Users where tblUser.UserID == userID select tblUser.Password).FirstOrDefault();

                    if (userID != 0)
                    {
                        var userRecord = (from tblUser in objAppWorksEntities.Users where tblUser.UserID == userID && tblUser.Password == password && tblUser.UserCode == userName select tblUser).FirstOrDefault();
                        User user = objAppWorksEntities.Users.Where(U => U.UserID == userID).FirstOrDefault();
                        user.RecordStatus = objFindUserProp.RecordStatus;
                        objAppWorksEntities.Users.Remove(userRecord);
                        objAppWorksEntities.SaveChanges();
                        value = objAppWorksEntities.SaveChanges();
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
            return value;
        }


    }

    public static class getDistinct
    {

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }

}

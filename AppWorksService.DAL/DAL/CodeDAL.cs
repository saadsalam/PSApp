using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Globalization;
using AppWorksService.Properties;
using AppWorksService.DAL.Edmx;

namespace AppWorksService.DAL
{
    public class CodeDAL
    {

        /// <summary>
        /// To Add the Record of Code
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-21,2016</createdOn>
        public int AddCodeAdmin(CodeProp objCodeProp)
        {
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                int codeID;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    Code objCode = new Code();
                    objCode.CodeType = objCodeProp.CodeType;
                    objCode.Code1 = objCodeProp.Code1;
                    objCode.CodeDescription = objCodeProp.CodeDescription;
                    objCode.Value1 = objCodeProp.Value1;
                    objCode.Value1Description = objCodeProp.Value1Description;
                    objCode.Value2 = objCodeProp.Value2;
                    objCode.Value2Description = objCodeProp.Value2Description;
                    objCode.RecordStatus = objCodeProp.RecordStatus;
                    objCode.SortOrder = objCodeProp.SortOrder;
                    objCode.CreationDate = objCodeProp.CreationDate;
                    objCode.CreatedBy = objCodeProp.CreatedBy;
                    objCode.UpdatedDate = objCodeProp.UpdatedDate;
                    objCode.UpdatedBy = objCodeProp.UpdatedBy;
                    objCode.CodeName = objCodeProp.CodeName;
                    objCode.Description = objCodeProp.Description;
                    objCode.DataType = objCodeProp.DataType;
                    objCode.DataSubType = objCodeProp.DataSubType;
                    objCode.DAIOnlyInd = objCodeProp.DAIOnlyInd;


                    psEntities.Codes.Add(objCode);      /// Insert the Record in BillingPeriod Table.
                    psEntities.SaveChanges();                                /// Check the Chenges in Table After Record Insertion.
                    codeID = objCode.CodeID;         /// Return the BillingPeriodID of Inserted Billing Period.
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {

                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return codeID;
            }
        }

        /// <summary>
        ///  This Method to use the Fill Code Type list
        /// </summary>
        /// <returns>List</returns>
        public List<string> CodeTypeList()
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var list = (from Tbl in psEntities.Codes orderby Tbl.CodeType,Tbl.Code1,Tbl.SortOrder,Tbl.CodeDescription select Tbl.CodeType).Distinct().ToList();
                    return list.ToList();
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
        /// To Find Code Record
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns>IList<BillingPeriod></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-23,2016</createdOn>
        public IList<CodeProp> FindCode(CodeProp objCodeProp)
        {
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                IList<CodeProp> listCodeProp;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    if (string.Equals(objCodeProp.CodeType, "All"))
                    {
                        listCodeProp = psEntities.Codes.Select(y => new CodeProp()
                        {
                            CodeID = y.CodeID,
                            Code1 = y.Code1,
                            CodeDescription = y.CodeDescription,
                            CodeName = y.CodeName,
                            CodeType = y.CodeType,
                            CreatedBy = y.CreatedBy,
                            CreationDate = y.CreationDate,
                            DAIOnlyInd = y.DAIOnlyInd,
                            DataSubType = y.DataSubType,
                            DataType = y.DataType,
                            Description = y.Description,
                            RecordStatus = y.RecordStatus,
                            SortOrder = y.SortOrder,
                            UpdatedBy = y.UpdatedBy,
                            UpdatedDate = y.UpdatedDate,
                            Value1 = y.Value1,
                            Value1Description = y.Value1Description, 
                            Value2 = y.Value2,
                            Value2Description = y.Value2Description
                        }).OrderBy(x => x.CodeType).ThenBy(x => x.SortOrder).ThenBy(x => x.Code1).ToList();
                    }
                    else
                    {
                        listCodeProp = psEntities.Codes.Where(x => string.Equals(x.CodeType, objCodeProp.CodeType)).Select(y => new CodeProp()
                        {
                            CodeID = y.CodeID,
                            Code1 = y.Code1,
                            CodeDescription = y.CodeDescription,
                            CodeName = y.CodeName,
                            CodeType = y.CodeType,
                            CreatedBy = y.CreatedBy,
                            CreationDate = y.CreationDate,
                            DAIOnlyInd = y.DAIOnlyInd,
                            DataSubType = y.DataSubType,
                            DataType = y.DataType,
                            Description = y.Description,
                            RecordStatus = y.RecordStatus,
                            SortOrder = y.SortOrder,
                            UpdatedBy = y.UpdatedBy,
                            UpdatedDate = y.UpdatedDate,
                            Value1 = y.Value1,
                            Value1Description = y.Value1Description,
                            Value2 = y.Value2,
                            Value2Description = y.Value2Description
                        }).OrderBy(x => x.CodeType).ThenBy(x => x.SortOrder).ThenBy(x => x.Code1).ToList(); 
                    }
                    //var pageCount = listCodeProp.ToList().Count;
                    //var queryResult = listCodeProp.ToList().Skip(objCodeProp.CurrentPageIndex * objCodeProp.PageSize).Take(objCodeProp.PageSize).ToList();
                    //if (queryResult != null && queryResult.Count > 0)
                    //{
                    //    queryResult.FirstOrDefault().TotalPageCount = pageCount;
                    //}
                    //return queryResult.OrderBy(x=>x.SortOrder).ThenBy(x=>x.CodeType).ThenBy(x=>x.Code1).ToList();

                    var pageCount = listCodeProp.ToList().Count;
                    if (objCodeProp.CurrentPageIndex == 0)
                    {
                        var queryResult = listCodeProp.ToList().Skip(objCodeProp.CurrentPageIndex * objCodeProp.PageSize).Take(objCodeProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.OrderBy(x => x.CodeType).ThenBy(x => x.SortOrder).ThenBy(x => x.Code1).ToList(); 
                    }
                    else
                    {
                        var queryResult = listCodeProp.ToList().Skip((objCodeProp.CurrentPageIndex * objCodeProp.PageSize) + (objCodeProp.DefaultPageSize - objCodeProp.PageSize)).Take(objCodeProp.PageSize).ToList();
                        //var queryResult = query.ToList().Skip(objFindUserProp.CurrentPageIndex * objFindUserProp.PageSize).Take(objFindUserProp.PageSize).ToList();
                        if (queryResult != null && queryResult.Count > 0)
                        {
                            queryResult.FirstOrDefault().TotalPageCount = pageCount;
                        }
                        return queryResult.OrderBy(x => x.CodeType).ThenBy(x => x.SortOrder).ThenBy(x => x.Code1).ToList(); 
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
        /// This method is used to update code details
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool UpdateCodeAdminDetails(CodeProp objCodeProp)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    Code code = psEntities.Codes.Where(c => c.CodeID == objCodeProp.CodeID).FirstOrDefault();
                    code.Code1 = objCodeProp.Code1;
                    code.CodeType = objCodeProp.CodeType;
                    code.RecordStatus = objCodeProp.RecordStatus;
                    code.CodeDescription = objCodeProp.CodeDescription;
                    code.Value1 = objCodeProp.Value1;
                    code.Value1Description = objCodeProp.Value1Description;
                    code.Value2 = objCodeProp.Value2;
                    code.Value2Description = objCodeProp.Value2Description;
                    code.SortOrder = objCodeProp.SortOrder;
                    code.UpdatedDate = objCodeProp.UpdatedDate;
                    code.UpdatedBy = objCodeProp.UpdatedBy;
                    psEntities.SaveChanges();
                    result = true;
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
        /// This method is used to delete code details from database
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool DeleteCodeAdminDetails(int CodeID)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {
                    var CodeData = psEntities.Codes.Where(C => C.CodeID == CodeID).FirstOrDefault();
                    if (CodeData != null)
                    {
                        psEntities.Codes.Remove(CodeData);
                        psEntities.SaveChanges();
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
        /// This method is used to check duplicate code and codetype values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool CheckDuplicateCodeAndType(string code, string codeType)
        {
            bool result = false;
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                using (PortStorageEntities psEntities = new PortStorageEntities())
                {

                    var codeChk = psEntities.Codes.Where(c => c.Code1 == code && c.CodeType == codeType).ToList().FirstOrDefault();
                    if (codeChk == null)
                        result = true;
                    else
                        result = false;

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
        /// This method is used to get the code list.
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="codeDesc"></param>
        /// <returns></returns>
        /// <createdOn>Jun-06-2016</createdon>
        public IList<CodeProp> GetCodeDetailsForInvoice(string codeType, string codeDesc)
        {
            using (PortStorageEntities psEntities = new PortStorageEntities())
            {
                IList<CodeProp> listCodeProp;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    listCodeProp = psEntities.Codes.Select(y => new CodeProp()
                    {
                        CodeID = y.CodeID,
                        Code1 = y.Code1,
                        CodeDescription = y.CodeDescription,
                        CodeName = y.CodeName,
                        CodeType = y.CodeType,
                        CreatedBy = y.CreatedBy,
                        CreationDate = y.CreationDate,
                        DAIOnlyInd = y.DAIOnlyInd,
                        DataSubType = y.DataSubType,
                        DataType = y.DataType,
                        Description = y.Description,
                        RecordStatus = y.RecordStatus,
                        SortOrder = y.SortOrder,
                        UpdatedBy = y.UpdatedBy,
                        UpdatedDate = y.UpdatedDate,
                        Value1 = y.Value1,
                        Value1Description = y.Value1Description,
                        Value2 = y.Value2,
                        Value2Description = y.Value2Description
                    }).Where(x => x.CodeType == codeType && x.CodeDescription == codeDesc).OrderBy(x => x.CodeType).ThenBy(x => x.SortOrder).ThenBy(x => x.Code1).ToList(); 
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
                return listCodeProp;
            }
        }
    }
}

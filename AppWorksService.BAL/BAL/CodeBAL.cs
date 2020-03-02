using System;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;

namespace AppWorksService.BAL
{
    public class CodeBAL
    {
        /// <summary>
        /// To Add the Record of Code
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-20,2016</createdOn>
        public int AddCodeAdmin(CodeProp objCodeProp)
        {
            int result;
            CodeDAL objCodeDAL = new CodeDAL();  /// Creating The Object of CodeDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                result = objCodeDAL.AddCodeAdmin(objCodeProp); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objCodeDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
        }
        /// <summary>
        /// This method is used to code type list
        /// </summary>
        /// <returns></returns>
        public List<string> CodeTypeList()
        {
            try
            {
                CodeDAL objCodeDAL = new CodeDAL();
                return objCodeDAL.CodeTypeList();
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
        /// To Find Billing Period Record
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>IList<BillingPeriod></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-20,2016</createdOn>
        public IList<CodeProp> FindCode(CodeProp objCodeProp)
        {
            IList<CodeProp> listCodeProp;
            CodeDAL objCodeDAL = new CodeDAL();  /// Creating The Object of BillingDAL 
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                listCodeProp = objCodeDAL.FindCode(objCodeProp); /// sending the new record model to the data access layer.
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objCodeDAL = null;
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "End {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return listCodeProp;
        }

        /// <summary>
        /// This method is used to update code details
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        public bool UpdateCodeAdminDetails(CodeProp objCodeProp)
        {
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeDAL objCodeDAL = new CodeDAL();
                return objCodeDAL.UpdateCodeAdminDetails(objCodeProp);
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
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeDAL objCodeDAL = new CodeDAL();
                return objCodeDAL.DeleteCodeAdminDetails(CodeID);
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
            CodeDAL objCodeDAL = new CodeDAL();
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                return objCodeDAL.CheckDuplicateCodeAndType(code, codeType);

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
            try
            {
                CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                CodeDAL objCodeDal = new CodeDAL();
                return objCodeDal.GetCodeDetailsForInvoice(codeType, codeDesc);
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

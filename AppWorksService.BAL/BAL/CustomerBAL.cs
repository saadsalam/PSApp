using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;
using AppWorksService.DAL;
using System.Globalization;
using System.Reflection;

namespace AppWorksService.BAL
{
    public class CustomerBAL
    {       
        /// <summary>
        /// Function To find the vehicle search details.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-2,2016</createdOn>
        public List<CustomerSearchProp> GetCustomerSearchDetails(CustomerSearchProp objCustomerProp)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling the function to get the details of customer searched.
                return objCustomerDAL.GetCustomerSearchDetails(objCustomerProp);
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
        /// Function To find the List of Customer Type.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-10,2016</createdOn>
        public List<string> CutomerType()
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Calling the Customer Type List.
                return objCustomerDAL.CustomerType();
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
        /// Get the code list coressponding to the given code type
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public List<CodeList> LoadCodeList(string codeType)
        {

            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.LoadCodeList(codeType);
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
        /// Get Location List for a perticular location type or all location type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="locationType"></param>
        /// <returns></returns>
        public List<LocationList> GetLocationList(LocationList objLocationProp)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.GetLocationList(objLocationProp);

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
        /// To get the data for billing address for a perticular customer.
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        public List<LocationList> GetBillingStreetAddress(int addressID)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.GetBillingStreetAddress(addressID);
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





        #region Code Should be removed after cleanup

        ///// <summary>
        ///// To get the for from location for a perticular customer..
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> FromLocationList(int customerID)
        //{
        //    CustomerDAL objCustomerDAL = new CustomerDAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerDAL.FromLocationList(customerID);
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
        ///// To get the data for to location for a perticular customer..
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //public List<LocationList> ToLocationList(int customerID)
        //{
        //    CustomerDAL objCustomerDAL = new CustomerDAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerDAL.ToLocationList(customerID);
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
        ///// To get the order history for a perticular customer.
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <param name="orderStatus"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //public List<OrderHistoryList> GetOrderHistory(int customerID, string orderStatus, DateTime? startDate, DateTime? endDate)
        //{
        //    CustomerDAL objCustomerDAL = new CustomerDAL();
        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerDAL.GetOrderHistory(customerID, orderStatus, startDate, endDate);
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
        /// To get the complete data for charge rate list for a customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="activeOnly"></param>
        /// <returns></returns>
        //public List<ChargeRateList> GetChargerateList(int customerID,string fromLocation,string toLocation,bool activeOnly)
        //{
        //    CustomerDAL objCustomerDAL = new CustomerDAL();

        //    CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        return objCustomerDAL.GetChargerateList(customerID, fromLocation, toLocation, activeOnly);
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



        /// <summary>
        /// To get the notes list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<CustomerNoteList> GetNotesList(int customerID, DateTime? startDate, DateTime? endDate)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.GetNotesList(customerID, startDate, endDate);
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
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PortStorageRateList> GetPortStorageRateList(PortStorageRateList portStorageRateProp)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.GetPortStorageRateList(portStorageRateProp);
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
        /// To add the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool AddPortStorageRate(PortStorageRateList portStorageRateProp)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.AddPortStorageRate(portStorageRateProp);
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
        /// To add the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool UpdatePortStorageRate(PortStorageRateList portStorageRateProp)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.UpdatePortStorageRate(portStorageRateProp);
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
        /// To add the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool DeletePortStorageRate(int portStorageRateID)
        {
            CustomerDAL objCustomerDal = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDal.DeletePortStorageRate(portStorageRateID);
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
        /// Get the origin location list for Location Performance Standard
        /// </summary>
        /// <returns></returns>
        public List<LocationList> LoadPerformanceStndrdOriginLocationList()
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.LoadPerformanceStndrdOriginLocationList();
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
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool AddLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                return objCustomerDAL.AddLocationEmailContact(objLocationEmailContact);
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
        /// For inserting LocationPerformancestandard in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.AddLocationPerformanceStandard(objLocationPerformanceStandard);
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
        /// For updating locationemailcontact information in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateLocationEmailContact(LocationEmailContactList objLocationEmailContact)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.UpdateLocationEmailContact(objLocationEmailContact);
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
        /// For updating LocationPerformancestandard informance in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.UpdateLocationPerformanceStandard(objLocationPerformanceStandard);
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
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public int AddLocation(LocationList objLocation)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.AddLocation(objLocation);
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
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool AddCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.AddCustomerNotes(objCustomerNotes);
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
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        public bool UpdateCustomerNotes(CustomerNoteList objCustomerNotes)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.UpdateCustomerNotes(objCustomerNotes);
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
        /// For updating other information of customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public bool UpdateCustomerSearchDetails(CustomerSearchProp objcustomer)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.UpdateCustomerSearchDetails(objcustomer);
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
        /// For inserting Customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        public int InsertCustomer(CustomerSearchProp objCustomer)
        {
            CustomerDAL objCustomerDAL = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDAL.InsertCustomer(objCustomer);
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
        ///  This Method to deete the location details
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public bool DeleteLocation(int locationID)
        {
            CustomerDAL objCustomerDal = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDal.DeleteLocation(locationID);
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
        /// For updating location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateLocation(LocationList objLocation)
        {
            CustomerDAL objCustomerDal = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDal.UpdateLocation(objLocation);
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
        /// For Updating customer address in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public bool UpdateCustomerAddredss(string addressType, int customerID, int locationID)
        {
            CustomerDAL objCustomerDal = new CustomerDAL();
            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDal.UpdateCustomerAddredss(addressType, customerID, locationID);
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
        /// For getting address type of customer from the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        public string GetCustomerAddredssType(int locationID, int customerID)
        {
            CustomerDAL objCustomerDal = new CustomerDAL();

            CommonDAL.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                return objCustomerDal.GetCustomerAddredssType(locationID, customerID);
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

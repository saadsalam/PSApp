using System.ServiceModel;
using AppWorksService.Properties;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace AppWorksService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.    
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAppWorksServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns></returns>
        [OperationContract]
        LoginProperties GetLoggedInUserDetails(LoginProperties objLoginProp);

        /// <summary>
        /// Implements To Validate Login
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>bool</returns>
        [OperationContract]
        LoginResult ValidateLogin(LoginProperties objLoginProp);

        /// <summary>
        /// Implements To Get Logged User Role
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        [OperationContract]
        List<string> GetUserRole(LoginProperties objLoginProp);

        /// <summary>
        /// Implements To Get The Logged User name
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>String</returns>
        [OperationContract]
        string UserName(LoginProperties objLoginProp);

        /// <summary>
        /// Implements To Get To check the User Exists
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns>Bool</returns>
        [OperationContract]
        bool IsUserExists(AdminUserProp objAdminUserProp, List<RoleList> lstRoles);

        /// <summary>
        /// Implements To Check The Exisiting User Role.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> ExistingUserRole(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To Check The All Roles.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> AllRoles(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To Check The Modified User Role.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> ModifiedUserRole(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To Check The Remove User Role.
        /// </summary>
        /// <param name="objAdminUserProp"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> RemoveUserRole(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To show The Record Status.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        List<string> RecordList();

        /// <summary>
        /// Implements To Show The Role List.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        List<string> RoleList();

        /// <summary>
        /// Implements To Show The Role List.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        List<FindUserDetails> GetUserRecord(FindUserProp objFindUserProp);

        /// <summary>
        /// Implements To Show The Modification Record.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        AdminUserDeatils GetModificationRecord(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To Save The Updated Record.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        void UpdateUserDetails(AdminUserProp objAdminUserProp, List<RoleList> lstRoles);

        /// <summary>
        /// Implements To SRemove Record.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        void DeleteUserDetails(AdminUserProp objAdminUserProp);

        /// <summary>
        /// Implements To Remove Record.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        int RemoveUserDetails(FindUserProp objFindUserProp);

        ///// <summary>
        ///// Get the search result for Vehicle locator
        ///// </summary>
        ///// <param name="objVehicleProp"></param>
        ///// <returns>List<VehicleProp></returns>
        [OperationContract]
        Task<List<VehicleProp>> GetVehicleSearchDetails(VehicleProp objVehicleProp);

        /// <summary>
        /// Get the search result for customer search
        /// </summary>
        /// <param name="objCustomerProp"></param>
        ///<returns>List<CustomerSearchProp></returns>
        //[OperationContract]
        //List<CustomerSearchProp> GetVehicleSearchDetails(CustomerSearchProp objCustomerProp);

        /// <summary>
        /// Insert the vehicle details for customer
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>int</returns>
        [OperationContract]
        int InsertVehicleDetails(VehicleProp objVehicleProp);

        ///// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        [OperationContract]
        List<PerDiemProp> GetPerDiemVehicalDetails(int portStorageVehiclesId);

        /// <summary>
        /// function to Edit The Damage Code
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-6,2016</createdOn>
        [OperationContract]
        int EditDamageCode(PortStorageDamageDetailsProp objPortStorageDamageDetailsProp);


        /// <summary>
        /// Funtion To Search The Customer Search Details
        /// </summary>
        /// <param name="objCustomerProp"></param>
        /// <returns>List</returns>
        [OperationContract]
        List<CustomerSearchProp> GetCustomerSearchDetails(CustomerSearchProp objCustomerProp);

        /// <summary>
        /// function to get the vehicle Per Diem details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        [OperationContract]
        List<PerDiemProp> GetVehiclePerDiemSearchDetails(int portStorageVehiclesId);

        /// <summary>
        /// Insert the Request Processing details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>int</returns>
        [OperationContract]
        int UpdatePortStorageProcessignDetails(VehicleProp objVehicleProp);

        /// <summary>
        /// Get the all type Of Customer.
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<string> CustomerType();

        /// <summary>
        /// Get the all type Of Customer.
        /// </summary>
        /// <param></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        int DecodeVIN(string Vin);



        /// <summary>
        /// Get the all post storage processign details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<VehicleProp> GetPortStorageProcessignDetails(string VIN);

        /// <summary>
        /// update the Request Processing date out details
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>int</returns>
        [OperationContract]
        int UpdatePortStorageDateOutProcessignDetails(VehicleProp objVehicleProp);


        /// <summary>
        /// Get the all post storage processign date out details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<VehicleProp> GetPortStorageDateOutProcessignDetails(string VIN);

        /// <summary>
        /// Get the all the PortStorage Vehical History Details.
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<PortStorageLocationHistoryProp> GetLocationHistory(int portStorageVehicalID);

        /// <summary>
        /// Get the all post storage processign details by VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        // [OperationContract]
        //VehicleProp GetVecheleDetailByVIN(string VIN);

        /// <summary>
        /// Get the all The Inspection Type Only.
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        VehicleProp GetVecheleDetailByVIN(string VIN,int? vehicleId);


        /// <summary>
        /// Update the Request Processing date for batch process
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>string</returns>
        [OperationContract]
        string RequestBatchProcess(VehicleProp objVehicleProp);
        /// <summary>
        /// Update the date out Processing for batch process
        /// </summary>
        /// <param name="objVehicleProp"></param>
        /// <returns>string</returns>
        [OperationContract]
        string DateOutBatchProcess(VehicleProp objVehicleProp);

        /// <summary>
        /// Get the all post storage processign details by VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<VehicleProp> CheckMultipleVecheleDetailByVIN(string VIN);
        [OperationContract]
        List<string> InspectionTypeOnly();

        /// <summary>
        /// Get the all Vehical StatusList
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<string> VehicalStatusList();

        /// <summary>
        /// Get the all Vehical Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        List<VehicleProp> CallVehialDetailsbyVIN(VehicleProp objVehicleProp);


        /// <summary>
        /// Get the Update Vehical Search Details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        bool UpdateVehicalSearchDetails(VehicleProp objVehicleProp);

        ///// <summary>
        /// function to get the vehicle d details
        /// </summary>
        /// <param name="portStorageVehiclesId"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-5,2016</createdOn>
        [OperationContract]
        List<PortStorageDamageDetailsProp> BindVehicleDamageDetail(int portStorageVehiclesId);

        /// <summary>
        /// function to add the damage code
        /// </summary>
        /// <param name="DamageCodeProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-9,2016</createdOn>
        [OperationContract]
        int AddDamageCode(DamageCodeProp objDamageCodeProp);
        /// <summary>
        /// function to deleet the vehicle details.
        /// </summary>
        /// <param name="VehicleId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteVehicalSearchDetails(int VehicleId);
        /// <summary>
        /// function to add the Billing Period
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-19,2016</createdOn>
        [OperationContract]
        int AddBillingPeriod(BillingPeriodprop objBillingPeriodprop);

        /// <summary>
        /// function to Find the Billing Period
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-20,2016</createdOn>
        [OperationContract]
        IList<BillingPeriodprop> FindBillingPeriod(BillingPeriodprop objBillingPeriodprop);

        /// <summary>
        /// function to add the Code
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-21,2016</createdOn>
        [OperationContract]
        int AddCodeAdmin(CodeProp objCodeProp);
        /// <summary>
        /// this is used to bind Code type 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> CodeTypeList();

        /// <summary>
        /// Get the Update billing Details
        /// </summary>
        /// <param name="objBillingPeriodprop"></param>
        /// <returns>List<VehicleProp></returns>
        [OperationContract]
        bool UpdateBillingPeriodAdminDetails(BillingPeriodprop objBillingPeriodprop);
        /// <summary>
        /// This is used to delete billing details
        /// </summary>
        /// <param name="BillingPeriodId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteBillingPeriodAdminDetails(int BillingPeriodId);

        /// <summary>
        /// this is used to Find Code 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<CodeProp> FindCode(CodeProp objCodeProp);
        /// <summary>
        /// This interface is used to Update code admin details.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        [OperationContract]
        bool ModifyCodeAdminRecord(CodeProp objCodeProp);

        /// <summary>
        /// This interface is used to delete record for code admin 
        /// </summary>
        /// <param name="CodeId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteCodeAdminDetails(int CodeId);

        /// <summary>
        /// this is used to Get Port Storage Invoices List 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<PortStorageInvoicesProp> GetPortStorageInvoicesList(Nullable<DateTime> cutoffDate);

        /// <summary>
        /// Get the code list coressponding to the given code type
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        [OperationContract]
        List<CodeList> LoadCodeList(string codeType);

        /// <summary>
        /// Get Location List for a perticular location type or all location type
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="locationType"></param>
        /// <returns></returns>
        [OperationContract]
        List<LocationList> GetLocationList(LocationList objLocationProp);

        /// <summary>
        /// To get the data for billing address for a perticular customer.
        /// </summary>
        /// <param name="addressID"></param>
        /// <returns></returns>
        [OperationContract]
        List<LocationList> GetBillingStreetAddress(int addressID);

        /// <summary>
        /// To get the complete data for charge rate list for a customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <param name="activeOnly"></param>
        /// <returns></returns>
        //[OperationContract]  
        //List<ChargeRateList> GetChargerateList(int customerID, string fromLocation, string toLocation, bool activeOnly);


        #region Code to be removed after code cleanup

        ///// <summary>
        ///// To get the for from location .
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //[OperationContract]
        //List<LocationList> FromLocationList(int customerID);

        ///// <summary>
        ///// To get the data for to location .
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <returns></returns>
        //[OperationContract]
        //List<LocationList> ToLocationList(int customerID);

        ///// <summary>
        ///// To get the order history for a perticular customer.
        ///// </summary>
        ///// <param name="customerID"></param>
        ///// <param name="orderStatus"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns></returns>
        //[OperationContract]
        //List<OrderHistoryList> GetOrderHistory(int customerID, string orderStatus, DateTime? startDate, DateTime? endDate);


        ///// <summary>
        ///// this is used to Get Port Storage print error Invoices List 
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //IList<PortStoragePrintErrorProp> GetPrintErrorsForInvoiceList();
        #endregion


        /// <summary>
        /// To get the notes list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerNoteList> NotesList(int customerID, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// To get the portstoragerate list for a perticular customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageRateList> GetPortStorageRateList(PortStorageRateList objPortStorageRateProp);

        /// <summary>
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddPortStorageRate(PortStorageRateList objPortStorageRateProp);

        /// <summary>
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePortStorageRate(PortStorageRateList objPortStorageRateProp);

        /// <summary>
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeletePortStorageRate(int portStorageRateID);


        /// <summary>
        /// Get the origin location list for Location Performance Standard
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<LocationList> LoadPerformanceStndrdOriginLocationList();

        /// <summary>
        /// For inserting locationemailcontact in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddLocationEmailContact(LocationEmailContactList objLocationEmailContact);

        /// <summary>
        /// For inserting LocationPerformancestandard in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard);

        /// <summary>
        /// For updating locationemailcontact information in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLocationEmailContact(LocationEmailContactList objLocationEmailContact);

        /// <summary>
        /// For updating LocationPerformancestandard informance in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLocationPerformanceStandard(LocationPerformanceStandardList objLocationPerformanceStandard);

        /// <summary>
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        [OperationContract]
        int AddLocation(LocationList objLocation);

        /// <summary>
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddCustomerNotes(CustomerNoteList objCustomerNotes);

        /// <summary>
        /// For inserting CustomerNotes in database.
        /// </summary>
        /// <param name="objLocationPerformanceStandard"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerNotes(CustomerNoteList objCustomerNotes);

        /// <summary>
        /// For updating other information of customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerSearchDetails(CustomerSearchProp objcustomer);

        /// <summary>
        /// For inserting Customer in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertCustomer(CustomerSearchProp objCustomer);

        /// <summary>
        ///  This Method to deete the location details
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteLocation(int locationID);

        /// <summary>
        /// function UpdateBilling to update the billing details.
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateBilling(BillingProp objBillingprop);

        /// <summary>
        /// Function to update the port storage vehicle records for invoice generation
        /// </summary>
        /// <param name="objPortStorageVehicleProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        [OperationContract]
        bool UpdatePostStorageVehicles(PortStorageVehicleProp objPortStorageVehicleProp);

        /// <summary>
        /// Function to insert the invoice billing details
        /// </summary>
        /// <param name="objBillingprop"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-29,2016</createdOn>
        [OperationContract]
        int InsertBillingId(BillingProp objBillingprop);

        /// <summary>
        /// Function to get the setting details
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        [OperationContract]
        string SetDefaultvalue(string valueKey);

        /// <summary>
        /// Function to update the setting table values
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        [OperationContract]
        bool UpdateSettingsValue(string invoiceNumber);

        /// <summary>
        /// Function to update the setting table values for next invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Feb-12,2018</createdOn>
        [OperationContract]
        bool UpdateNextInvoiceNumberValue(string iInvoiceNumber);
        

        /// <summary>
        /// Function to get the billing details count
        /// </summary>
        /// <param name="valueKey"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        [OperationContract]
        int GetBillingCount(string invoiceNumber);


        /// <summary>
        /// Function to get thevehicle list for generate invoice
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <param name="customerId"></param>
        /// <returns>IList</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        [OperationContract]
        IList<PortStorageVehicleProp> GetPortStorageVehicleList(Nullable<DateTime> cutoffDate, int customerId);

        /// <summary>
        /// For Inserting location information in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLocation(LocationList objLocation);


        /// <summary>
        /// Function to calculate the per diem for generate invoice
        /// </summary>
        /// <param name="psVehicleId"></param>
        /// <param name="userCode"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-1,2016</createdOn>
        [OperationContract]
        string CalculatePortStoragePerDiem(int psVehicleId, string userCode);


        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        [OperationContract]
        int PsRatesCount(DateTime? startDate, int customerId);

        /// <summary>
        /// Function to get the port storage rates details count
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="customerId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        [OperationContract]
        PSRatesInvoiceProp PsRatesInvoice(DateTime? startDate, int customerId);

        /// <summary>
        /// Function to update the rate details in port storage table
        /// </summary>
        /// <param name="entryRate"></param>
        /// <param name="perDiemGraceDays"></param>
        /// <param name="vehicleID"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-2,2016</createdOn>
        [OperationContract]
        bool UpdateVehicleRates(decimal? entryRate, int? perDiemGraceDays, int vehicleID);

        /// <summary>
        /// This method is used to check duplicate code and codetype values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        [OperationContract]
        bool CheckDuplicateCodeAndType(string code, string codeType);

        /// <summary>
        /// Function to get the customer information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        [OperationContract]
        CustomerSearchProp GetCustomerInfo(int customerId);

        /// <summary>
        /// Function to get the outside carrier leg list details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerSearchProp</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        [OperationContract]
        List<LoadInfoList> GetLoadInfo(int billingId, decimal pvRatePercentage);

        /// <summary>
        /// Function to get the table column names
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>List<string></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-3,2016</createdOn>
        [OperationContract]
        List<string> GetTableColumnNames(string tableName);

        /// <summary>
        /// This method is used to check duplicate calender  and period values.
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>May-24-2016</createdon>
        [OperationContract]
        bool CheckDuplicateCalenderAndPeriod(int year, int period);

        /// <summary>
        /// Function to get the vehicle leg list count details
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>List<VehicleLegCountProp></returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        [OperationContract]
        List<VehicleLegCountProp> GetVehicleLegsInfo(int recordId);

        /// <summary>
        /// Function to get the Invoice Credit Cost Center Number
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        [OperationContract]
        int GetInvoiceCreditCostCenterNumber();

        /// <summary>
        /// Function to insert the invoice billing line items details
        /// </summary>
        /// <param name="BillingLineItemsProp"></param>
        /// <returns>int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jun-4,2016</createdOn>
        [OperationContract]
        int InsertBillingLineItems(BillingLineItemsProp objBillingLineprop);

        /// <summary>
        /// For Updating customer address in the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCustomerAddredss(string addressType, int customerID, int locationID);

        /// <summary>
        /// For getting address type of customer from the database.
        /// </summary>
        /// <param name="objLocation"></param>
        /// <returns></returns>
        [OperationContract]
        string GetCustomerAddredssType(int locationID, int customerID);

        /// <summary>
        /// This method is used to get the code list.
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="codeDesc"></param>
        /// <returns></returns>
        /// <createdOn>Jun-06-2016</createdon>
        [OperationContract]
        IList<CodeProp> GetCodeDetailsForInvoice(string codeType, string codeDesc);


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
        [OperationContract]
        IList<PortStoragePrintInvoiceProp> GetInvoiceDataForInvoicePrint(int ReprintInd, int ReprintType, int DateType, Nullable<DateTime> InvoiceDateFrom, Nullable<DateTime> InvoiceDateTo, string InvoiceNumberFrom, string InvoiceNumberTo);

        /// <summary>
        /// this is used to Get vehicle listing report data 
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="GroupByDealerInd"></param>
        /// <returns></returns>
        [OperationContract]
        IList<VehicleListingReportProp> GetVehicleListingReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate, bool GroupByDealerInd);

        /// <summary>
        /// This interface is used to get all customer list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageRequestsReportProp> LoadCustomerList();

        /// <summary>
        /// This interface is used to get all Load batch list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageRequestsReportProp> LoadBatchList();
        /// <summary>
        /// This interface is used to Get Port Storage Request Report list
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="CustomerId"></param>
        /// <param name="VIN"></param>
        /// <param name="RequestDateFrom"></param>
        /// <param name="RequestDateTo"></param>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        [OperationContract]
        IList<PortStorageRequestsReportProp> GetPortStorageRequestReport(int ReportType, int CustomerId, string VIN, Nullable<DateTime> RequestDateFrom, Nullable<DateTime> RequestDateTo, int BatchId);
        /// <summary>
        /// This interface is used to Get Port Storage Vehicle summery Report list
        /// </summary>
        /// <param name="ReportType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [OperationContract]
        IList<VehicleListingReportProp> GetPortStorageVehicleSummeryReport(int ReportType, Nullable<DateTime> StartDate, Nullable<DateTime> EndDate);
        /// <summary>
        /// This interface is used to Get Port Storage Vehicle Lane Report list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageInventoryList> GetPortStorageLaneSummaryList();

        /// <summary>
        /// This interface is used to Port Storage InBound List
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [OperationContract]
        Task<List<PortStorageInventoryList>> GetPortStorageInBoundList(Nullable<DateTime> StartDate, Nullable<DateTime> EndDate);

        /// <summary>
        /// For getting Customer's AddressName
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserApplicationConstantsProp> GetDAIAddressName(string userCode);

        /// <summary>
        /// For getting Invoice list coressponding to a billingID and time period
        /// </summary>
        /// <param name="billinID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<InvoiceListProp> LoadInvoiceList(int? billinID, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// For getting batch list for inventory comparision 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageInventoryList> GetBatchLocationImport();



        /// <summary>
        /// For getting comparsion list coressponding to batch
        /// </summary>
        /// <param name="batchID"></param>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageInventoryList> GetInventoryComparisionList(int? batchID);


        /// <summary>
        /// For updating batch id of print request vehical list
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRequestPrintIndexForVehicles(List<string> vehicleIds);
        

        /// <summary>
        /// To get the user list for customer web portal.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<WebPortalUserList> GetUsers(WebPortalUserList objUserList);

        /// <summary>
        /// To get list of all modules with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<ModuleList> GetModules(int userID);

        /// <summary>
        /// To get list of all customer with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserCustomerList> GetCustomers(int userID);

        /// <summary>
        /// For inserting New User in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddUser(WebPortalUserList objUser);

        /// <summary>
        /// For inserting Customers of a user in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddUserCustomer(UserCustomerList objUserCustomer);

        /// <summary>
        /// For inserting user modules in database.
        /// </summary>
        /// <param name="objLocationEmailContact"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddUserModule(ModuleList objModuleUser);

        /// <summary>
        ///  This Method to deete a particular user
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteUser(int userID);

        /// <summary>
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<RoleList> GetRoles(int userID);

        /// <summary>
        /// To get list of all groups with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<GroupList> GetGroups(int userID);

        /// <summary>
        /// Method for inserting a new user with customers,modules,roles and groups
        /// </summary>
        /// <param name="objUser"></param>
        /// <param name="lstUserCustomer"></param>
        /// <param name="lstUserModules"></param>
        /// <param name="lstUserRoles"></param>
        /// <param name="lstUserGroups"></param>
        /// <returns></returns>
        [OperationContract]
        bool InsertUpdateUser(WebPortalUserList objUser, List<UserCustomerList> lstUserCustomer, List<ModuleList> lstUserModules, List<RoleList> lstUserRoles, List<GroupList> lstUserGroups);

        /// <summary>
        /// method for checking duplicate emailid
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckDuplicateEmail(string emailID);

        /// <summary>
        /// method for checking duplicate username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckDuplicatePortalUserName(string userName);

        /// <summary>
        /// To get the vehicle inventory details coressponding to vin number and customer id.
        /// </summary>
        /// <param name="vinNUmber"></param>
        /// <param name="customerID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageVehicleProp> GetPortStorageInventoryDetails(PortStorageVehicleProp objPortStorageProp);

        /// <summary>
        ///  This Method is to update the complete list of  request checked vehicles
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRequestCheckedVehicles(List<PortStorageVehicleProp> lstRequestedVechicles, string user);

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckMultipleVIN(string vIN);

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        [OperationContract]
        PortStorageVehicleProp GetDecodedPortStorageVIN(string vin);


        /// <summary>
        ///  This Method is for inserting information in  the decodevin  table
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        [OperationContract]
        bool InsertDecodeVIN(VINDecodeList objVINDecode);

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        [OperationContract]
        VINDecodeList GetDecodedVINForVINDecode(string vin);

        /// <summary>
        ///  This Method is for checking duplicate VIN number
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns></returns>
        [OperationContract]
        VINDecodeList GetDecodeDataFromWeb(string vin, string bodyStylep);


        /// <summary>
        /// To get list of all roles with their selection status coressponding to UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<RoleList> GetRolesSelection(int userID);

        /// <summary>
        /// This method is used to Get Billing Record Export
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="ExportDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<BillingLineItemsProp> GetBillingRecordExport(int ExportType, DateTime? ExportDate);

        /// <summary>
        /// This method is used to get batch id 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetBillingExportBatchId();

        /// <summary>
        /// Update table for next batch id
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns>bool</returns>
        [OperationContract]
        bool SetBillingExportNextBatchId(int BatchId);

        /// <summary>
        /// This method is used to Get billing Export File Path
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string GetBillingExportFilePath();


        /// <summary>
        /// This method is used to update Billing Line Item
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="BillingLineItemsID"></param>
        /// <param name="UserCode"></param>
        [OperationContract]
        void UpdateBillingLineItem(int BatchId, int BillingLineItemsID, string UserCode);

        /// <summary>
        /// Log Error to database using Entity
        /// </summary>
        [OperationContract]
        void LogErrorToDb(ErrorLogProp errorProp);

        #region :: Sprint 5 ::
        /// <summary>
        /// Implements To Storage Vehicle Outgate for security.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        List<StorageVehicleOutgateProp> UpdateStorageVehicleOutgate(StorageVehicleOutgateProp objStorageVehicleOutgateProp);
        /// <summary>
        /// This is used to Get batchId from database
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetPortStorageVehiclesBatchId();
        /// <summary>
        /// This is used to Set batch ID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SetPortStorageVehiclesNextBatchId(int BatchId);

        /// <summary>
        /// This is used to get message from storeprocedure
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        [OperationContract]
        PortStorageVehicleImportProp ImportPortStorageVehicles(int BatchId, string User);
        /// <summary>
        /// This is used to get all list Port Storage Vehicle Import
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageVehicleImportProp> GetPortStorageVehicleImportList(int BatchId);

        [OperationContract]
        List<PortStorageVehicleImportProp> LoadVehiclesBatchList(string Vin);
        /// <summary>
        /// This is used to Get Port Storage Vehicles Import File Directory
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetPortStorageVehiclesImportFileDirectory();

        /// <summary>
        /// This is used to Get Port Storage Vehicles Import File Name
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetPortStorageVehiclesImportFileName();

        /// <summary>
        /// This is used to Get Port Storage Vehicles Import File Archive Directory
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetPortStorageVehiclesImportFileArchiveDirectory();

        [OperationContract]
        int GetPortStorageLocationBatchId();
        /// <summary>
        /// This is used to Set batch ID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SetPortStorageLocationNextBatchId(int BatchId);

        /// <summary>
        /// This is used to get message from storeprocedure
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        [OperationContract]
        PortStorageVehicleImportProp ImportPortStorageLocation(int BatchId, string User);
        /// <summary>
        /// This is used to get all list Port Storage Vehicle Import
        /// </summary>
        /// <param name="BatchId"></param>
        /// <returns></returns>
        [OperationContract]
        List<PortStorageVehicleImportProp> GetPortStorageLocationImportList(int BatchId);

        [OperationContract]
        List<PortStorageVehicleImportProp> LoadLocationBatchList(string Vin);

        /// <summary>
        /// This is used to Get Port Storage Vehicles Import File Name
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetPortStorageLocationImportFileName();
        /// <summary>
        /// This is used to get data and insert location table.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        [OperationContract]
        bool LocationImportTransactionProcess(int BatchId, string User);
        /// <summary>
        /// This is used to get data and insert location table.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        [OperationContract]
        bool VehicleImportTransactionProcess(int BatchId, string User);

        /// <summary>
        /// To get list of system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        List<AdminUserProp> GetSystemSettings();

        /// <summary>
        /// For updating compnay information
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCompanyInfo(UserApplicationConstantsProp objCompanyinfo);


        /// <summary>
        /// This method is used to get storage vehicle details list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-20-2016</createdon>
        [OperationContract]
        List<PortStorageVehicleProp> GetStorageVehicleDetails(PortStorageVehicleProp objPortstorageProp);


        /// <summary>
        /// This method is used to get invoice line items list
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        [OperationContract]
        List<BillingLineItemsProp> GetLineItemsList(BillingLineItemsProp objLineItems);

        /// <summary>
        /// This method is used to reset exported ind
        /// </summary>
        /// <param name="objCodeProp"></param>
        /// <returns></returns>
        /// <createdOn>Jul-21-2016</createdon>
        [OperationContract]
        bool ResetExportedInd(int billingID);


        
        /// <summary>
        /// To Add Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        [OperationContract]
        int InsertBilling(BillingListProp objBillingProp);


        /// <summary>
        /// To Update Record in Billing table
        /// </summary>
        /// <param name="objBillingPeriod"></param>
        /// <returns>Int</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul-22,2016</createdOn>
        [OperationContract]
        bool UpdateBillingTab(BillingListProp objBillingProp);

        /// <summary>
        ///  This method is used  for deleting data from billing and related tables
        /// </summary>
        /// <param name="BillingPeriodID"></param>
        /// <returns></returns>
        /// // <createdOn>May-23,2016</createdOn>
        [OperationContract]
        bool DeleteBillingData(int BillingID);

        /// <summary>
        /// To get the billing search details coressponding to search criteria.
        /// </summary>
        /// <param name="BillingProp"></param>
        /// <returns>List<BillingProp></returns>
        [OperationContract]
        Task<List<BillingProp>> BillingSearch(BillingProp objPortStorageProp);

        #region CODE TO BE REMOVED AFTER CODE CLEANUP

        ///// <summary>
        ///// This method is used to get the invoice details for a billing id.
        ///// </summary>
        ///// <param name="objCodeProp"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-20-2016</createdon>
        //[OperationContract]
        //List<BillingInvoiceDetailsProp> GetInvoiceDetails(bool CreditedOutInd, bool CreditMemoInd, bool NewRunInd, int BillingID, int RunID, int CustomerID);

        ///// <summary>
        ///// This method is used to get the list of the drivers.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //[OperationContract]
        //List<string> DriverList();

        ///// <summary>
        ///// This method is used to get the list of outside carrier.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns></returns>
        ///// <createdOn>Jul-21-2016</createdon>
        //[OperationContract]
        //List<string> OutsideCarrier();

        #endregion


        /// <summary>
        /// To get bililng data from billing table
        /// </summary>
        /// <param name="billingID"></param>
        /// <returns></returns>
        [OperationContract]
        BillingListProp GetBillingData(int billingID);

        /// <summary>
        /// Implements To Storage Vehicle Outgate for security.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [OperationContract]
        StorageVehicleOutgateProp UpdateStorageVehicleOutgateData(StorageVehicleOutgateProp objStorageVehicleOutgateProp);

        /// <summary>
        /// To update system settings
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSystemSettings(AdminUserProp setting);

        [OperationContract]
        List<AdminUserProp> FindSystemSettings(AdminUserProp setting);

        [OperationContract]
        bool DeleteSystemSettings(AdminUserProp setting);

        [OperationContract]
        bool InsertSystemSettings(AdminUserProp setting);
        #endregion

    }
}

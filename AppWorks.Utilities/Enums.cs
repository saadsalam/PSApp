namespace AppWorks.Utilities
{
    public class Enums
    {
        public enum ErrorCodes
        {
            /// <summary>
            /// VIN not found in Port Storage Vehicles table
            /// </summary>
            VinNotFound = 100002,

            /// <summary>
            /// Multiple matches found for VIN
            /// </summary>
            MultipleVinFound = 100003,

            /// <summary>
            /// No Request Date Entered
            /// </summary>
            NoRequestDateEntered = 100001,

            /// <summary>
            /// No VIN Number Entered.
            /// </summary>
            NoVINEntered = 100000
        }

        public enum SuccessCode
        {
            /// <summary>
            /// Shows that record saved successfuly into database
            /// </summary>
            SavedSuccessFully = 1,

        }
    }


    #region Common for Users
    /// <summary>
    /// Constants for User Roles
    /// </summary>
    public static class UserRoles
    {
        public const string Administrator = "Administrator";
        public const string AutoportExport = "AutoportExport";
        public const string Billing = "Billing";
        public const string Claims = "Claims";
        public const string CreditAuthorization = "Credit Authorization";
        public const string DefaultRole = "Default Role";
        public const string Developer = "Developer";
        public const string Dispatch = "Dispatch";
        public const string Driver = "Driver";
        public const string FleetAdmin = "FleetAdmin";
        public const string Payroll = "Payroll";
        public const string PayrollApproval = "Payroll Approval";
        public const string PhotoAdmin = "PhotoAdmin";
        public const string PieceRateWorker = "PieceRateWorker";
        public const string PortStorage = "PortStorage";
        public const string RateOverride = "Rate Override";
        public const string RoleAdmin = "Role Admin";
        public const string Sales = "Sales";
        public const string Security = "Security";
        public const string TimeclockUser = "Timeclock User";
        public const string VPCAdmin = "VPC Admin";
        public const string VPCPayroll = "VPC Payroll";
        public const string VPCWorker = "VPC Worker";
        public const string WIPOnly = "WIPOnly";
        public const string YardInspector = "YardInspector";
        public const string YardOperations = "YardOperations";


        public static string GetUserRoleToCompare(this string userRole)
        {
            if (string.IsNullOrEmpty(userRole))
                return string.Empty;

            return userRole.Replace(" ", "").Trim().ToLower();
        }
    }


    /// <summary>
    /// Constants for Windows titles
    /// </summary>
    public static class WindowsTitles
    {
        public const string LoginWindowTitle = "Login";
        public const string LogoutWindowTitle = "Logout";
    }
    #endregion

    #region Request Processing Window status
    public static class RequestedVehicleStatus
    {
        public const string Updated = "Updated Successfully";
        public const string UpdatePending = "Update Pending";
    }
    #endregion

    #region Sorting Constants and Grid Columns Names

    public static class VehicleLocatorGridColumns
    {
        public const string VIN = "VIN";
        public const string CustomerName = "Customer Name";
        public const string MakeModel = "Make/Model";
        public const string Location = "Location";
        public const string DateIn = "Date In";
        public const string DateOut = "Date Out";
        public const string Days = "Days";
        public const string Requested = "Requested";
    }

    public static class SortOrder
    {
        public const string ASC = "Ascending";
        public const string DESC = "Descending";
    }

    #endregion
}

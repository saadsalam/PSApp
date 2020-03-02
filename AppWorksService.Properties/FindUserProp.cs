using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AppWorksService.Properties
{
    /// <summary>
    /// Class for holding Find User Properties
    /// </summary>
    //[Serializable]
    public class FindUserProp
    {
        public string UserID { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PIN { get; set; }
        public string Phone { get; set; }
        public string PhoneExtension { get; set; }
        public string CellPhone { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string LabelXOffset { get; set; }
        public string LabelYOffset { get; set; }
        public string IMEI { get; set; }
        public Nullable<System.DateTime> LastConnection { get; set; }
        public string datsVersion { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string EmployeeNumber { get; set; }
        public string PortPassIDNumber { get; set; }
        public string Department { get; set; }
        public string StraightTimeRate { get; set; }
        public string PieceRateRate { get; set; }
        public string PDIRate { get; set; }
        public string FlatBenefitPayRate { get; set; }
        public string AlternateEmailAddress { get; set; }
       private List<string> listRoles = new List<string>();
        public List<string> lstRoles { get { return listRoles; } } 
        public string newRole { get; set; }
        public string SelectedRole { get; set;}
        public string selectedStatusRole { get;set;}
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }
    }

    public class FindUserDetails
    {
        #region

        private int userID;
        private string firstName;
        private string lastName;
        private string userCode;
        private string userStatus;
        private string userRole;
        private long totalPageCount;

        #endregion

        public int UserID
        {
            get { return userID; }
            set
            {
                if (value != null)
                {
                    userID = value;
                }
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != null)
                {
                    firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != null)
                {
                    lastName = value;
                }
            }
        }

        public string UserCode
        {
            get { return userCode; }
            set
            {
                if (value != null)
                {
                    userCode = value;
                }
            }
        }

        public string UserStatus
        {
            get { return userStatus; }
            set
            {
                if (value != userStatus)
                {
                    userStatus = value;
                }
            }
        }

        public string UserRole
        {
            get { return userRole; }
            set
            {
                if (value != userRole)
                {
                    userRole = value;
                }
            }
        }
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                if (value != totalPageCount)
                {
                    totalPageCount = value;
                }
            }
        }
    }

}

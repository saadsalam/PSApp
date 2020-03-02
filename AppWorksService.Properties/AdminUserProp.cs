using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AppWorksService.Properties
{
    /// <summary>
    /// Class for holding UserAdmin Properties
    /// </summary>

    public class AdminUserProp
    {
        public int loggedUserID { get; set; }
        public int UserID { get; set; }
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
        public string ValueKey { get; set; }
        public string ValueDescription { get; set; }
    }

    public class AdminUserDeatils
    {
        private int userID;
        public int UserIDInfo
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

        private string userCode;
        public string UserCodeInfo
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

        private string firstName;
        public string FirstNameInfo
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

        private string lastName;
        public string LastNameInfo
        {
            get { return lastName; }
            set
            {
                if(value!=null)
                {
                    lastName = value;
                }
            }
        }

        private string phone;
        public string PhoneInfo
        {
            get { return phone; }
            set
            {
                if (value != null)
                {
                    phone = value;
                }
            }
        }

        private string extention;
        public string ExtentionInfo
        {
            get { return extention; }
            set
            {
                if (value != null)
                {
                    extention = value;
                }
            }
        }

        private string cellPhone;
        public string CellPhoneInfo
        {
            get { return cellPhone; }
            set
            {
                if (value != null)
                {
                    cellPhone = value;
                }
            }
        }

        private string faxnumber;
        public string FaxNumberInfo
        {
            get { return faxnumber; }
            set
            {
                if(value !=null)
                {
                    faxnumber = value;
                }
            }
        }

        private string email;
        public string EmailInfo
        {
            get { return email; }
            set
            {
                if(value!=null)
                {
                    email = value;
                }
            }
        }

        private string password;
        public string PasswordInfo
        {
            get { return password; }
            set
            {
                if(value !=null)
                {
                    password = value;
                }
            }
        }

        private string pin;
        public string PinInfo
        {
            get { return pin; }
            set
            {
                if (value != null)
                {
                    pin = value;
                }
            }
        }

        private decimal lblXOffset;
        public decimal LblXOffsetInfo
        {
            get { return lblXOffset; }
            set
            {
                if (value != null)
                {
                    lblXOffset = value;
                }
            }
        }

        private decimal lblYOffset;
        public decimal LblYOffsetInfo
        {
            get { return lblYOffset; }
            set
            {
                if (value != null)
                {
                    lblYOffset = value;
                }
            }
        }

        private string employee;
        public string EmployeeInfo
        {
            get { return employee; }
            set
            {
                if (value != null)
                {
                    employee = value;
                }
            }
        }


        private string recordStatus;
        public string RecordStatusInfo
        {
            get { return recordStatus; }
            set
            {
                if (value != null)
                {
                    recordStatus = value;
                }
            }
        }


        private string portPassId;
        public string PortPassIdInfo
        {
            get { return portPassId; }
            set
            {
                if (value != null)
                {
                    portPassId = value;
                }
            }
        }

        private Nullable<System.DateTime> creationDate;
        public Nullable<System.DateTime> CreationDateInfo
        {
            get { return creationDate; }
            set
            {
                if (value != null)
                {
                    creationDate = value;
                }
            }
        }

        private string createdBy;
        public string CreatedByInfo
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                {
                    createdBy = value;
                }
            }
        }
    }
}

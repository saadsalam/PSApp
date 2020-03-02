using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class WebPortalUserList
    {
        public int userID { get; set; }
        public int? customerID { get; set; }
        public int? moduleID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Nullable<int> isActive { get; set; }
        public Nullable<int> isSuperUser { get; set; }
        public Nullable<System.DateTime> lastLogin { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateModified { get; set; }
        public string whoModified { get; set; }
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class UserCustomerList
    {
        public int? UserID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public bool? IsSelected { get; set; }
    }
}

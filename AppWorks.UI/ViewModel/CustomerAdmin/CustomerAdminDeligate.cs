using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorks.UI.Model;

namespace AppWorks.UI.ViewModel.CustomerAdmin
{
   public class CustomerAdminDeligate
   {

       /// <summary>
       /// SetValueMethod function
       /// </summary>
       /// <param name="value"></param>
       public delegate void SetBillStreetAddrValue(LocationModel value);
       public static event SetBillStreetAddrValue OnSetBillStreetAddrValueEvt;
       public static void SetValueBillStreetAddrMethod(LocationModel value)
       {
           if (OnSetBillStreetAddrValueEvt != null)
               OnSetBillStreetAddrValueEvt(value);
       }

       /// <summary>
       /// SetValueMethod function
       /// </summary>
       /// <param name="value"></param>
      
       public delegate void SetCustomerAdminPropertiesValue(bool value);
       public static event SetCustomerAdminPropertiesValue OnSetCustomerAdminPropertiesValueEvt;
       public static void SetValueCustomerAdminPropertiesMethod(bool value)
       {
           if (OnSetCustomerAdminPropertiesValueEvt != null)
               OnSetCustomerAdminPropertiesValueEvt(value); 
       }

       public delegate void SetUpdateAddressValue(string value);
       public static event SetUpdateAddressValue OnSetUpdateAddressValueEvt;
       public static void SetUpdateAddressMethod(string value)
       {
           if (OnSetUpdateAddressValueEvt != null)
               OnSetUpdateAddressValueEvt(value);
       }


       public delegate void SetValuePageNumberClick(int pageNumber);
       public static event SetValuePageNumberClick OnSetValuePageNumberCmd;
       public static void SetValueMethodPageNumber(int val)
       {
           if (OnSetValuePageNumberCmd != null)
               OnSetValuePageNumberCmd(val);
       }
   }
}

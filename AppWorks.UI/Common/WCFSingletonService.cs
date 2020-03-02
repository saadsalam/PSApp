using AppWorks.UI.AppService;
using System;
using System.ServiceModel;

namespace AppWorks.UI.Common
{
    /// <summary>
    /// This class is used to get the single instance of WCF service per user
    /// </summary>
    public class WcfSingleInstance
    {
        protected static AppWorksServicesClient _serviceInstance = null;

        public WcfSingleInstance()
        {
            if (_serviceInstance == null)
            {
                try
                {
                    _serviceInstance = new AppWorksServicesClient();                 
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw new FaultException("Some error occurred while creating a service instance.");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.WCFAuthentication.Lib.Behaviours
{
    /// <summary>
    /// This class will act as a custom context, an extension to the OperationContext.
    /// This class holds all context information for our application.
    /// </summary>
    public class ServerContext : IExtension<OperationContext>
    {
        public string UserToken;    

        // Get the current one from the extensions that are added to OperationContext.
        public static ServerContext Current
        {
            get
            {
                return OperationContext.Current.Extensions.Find<ServerContext>();
            }
        }

        #region IExtension<OperationContext> Members
        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.Utilities
{
    public class LogManager : LoggerBase
    {
        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public void LogError(Type type, Exception e)
        {
            this.LogError(type.FullName, e);
        }


        public void LogInfo(Type type, string message)
        {
            this.LogInfo(type.FullName + " - " + message);
        }
    }
}

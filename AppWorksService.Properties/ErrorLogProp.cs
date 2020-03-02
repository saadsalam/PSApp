using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class ErrorLogProp
    {
        public int Id { get; set; }
        public string SystemMac { get; set; }
        public System.DateTime DateTime { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Ndc { get; set; }
        public string Message { get; set; }
        public string BuildVersion { get; set; }
    }
}

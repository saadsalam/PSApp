using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    /// <summary>
    /// DateoutProcessingModel Class allows user to hold the print Data
    /// </summary>
    public class DateoutProcessingModel
    {
        public string VehiclesID { get; set; }
        public string Vin { get; set; }
        public string Name { get; set; }
        public string MakeModel { get; set; }
        public string BayLocation { get; set; }
    }
}

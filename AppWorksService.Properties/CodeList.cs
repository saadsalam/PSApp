using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class CodeList
    {
        public int CodeID { get; set; }
        public string CodeType { get; set; }
        public string Code1 { get; set; }
        public string CodeDescription { get; set; }
        public string Value1 { get; set; }
        public string Value1Description { get; set; }
        public string Value2 { get; set; }
        public string Value2Description { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public string DataSubType { get; set; }
        public Nullable<int> DAIOnlyInd { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWorksService.DAL.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class DealerStorageRequestItem
    {
        public int requestItemId { get; set; }
        public Nullable<int> requestId { get; set; }
        public Nullable<int> vehicleId { get; set; }
        public string vinkey { get; set; }
        public string origLocation { get; set; }
        public Nullable<int> pendingMove { get; set; }
        public Nullable<System.DateTime> dateMoved { get; set; }
    }
}

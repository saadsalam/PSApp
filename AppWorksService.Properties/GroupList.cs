﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class GroupList
    {
        public Nullable<int> GroupID { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string WhoCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string WhoUpdated { get; set; }
        public bool? IsSelected { get; set; }

    }
}

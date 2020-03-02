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
    
    public partial class UserAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccount()
        {
            this.GroupMembers = new HashSet<GroupMember>();
            this.ModuleUsers = new HashSet<ModuleUser>();
            this.RoleMembers = new HashSet<RoleMember>();
            this.UserCustomerRoles = new HashSet<UserCustomerRole>();
        }
    
        public int userID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public Nullable<int> isActive { get; set; }
        public Nullable<int> isSuperUser { get; set; }
        public Nullable<System.DateTime> lastLogin { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public string whoCreated { get; set; }
        public Nullable<System.DateTime> dateModified { get; set; }
        public string whoModified { get; set; }
        public string Phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleUser> ModuleUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleMember> RoleMembers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCustomerRole> UserCustomerRoles { get; set; }
    }
}

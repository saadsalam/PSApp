using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace AppWorksService.Properties
{
    /// <summary>
    /// Class for holding Login Properties
    /// </summary>
    //[Serializable]
    [DataContract]
    public class LoginProperties
    {
        public LoginProperties()
        {
            UserRoles = new List<string>();
        }

        /// <summary>
        /// Propeorty for user id
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// property for username 
        /// </summary>
        ///
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// Property for password
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Property for logged in user roles
        /// </summary>
        [DataMember]
        public List<string> UserRoles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FullUserName { get; set; }
    }
}

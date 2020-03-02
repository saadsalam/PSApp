namespace AppWorksService.Properties
{
    /// <summary>
    /// Used to get the Login Result
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Used to check whether login is successful
        /// </summary>
        public bool IsLoginSuccessful { get; set; }

        /// <summary>
        /// Used to store the token from the service
        /// </summary>
        public string Token { get; set; }
    }
}

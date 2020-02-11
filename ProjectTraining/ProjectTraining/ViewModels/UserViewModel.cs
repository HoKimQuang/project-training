using System;

namespace ProjectTraining.ViewModels
{
    /// <summary>
    /// define UserViewModel
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; }
        public DateTime ExpireDate { get; set; }
        public int AccountRenewal { get; set; }
    }
}
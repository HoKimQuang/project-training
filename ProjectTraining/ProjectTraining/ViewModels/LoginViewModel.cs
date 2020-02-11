namespace ProjectTraining.ViewModels
{
    /// <summary>
    ///  LoginModel provide necessary for Login Action
    /// </summary>
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string ReturnUrl { get; set; }
    }
}
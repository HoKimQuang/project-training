namespace ProjectTraining.Common
{
    public class Constants
    {
        public static string ConnectionString = "DefaultConnect";
        
        public const string PathError400 = "/Error/400";
        public const string PathError401 = "/Error/401";
        public const string PathError404 = "/Error/404";
        public const string PathError500 = "/Error/500";
        public const string PathHomeError = "/Home/Error";
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string ReturnUrlParameter = "ReturnUrl";

        public const string JwtIssuer = "Jwt:Issuer";
        public const string JwtKey = "Jwt:Key";
        
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AllowAll = "AllowAll";

        public const string RoleAdmin = "admin";
        public const string RoleUser = "User";

    }
}
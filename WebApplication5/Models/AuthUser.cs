namespace WebApplication5.Models
{
    public class AuthUser
    {
        public AuthUser()
        {
            TheRole = "User";
        }

        public string UserName
        { get; set; }
        public string TheRole { get; set; }
        public string Password { get; set; }

    }
}

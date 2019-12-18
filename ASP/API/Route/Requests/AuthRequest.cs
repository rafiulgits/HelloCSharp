namespace API.Route.Requests
{
    public class AuthRequest
    {
        public class Login
        {
            public string Email {get; set;}
            public string Password {get; set;}
        }

        public class Register
        {
            public string UserName {get; set;}
            public string Email {get; set;}
            public string Password {get; set;}
        }
    }
}
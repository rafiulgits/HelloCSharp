namespace API.Route.Requests
{
    public class AuthRequest
    {
        public class Login : IFormRequest
        {
            public string Email {get; set;}
            public string Password {get; set;}

            public bool IsValid()
            {
                if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    return false;
                }
                return true;
            }
        }

        public class Register : IFormRequest
        {
            public string Name {get; set;}
            public string Email {get; set;}
            public string Password {get; set;}

            public bool IsValid()
            {
                if(
                    string.IsNullOrEmpty(Email) || 
                    string.IsNullOrEmpty(Password) || 
                    string.IsNullOrEmpty(Name))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
using API.Models;

namespace API.Route.Response
{
    public class UserResponse
    {
        public class Profile
        {
            public string Name {set; get;}
            public string Email {set; get;}

            public Profile(User user)
            {
                Name = user.Name;
                Email = user.Email;
            }
        }
    }
}
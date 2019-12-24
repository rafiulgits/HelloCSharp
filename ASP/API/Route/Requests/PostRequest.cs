namespace API.Route.Requests
{
    public class PostRequest
    {
        public class Create : IFormRequest
        {
            public string Title {set; get;}
            public string Body {set; get;}

            public bool IsValid()
            {
                if(string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Body))
                {
                    return false;
                }
                return true;
            }
        }

        public class Update : IFormRequest
        {
            public string Title {set; get;}
            public string Body {set; get;}

            public bool IsValid()
            {
                if( string.IsNullOrEmpty(Title) ||
                    string.IsNullOrEmpty(Body))
                {
                    return false;
                }
                return true;
            }
        }

        public class AddComment : IFormRequest
        {
            public string Body {set; get;}

            public bool IsValid()
            {
                if(string.IsNullOrEmpty(Body))
                {
                    return false;
                }
                return true;
            }
        }

        public class UpdateComment : IFormRequest
        {
            public string Body {set; get;}

            public bool IsValid()
            {
                if(string.IsNullOrEmpty(Body))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
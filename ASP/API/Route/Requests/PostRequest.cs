namespace API.Route.Requests
{
    public class PostRequest
    {
        public class Create
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

        public class Update
        {
            public string PostId {set; get;}
            public string Title {set; get;}
            public string Body {set; get;}
            
        }

        public class AddComment
        {
            public string PostId {set; get;}
            public string Body {set; get;}
        }

        public class UpdateComment
        {
            public string PostId {set; get;}
            public string CommentId {set; get;}
            public string Body {set; get;}
        }
    }
}
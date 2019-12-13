namespace API.Models 
{
    public interface DatabaseSettings 
    {
        string DatabaseHost {set; get;}
        string DatabaseName {set; get;}
        string CollectionName {set; get;}
    }


    public class UserDatabaseSettings : DatabaseSettings
    {
        public string DatabaseHost {set; get;}
        public string DatabaseName {set; get;}
        public string CollectionName {set; get;}
    }


    public class PostDatabaseSettings : DatabaseSettings
    {
        public string DatabaseHost {set; get;}
        public string DatabaseName {set; get;}
        public string CollectionName {set; get;}
    }
}
using Live.Models;
using System.Threading.Tasks;


namespace Live.Dispatchers
{
    public interface IHubDispatcher
    {
        Task Dispatch(Notification notification);
    }
}
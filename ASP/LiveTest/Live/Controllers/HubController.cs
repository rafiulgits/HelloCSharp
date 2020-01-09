using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Live.Models;
using Live.Dispatchers;

namespace Live.Controllers
{
    [Route("[controller]")]
    public class HubController : Controller
    {
        private readonly IHubDispatcher _dispatcher;

        public HubController(IHubDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Hello([FromBody] Notification notification)
        {
            System.Console.WriteLine("called");
            await _dispatcher.Dispatch(notification);
            return Ok("{response : OK}");
        }
    }
}
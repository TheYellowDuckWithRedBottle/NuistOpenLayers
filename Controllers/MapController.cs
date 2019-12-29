using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XinYiThree.Data.Logger;

namespace XinYiThree.Controllers
{
    public class MapController:Controller
    {
        private readonly ILogger<MapController> Logger;
        public MapController(ILogger<MapController> logger)
        {
            Logger = logger;
        }

        

        public IActionResult Index()
        {
            
            Logger.LogInformation(MyLogEventIds.HomePage,"Visit the map control");
            return View();
        }
    }
}

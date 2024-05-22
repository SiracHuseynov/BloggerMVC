using Blogger.Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

		public HomeController(IEventService eventService)
		{
			_eventService = eventService;
		}

		public IActionResult Index()
        {
            var events = _eventService.GEtAllEvents();
            return View(events);
        }

      
    }
}
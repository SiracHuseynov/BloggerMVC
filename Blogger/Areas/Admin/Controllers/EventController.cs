using Blogger.Business.Exceptions;
using Blogger.Business.Services.Abstracts;
using Blogger.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Blogger.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]

	public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        //public IActionResult Index()
        //{
        //    var events = _eventService.GEtAllEvents();
        //    return View(events);
        //}

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
        {
            var paginatedEvents = await _eventService.GetPaginatedEventAsync(pageIndex, pageSize);
            return View(paginatedEvents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event eventt)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _eventService.AddAsyncEvent(eventt);
            }
            catch (MustBeImageFileException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existEvent = _eventService.GetEvent(x => x.Id == id);

            if (existEvent == null)
                return NotFound();

            return View(existEvent);
        }

        [HttpPost]
        public IActionResult Update(Event eventt)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _eventService.UpdateEvent(eventt.Id, eventt);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existEvent = _eventService.GetEvent(x => x.Id == id);

            if (existEvent == null)
                return NotFound();

            return View(existEvent);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _eventService.DeleteEvent(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}

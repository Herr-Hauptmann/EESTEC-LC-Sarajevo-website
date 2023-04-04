using EESTEC.Data;
using EESTEC.Helpers;
using EESTEC.Interfaces;
using EESTEC.Models;
using EESTEC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EESTEC.Controllers
{
    public class LocalEventController : Controller
    {
        private readonly ILocalEventRepository _localEventRepository;

        public LocalEventController(ILocalEventRepository localEventRepository)
        {
            _localEventRepository = localEventRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int page=1)
        {
            IEnumerable<LocalEvent> events = await _localEventRepository.GetAll();

            //Pagination
            const int pageSize = 15;
            if (page < 1)
                page = 1;
            int itemsCount = events.Count();
            var pager = new Pager(itemsCount, page, pageSize);
            int itemSkip = (page - 1) * pageSize;
            var data = events.Skip(itemSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LocalEvent localEvent)
        {
            if (!ModelState.IsValid)
                return View(localEvent);
            _localEventRepository.Create(localEvent);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var localEvent = await _localEventRepository.GetById(id);
            if (localEvent == null)
            {
                return View("Error");
            }
            var editVM = new EditLocalEventViewModel
            {
                Title = localEvent.Title,
                Description = localEvent.Description,
                Date = localEvent.Date,
                EventType = localEvent.EventType,
            };
            return View(editVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLocalEventViewModel eventVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Greška pri uređivanju lokalnog događaja!");
                return View("Edit", eventVM);
            }
            var localEvent = await _localEventRepository.GetById(id);
            if (localEvent==null)
                return View("Edit", eventVM);
            
            localEvent.Date = eventVM.Date;
            localEvent.EventType = eventVM.EventType;
            localEvent.Title = eventVM.Title;
            localEvent.Description = eventVM.Description;

            _localEventRepository.Update(localEvent);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var localEvent = await _localEventRepository.GetById(id);
            if (localEvent == null)
                return RedirectToAction("Index");
            _localEventRepository.Delete(localEvent);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LocalEventsPartial()
        {
            var localEvents = await _localEventRepository.GetMostRecent();
            return PartialView("~/Views/LocalEvent/_LocalEvents.cshtml", localEvents);
        }

        public async Task<IActionResult> AllLocalEventsPartial()
        {
            var localEvents = await _localEventRepository.GetAll();
            return PartialView("~/Views/LocalEvent/_LocalEvents.cshtml", localEvents);
        }

        public IActionResult Show()
        {
            return View();
        }
    }
}

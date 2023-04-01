using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;
using EESTEC.ViewModel;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<LocalEvent> events = await _localEventRepository.GetAll();
            return View(events);
        }
        
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var localEvent = await _localEventRepository.GetById(id);
            if (localEvent == null)
                return RedirectToAction("Index");
            _localEventRepository.Delete(localEvent);
            return RedirectToAction("Index");
        }
    }
}

using EESTEC.Data;
using EESTEC.Interfaces;
using EESTEC.Models;
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
    }
}

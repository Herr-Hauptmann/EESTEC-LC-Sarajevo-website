using EESTEC.Interfaces;
using EESTEC.Models;
using EESTEC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EESTEC.Controllers
{
    public class PartnerCategoryController : Controller
    {
        private readonly IPartnerCategoryRepository _partnerCategoryRepository;

        public PartnerCategoryController(IPartnerCategoryRepository partnerCategoryRepository)
        {
            _partnerCategoryRepository = partnerCategoryRepository;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var partnerCategories = await _partnerCategoryRepository.GetAllAsync();
            return View(partnerCategories);
        }

        public async Task<IActionResult> ShowPartial()
        {
            var partners = await _partnerCategoryRepository.GetAllAsync();
            return PartialView("~/Views/PartnerCategory/_PartnersPartial.cshtml", partners);
        }

        [Authorize]
        public IActionResult Create()
        {
            var partnerCategoryVM = new PartnerCategoryViewModel();
            return View(partnerCategoryVM);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PartnerCategoryViewModel partnerCategoryVM)
        {
            if (!ModelState.IsValid)
                return View(partnerCategoryVM);
            var partnerCategory = new PartnerCategory
            {
                Name = partnerCategoryVM.Name,
                DisplayOrder = partnerCategoryVM.DisplayOrder,
            };
            _partnerCategoryRepository.Create(partnerCategory);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(id);
            if (partnerCategory == null)
            {
                return View("Error");
            }
            var partnerCategoryVM = new PartnerCategoryViewModel
            {
                Name = partnerCategory.Name,
                DisplayOrder = partnerCategory.DisplayOrder,
            };
            return View(partnerCategoryVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PartnerCategoryViewModel partnerCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Greška pri uređivanju lokalnog događaja!");
                return View("Edit", partnerCategoryVM);
            }
            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(id);
            if (partnerCategory == null)
                return View("Edit", partnerCategoryVM);

            partnerCategory.Name = partnerCategoryVM.Name;
            partnerCategory.DisplayOrder = partnerCategoryVM.DisplayOrder;

            _partnerCategoryRepository.Update(partnerCategory);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(id);
            if (partnerCategory == null)
                return RedirectToAction("Index");
            _partnerCategoryRepository.Delete(partnerCategory);
            return RedirectToAction("Index");
        }
    }
}

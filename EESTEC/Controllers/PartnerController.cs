using EESTEC.Interfaces;
using EESTEC.Models;
using EESTEC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EESTEC.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerCategoryRepository _partnerCategoryRepository;
        private readonly IPartnerRepository _partnerRepository;

        public PartnerController(IPartnerCategoryRepository partnerCategoryRepository, IPartnerRepository partnerRepository)
        {
            _partnerCategoryRepository = partnerCategoryRepository;
            _partnerRepository = partnerRepository;
        }

        private async Task<List<SelectListItem>> GetPartnerCategoriesAsync()
        {
            var list = new List<SelectListItem>();
            var partnerCategories = await _partnerCategoryRepository.GetAllAsync();
            foreach (var category in partnerCategories)
            {
                list.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                });
            }
            return list;
        }
        public async Task<IActionResult> Index()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return View(partners);
        }

        public async Task<IActionResult> Create()
        {
            var partnerVM = new CreatePartnerViewModel
            {
                PartnerCategoriesSelectList = await GetPartnerCategoriesAsync(),
            };

            return View(partnerVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();
            var partnerVM = new CreatePartnerViewModel
            {
                Name = partner.Name,
                Image = partner.Image,
                SelectedCategory = partner.PartnerCategory.Id.ToString(),
                Website = partner.Website,
                PartnerCategoriesSelectList = await GetPartnerCategoriesAsync(),
            };
            foreach(var cat in partnerVM.PartnerCategoriesSelectList)
            {
                if (cat.Value == partnerVM.SelectedCategory)
                {
                    cat.Selected = true;
                    break;
                }
            }
            return View(partnerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartnerViewModel partnerVM)
        {
            if (!ModelState.IsValid)
            {
               partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }
            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(Int32.Parse(partnerVM.SelectedCategory));
            if (partnerCategory == null)
            {
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            var partner = new Partner
            {
                Name = partnerVM.Name,
                PartnerCategory = partnerCategory,
                Website = partnerVM.Website,
                Image = partnerVM.Image,
            };

            _partnerRepository.Create(partner);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePartnerViewModel partnerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Greška pri uređivanju partnera!");
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View("Edit", partnerVM);
            }
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();

            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(Int32.Parse(partnerVM.SelectedCategory));
            if (partnerCategory == null)
            {
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            partner.PartnerCategory = partnerCategory;
            partner.Website = partnerVM.Website;
            partner.Image = partnerVM.Image;
            partner.Name = partnerVM.Name;

            _partnerRepository.Update(partner);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();
            _partnerRepository.Delete(partner);
            return RedirectToAction("Index");
        }
    }
}

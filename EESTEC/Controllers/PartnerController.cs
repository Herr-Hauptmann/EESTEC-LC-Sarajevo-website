using EESTEC.Interfaces;
using EESTEC.Models;
using EESTEC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EESTEC.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerCategoryRepository _partnerCategoryRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IPhotoService _photoService;
        private readonly IDataProtector _dataProtector;
        public PartnerController(IPartnerCategoryRepository partnerCategoryRepository, IPartnerRepository partnerRepository, IPhotoService photoService, IDataProtectionProvider dataProvider, IConfiguration configuration)
        {
            _partnerCategoryRepository = partnerCategoryRepository;
            _partnerRepository = partnerRepository;
            _photoService = photoService;
            _dataProtector = dataProvider.CreateProtector(configuration.GetValue<String>("ProtectionString"));
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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return View(partners);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var partnerVM = new CreatePartnerViewModel
            {
                PartnerCategoriesSelectList = await GetPartnerCategoriesAsync(),
            };

            return View(partnerVM);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();

            string decryptedAccountNumber = (partner.AccountNumber == null) ? "" : _dataProtector.Unprotect(partner.AccountNumber);

            var partnerVM = new EditPartnerViewModel
            {
                Name = partner.Name,
                AccountNumber = decryptedAccountNumber,
                Image = null,
                ImageUrl = partner.Image,
                SelectedCategory = partner.PartnerCategory.Id.ToString(),
                Website = partner.Website,
                PartnerCategoriesSelectList = await GetPartnerCategoriesAsync(),
            };
            foreach (var cat in partnerVM.PartnerCategoriesSelectList)
            {
                if (cat.Value == partnerVM.SelectedCategory)
                {
                    cat.Selected = true;
                    break;
                }
            }
            return View(partnerVM);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePartnerViewModel partnerVM)
        {
            if (!ModelState.IsValid)
            {
               partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            //Image
            var result = await _photoService.AddPhotoAsync(partnerVM.Image);
            if (result.Error != null)
            {
                ModelState.AddModelError("Photo", "Fotografija nije uspješno učitana");
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(Int32.Parse(partnerVM.SelectedCategory));
            if (partnerCategory == null)
            {
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            string? encryptedAccountNumber = (partnerVM.AccountNumber == null) ? null : _dataProtector.Protect(partnerVM.AccountNumber);

            var partner = new Partner
            {
                Name = partnerVM.Name,
                AccountNumber = encryptedAccountNumber,
                PartnerCategory = partnerCategory,
                Website = partnerVM.Website,
                Image = result.Url.ToString(),
            };

            _partnerRepository.Create(partner);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPartnerViewModel partnerVM)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Greška pri uređivanju partnera!");
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                partnerVM.ImageUrl = partner.Image;
                return View("Edit", partnerVM);
            }
            

            var partnerCategory = await _partnerCategoryRepository.GetByIdAsync(Int32.Parse(partnerVM.SelectedCategory));
            if (partnerCategory == null)
            {
                partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                return View(partnerVM);
            }

            if (partnerVM.Image != null)
            {
                await _photoService.DeletePhotoAsync(partner.Image);
                var result = await _photoService.AddPhotoAsync(partnerVM.Image);
                if (result.Error != null)
                {
                    ModelState.AddModelError("Photo", "Fotografija nije uspješno učitana");
                    partnerVM.PartnerCategoriesSelectList = await GetPartnerCategoriesAsync();
                    partnerVM.ImageUrl = partner.Image;
                    return View(partnerVM);
                }
                partnerVM.ImageUrl = result.Url.ToString();

            }

            partner.PartnerCategory = partnerCategory;
            partner.Website = partnerVM.Website;
            partner.AccountNumber = (partnerVM.AccountNumber == null) ? null : _dataProtector.Protect(partnerVM.AccountNumber);
            if (partnerVM.ImageUrl!= null)
                partner.Image = partnerVM.ImageUrl;
            partner.Name = partnerVM.Name;

            _partnerRepository.Update(partner);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner == null)
                return NotFound();
            var result = await _photoService.DeletePhotoAsync(partner.Image);
            if (result.Error != null)
            {
                return RedirectToAction("Index");
            }
            _partnerRepository.Delete(partner);
            return RedirectToAction("Index");
        }
    }
}

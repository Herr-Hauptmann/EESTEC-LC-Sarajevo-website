using EESTEC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class EditPartnerViewModel
    {
        [Required(ErrorMessage = "Naziv partnera je obavezan")]
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string Website { get; set; }
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Obavezno odabrati kategoriju partnera")]
        public string SelectedCategory { get; set; }
        public List<SelectListItem>? PartnerCategoriesSelectList { get; set; }
    }
}

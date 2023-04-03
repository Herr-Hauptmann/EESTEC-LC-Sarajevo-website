using EESTEC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class CreatePartnerViewModel
    {
        [Required(ErrorMessage = "Naziv partnera je obavezan")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Logotip partnera je obavezan")]
        public string Image { get; set; }
        public string Website { get; set; }

        [Required(ErrorMessage = "Obavezno odabrati kategoriju partnera")]
        public string SelectedCategory { get; set; }
        public List<SelectListItem>? PartnerCategoriesSelectList { get; set; }
    }
}

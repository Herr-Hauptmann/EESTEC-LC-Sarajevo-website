using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class PartnerCategoryViewModel
    {
        [Required(ErrorMessage = "Naziv kategorije je obavezan!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obavezno dodati red prikazivanja")]
        public int DisplayOrder { get; set; }
    }
}

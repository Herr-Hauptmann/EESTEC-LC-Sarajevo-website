using System.ComponentModel.DataAnnotations;

namespace EESTEC.Models
{
    public class Partner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public string Website { get; set; }
        public PartnerCategory PartnerCategory { get; set; }
    }
}

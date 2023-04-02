using System.ComponentModel.DataAnnotations;

namespace EESTEC.Models
{
    public class PartnerCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public ICollection<Partner> Partners { get; set; }
    }
}

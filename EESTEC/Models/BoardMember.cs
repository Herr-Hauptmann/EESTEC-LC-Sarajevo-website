using EESTEC.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EESTEC.Models
{
    public class BoardMember
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public Position Position { get; set; }
        public string LinkedIn { get; set; }
        public Board Board { get; set; }
    }
}

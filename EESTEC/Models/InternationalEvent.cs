using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EESTEC.Models
{
    public class InternationalEvent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public User user { get; set; }
    }
}

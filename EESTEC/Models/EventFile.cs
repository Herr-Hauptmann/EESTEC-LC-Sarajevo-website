using System.ComponentModel.DataAnnotations;

namespace EESTEC.Models
{
    public class EventFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public LocalEvent LocalEvent { get; set; }

    }
}

using EESTEC.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EESTEC.ViewModel
{
    public class EditLocalEventViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public EventType EventType { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}

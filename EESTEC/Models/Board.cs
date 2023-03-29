using System.ComponentModel.DataAnnotations;

namespace EESTEC.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Mandate { get; set; }
        public ICollection<BoardMember> BoardMembers { get; set; }
    }
}

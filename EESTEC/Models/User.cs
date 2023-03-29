using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EESTEC.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

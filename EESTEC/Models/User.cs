using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EESTEC.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}

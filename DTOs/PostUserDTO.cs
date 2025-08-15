using System.ComponentModel.DataAnnotations;

namespace AppNum5.Models
{
    public class PostUserDTO
    {
        
        [Required(ErrorMessage = "Name required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "errmsg?")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "errmsg")]
        [EmailAddress(ErrorMessage = "errmsg errmsg")]

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
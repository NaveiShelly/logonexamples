using System.ComponentModel.DataAnnotations;

namespace UserManagerApi.Models
{
    public class UserItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Admin|Editor|Viewer", ErrorMessage = "Role must be Admin, Editor or Viewer")]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
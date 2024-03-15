using Nest;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class ChangePassword
    {
        [Key]
        public int? UserId { get; set; }

        public int? Bit { get; set; }

        [StringLength(12, MinimumLength = 5)]
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}

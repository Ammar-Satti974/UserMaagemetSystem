using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }
        public int Bit { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

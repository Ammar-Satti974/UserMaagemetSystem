using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Users
    {
        [Key]

        public int UserId { get; set; }

        public int Bit { get; set; }

        [Display (Name ="User Name")]
        [Required(ErrorMessage = "Please Enter Username")]
        [RegularExpression(@"^[a-zA-Z0-9'@&#.\s]{6,15}$", ErrorMessage = "Please enter character between 6 to 15")]
        public string Username { get; set; }

        
        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 5)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //public string? Password { get; set; }

        //[Display(Name = "Confirm Password")]
        //[Required(ErrorMessage = "Confirmation Password is required.")]
        //[Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}

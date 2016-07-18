using System.ComponentModel.DataAnnotations;
using Swappler.Utilities;

namespace Swappler.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First name must be between 2 - 10 characters long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Last name must be between 2 - 10 characters long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Username must be between 2 - 25 characters long.")]
        [RegularExpression(RegexPattern.Username, ErrorMessage = "Username can start only with letter, can include numbers and one sign.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(253, MinimumLength = 3, ErrorMessage = "Email must be between 3 - 253 characters long.")]
        [RegularExpression(RegexPattern.Email, ErrorMessage = "Enter valid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be between 3 - 100 characters long.")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Please enter valid password!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required!")]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Terms and Conditions must be agreed!")]
        [Range(typeof(int), "1", "1", ErrorMessage = "Terms and Conditions must be agreed!")]
        public int TermsAndConditions { get; set; }
       
       
    }
}
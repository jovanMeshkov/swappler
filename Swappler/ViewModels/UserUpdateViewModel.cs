
using System.ComponentModel.DataAnnotations;
using System.Web;
using Swappler.Utilities;

namespace Swappler.ViewModels
{
    public class UserUpdateViewModel
    {
        public HttpPostedFileBase Photo { get; set; }

        public int UserId { get; set; }


        [StringLength(10, MinimumLength = 2, ErrorMessage = "First name must be between 2 - 10 characters long.")]
        public string FirstName { get; set; }

        [StringLength(10, MinimumLength = 2, ErrorMessage = "Last name must be between 2 - 10 characters long.")]
        public string LastName { get; set; }

        [StringLength(25, MinimumLength = 2, ErrorMessage = "Username must be between 2 - 25 characters long.")]
        [RegularExpression(RegexPattern.Username, ErrorMessage = "Username can start only with letter, can include numbers and one sign.")]
        public string Username { get; set; }

        [StringLength(253, MinimumLength = 3, ErrorMessage = "Email must be between 3 - 253 characters long.")]
        [RegularExpression(RegexPattern.Email, ErrorMessage = "Enter valid email!")]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be between 3 - 100 characters long.")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Please enter valid password!")]
        public string CurrentPassword { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be between 3 - 100 characters long.")]
        [RegularExpression(RegexPattern.Password, ErrorMessage = "Please enter valid password!")]
        public string NewPassword { get; set; }
        
        [Compare("NewPassword", ErrorMessage = "Passwords don't match!")]
        public string PasswordConfirmation { get; set; }


        public bool DataAvailable
        {
            get
            {
                return
                    Photo != null ||
                    FirstName != null ||
                    LastName != null ||
                    Username != null ||
                    Email != null ||
                    CurrentPassword != null ||
                    NewPassword != null ||
                    PasswordConfirmation != null;
            }
        }
    }
}
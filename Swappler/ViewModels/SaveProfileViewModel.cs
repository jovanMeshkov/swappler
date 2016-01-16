
using System.Web;

namespace Swappler.ViewModels
{
    public class SaveProfileViewModel
    {
        public HttpPostedFileBase Photo { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
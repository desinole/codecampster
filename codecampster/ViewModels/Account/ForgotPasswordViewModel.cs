using System.ComponentModel.DataAnnotations;

namespace Codecamp2018.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

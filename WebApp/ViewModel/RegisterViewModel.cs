using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; init; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; init; }
    }
}
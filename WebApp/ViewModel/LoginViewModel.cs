using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email address"), Required(ErrorMessage = "Email address is required")] 
        public string EmailAddress { get; init; }
        [Required, DataType(DataType.Password)] 
        public string Password { get; init; }
    }
}
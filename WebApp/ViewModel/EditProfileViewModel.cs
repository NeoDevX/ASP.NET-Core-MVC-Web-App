using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.ViewModel
{
    public class EditProfileViewModel
    {
        public Address? Address { get; init; }
        public int? Mileage { get; init; }
        public string? UserName { get; init; }
        public int? Pace { get; init; }
        public IFormFile Image { get; init; }
    }
}
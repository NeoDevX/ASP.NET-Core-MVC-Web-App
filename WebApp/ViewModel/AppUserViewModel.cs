using WebApp.Models;

namespace WebApp.ViewModel
{
    public class AppUserViewModel
    {
        public string Id { get; init; }
        public string? UserName { get; init; }
        public int? Pace { get; init; }
        public int? Mileage { get; init; }
        public Address? Address { get; init; }
        public string? ProfileImageUrl { get; init; }
    }
}
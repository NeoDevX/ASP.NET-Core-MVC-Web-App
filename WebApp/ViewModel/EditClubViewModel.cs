using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.ViewModel
{
    public class EditClubViewModel
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public IFormFile Image { get; init; }
        public int? AddressId { get; init; }
        public Address? Address { get; init; }
        public ClubCategory ClubCategory { get; init; }
    }
}
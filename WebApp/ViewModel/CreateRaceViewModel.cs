using Microsoft.AspNetCore.Http;
using WebApp.Models;

namespace WebApp.ViewModel
{
    public class CreateRaceViewModel
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public IFormFile Image { get; init; }
        public Address Address { get; init; }
        public RaceCategory RaceCategory { get; init; }
        public string AppUserId { get; init; }
    }
}
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.ViewModel
{
    public class DashboardViewModel
    {
        public IEnumerable<Race> Races { get; init; }
        public IEnumerable<Club> Clubs { get; init; }
    }
}
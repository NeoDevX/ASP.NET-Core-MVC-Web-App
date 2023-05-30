using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Dashboard;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _dashboardService.GetAllClubs();
            var races = await _dashboardService.GetAllRaces();
            var dashboardViewModel = new DashboardViewModel
            {
                Clubs = clubs,
                Races = races
            };
            return View(dashboardViewModel);
        }
    }
}
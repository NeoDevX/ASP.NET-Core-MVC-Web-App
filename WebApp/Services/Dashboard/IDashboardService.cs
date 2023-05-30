using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<IEnumerable<Club>> GetAllClubs();
        Task<IEnumerable<Race>> GetAllRaces();
    }
}
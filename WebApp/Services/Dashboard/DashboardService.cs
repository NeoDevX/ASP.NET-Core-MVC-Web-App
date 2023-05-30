using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(ApplicationDataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Club>> GetAllClubs()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var clubs = _dataContext.Clubs.Where(club => club.AppUser.Id == userId);
            return await clubs.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var clubs = _dataContext.Races.Where(race => race.AppUser.Id == userId);
            return await clubs.ToListAsync();
        }
    }
}
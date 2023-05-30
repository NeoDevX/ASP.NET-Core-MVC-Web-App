using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services.Data
{
    public interface IDataProvider
    {
        Task<IEnumerable<Club>> GetClubs();
        Task<Club> GetClubBy(int id, bool noTracking = false);
        Task<IEnumerable<Race>> GetRaces();
        Task<Race> GetRaceBy(int id, bool noTracking = false);
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserBy(string id, bool noTracking = false);
    }
}
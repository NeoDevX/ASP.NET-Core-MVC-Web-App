using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services.Data
{
    public class DataProvider : IDataProvider
    {
        private readonly ApplicationDataContext _dataContext;

        public DataProvider(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Club>> GetClubs() => await _dataContext.Clubs.ToListAsync();
        
        public async Task<Club> GetClubBy(int id, bool noTracking = false)
        {
            if (noTracking)
            {
                return await  _dataContext.Clubs
                    .Include(club => club.Address)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(club => club.Id == id) ?? throw new NullReferenceException();
            }
            
            return await _dataContext.Clubs
                .Include(club => club.Address)
                .FirstOrDefaultAsync(club => club.Id == id) ?? throw new NullReferenceException();
        }
        
        public async Task<IEnumerable<Race>> GetRaces() => await _dataContext.Races.ToListAsync();
        
        public async Task<Race> GetRaceBy(int id, bool noTracking = false)
        {
            if (noTracking)
            {
                return await  _dataContext.Races
                    .Include(race => race.Address)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(race => race.Id == id) ?? throw new NullReferenceException();
            }
            
            return await _dataContext.Races
                   .Include(race => race.Address)
                   .FirstOrDefaultAsync(race => race.Id == id) 
                   ?? throw new NullReferenceException();
        }
    }
}
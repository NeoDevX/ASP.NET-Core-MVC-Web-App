using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions context) : base(context) { }

        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
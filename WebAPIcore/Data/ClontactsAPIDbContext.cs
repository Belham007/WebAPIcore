using Microsoft.EntityFrameworkCore;
using WebAPIcore.Models;

namespace WebAPIcore.Data
{
    public class ClontactsAPIDbContext : DbContext
    {
        public ClontactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}

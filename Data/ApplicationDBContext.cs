using Learn_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Learn_Web_App.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
    }
}

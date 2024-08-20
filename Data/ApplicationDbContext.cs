using Microsoft.EntityFrameworkCore;
using hw_sorting_filtering_pagination.Models;

namespace hw_sorting_filtering_pagination.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}

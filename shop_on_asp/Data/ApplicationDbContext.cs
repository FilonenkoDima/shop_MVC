using shop_on_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace shop_on_asp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories {  get; set; } 
    }
}

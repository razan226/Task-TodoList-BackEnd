


using Microsoft.EntityFrameworkCore;
using TodoListAPIs.Models.Dtos;

namespace Dbcontext
{
    public class AppDbContext : DbContext
    {
        public DbSet<MainList> MainLists { get; set; }
        public DbSet<SubList> SubLists { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}

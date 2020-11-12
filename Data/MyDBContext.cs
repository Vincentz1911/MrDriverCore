using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MrDriverCore.Models;

namespace MrDriverCore.Data
{
    public class MyDBContext:DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Location> Locations { get; set; }       
        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration["SqlConnectionString"]);
        }

        public MyDBContext(DbContextOptions<MyDBContext> options, IConfiguration configuration) :base(options) 
        { 
            Configuration = configuration; 
        }   
    }
}

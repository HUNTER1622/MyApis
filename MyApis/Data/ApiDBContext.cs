using Microsoft.EntityFrameworkCore;
using MyApis.Model;

namespace MyApis.Data
{
    public class ApiDBContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SHIVAAY;Database=Realstate;Trusted_Connection=True");
        }

    }
}

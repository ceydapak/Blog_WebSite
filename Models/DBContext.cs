using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_WebSite.Models
{
    public class DBContext :IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options) :base (options)  { } 
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}

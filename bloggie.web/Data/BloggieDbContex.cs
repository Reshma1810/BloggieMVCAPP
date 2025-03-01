using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace bloggie.web.Data
{
    public class BloggieDbContex:DbContext
    {
        public BloggieDbContex(DbContextOptions Options) : base(Options)
        {

        }
        public DbSet <Blogpost> Blogposts { get; set; }
        public DbSet <Tag> Tags{ get; set; }
    }
}

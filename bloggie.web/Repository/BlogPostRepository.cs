using bloggie.web.Data;
using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace bloggie.web.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContex bloggieDbContex;

        public BlogPostRepository(BloggieDbContex bloggieDbContex)
        {
            this.bloggieDbContex = bloggieDbContex;
        }

        public async Task<Blogpost> AddAsync(Blogpost blogpost)
        {
            await bloggieDbContex.AddAsync(blogpost);
            await bloggieDbContex.SaveChangesAsync();
            return blogpost;
        }

        // Updated DeleteAsync method to accept Blogpost object instead of Guid
        public async Task<Blogpost?> DeleteAsync(Guid id)
        {
            var existingBlog = await bloggieDbContex.Blogposts.FindAsync(id);
            if (existingBlog != null)
            {
                bloggieDbContex.Blogposts.Remove(existingBlog);
                await bloggieDbContex.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<Blogpost>> GetAllAsync()
        {
            return await bloggieDbContex.Blogposts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<Blogpost?> GetAsync(Guid id)
        {
            return await bloggieDbContex.Blogposts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Blogpost?> GetUrlHandleAsync(string urlHandle)
        {
           return await bloggieDbContex.Blogposts.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Blogpost?> UpdateAsync(Blogpost blogpost)
        {
            var existingBlog = await bloggieDbContex.Blogposts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == blogpost.ID);
            if (existingBlog != null)
            {
                existingBlog.ID = blogpost.ID;
                existingBlog.Heading = blogpost.Heading;
                existingBlog.PageTitle = blogpost.PageTitle;
                existingBlog.Content = blogpost.Content;
                existingBlog.Author = blogpost.Author;
                existingBlog.FeaturedImageUrl = blogpost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogpost.UrlHandle;
                existingBlog.ShortDescription = blogpost.ShortDescription;
                existingBlog.PublishedDate = blogpost.PublishedDate;
                existingBlog.Visible = blogpost.Visible;
                existingBlog.Tags = blogpost.Tags;

                await bloggieDbContex.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}

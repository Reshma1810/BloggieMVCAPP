using Azure;
using bloggie.web.Data;
using bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace bloggie.web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContex bloggieDbContex;

        public TagRepository(BloggieDbContex bloggieDbContex)
        {
            this.bloggieDbContex = bloggieDbContex;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContex.Tags.AddAsync(tag);
            await bloggieDbContex.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var existingTag= await bloggieDbContex.Tags.FindAsync(id);
            if(existingTag != null)
            {
                bloggieDbContex.Tags.Remove(existingTag);
                await bloggieDbContex.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllASync()
        {
            return await bloggieDbContex.Tags.ToListAsync();

        }

        public async Task<Tag?> GetAsync(Guid id)
        {
          return  await  bloggieDbContex.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
           var existigTag= await bloggieDbContex.Tags.FindAsync(tag.Id);
            if (existigTag != null)
            {
                existigTag.Name = tag.Name;
                existigTag.DisplayName = tag.DisplayName;
                await bloggieDbContex.SaveChangesAsync();
                return existigTag;
            }
            return null;
        }
    }
}

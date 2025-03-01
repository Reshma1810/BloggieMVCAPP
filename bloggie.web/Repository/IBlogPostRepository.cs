using bloggie.web.Models.Domain;

namespace bloggie.web.Repository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<Blogpost>> GetAllAsync();

        Task<Blogpost?> GetUrlHandleAsync(string urlHandle);
        Task<Blogpost?> GetAsync(Guid id);
        Task<Blogpost> AddAsync(Blogpost blogpost);
        Task<Blogpost?> UpdateAsync(Blogpost blogpost);
        Task<Blogpost?> DeleteAsync(Guid id);



    }
}

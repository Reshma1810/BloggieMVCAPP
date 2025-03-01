using bloggie.web.Models.Domain;
using System.Numerics;

namespace bloggie.web.Repository
{
    public interface ITagRepository
    {
       Task<IEnumerable<Tag>> GetAllASync();
        Task<Tag?> GetAsync (Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid tag);



    }
}

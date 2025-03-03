using bloggie.web.Models.Domain;

namespace bloggie.web.Models.viewModels
{
    public class HomeView
    {
        public IEnumerable<Blogpost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}

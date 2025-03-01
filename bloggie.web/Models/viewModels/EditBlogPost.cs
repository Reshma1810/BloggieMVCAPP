using Microsoft.AspNetCore.Mvc.Rendering;

namespace bloggie.web.Models.viewModels
{
    public class EditBlogPost
    {
        public Guid ID { get; set; }
        public String Heading { get; set; }
        public String PageTitle { get; set; }
        public String Content { get; set; }
        public String ShortDescription { get; set; }
        public String FeaturedImageUrl { get; set; }
        public String UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public String Author { get; set; }
        public String Visible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        //collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<String>();
    }
}

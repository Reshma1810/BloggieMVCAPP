namespace bloggie.web.Models.Domain
{
    public class Blogpost
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

        //Navigation property
        public ICollection<Tag> Tags { get; set; }
    }
}

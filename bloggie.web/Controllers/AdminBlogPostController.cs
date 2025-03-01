using bloggie.web.Models.Domain;
using bloggie.web.Models.viewModels;
using bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bloggie.web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        // GET: Add Blog Post
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllASync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            return View(model);
        }

        // POST: Add Blog Post
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // Map the view model to the domain model
            var blogPost = new Blogpost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            // Map tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

        // GET: List of Blog Posts
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        // GET: Edit Blog Post
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllASync();

            if (blogPost != null)
            {
                var model = new EditBlogPost
                {
                    ID = blogPost.ID,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }

            return View(null);
        }

        // POST: Edit Blog Post
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPost editBlogPostRequest)
        {
            // Map view model back to domain model
            var blogPostDomainModel = new Blogpost
            {
                ID = editBlogPostRequest.ID,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                ShortDescription = editBlogPostRequest.ShortDescription,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible
            };

            // Map tags to domain model
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            // Submit information to repository
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                return RedirectToAction("Edit", new { id = blogPostDomainModel.ID });
            }

            return RedirectToAction("Edit", new { id = blogPostDomainModel.ID });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPost editBlogPostRequest)
        {
            //talk to repo to  delete this blog post and tags
            var deletedblogpost= await blogPostRepository.DeleteAsync(editBlogPostRequest.ID);
            //display the response
            if(deletedblogpost !=null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editBlogPostRequest.ID });
        }
    }
}

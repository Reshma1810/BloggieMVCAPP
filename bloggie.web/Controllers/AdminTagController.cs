using Azure;
using bloggie.web.Data;
using bloggie.web.Models.Domain;
using bloggie.web.Models.view_Models;
using bloggie.web.Models.viewModels;
using bloggie.web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace bloggie.web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        //private readonly BloggieDbContex bloggieDbContex;

        //public AdminTagController(BloggieDbContex bloggieDbContex)
        public AdminTagController(ITagRepository tagRepository)

        {
            this.tagRepository = tagRepository;
            //this.bloggieDbContex = bloggieDbContex;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var DisplayName = Request.Form["Display Name"];

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            await tagRepository.AddAsync(tag);

            //return View("Add");
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use db contex to read the tags
            var tags = await tagRepository.GetAllASync();
            return View(tags);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // First method
            //var tag= bloggieDbContex.Tags.Find(id);
            // Second method
            //var tag = await bloggieDbContex.Tags.FirstOrDefaultAsync(x => x.Id == id);
            var tag = await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //sucess notification              
            }
            else
            {
                //show error Notification
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag= await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                /// show sucess notification 
                return RedirectToAction("List");
            }
            //shown an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}

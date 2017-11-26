using FileUploadsInAspNetMvc.DAL;
using FileUploadsInAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUploadsInAspNetMvc.Controllers
{
    public class ImageController : Controller
    {
        private readonly DatabaseContext dbContext;

        public ImageController()
        {
            this.dbContext = new DatabaseContext();
        }

        public ImageController(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var images = dbContext.Images.AsNoTracking();
            return View(images);
        }


        public ActionResult Create()
        {
            return View(new ImageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    Title = model.Title,
                    AltText = model.AltText,
                    Caption = model.Caption
                };


                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);                    
                    model.ImageUpload.SaveAs(imagePath);
                    image.ImageUrl = imageUrl;
                }


                dbContext.Images.Add(image);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var image = dbContext.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            var model = new ImageEditViewModel
            {
                Id = image.Id,
                Title = image.Title,
                AltText = image.AltText,
                Caption = image.Caption
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ImageEditViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var image = dbContext.Images.Find(id);
                if (image == null)
                {
                    return new HttpNotFoundResult();
                }

                image.Title = model.Title;
                image.AltText = model.AltText;
                image.Caption = model.Caption;

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    image.ImageUrl = imageUrl;
                }

                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
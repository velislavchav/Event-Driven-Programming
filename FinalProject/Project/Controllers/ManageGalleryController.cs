using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.IO;
using System.Data.Entity;
using System.Net;

namespace Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageGalleryController : Controller
    {
        // GET: ManageGallery
        public ActionResult Index()
        {
            List<Gallery> galleries = new List<Gallery>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                galleries = db.Galleries.ToList();
            }
            return View(galleries);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryViewModels model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Gallery newGallery = new Gallery();
                newGallery.Name = model.Name;
                newGallery.Author = model.Author;
                newGallery.Price = model.Price;
                string path = Path.Combine(
                  Server.MapPath("~/Images"),
                  Path.GetFileName(model.Cover.FileName)
                  );
                newGallery.CoverPath = model.Cover.FileName;
                model.Cover.SaveAs(path);
                db.Galleries.Add(newGallery);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "ManageGallery");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries
                    .Find(id);

                if (gallery == null)
                {
                    return RedirectToAction("Index");
                }

                var galleryModel = new GalleryViewModels()
                {
                    Name = gallery.Name,
                    Author = gallery.Author,
                    Price = gallery.Price
                };
                ViewBag.Id = id; //za predavane na danni
                return View(galleryModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, GalleryViewModels model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries.Find(id);
                gallery.Name = model.Name;
                gallery.Author = model.Author;
                gallery.Price = model.Price;

                if (model.Cover != null)
                {
                    var path = GetPath(model);
                    gallery.CoverPath = path;
                }

                db.Entry(gallery).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ManageGallery");
            }
        }

        private string GetPath(GalleryViewModels model)
        {
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(model.Cover.FileName));
            model.Cover.SaveAs(path);
            return model.Cover.FileName.Split('\\').Last();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries.FirstOrDefault(m => m.Id == id);
                if (gallery != null)
                {
                    db.Galleries.Remove(gallery);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
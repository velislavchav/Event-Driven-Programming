using System.Data.Entity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class GalleryController : Controller
    {
        private int id;

        // GET: Gallery
        public ActionResult Index(string searchValue)
        {
            //List<Gallery> galleries = new List<Gallery>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var galleries = db.Galleries
                    .Select(g => new ManageGalleryIndex()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Author = g.Author,
                        Price = g.Price,
                        CoverPath = g.CoverPath
                    })
                    .ToList();
                //search...
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    galleries = galleries
                        .Where(g => g.Name.ToLower().Contains(searchValue) || g.Author.ToLower().Contains(searchValue))
                        .ToList();
                    ViewBag.SearchValue = searchValue;
                }
                return View(galleries);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries
                    .Include(g => g.Comments)
                    .FirstOrDefault(g => g.Id == id);

                if (gallery == null)
                {
                    return RedirectToAction("Index");
                }

                var galleryModel = new Details()
                {
                    Name = gallery.Name,
                    Author = gallery.Author,
                    CoverPath = gallery.CoverPath,
                    Price = gallery.Price,
                    Comments = gallery.Comments
                        .Select(c => new DetailsComment()
                        {
                            Text = c.Text,
                            Owner = c.Owner
                        }).ToList()
                };
                ViewBag.Id = id; //prashta id-to vyv view
                return View(galleryModel);
            }
        }

        [HttpGet]
        public ActionResult AddComment(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries.FirstOrDefault(m => m.Id == id);
                if (gallery == null)
                {
                    return RedirectToAction("Index", "Gallery");
                }
                ViewBag.Id = id;
                ViewBag.Title = gallery.Name;
                return View();
            }           
        }

        public ActionResult AddComment(int id, GalleryDetails model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var gallery = db.Galleries.FirstOrDefault(m => m.Id == id);
                if (gallery == null)
                {
                    return RedirectToAction("Index", "Movies");
                }
                var comment = db.Comments.Add(new Comment()
                {
                    Text = model.Text,
                    Owner = User.Identity.Name
                });
                gallery.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Gallery", new { id });
            }
        }
    }
}
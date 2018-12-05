using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class GalleryViewModels
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        [Required]
        public HttpPostedFileBase Cover { get; set; }
    }
}
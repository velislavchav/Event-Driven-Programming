using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; }

        public string Owner { get; set; }

        public string Text { get; set; }
    }
}
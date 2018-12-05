using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Gallery
    {
        public Gallery()
        {
            this.Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public string CoverPath { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
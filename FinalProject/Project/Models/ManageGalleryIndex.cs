using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ManageGalleryIndex
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public string CoverPath { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Details
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public string CoverPath { get; set; }

        public List<DetailsComment> Comments { get; set; }
    }
}
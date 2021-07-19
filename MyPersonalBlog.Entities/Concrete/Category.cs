using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.Entities.Concrete
{
    public class Category:ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<CategoryBlog> CategoryBlogs { get; set; }
    }
}

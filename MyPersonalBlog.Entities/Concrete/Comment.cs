using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.Entities.Concrete
{
    public class Comment:ITable
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } 
        public bool Confirmed { get; set; } = false;

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public List<Comment> SubComments { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}

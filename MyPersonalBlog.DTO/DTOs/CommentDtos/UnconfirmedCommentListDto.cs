using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DTO.DTOs.CommentDtos
{
    public class UnconfirmedCommentListDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public Blog Blog { get; set; }
    }
}

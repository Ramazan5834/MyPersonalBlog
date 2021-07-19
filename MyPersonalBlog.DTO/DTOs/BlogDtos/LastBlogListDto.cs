using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalBlog.DTO.DTOs.BlogDtos
{
    public class LastBlogListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedTime { get; set; }
        public string ImagePath { get; set; }
    }
}

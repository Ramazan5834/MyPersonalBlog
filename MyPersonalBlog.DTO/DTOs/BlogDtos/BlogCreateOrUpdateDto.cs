using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalBlog.DTO.DTOs.BlogDtos
{
    public class BlogCreateOrUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public int ViewsNumber { get; set; }
        public int AppUserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalBlog.DTO.DTOs.CategoryDtos
{
    public class CategoryCreateOrUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

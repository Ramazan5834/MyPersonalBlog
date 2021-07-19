using System;
using System.Collections.Generic;
using System.Text;

namespace MyPersonalBlog.DTO.DTOs.AppUserDtos
{
    public class AppUserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GitHubAddress { get; set; }
        public string FacebookAddress { get; set; }
        public string YouTubeAddress { get; set; }
        public string LinkedInAddress { get; set; }
        public string Autobiography { get; set; }

    }
}

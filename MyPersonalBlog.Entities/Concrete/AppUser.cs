using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyPersonalBlog.Entities.Interfaces;

namespace MyPersonalBlog.Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; }
        public string GitHubAddress { get; set; }
        public string FacebookAddress { get; set; }
        public string YouTubeAddress { get; set; }
        public string LinkedInAddress { get; set; }
        public string Autobiography { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}

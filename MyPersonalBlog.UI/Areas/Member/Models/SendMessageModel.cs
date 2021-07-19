using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalBlog.UI.Areas.Member.Models
{
    public class SendMessageModel
    {
        [Required]
        [NotNull]
        public string SenderName { get; set; }
        [Required]
        [NotNull]
        public string SenderEmail { get; set; }
        [Required]
        [NotNull]
        public string MessageBody { get; set; }

    }
}

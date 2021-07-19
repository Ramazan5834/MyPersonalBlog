using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        private readonly MyPersonalBlogContext _context;
        public EfCommentRepository(MyPersonalBlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> GetAllCommentsWithSubComments(int blogId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            GetComments(blogId, parentId, result);
            return result;
        }

        private void GetComments(int blogId, int? parentId, List<Comment> result)
        {
            var comments = _context.Comments
                .Where(I => I.BlogId == blogId && I.ParentCommentId == parentId)
                .OrderByDescending(I => I.PostedTime).ToList();
            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<Comment>();

                    GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }


        public int GetUnconfirmedCommentsCount()
        {
            return _context.Comments.Where(I => I.Confirmed == false).ToList().Count();
        }

        public List<Comment> GetUnconfirmedCommentsWithBlog()
        {
            return _context.Comments.Include(I => I.Blog)
                .Where(I => I.Confirmed == false).ToList();
        }

    }
}

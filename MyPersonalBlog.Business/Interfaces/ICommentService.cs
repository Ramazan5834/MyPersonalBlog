using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Interfaces
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> GetAllCommentsWithSubComments(int blogId, int? parentId);
        int GetUnconfirmedCommentsCount();
        List<Comment> GetUnconfirmedCommentsWithBlog();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Interfaces
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        List<Comment> GetAllCommentsWithSubComments(int blogId, int? parentId);
        int GetUnconfirmedCommentsCount();
        List<Comment> GetUnconfirmedCommentsWithBlog();
    }
}

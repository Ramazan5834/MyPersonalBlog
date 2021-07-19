using System;
using System.Collections.Generic;
using System.Text;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericDal,ICommentDal commentDal) : base(genericDal)
        {
            _commentDal = commentDal;
        }


        public List<Comment> GetAllCommentsWithSubComments(int blogId, int? parentId)
        {
            return _commentDal.GetAllCommentsWithSubComments(blogId, parentId);
        }

        public int GetUnconfirmedCommentsCount()
        {
            return _commentDal.GetUnconfirmedCommentsCount();
        }

        public List<Comment> GetUnconfirmedCommentsWithBlog()
        {
            return _commentDal.GetUnconfirmedCommentsWithBlog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;
using MyPersonalBlog.DTO.DTOs.BlogDtos;
using MyPersonalBlog.DTO.DTOs.CategoryBlogDtos;
using MyPersonalBlog.DTO.DTOs.CategoryDtos;
using MyPersonalBlog.DTO.DTOs.CommentDtos;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.Areas.Member.Models;

namespace MyPersonalBlog.UI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region AppUser-AppUserDto
            CreateMap<AppUserListDto, AppUser>();
            CreateMap<AppUser, AppUserListDto>();
            CreateMap<AppUserUpdateDto, AppUser>();
            CreateMap<AppUser, AppUserUpdateDto>();
            #endregion

            #region Blog-BlogDto
            CreateMap<LastBlogListDto, Blog>();
            CreateMap<Blog, LastBlogListDto>();
            CreateMap<BlogListDto, Blog>();
            CreateMap<Blog, BlogListDto>();
            CreateMap<BlogCreateOrUpdateDto, Blog>();
            CreateMap<Blog, BlogCreateOrUpdateDto>();
            #endregion

            #region Category-CategoryDto
            CreateMap<CategoryCreateOrUpdateDto, Category>();
            CreateMap<Category, CategoryCreateOrUpdateDto>();
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();
            #endregion

            #region Comment-CommentDto
            CreateMap<CommentAddModel, Comment>();
            CreateMap<Comment, CommentAddModel>();
            CreateMap<UnconfirmedCommentListDto, Comment>();
            CreateMap<Comment, UnconfirmedCommentListDto>();
            #endregion

            #region CategoryBlog-CategoryBlogDto
            CreateMap<CategoryBlogAddDto, CategoryBlog>();
            CreateMap<CategoryBlog, CategoryBlogAddDto>();

            #endregion

        }
    }
}

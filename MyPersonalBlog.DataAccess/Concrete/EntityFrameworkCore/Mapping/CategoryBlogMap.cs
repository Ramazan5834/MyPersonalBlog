using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CategoryBlogMap : IEntityTypeConfiguration<CategoryBlog>
    {
        public void Configure(EntityTypeBuilder<CategoryBlog> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.HasIndex(I => new { I.BlogId, I.CategoryId }).IsUnique();
        }
    }
}

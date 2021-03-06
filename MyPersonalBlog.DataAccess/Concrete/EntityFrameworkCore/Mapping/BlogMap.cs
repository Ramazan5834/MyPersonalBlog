using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.Title).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ShortDescription).HasMaxLength(300).IsRequired();
            builder.Property(I => I.Description).HasColumnType("nvarchar(MAX)");
            builder.Property(I => I.ImagePath).HasMaxLength(300);
            builder.Property(I => I.ViewsNumber);

            builder.HasMany(I => I.Comments)
                .WithOne(I => I.Blog)
                .HasForeignKey(I => I.BlogId);
            builder.HasMany(I => I.CategoryBlogs)
                .WithOne(I => I.Blog)
                .HasForeignKey(I => I.BlogId);
        }
    }
}

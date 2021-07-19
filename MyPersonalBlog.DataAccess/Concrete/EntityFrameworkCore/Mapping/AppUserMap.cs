using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(I => I.Name).HasMaxLength(100);
            builder.Property(I => I.Surname).HasMaxLength(100);
            builder.Property(I => I.Picture).HasDefaultValue("default.png");
            builder.Property(I => I.GitHubAddress).HasMaxLength(200);
            builder.Property(I => I.FacebookAddress).HasMaxLength(200);
            builder.Property(I => I.YouTubeAddress).HasMaxLength(200);
            builder.Property(I => I.LinkedInAddress).HasMaxLength(200);
            builder.Property(I => I.Autobiography).HasColumnType("nvarchar(MAX)");

            builder.HasMany(I => I.Blogs)
                .WithOne(I => I.AppUser)
                .HasForeignKey(I => I.AppUserId);

        }
    }
}

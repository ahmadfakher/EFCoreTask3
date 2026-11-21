using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using task3efcore.Models;

namespace task3efcore.Configurations
{
    internal class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.HasKey(c => c.Id);

            category.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            category.Property(c => c.Description)
                .HasMaxLength(500);

            category.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId);

            category.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Backend",
                    Description = "Backend development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "Frontend",
                    Description = "Frontend development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 4,
                    Name = "ASP.NET",
                    Description = "ASP.NET courses",
                    ParentCategoryId = 2
                },
                new Category
                {
                    Id = 5,
                    Name = "Angular",
                    Description = "Angular courses",
                    ParentCategoryId = 3
                }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task3efcore.Models;
using task3efcore.Models.Enums;

namespace task3efcore.Configurations
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course.HasKey(c => c.Id);

            course.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(150);

            course.Property(c => c.Description)
                .HasMaxLength(1000);

            course.Property(c => c.Price)
                .HasPrecision(10, 2);

            course.Property(c => c.Level)
                .IsRequired();

            course.Property(c => c.IsPublished)
                .IsRequired();

            course.Property(c => c.PublishedAt)
                .IsRequired(false);

            course.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId);

            course.HasOne(c => c.Category)
                .WithMany(ca => ca.Courses)
                .HasForeignKey(c => c.CategoryId);

            course.HasData(
                new Course
                {
                    Id = 1,
                    Title = "C#",
                    Description = "C# basics",
                    Price = 100,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 1),
                    InstructorId = 1,
                    CategoryId = 2
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET",
                    Description = "learn ASP.Net development",
                    Price = 200,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 5),
                    InstructorId = 1,
                    CategoryId = 4
                },
                new Course
                {
                    Id = 3,
                    Title = "Angular",
                    Description = "Learn Angular",
                    Price = 150,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 10),
                    InstructorId = 2,
                    CategoryId = 5
                },
                new Course
                {
                    Id = 4,
                    Title = "Advanced Angular",
                    Description = "Advanced Angular topics",
                    Price = 300,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 2,
                    CategoryId = 3
                },
                new Course
                {
                    Id = 5,
                    Title = "Backend",
                    Description = "Backend concepts",
                    Price = 250,
                    Level = CourseLevel.Advanced,
                    IsPublished = true,
                    PublishedAt = new DateTime(2025, 4, 15),
                    InstructorId = 3,
                    CategoryId = 2
                },
                new Course
                {
                    Id = 6,
                    Title = "Introduction to Frontend",
                    Description = "Frontend development basics",
                    Price = 80,
                    Level = CourseLevel.Beginner,
                    IsPublished = false,
                    PublishedAt = null,
                    InstructorId = 2,
                    CategoryId = 3
                }
            );
        }
    }
}

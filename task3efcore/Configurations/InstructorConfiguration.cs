using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task3efcore.Models;

namespace task3efcore.Configurations
{
    internal class InstructorConfiguration: IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> instructor)
        {
            instructor.HasKey(i => i.Id);

            instructor.Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(100);

            instructor.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(150);

            instructor.Property(i => i.Bio)
                .HasMaxLength(500);

            instructor.Property(i => i.JoinedAt)
                .IsRequired();

            instructor.HasData(
                new Instructor
                {
                    Id = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed@gmail.com",
                    Bio = "this is an example",
                    JoinedAt = new DateTime(2025, 1, 10)
                },
                new Instructor
                {
                    Id = 2,
                    FullName = "Ali Mohammed",
                    Email = "ali@gmail.com",
                    Bio = "I am Ali, 20 years old",
                    JoinedAt = new DateTime(2025, 2, 15)
                },
                new Instructor
                {
                    Id = 3,
                    FullName = "Abdallah Ahmed",
                    Email = "abdallah@gmail.com",
                    Bio = "I am the best",
                    JoinedAt = new DateTime(2025, 3, 20)
                }
            );
        }
    }
}

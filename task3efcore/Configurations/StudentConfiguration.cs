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
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student.HasKey(s => s.Id);

            student.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            student.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(150);

            student.Property(s => s.DateOfBirth)
                .IsRequired();
        }
    }
}

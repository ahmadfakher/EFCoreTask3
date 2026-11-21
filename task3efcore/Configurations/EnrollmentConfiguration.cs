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
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> enrollment)
        {
            enrollment.HasKey(e => new { e.StudentId, e.CourseId });

            enrollment.Property(e => e.EnrolledAt)
                .IsRequired();

            enrollment.Property(e => e.Progress)
                .HasPrecision(5, 2)
                .IsRequired();

            enrollment.Property(e => e.Status)
                .IsRequired();

            enrollment.Property(e => e.FinalGrade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            enrollment.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            enrollment.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}

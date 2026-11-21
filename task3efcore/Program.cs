using Microsoft.EntityFrameworkCore;
using task3efcore.DbContexts;
using task3efcore.Models;
using task3efcore.Models.Enums;

namespace task3efcore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();
            SeedStudentsAndEnrollments(context);

            Console.WriteLine("\n========== CREATE ==========");

            AddInstructor(context);

            AddCourse(context, 1, 4);

            AddSeveralStudents(context);

            EnrollStudent(context, 1, 1);

            Console.WriteLine("\n========== READ ==========");

            FindCourseById(context, 1);

            FindCourseByTitle(context, "C# Fundamentals");

            FilterCourses(context, 100, 300);

            PrintSubCategories(context, 1);

            PrintTopLevelCategories(context);

            PrintCourseRoster(context, 1);

            Console.WriteLine("\n========== UPDATE ==========");

            ChangeCoursePrice(context, 1, 250);

            UpdateProgress(context, 1, 1, 75);

            PublishCourse(context, 4);

            Console.WriteLine("\n========== DELETE ==========");

            UnenrollStudent(context, 1, 1);

            TryDeleteCategory(context, 2);

            SafeDeleteCategory(context, 3);

            DeleteCourse(context, 2);

            Console.WriteLine("\n========== BULK OPERATIONS ==========");

            BulkPriceRaise(context, CourseLevel.Beginner);

            BulkDeleteDroppedEnrollments(context);

            Console.WriteLine("\nAll operations completed.");
        }

        static void SeedStudentsAndEnrollments(AppDbContext context)
        {
            if (!context.Students.Any())
            {
                var students = new List<Student>
                    {
                        new Student
                        {
                            FullName = "Hassan Ahmed",
                            Email = "hassan@gmail.com",
                            DateOfBirth = new DateTime(2002, 5, 10)
                        },
                        new Student
                        {
                            FullName = "Hana Ahmed",
                            Email = "hana@gmail.com",
                            DateOfBirth = new DateTime(2001, 8, 20)
                        },
                        new Student
                        {
                            FullName = "Youssef Ali",
                            Email = "youssef@gmail.com",
                            DateOfBirth = new DateTime(2003, 1, 15)
                        },
                        new Student
                        {
                            FullName = "Nour Khaled",
                            Email = "nour@gmail.com",
                            DateOfBirth = new DateTime(2002, 11, 3)
                        },
                        new Student
                        {
                            FullName = "Noha Mostafa",
                            Email = "noha@gmail.com",
                            DateOfBirth = new DateTime(2001, 3, 25)
                        }
                    };

                context.Students.AddRange(students);
                context.SaveChanges();
                Console.WriteLine("Students added successfully");

                var enrollments = new List<Enrollment>
                    {
                        new Enrollment
                        {
                            StudentId = students[0].Id,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2025, 5, 1),
                            Progress = 50,
                            Status = EnrollmentStatus.Active
                        },
                        new Enrollment
                        {
                            StudentId = students[0].Id,
                            CourseId = 2,
                            EnrolledAt = new DateTime(2025, 5, 2),
                            Progress = 100,
                            Status = EnrollmentStatus.Completed,
                            FinalGrade = 95
                        },
                        new Enrollment
                        {
                            StudentId = students[1].Id,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2025, 5, 3),
                            Progress = 20,
                            Status = EnrollmentStatus.Active
                        },
                        new Enrollment
                        {
                            StudentId = students[1].Id,
                            CourseId = 3,
                            EnrolledAt = new DateTime(2025, 5, 4),
                            Progress = 100,
                            Status = EnrollmentStatus.Completed,
                            FinalGrade = 88
                        },
                        new Enrollment
                        {
                            StudentId = students[2].Id,
                            CourseId = 2,
                            EnrolledAt = new DateTime(2025, 5, 5),
                            Progress = 30,
                            Status = EnrollmentStatus.Dropped
                        },
                        new Enrollment
                        {
                            StudentId = students[2].Id,
                            CourseId = 3,
                            EnrolledAt = new DateTime(2025, 5, 6),
                            Progress = 60,
                            Status = EnrollmentStatus.Active
                        },
                        new Enrollment
                        {
                            StudentId = students[3].Id,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2025, 5, 7),
                            Progress = 100,
                            Status = EnrollmentStatus.Completed,
                            FinalGrade = 92
                        },
                        new Enrollment
                        {
                            StudentId = students[4].Id,
                            CourseId = 2,
                            EnrolledAt = new DateTime(2025, 5, 8),
                            Progress = 10,
                            Status = EnrollmentStatus.Dropped
                        }
                    };

                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();
                Console.WriteLine("Enrollments added successfully");
            }
        }
        
        // CREATE
        static void AddInstructor(AppDbContext context)
        {
            var instructor = new Instructor
            {
                FullName = "John Smith",
                Email = "john@example.com",
                Bio = "Software engineer",
                JoinedAt = DateTime.Now
            };

            context.Instructors.Add(instructor);

            context.SaveChanges();

            Console.WriteLine(
                $"Instructor added successfully. ID: {instructor.Id}");
        }
        static void AddCourse(AppDbContext context, int instructorId, int categoryId)
        {
            bool instructorExists = context.Instructors
                .Any(i => i.Id == instructorId);

            if (!instructorExists)
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            bool categoryExists = context.Categories
                .Any(c => c.Id == categoryId);

            if (!categoryExists)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            var course = new Course
            {
                Title = "Entity Framework Core",
                Description = "Learn EF Core",
                Price = 180,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                PublishedAt = null,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);

            context.SaveChanges();

            Console.WriteLine($"Course added. ID: {course.Id}");
        }
        static void AddSeveralStudents(AppDbContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    FullName = "Student One",
                    Email = "one@example.com",
                    DateOfBirth = new DateTime(2002, 1, 1)
                },
                new Student
                {
                    FullName = "Student Two",
                    Email = "two@example.com",
                    DateOfBirth = new DateTime(2003, 2, 2)
                },
                new Student
                {
                    FullName = "Student Three",
                    Email = "three@example.com",
                    DateOfBirth = new DateTime(2001, 3, 3)
                }
            };

            context.Students.AddRange(students);

            context.SaveChanges();

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.FullName} - ID: {student.Id}");
            }
        }
        static void EnrollStudent(AppDbContext context, int studentId, int courseId)
        {
            var student = context.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine("Cannot enroll in an unpublished course.");
                return;
            }

            bool alreadyEnrolled = context.Enrollments.Any(e =>
                e.StudentId == studentId &&
                e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                Console.WriteLine("Student is already enrolled.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrolledAt = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            };

            context.Enrollments.Add(enrollment);

            context.SaveChanges();

            Console.WriteLine("Student enrolled successfully.");
        }

        // READ
        static void FindCourseById(AppDbContext context, int id)
        {
            var course = context.Courses.Find(id);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.WriteLine(
                $"{course.Id} - {course.Title} - {course.Price}");
        }
        static void FindCourseByTitle(AppDbContext context, string title)
        {
            var course = context.Courses
                .FirstOrDefault(c => c.Title == title);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.WriteLine(
                $"{course.Id} - {course.Title} - {course.Price}");
        }
        static void FilterCourses(AppDbContext context, decimal minPrice, decimal maxPrice)
        {
            var courses = context.Courses
                .Where(c => c.Price >= minPrice &&
                            c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new
                {
                    c.Title,
                    c.Level,
                    c.Price
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"{course.Title} | {course.Level} | {course.Price}");
            }
        }
        static void PrintSubCategories(AppDbContext context, int categoryId)
        {
            bool categoryExists = context.Categories
                .Any(c => c.Id == categoryId);

            if (!categoryExists)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            Console.WriteLine("Sub-categories:");

            foreach (var category in subCategories)
            {
                Console.WriteLine(category.Name);
            }
        }
        static void PrintTopLevelCategories(AppDbContext context)
        {
            var categories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("Top-level categories:");

            foreach (var category in categories)
            {
                Console.WriteLine(category.Name);
            }
        }
        static void PrintCourseRoster(AppDbContext context, int courseId)
        {
            bool courseExists = context.Courses
                .Any(c => c.Id == courseId);

            if (!courseExists)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            Console.WriteLine($"Number of enrolled students: {students.Count}");

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }
        
        // UPDATE
        static void ChangeCoursePrice(AppDbContext context, int courseId, decimal newPrice)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (newPrice < 0)
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }

            course.Price = newPrice;

            context.SaveChanges();

            Console.WriteLine("Course price updated.");
        }
        static void UpdateProgress(AppDbContext context, int studentId, int courseId, decimal newProgress)
        {
            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine("Progress must be between 0 and 100.");
                return;
            }

            var enrollment = context.Enrollments.Find(
                studentId,
                courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Dropped)
            {
                Console.WriteLine("Cannot update a dropped enrollment.");
                return;
            }

            enrollment.Progress = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            context.SaveChanges();

            Console.WriteLine("Progress updated successfully.");
        }
        static void PublishCourse(AppDbContext context, int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (course.IsPublished)
            {
                Console.WriteLine("Course is already published.");
                return;
            }

            course.IsPublished = true;

            course.PublishedAt = DateTime.Now;

            context.SaveChanges();

            Console.WriteLine("Course published successfully.");
        }

        // DELETE
        static void UnenrollStudent(AppDbContext context, int studentId, int courseId)
        {
            var enrollment = context.Enrollments.Find(
                studentId,
                courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            context.Enrollments.Remove(enrollment);

            context.SaveChanges();

            Console.WriteLine("Student unenrolled successfully.");
        }
        static void TryDeleteCategory(AppDbContext context, int categoryId)
        {
            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            context.Categories.Remove(category);

            try
            {
                context.SaveChanges();

                Console.WriteLine("Category deleted.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    $"Could not delete category: {ex.GetBaseException().Message}");
            }
        }
        static void SafeDeleteCategory(AppDbContext context, int categoryId)
        {
            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            bool hasSubCategories = context.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                Console.WriteLine(
                    "Cannot delete this category because it has sub-categories.");

                return;
            }

            bool hasCourses = context.Courses
                .Any(c => c.CategoryId == categoryId);

            if (hasCourses)
            {
                Console.WriteLine(
                    "Cannot delete this category because courses belong to it.");

                return;
            }

            context.Categories.Remove(category);

            context.SaveChanges();

            Console.WriteLine("Category deleted successfully.");
        }
        static void DeleteCourse(AppDbContext context, int courseId)
        {
            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            int beforeCount = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments before deletion: {beforeCount}");

            context.Courses.Remove(course);

            context.SaveChanges();

            int afterCount = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments after deletion: {afterCount}");
        }

        // BULK OPERATIONS
        static void BulkPriceRaise(AppDbContext context, CourseLevel level)
        {
            int affectedRows = context.Courses
                .Where(c => c.IsPublished && c.Level == level)
                .ExecuteUpdate(setters =>
                    setters.SetProperty(
                        c => c.Price,
                        c => c.Price * 1.10m
                    ));

            Console.WriteLine(
                $"Number of affected courses: {affectedRows}");
        }
        static void BulkDeleteDroppedEnrollments(AppDbContext context)
        {
            int affectedRows = context.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Dropped)
                .ExecuteDelete();

            Console.WriteLine(
                $"Deleted enrollments: {affectedRows}");
        }
    }
}

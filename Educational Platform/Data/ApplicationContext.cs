using Educational_Platform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Educational_Platform.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; } 
        public DbSet<CompletedLesson> CompletedLessons { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Many-to-many relationship between Student and Course

            builder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentId);

            builder.Entity<Enrollment>()
                .HasOne<ApplicationUser>(e => e.ApplicationUser)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.StudentId);

            builder.Entity<Enrollment>()
                .HasOne<Course>(e => e.Course)
                .WithMany(c=>c.Enrollments)
                .HasForeignKey(e=>e.CourseId);


            // One-to-many relationship from Teacher to Course
            
            builder.Entity<ApplicationUser>()
                .HasMany<Course>(u=>u.Courses)
                .WithOne(c=>c.ApplicationUser)
                .HasForeignKey(c=>c.TeacherId);


            // One-to-many relationship from Course to Lesson

            builder.Entity<Course>()
                .HasMany<Lesson>(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId);


            // One-to-many relationship from Course to Quiz

            builder.Entity<Quiz>()
                .HasOne<Course>(q => q.Course)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(q => q.CourseId);


            // One-to-many relationship from Quiz to Questions

            builder.Entity<Quiz>()
                .HasMany<Question>(qz => qz.Questions)
                .WithOne(qe => qe.Quiz)
                .HasForeignKey(qe => qe.QuizId);


            builder.Entity<CompletedLesson>()
                .HasOne(cl => cl.ApplicationUser)
                .WithMany() // You can specify the navigation property if needed
                .HasForeignKey(cl => cl.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete


            builder.Entity<CompletedLesson>()
                .HasOne(cl => cl.Lesson)
                .WithMany() // You can specify the navigation property if needed
                .HasForeignKey(cl => cl.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<CompletedLesson>()
            //    .HasOne<Course>(cl => cl.Course)
            //    .WithMany()
            //    .HasForeignKey(cl => cl.CourseId)
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

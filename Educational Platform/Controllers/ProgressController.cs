using Educational_Platform.Repositories.Interfaces;
using Educational_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Educational_Platform.Data;

namespace Educational_Platform.Controllers
{
    public class ProgressController : Controller
    {
        ICourseRepo courseRepo;
        ILessonRepo lessonRepo;
        ApplicationContext context;
        IEnrollmentRepo enrollmentRepo;
        UserManager<ApplicationUser> userManager; 

        public ProgressController(IEnrollmentRepo enrollmentRepo, ICourseRepo courseRepo , ApplicationContext context 
             ,UserManager<ApplicationUser> userManager , ILessonRepo lessonRepo)
        {
            this.context = context;
            this.lessonRepo = lessonRepo;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.enrollmentRepo = enrollmentRepo;
        }

        public async Task<IActionResult> MarkAsCompleted(string CourseName , int LessonId)
        {
            Course course = courseRepo.GetByName(CourseName);
            var user = await userManager.GetUserAsync(User);
            Lesson lesson = lessonRepo.GetById(LessonId);


            CompletedLesson completedLesson = new CompletedLesson()
            {
                StudentId = user.Id,
                LessonId = lesson.LessonId,
                CourseName = CourseName,
                //CourseId = course.CourseId
            };

            await context.CompletedLessons.AddAsync(completedLesson);
            await context.SaveChangesAsync();
            enrollmentRepo.Save();
            
            return RedirectToAction("ShowAll", "Lesson", new {CourseName = CourseName}) ;
        }
    }
}

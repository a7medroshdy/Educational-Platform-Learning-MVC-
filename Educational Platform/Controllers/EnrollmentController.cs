using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;
using Educational_Platform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    public class EnrollmentController : Controller
    {
        ILessonRepo lessonRepo;
        ICourseRepo CourseRepo;
        ApplicationContext context;
        IEnrollmentRepo enrollmentRepo;
        UserManager<ApplicationUser> userManager;
        public EnrollmentController(IEnrollmentRepo enrollmentRepo ,ApplicationContext context , 
            UserManager<ApplicationUser> userManager, ILessonRepo lessonRepo , ICourseRepo courseRepo)
        {
            this.context = context;
            this.lessonRepo = lessonRepo;
            this.CourseRepo = courseRepo;
            this.userManager = userManager;
            this.enrollmentRepo = enrollmentRepo;
        }


        [AllowAnonymous]
        public IActionResult Enrollment(int CourseId)
        {
            return View("Enrollment", CourseId);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEnrollment(int CourseId, string StudentEmail)
        {
            var user = await userManager.FindByEmailAsync(StudentEmail);

            if (user == null)
            {
                TempData["Message"] = "This student is not exist."; 
                return View("Enrollment", CourseId);
            }

            var isStudent = await userManager.IsInRoleAsync(user, "Student");

            if (!isStudent)
            {
                TempData["Message"] = "This user is not a student.";
                return View("Enrollment", CourseId);
            }

            if (enrollmentRepo.IsStudentEnrolledInCourse(CourseId, user.Id))
            {
                ModelState.AddModelError("", "The student is already enrolled in this course.");
                return View("Enrollment", CourseId);
            }

            Enrollment newEnrollment = new Enrollment()
            {
                CourseId = CourseId,
                StudentId = user.Id
            };

            enrollmentRepo.Add(newEnrollment);
            enrollmentRepo.Save();
            return RedirectToAction("DisplayEnrolledCourses");
        }


        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DisplayEnrolledCourses()
        {
            var user = await userManager.GetUserAsync(User);
            var Courses = enrollmentRepo.GetByStudentId(user.Id);

            List<EnrolledCoursesViewModel> enrolledCourses = new List<EnrolledCoursesViewModel>();

            foreach (var course in Courses)
            {
                EnrolledCoursesViewModel model = new EnrolledCoursesViewModel()
                {
                    Title = course.Title,
                    CourseId = course.CourseId,
                    Description = course.Description,
                    TeacherEmail = course.TeacherEmail,
                };

                //var enrollment = enrollmentRepo.GetByCourseIdAndStudentId(course.CourseId, user.Id);
                //var completedLessons = enrollment.CompletedLessons;
                var completedLessons = context.CompletedLessons.Where(cl=> cl.CourseName == course.Title && cl.StudentId == user.Id).Count();
                var TotalLessons = lessonRepo.GetByCourseName(course.Title).Count;

                model.Progress = (TotalLessons > 0) ? ((double)completedLessons / TotalLessons) * 100.0 : 0;
                enrolledCourses.Add(model);
            }
            return View("DisplayEnrolledCourses", enrolledCourses);
        }


        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteEnrollment(int CourseId)
        {
            var user = await userManager.GetUserAsync(User);
            var enrollment = enrollmentRepo.GetByCourseIdAndStudentId(CourseId, user.Id);

            enrollmentRepo.Delete(enrollment.EnrollmentId);
            enrollmentRepo.Save();

            var course = CourseRepo.GetById(CourseId);
            var courseLessons = lessonRepo.GetByCourseName(course.Title);

            var completedLessons = context.CompletedLessons
                                  .Where(cl => courseLessons.Select(l => l.LessonId).Contains(cl.LessonId)
                                           && cl.StudentId == user.Id);

            context.CompletedLessons.RemoveRange(completedLessons);
            await context.SaveChangesAsync();

            TempData["Message"] = "Enrollment is deleted successfully.";
            return RedirectToAction("DisplayEnrolledCourses");
        }
    }
}

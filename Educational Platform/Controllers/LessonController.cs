using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;
using Educational_Platform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    public class LessonController : Controller
    {
        ILessonRepo repo;
        ICourseRepo courseRepo;
        ApplicationContext context;
        IEnrollmentRepo enrollmentRepo;
        UserManager<ApplicationUser> userManager;

        public LessonController(ILessonRepo repo , ICourseRepo courseRepo , UserManager<ApplicationUser> userManager ,
               IEnrollmentRepo enrollmentRepo , ApplicationContext context)
        {
            this.repo = repo;
            this.context = context;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.enrollmentRepo = enrollmentRepo;
        }

        [Authorize]
        public async Task<IActionResult> ShowAll(string CourseName)
        {
            var user = await userManager.GetUserAsync(User);

            if(await userManager.IsInRoleAsync(user,"Student"))
            {
                Course course = courseRepo.GetByName(CourseName);
                bool IsEnrolled = enrollmentRepo.IsStudentEnrolledInCourse(course.CourseId,user.Id);

                if (!IsEnrolled)
                {
                    return Forbid();
                }
            }

            List<Lesson> lessons = repo.GetByCourseName(CourseName);

            var CompletedLessons = context.CompletedLessons.Where(cl=>cl.StudentId==user.Id)
                .Select(cl=>cl.LessonId).ToList();

            ViewBag.CourseName = CourseName;
            ViewBag.CompletedLessons = CompletedLessons;
            return View("ShowAll", lessons);
        }

        [Authorize(Roles ="Teacher,Admin")]
        public IActionResult Add(string CourseName)
        {
            ViewBag.CourseName = CourseName;    
            return View();
        }

        [HttpPost]
        public IActionResult SaveAdd(AddLessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepo.GetByName(model.CourseName);

                if (course == null)
                {
                    ModelState.AddModelError("CourseName", "There is no course with this title");
                    return View("Add", model);
                }

                Lesson lesson = new Lesson()
                {
                    Title = model.Title,
                    Content = model.Content,
                    CourseId = course.CourseId,
                };

                repo.Add(lesson); 
                repo.Save();
                return RedirectToAction("ShowAll",new { CourseName = model.CourseName });
            }
            return View("Add",model);
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Edit(int Id)
        {
            Lesson lesson = repo.GetById(Id);
            string courseName = courseRepo.GetById(lesson.CourseId).Title;

            AddLessonViewModel model = new AddLessonViewModel()
            {
                Title = lesson.Title,
                Content = lesson.Content,
                CourseName = courseName
            };

            ViewBag.LessonId=Id;    
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult SaveEdit(AddLessonViewModel model , int LessonId)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepo.GetByName(model.CourseName);

                if (course == null)
                {
                    ModelState.AddModelError("CourseName", "There is no course with this title");
                    return View("Edit", model);
                }

                Lesson lesson = repo.GetById(LessonId);

                lesson.Title = model.Title;
                lesson.Content = model.Content;
                lesson.CourseId = course.CourseId;
                repo.Save();

                return RedirectToAction("ShowAll", new { CourseName = model.CourseName});
            }
            return View("Edit",model);
        }

        [Authorize(Roles ="Teacher,Admin")]
        public IActionResult Delete(int LessonId , string CourseName)
        {
            ViewBag.CourseName = CourseName;
            Lesson lesson = repo.GetById(LessonId);
            return View("Delete",lesson); 
        }

        [HttpPost]
        public IActionResult ConfirmDelete (int LessonId , string CourseName)
        {
            repo.Delete(LessonId);
            repo.Save();
            return RedirectToAction("ShowAll", new { CourseName = CourseName });
        }
    }
}

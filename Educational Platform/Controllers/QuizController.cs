using Educational_Platform.ViewModels;
using Educational_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using Educational_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Educational_Platform.Controllers
{
    public class QuizController : Controller
    {
        IQuizRepo repo;
        ICourseRepo courseRepo;
        IEnrollmentRepo enrollmentRepo;
        UserManager<ApplicationUser> userManager;

        public QuizController(IQuizRepo repo, ICourseRepo courseRepo , UserManager<ApplicationUser> userManager
            , IEnrollmentRepo enrollmentRepo)
        {
            this.repo = repo;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.enrollmentRepo = enrollmentRepo;
        }

        [Authorize]
        public async Task<IActionResult> ShowAll(string CourseName)
        {
            var user = await userManager.GetUserAsync(User);
            Course course = courseRepo.GetByName(CourseName);

            if(await userManager.IsInRoleAsync(user,"Student"))
            {
                var enrollment = enrollmentRepo.GetByCourseIdAndStudentId(course.CourseId, user.Id);

                if (enrollment == null)
                {
                    return Forbid();
                }
            }

            ViewBag.CourseName = CourseName;
            var quizzes = repo.GetByCourseName(CourseName);
            return View("ShowAll",quizzes);
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Add(string CourseName)
        {
            AddQuizViewModel model = new AddQuizViewModel();
            model.CourseName = CourseName;
            return View("Add", model);
        }

        [HttpPost]
        public IActionResult SaveAdd(AddQuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepo.GetByName(model.CourseName);

                if (course == null)
                {
                    ModelState.AddModelError("CourseName", "There is no course with this name");
                    return View("Add", model);
                }

                Quiz quiz = repo.GetByName(model.Title, course.Title);

                if(quiz != null)
                {
                    ModelState.AddModelError("Title", $"There is a quiz with this title for {course.Title} course.");
                    return View("Add", model);
                }

                Quiz newQuiz = new Quiz()
                {
                    Title = model.Title,
                    Description = model.Description,
                    CourseId = course.CourseId
                };

                repo.Add(newQuiz);
                repo.Save();
                return RedirectToAction("ShowAll",new {CourseName = model.CourseName});
            }
            return View("Add", model);
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Edit(int QuizId , string CourseName)
        {
            Quiz quiz = repo.GetById(QuizId);

            AddQuizViewModel model = new AddQuizViewModel()
            {
                Title = quiz.Title,
                Description = quiz.Description,
                CourseName = CourseName
            };
            ViewBag.QuizId = QuizId;
            return View("Edit", model); 
        }

        [HttpPost]
        public IActionResult SaveEdit(AddQuizViewModel model , int QuizId)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepo.GetByName(model.CourseName);

                if (course == null)
                {
                    ModelState.AddModelError("CourseName", "There is no course with this name");
                    return View("Edit", model);
                }

                Quiz quizExist = repo.GetByName(model.Title, course.Title);

                if (quizExist != null && quizExist.QuizId != QuizId)
                {
                    ModelState.AddModelError("Title", $"There is a quiz with this title for {course.Title} course.");
                    return View("Add", model);
                }

                Quiz quiz = repo.GetById(QuizId);

                quiz.Title = model.Title;
                quiz.Description = model.Description;
                quiz.CourseId = course.CourseId;
                repo.Save();
                return RedirectToAction("ShowAll",new {CourseName = model.CourseName});
            }
            return View("Edit", model);
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Delete(int QuizId , string CourseName)
        {
            ViewBag.CourseName=CourseName;
            Quiz quiz = repo.GetById(QuizId);
            return View("Delete", quiz);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int QuizId , string CourseName)
        {
            repo.Delete(QuizId);
            repo.Save();
            return RedirectToAction("ShowAll", new { CourseName = CourseName });
        }
    }
}

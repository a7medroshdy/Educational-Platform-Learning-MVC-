using Educational_Platform.Repositories.Interfaces;
using Educational_Platform.ViewModels;
using Educational_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Educational_Platform.Repositories;

namespace Educational_Platform.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionRepo repo;
        IQuizRepo quizRepo;
        ICourseRepo courseRepo;
        IEnrollmentRepo enrollmentRepo;
        UserManager<ApplicationUser> userManager;
        public QuestionController(IQuestionRepo repo,IQuizRepo quizRepo, IEnrollmentRepo enrollmentRepo
            , UserManager<ApplicationUser> userManager , ICourseRepo courseRepo)
        {
            this.repo = repo;
            this.quizRepo = quizRepo;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
            this.enrollmentRepo = enrollmentRepo;
        }

        [Authorize]
        public async Task<IActionResult> ShowAll(string QuizName , string CourseName)
        {
            var user = await userManager.GetUserAsync(User);

            if(await userManager.IsInRoleAsync(user,"Student"))
            {
                Course course = courseRepo.GetByName(CourseName);
                bool isEnrolled = enrollmentRepo.IsStudentEnrolledInCourse(course.CourseId, user.Id);

                if (!isEnrolled)
                {
                    return Forbid();
                }
            }
            var questions = repo.GetByQuizName(QuizName , CourseName);
            ViewBag.QuizName = QuizName;    
            ViewBag.CourseName = CourseName;    
            return View("ShowAll", questions);
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Add(string QuizName , string CourseName)
        {
            AddQuestionViewModel model = new AddQuestionViewModel()
            {
                QuizName = QuizName
            };
            ViewBag.CourseName = CourseName;    
            return View("Add", model);
        }

        [HttpPost]
        public IActionResult SaveAdd(AddQuestionViewModel model,string CourseName)
        {
            if (ModelState.IsValid)
            {
                Quiz quiz = quizRepo.GetByName(model.QuizName,CourseName);

                if (quiz == null)
                {
                    ModelState.AddModelError("QuizName", $"There is no quiz with this name for {CourseName} course.");
                    return View("Add", model);
                }

                Question question = new Question()
                {
                    Text = model.Text,
                    AnswerOptions = model.AnswerOptions,
                    CorrectAnswer = model.CorrectAnswer,
                    QuizId = quiz.QuizId
                };

                repo.Add(question);
                repo.Save();
                return RedirectToAction("ShowAll", new { QuizName = quiz.Title, CourseName = CourseName });
            }
            return View("Add", model);
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Edit(int QuestionId , string QuizName , string CourseName)
        {
            Question question = repo.GetById(QuestionId);

            AddQuestionViewModel model = new AddQuestionViewModel()
            {
                Text= question.Text,
                AnswerOptions = question.AnswerOptions,
                CorrectAnswer = question.CorrectAnswer,
                QuizName = QuizName
            };
            ViewBag.QuestionId = QuestionId;
            ViewBag.CourseName = CourseName;
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult SaveEdit(AddQuestionViewModel model, string CourseName , int QuestionId)
        {
            if (ModelState.IsValid)
            {
                Quiz quiz = quizRepo.GetByName(model.QuizName , CourseName);

                if (quiz == null)
                {
                    ModelState.AddModelError("QuizName", $"There is no quiz with this name for {CourseName} course.");
                    return View("Edit", model);
                }

                Question question = repo.GetById(QuestionId);

                question.Text = model.Text;
                question.QuizId = quiz.QuizId;
                question.AnswerOptions = model.AnswerOptions;
                question.CorrectAnswer = model.CorrectAnswer;
                repo.Save();
                return RedirectToAction("ShowAll", new {QuizName=quiz.Title , CourseName = CourseName});
            }
            return View("Edit", model); 
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Delete(int QuestionId , string CourseName , string QuizName)
        {
            Question question = repo.GetById(QuestionId);
            ViewBag.CourseName = CourseName;
            ViewBag.QuizName = QuizName;
            return View("Delete", question);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int QuestionId , string QuizName , string CourseName)
        {
            repo.Delete(QuestionId);
            repo.Save();
            return RedirectToAction("ShowAll", new { QuizName = QuizName, CourseName = CourseName });
        }
    }
}

using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.Repositories.Interfaces;
using Educational_Platform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Educational_Platform.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepo courseRepo;
        ApplicationContext context;
        UserManager<ApplicationUser> userManager;

        public CourseController(ICourseRepo courseRepo,UserManager<ApplicationUser> userManager,
            ApplicationContext context)
        {
            this.context = context;
            this.courseRepo = courseRepo;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult ShowAll()
        {
            return View("ShowAll",courseRepo.GetAll());
        }

        [Authorize(Roles ="Admin,Teacher")]
        public IActionResult Add()
        {
            return View("Add");  
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> SaveAdd(AddCourseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var userExist = await userManager.FindByEmailAsync(obj.TeacherEmail);

                if (userExist == null)
                {
                    ModelState.AddModelError("TeacherEmail", "This email does not exist.");
                    goto ar7w;
                }

                var courseExist = courseRepo.GetByName(obj.Title);

                if(courseExist != null)
                {
                    ModelState.AddModelError("Title", "There is a course with this title.");
                    return View("Add", obj);
                }
                
                var teacherExist = await userManager.IsInRoleAsync(userExist, "Teacher");

                if (teacherExist)
                {
                    Course course = new Course()
                    {
                        Title = obj.Title,
                        Description = obj.Description,
                        TeacherId = userExist.Id,
                        TeacherEmail = obj.TeacherEmail
                    };

                    courseRepo.Add(course);
                    courseRepo.Save();
                    return RedirectToAction("ShowAll");
                }
                ModelState.AddModelError("TeacherEmail", "This user is not a teacher.");
            }
            ar7w:
            return View("Add", obj);
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Edit(int id)
        {
            ViewBag.CourseId = id;
            var courseFromDB = courseRepo.GetById(id);
            
            var obj = new AddCourseViewModel()
            {
                Title=courseFromDB.Title,
                Description=courseFromDB.Description,
                TeacherEmail=courseFromDB.TeacherEmail
            };
            return View("Edit",obj);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> SaveEdit(AddCourseViewModel obj,int CourseId)
        {
            if (ModelState.IsValid)
            {
                var userExist = await userManager.FindByEmailAsync(obj.TeacherEmail);

                if (userExist == null)
                {
                    ModelState.AddModelError("TeacherEmail", "This email does not exist.");
                    goto ar7w;
                }

                var courseExist = courseRepo.GetByName(obj.Title);

                if (courseExist != null && courseExist.CourseId != CourseId)
                {
                    ModelState.AddModelError("Title", "There is a course with this title.");
                    return View("Add", obj);
                }

                bool teacherExist = await userManager.IsInRoleAsync(userExist,"Teacher");

                if(teacherExist)
                {
                    var course= courseRepo.GetById(CourseId);
                    
                    course.Title = obj.Title; 
                    course.Description = obj.Description;
                    course.TeacherEmail = obj.TeacherEmail;
                    courseRepo.Save();
                    return RedirectToAction("ShowAll");
                }

                ModelState.AddModelError("TeacherEmail", "This user is not a teacher.");
                ar7w:
                return View("Edit", obj);
            }
            return View("Edit", obj);
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Delete(int id)
        {
            ViewBag.CourseId = id;  
            return View("Delete",courseRepo.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult SaveDelete(int id)
        {
            courseRepo.Delete(id);
            courseRepo.Save();
            return RedirectToAction("ShowAll");
        }
    }
}

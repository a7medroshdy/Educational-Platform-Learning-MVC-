using Educational_Platform.Data;
using Educational_Platform.Models;
using Educational_Platform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        public readonly RoleManager<IdentityRole> roleManager;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly ApplicationContext context;
        public RolesController(RoleManager<IdentityRole>roleManager,ApplicationContext context,
            UserManager<ApplicationUser> userManager) 
        {
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> ShowAll()
        {
            ShowAllViewModel model = new ShowAllViewModel();

            model.Roles = roleManager.Roles.ToList();
            model.Students = new List<ApplicationUser>();
            model.Teachers = new List<ApplicationUser>();
            model.Admins = new List<ApplicationUser>();

            var users= context.Users.ToList();

            foreach (var user in users) 
            {
                if (await userManager.IsInRoleAsync(user, "Teacher"))
                {
                    model.Teachers.Add(user);
                }
                if (await userManager.IsInRoleAsync(user, "Student"))
                {
                    model.Students.Add(user);
                }
                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    model.Admins.Add(user);
                }
            }
            return View("ShowAll",model);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await roleManager.RoleExistsAsync(model.Name);
                
                if (roleExist)
                {
                    ModelState.AddModelError("", "Role already exists");
                    return View("Create",model);
                }

                IdentityRole role = new IdentityRole(){ Name=model.Name };
                //role.Name = model.Name;

                IdentityResult result = await roleManager.CreateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("ShowAll");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Create", model);
        }


        public async Task<IActionResult> Delete(string RoleId)
        {
            return View("Delete",await roleManager.FindByIdAsync(RoleId));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string RoleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(RoleId);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("ShowAll");
        }

        public async Task<IActionResult> AssignAdminRole(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            
            if(await userManager.IsInRoleAsync(user,"Admin"))
            {
                TempData["Message"]= "User already has the Admin role.";
            }
            else
            {
                await userManager.AddToRoleAsync(user, "Admin");
                TempData["Message"] = "Admin role assigned successfully.";
            }
            return RedirectToAction("ShowAll");
        }

        public async Task<IActionResult> RevokeAdminRole(string Id)
        {
            await userManager.RemoveFromRoleAsync(await userManager.FindByIdAsync(Id),"Admin");
            TempData["Message"] = "User removed from Admin role ";
            return RedirectToAction("ShowAll");
        }
    }
}

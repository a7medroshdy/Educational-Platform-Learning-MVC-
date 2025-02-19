using Educational_Platform.Models;
using Educational_Platform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Educational_Platform.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly SignInManager<ApplicationUser> signInManager;
        public readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager ,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Register()
        {
            RegisterViewModel model=new RegisterViewModel();    
            var roles = roleManager.Roles.ToList();

            foreach (var item in roles)
            {
                if(item.Name!="Admin")
                    model.Roles.Add(item);
            }
            return View("Register",model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel model)
        {
            var roles = await roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                if(role.Name != "Admin")
                {
                    model.Roles.Add(role);  // dah 34an el validation cases lma arg3 el view tane el list bta3t el roles mtb2a4 fadya
                }
            }

            if (ModelState.IsValid)
            {
                var ExistingUser = await userManager.FindByEmailAsync(model.Email);

                if (ExistingUser != null)
                { 
                    ModelState.AddModelError("Email", "This email address is already taken.");
                    return View("Register", model);
                }

                ApplicationUser appUser = new ApplicationUser();
                appUser.Email = model.Email;
                appUser.UserName = model.Email;
                appUser.FullName = model.FullName;
                appUser.PhoneNumber = model.PhoneNumber;
                
                IdentityResult result = await userManager.CreateAsync(appUser,model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser,model.SelectedRole);
                    await signInManager.SignInAsync(appUser,false);
                    return RedirectToAction("Index","Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return View("Register",model);
        }


        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var appUser = await userManager.FindByEmailAsync(model.Email);

                if (appUser != null)
                {
                    bool passwordOk = await userManager.CheckPasswordAsync(appUser , model.Password);

                    if (passwordOk)
                    {
                        await signInManager.SignInAsync(appUser, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email or Password is wrong");
            }
            return View("Login",model);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

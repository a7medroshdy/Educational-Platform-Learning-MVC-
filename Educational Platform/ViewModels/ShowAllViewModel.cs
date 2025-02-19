using Educational_Platform.Models;
using Microsoft.AspNetCore.Identity;

namespace Educational_Platform.ViewModels
{
    public class ShowAllViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public List<ApplicationUser> Students { get; set; }
        public List<ApplicationUser> Teachers { get; set; }
        public List<ApplicationUser> Admins { get; set; }

    }
}

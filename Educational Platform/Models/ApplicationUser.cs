using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        public IList<Course> Courses { get; set; } = new List<Course>();
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public IList<CompletedLesson> CompletedLessons { get; set; } = new List<CompletedLesson>();
    }
}
